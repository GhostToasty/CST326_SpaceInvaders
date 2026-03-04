using UnityEngine;

public class EnemyEx : MonoBehaviour
{
    public delegate void EnemyDiedFuncEx(float points);
    //associated with the class as a whole, not the object instance
    public static event EnemyDiedFuncEx OnEnemyDiedEx;
    public AudioClip ticClip;
    public AudioClip tacClip;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch!");
        
        // todo - destroy the bullet
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            OnEnemyDiedEx?.Invoke(10);
        }

        // todo - trigger death animation
    }

    public void PlayTicSound()
    {
        GetComponent<AudioSource>().PlayOneShot(ticClip);
    }

    public void PlayTacSound()
    {
        GetComponent<AudioSource>().PlayOneShot(tacClip);
    }
}
