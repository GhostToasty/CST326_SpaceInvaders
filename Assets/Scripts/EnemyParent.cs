using System;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    float time = 0;
    public double timeStepAmount = 1;
    double nextTime;
    float moveX = 1f;
    float moveY = 0f;
    public float enemiesHit = 0;
    public float speedEffect = 0.03f;
    // public Enemy enemy;

    public delegate void ChangeFrameFunc(float frameNum);
    public static event ChangeFrameFunc OnChangeFrame;
    private float frameNum;

    
    

    void Start()
    {
        Enemy.OnSwitchDirection += OnSwitchDirection;
        frameNum = 0;
        OnChangeFrame?.Invoke(frameNum);
    }


    void Update()
    {
        time += Time.deltaTime;

        if (Convert.ToDouble(time) >= nextTime)
        {
            timeStepAmount = 1 - (speedEffect * enemiesHit);
            // Debug.Log($"Time Between Movement: {timeStepAmount}");
            // Debug.Log(enemiesHit);  
            
            if (frameNum == 0)
                frameNum = 0.5f;
            else if (frameNum == 0.5f)
                frameNum = 0;
            OnChangeFrame?.Invoke(frameNum);

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
