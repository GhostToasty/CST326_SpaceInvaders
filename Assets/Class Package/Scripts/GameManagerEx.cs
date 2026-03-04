using UnityEngine;

public class GameManagerEx : MonoBehaviour
{
    void Start()
    {
       // todo - sign up for notification about enemy death 
       EnemyEx.OnEnemyDiedEx += OnEnemyDiedEx;

    }

    void OnEnemyDiedEx(float score)
    {
        Debug.Log($"Killed enemy, worth: {score}");
    }

    void OnDestroy()
    {
        EnemyEx.OnEnemyDiedEx -= OnEnemyDiedEx;
    }
    
}
