using UnityEngine;

public class Barrier : MonoBehaviour
{
    int hitCount = 0;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet") || collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            hitCount += 1;
            Destroy(collision.gameObject);

            if (hitCount == 4)
               Destroy(gameObject);             
        }
    }

}
