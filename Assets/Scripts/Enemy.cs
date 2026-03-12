using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDiedFunc(int points);
    //associated with the class as a whole, not the object instance
    public static event EnemyDiedFunc OnEnemyDied;

    public delegate void SwitchDirectionFunc(char direction);
    public static event SwitchDirectionFunc OnSwitchDirection;
    public delegate void LoadCreditsFunc();
    public static event LoadCreditsFunc OnLoadCredits;

    
    public AudioManager audioManager;
    public GameObject bulletEnemyPrefab;
    float time;
    int nextTime = 2;
    int nextShip = 5;
    int speedShip = 10;
    public EnemyParent enemyParent;
    Animator _animator;
    private string animIdleName;

    
    void Start()
    {
        _animator = GetComponent<Animator>();
        
        if (gameObject.CompareTag("Ship") == false)
            EnemyParent.OnChangeFrame += OnChangeFrame;

        if (gameObject.CompareTag("Octopus"))
            animIdleName = "EnemyOctoIdle";
        if (gameObject.CompareTag("LilGuy"))
            animIdleName = "EnemyLilIdle";
        if (gameObject.CompareTag("Jellyfish"))
            animIdleName = "EnemyJellyIdle";

        _animator.SetFloat("SpeedMult", 0f);
    }
    
    void Update()
    {          
        if (gameObject.CompareTag("Octopus"))
        {
            time += Time.deltaTime;
            if (Convert.ToInt16(time) >= nextTime)
            {
                nextTime = Convert.ToInt16(time) + 1;
                if (UnityEngine.Random.Range(0, 10) == 0 && _animator.GetBool("Shoot") == false)
                {
                    _animator.SetBool("Shoot", true);
                    GameObject shot = Instantiate(bulletEnemyPrefab, transform.position, Quaternion.identity);
                    Destroy(shot, 4f);
                }
            }
        }

        if (gameObject.CompareTag("Ship"))
        {
            time += Time.deltaTime;
            if (Convert.ToInt16(time) >= nextShip)
            {
                if(transform.position.x > -21 || transform.position.x < 21)
                    transform.position = new Vector2 (transform.position.x + (speedShip * Time.deltaTime), 8);
                    
                if (transform.position.x > 21 || transform.position.x < -21)
                {
                    nextShip = Convert.ToInt16(time) + 5;
                    speedShip *= -1;
                }
            }
        }
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            EnemyParent.OnChangeFrame -= OnChangeFrame;
            _animator.SetBool("Died", true);

            int points = 0;
            
            if(gameObject.CompareTag("Ship"))
                points = System.Convert.ToInt16(UnityEngine.Random.Range(100, 500));
            if (gameObject.CompareTag("Octopus"))
                points = 30;
            if(gameObject.CompareTag("LilGuy"))
                points = 20;
            if(gameObject.CompareTag("Jellyfish"))
                points = 10;   

            Destroy(collision.gameObject);
            enemyParent.enemiesHit += 1;

            OnEnemyDied?.Invoke(points);
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {   
        if (!gameObject.CompareTag("Ship"))
        {
            char direction = 'X';

            if (other.gameObject.CompareTag("BarrierL"))
                direction = 'R';
            if (other.gameObject.CompareTag("BarrierR"))
                direction = 'L';
            
            OnSwitchDirection?.Invoke(direction);
        }
    }

    void StopShootAnim()
    {
        _animator.SetBool("Shoot", false);
    }

    void StopExplodeAnim()
    {
        Destroy(gameObject);
        if (enemyParent.enemiesHit == 28)
            OnLoadCredits?.Invoke();
    }


    void OnChangeFrame(float frameNum)
    {
        _animator.Play(animIdleName, 0, frameNum);
    }

    void StartShootSound()
    {
        audioManager.EnemyShootSound();
    }

    void StartExplodeSound()
    {
        audioManager.EnemyExplodeSound();
    }

}
