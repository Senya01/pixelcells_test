using UnityEngine;

public class BeeScout : Bee
{
    [SerializeField,Tooltip("Радиус случайного перемещения")] public float radius;
    
    private Vector2 _flower;
    
    // hive timer
    private bool hiveTimerOn = false;
    private float hiveTimeLeft = 0;
    
    void Start()
    {
        SetHiveTimer();
        SetRandomTarget();
    }
    
    void Update()
    {
        Rotation();
        MoveTo();

        HiveTimer();
        CheckPosition();
        BeeTouchHive();
    }

    private void SetHiveTimer()
    {
        hiveTimeLeft = Random.Range(timeInHive.x, timeInHive.y);
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
                hive.knownFlowers.Add(_flower);
                _flower = Vector2.zero;
            }
        }
    }
    
    private void CheckPosition()
    {
        if ((Vector2)transform.position == target && _flower == Vector2.zero)
        {
            SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        toHive = false;
        target = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius)) + (Vector2)hive.transform.position;
    }
    
    private void BeeTouchHive()
    {
        if (_flower != Vector2.zero && toHive && (Vector2)transform.position == (Vector2)transform.parent.position && !hiveTimerOn)
        {
            SetHiveTimer();
            hiveTimerOn = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flower"))
        {
            if (!hive.knownFlowers.Contains(col.transform.position))
            {
                _flower = col.transform.position;
                ReturnToHive();
            }
        }
    }
}
