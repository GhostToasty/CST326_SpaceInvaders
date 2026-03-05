using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootOffsetTransform;
    public AudioManager audioManager;
    public float speed = 5f;

    public delegate void PlayerDiedFunc();
    public static event PlayerDiedFunc OnPlayerDied;
    

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject shot = Instantiate(bulletPrefab, shootOffsetTransform.position, Quaternion.identity);
            audioManager.PlayerShoot();
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
            Destroy(collision.gameObject);
            Destroy(gameObject);

            Debug.Log("Game Over");

            OnPlayerDied?.Invoke();
        }
    }
}
