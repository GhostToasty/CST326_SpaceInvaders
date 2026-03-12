using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootOffsetTransform;
    public float speed = 5f;

    public delegate void PlayerDiedFunc();
    public static event PlayerDiedFunc OnPlayerDied;
    public delegate void LoadCreditsFunc();
    public static event LoadCreditsFunc OnLoadCredits;
    public AudioManager audioManager;
    Animator _animator;

    

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && _animator.GetBool("Shoot") == false)
        {
            _animator.SetBool("Shoot", true);
            GameObject shot = Instantiate(bulletPrefab, shootOffsetTransform.position, shootOffsetTransform.rotation);
            Destroy(shot, 4f);
        }

        if (Keyboard.current.dKey.isPressed)
            transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);

        if (Keyboard.current.aKey.isPressed)
            transform.position = new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            _animator.SetBool("Died", true);

            Destroy(collision.gameObject);
            Debug.Log("Game Over");
        }
    }

    void StopShootAnim()
    {
        _animator.SetBool("Shoot", false);
    }

    void StopExplodeAnim()
    {
        // Destroy(gameObject);
        OnPlayerDied?.Invoke();
        OnLoadCredits?.Invoke();
    }

    void StartShootSound()
    {
        audioManager.PlayerShootSound();
    }

    void StartExplodeSound()
    {
        audioManager.PlayerExplodeSound();
    }

}
