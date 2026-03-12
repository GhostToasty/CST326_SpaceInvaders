using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip enemyShoot; 
    public AudioClip enemyExplode;
    public AudioClip playerShoot;
    public AudioClip playerExplode;
    

    public AudioSource backgroundMusicSource;
    public AudioSource enemyShootSource;
    public AudioSource enemyExplodeSource;
    public AudioSource playerShootSource;
    public AudioSource playerExplodeSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.playOnAwake = true;
        backgroundMusicSource.loop = true;
    }

    public void EnemyShootSound()
    {
        enemyShootSource.PlayOneShot(enemyShoot);
    }

    public void EnemyExplodeSound()
    {
        enemyExplodeSource.PlayOneShot(enemyExplode);
    }

    public void PlayerShootSound()
    {
        playerShootSource.PlayOneShot(playerShoot);
    }

    public void PlayerExplodeSound()
    {
        playerExplodeSource.PlayOneShot(playerExplode);
    }


}
