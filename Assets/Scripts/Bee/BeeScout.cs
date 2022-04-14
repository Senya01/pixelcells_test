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
        // достик цели и нет цветка
        if ((Vector2)transform.position == target && _flower == Vector2.zero)
        {
            SetRandomTarget();
        }
    }

    // установить случайную цель
    private void SetRandomTarget()
    {
        toHive = false;
        target = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius)) + (Vector2)hive.transform.position;
    }
    
    private void BeeTouchHive()
    {
        // если есть новый цветок, цель - улей, достиг улья
        if (_flower != Vector2.zero && toHive && (Vector2)transform.position == (Vector2)transform.parent.position && !hiveTimerOn)
        {
            SetHiveTimer();
            hiveTimerOn = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // коснулся цветка
        if (col.CompareTag("Flower"))
        {
            // если цветка нет в уже известных
            if (!hive.knownFlowers.Contains(col.transform.position))
            {
                // запомнить новый цветок
                _flower = col.transform.position;
                // вернуться в улей
                ReturnToHive();
            }
        }
    }
}
