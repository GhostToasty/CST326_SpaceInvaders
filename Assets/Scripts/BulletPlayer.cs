using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float speed = 10;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * speed;
    }

}
