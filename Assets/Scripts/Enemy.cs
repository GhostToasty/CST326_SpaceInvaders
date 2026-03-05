using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDiedFunc(int points);
    //associated with the class as a whole, not the object instance
    public static event EnemyDiedFunc OnEnemyDied;

    public delegate void SwitchDirectionFunc(char direction);
    public static event SwitchDirectionFunc OnSwitchDirection;
    
    public AudioManager audioManager;
    public GameObject bulletEnemyPrefab;
    float time;
    int nextTime = 1;
    int nextShip = 5;
    int speedShip = 10;
    public EnemyParent enemyParent;

    
    void Update()
    {          
        if (gameObject.CompareTag("Octopus"))
        {
            time += Time.deltaTime;
            if (Convert.ToInt16(time) >= nextTime)
            {
                nextTime = Convert.ToInt16(time) + 1;
                if (UnityEngine.Random.Range(0, 10) == 0)
                {
                    GameObject shot = Instantiate(bulletEnemyPrefab, transform.position, Quaternion.identity);
                    audioManager.EnemyShoot();
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
            Destroy(gameObject);
            enemyParent.enemiesHit += 1;

            OnEnemyDied?.Invoke(points);
        }

        // todo - trigger death animation
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


}
