using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeDefender : BattleBee
{
    [SerializeField,Tooltip("Радиус патрулирования")] float patrolRadius;
    [SerializeField,Tooltip("Количество точек патрулирования")] int patrolPointsCount;

    private int patrolPointsCounter = 0;
    
    // hive timer
    private bool hiveTimerOn = false;
    private float hiveTimeLeft = 0;

    void Update()
    {
        MoveTo();
        Rotation();
        
        BeeTouchHive();
        HiveTimer();
        CheckPosition();
    }

    void Start()
    {
        SetRandomTarget();
    }
    
    private void SetRandomTarget()
    {
        toHive = false;
        target = new Vector2(Random.Range(-patrolRadius, patrolRadius), Random.Range(-patrolRadius, patrolRadius)) + (Vector2)hive.transform.position;
    }
    
    private void SetHiveTimer()
    {
        hiveTimeLeft = Random.Range(timeInHive.x, timeInHive.y);
    }

    private void AddPatrolPointCounter()
    {
        patrolPointsCounter++;
        if (patrolPointsCounter >= patrolPointsCount)
        {
            ReturnToHive();
        }
    }
    
    private void CheckPosition()
    {
        if ((Vector2)transform.position == target)
        {
            SetRandomTarget();
            AddPatrolPointCounter();
        }
    }
    
    private void BeeTouchHive()
    {
        if (toHive && (Vector2)transform.position == (Vector2)transform.parent.position && !hiveTimerOn)
        {
            SetHiveTimer();
            hiveTimerOn = true;
        }
    }
    
    private void HiveTimer()
    {
        if (hiveTimerOn)
        {
            target = transform.parent.position;
            if (hiveTimeLeft > 0)
            {
                hiveTimeLeft -= Time.deltaTime;
            }
            else
            {
                hiveTimerOn = false;
                patrolPointsCounter = 0;
            }
        }
    }
}
