using UnityEngine;
using UnityEngine.Serialization;
using UnityEngineInternal;

public class BeeWorker : Bee
{
    [SerializeField, Tooltip("Время зависания над цветком")]
    Vector2 timeOverFlower;

    private Vector2 _flowerVector;
    private bool _pollinated = false;

    // hive timer
    private bool hiveTimerOn = false;
    private float hiveTimeLeft = 0;
    
    // hive timer
    private bool flowerTimerOn = false;
    private float flowerTimeLeft = 0;
    
    // делает целью случайный вектор
    private void RandomFlowerTarget()
    {
        toHive = false;
        target = hive.knownFlowers[Random.Range(0, hive.knownFlowers.Count - 1)];
    }
    
    
    // таймеры не работают
    private void Update()
    {
        // если нет цели, известен хотя бы 1 цветок, то установить цель на случайный цветок
        if (target == Vector2.zero && hive.knownFlowers.Count != 0) RandomFlowerTarget();

        if (target != Vector2.zero)
        {
            MoveTo();
            Rotation();

            HiveTimer();
            FlowerTimer();
            
            CheckPosition();
            BeeTouchHive();
        }
    }

    // не работает
    private void FlowerTimer()
    {
        if (flowerTimerOn)
        {
            target = transform.parent.position;
            if (flowerTimeLeft > 0)
            {
                flowerTimeLeft -= Time.deltaTime;
            }
            else
            {
                flowerTimerOn = false;
                _pollinated = true;
                ReturnToHive();
            }
        }
    }

    // не работает
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
                _pollinated = false;
                hive.CheckSpawnBee();
                RandomFlowerTarget();
            }
        }
    }
    
    private void SetHiveTimer()
    {
        hiveTimeLeft = Random.Range(timeInHive.x, timeInHive.y);
    }
    
    private void SetFlowerTimer()
    {
        flowerTimeLeft = Random.Range(timeOverFlower.x, timeOverFlower.y);
    }

    private void CheckPosition()
    {
        // если достиг цели и цель не улей
        if ((Vector2) transform.position == target && !toHive)
        {
            // точка для зависания = цветку
            _flowerVector = transform.position;
            // включить таймер
            SetFlowerTimer();
            flowerTimerOn = true;
        }
    }

    private void BeeTouchHive()
    {
        // опылён, цель - улей, достиг улья
        if (_pollinated && toHive && (Vector2) transform.position == (Vector2) transform.parent.position)
        {
            SetHiveTimer();
            hiveTimerOn = true;
        }
    }
}