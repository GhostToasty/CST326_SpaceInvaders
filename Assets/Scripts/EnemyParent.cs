using System;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    float time = 0;
    double timeStepAmount = 1;
    double nextTime;
    float moveX = 1f;
    float moveY = 0f;
    public float enemiesHit = 0;
    float speedEffect = 0.02f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy.OnSwitchDirection += OnSwitchDirection;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Convert.ToDouble(time) >= nextTime)
        {
            timeStepAmount = 1 - (speedEffect * enemiesHit);
            Debug.Log(timeStepAmount);
            Debug.Log(enemiesHit);
            nextTime = Convert.ToDouble(time) + timeStepAmount;
            if (moveY != 0)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + moveY);
                moveY = 0;
            }
            else   
                transform.position = new Vector2(transform.position.x + moveX, transform.position.y);
        }
    }

    void OnSwitchDirection(char direction)
    {
        if (direction == 'R')
            moveX = Mathf.Abs(moveX);

        if (direction == 'L')
            moveX = -Mathf.Abs(moveX);

        moveY = -2f;
    }
}
