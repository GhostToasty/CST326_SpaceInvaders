using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip enemyShoot; 
    public AudioClip playerShoot; 

    public AudioSource backgroundMusicSource;
    public AudioSource enemyShootSource;
    public AudioSource playerShootSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.playOnAwake = true;
        backgroundMusicSource.loop = true;
    }

    public void EnemyShoot()
    {
        enemyShootSource.PlayOneShot(enemyShoot);
    }

    public void PlayerShoot()
    {
        enemyShootSource.PlayOneShot(playerShoot);
    }

}
