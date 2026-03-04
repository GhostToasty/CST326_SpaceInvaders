using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 10;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * speed;
    }

}
