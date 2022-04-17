using UnityEngine;
using UnityEngine.Serialization;
using UnityEngineInternal;

public class BeeWorker : Bee
{
    [SerializeField, Tooltip("Время зависания над цветком")]
    Vector2 timeOverFlower;

    [SerializeField] GameObject hivePrefab;
    
    private Vector2 _flowerVector;
    private bool _pollinated = false;

    // hive timer
    private bool hiveTimerOn = false;
    private float hiveTimeLeft = 0;
    
    // flower timer
    private bool flowerTimerOn = false;
    private float flowerTimeLeft = 0;
    
    private void RandomFlowerTarget()
    {
        toHive = false;
        target = hive.knownFlowers[Random.Range(0, hive.knownFlowers.Count - 1)];
    }
    
    private void Update()
    {
        if (target == Vector2.zero && hive.knownFlowers.Count != 0) RandomFlowerTarget();

        if (target != Vector2.zero)
        {
            MoveTo();
            Rotation();

            CheckPosition();
            BeeTouchHive();
        }

        if (!newHive)
        {
            HiveTimer();
            FlowerTimer();
        }
    }

    private void FlowerTimer()
    {
        if (flowerTimerOn)
        {
            target = _flowerVector;
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

    private void CheckPosition()
    {
        if ((Vector2) transform.position == target && !toHive && !flowerTimerOn && !newHive)
        {
            _flowerVector = transform.position;
            flowerTimeLeft = Random.Range(timeOverFlower.x, timeOverFlower.y);
            flowerTimerOn = true;
        }
        else if ((Vector2)transform.position == target && newHive)
        {
            Instantiate(hivePrefab, transform.position, Quaternion.identity);
            Destroy(this);
        }
    }

    private void BeeTouchHive()
    {
        if (_pollinated && toHive && (Vector2) transform.position == (Vector2) transform.parent.position && !hiveTimerOn && !newHive)
        {
            hiveTimeLeft = Random.Range(timeInHive.x, timeInHive.y);
            hiveTimerOn = true;
        }
    }
}