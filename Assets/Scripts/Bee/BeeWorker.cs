using UnityEngine;

public class BeeWorker : Bee
{
    [SerializeField,Tooltip("Время зависания над цветком")] Vector2 time;

    private Vector2 _flowerVector;

    // timer
    private float _timeLeft;
    private bool _timerOn;

    private bool _pollinated;

    void Start()
    {
        _timeLeft = Random.Range(time.x, time.y);
    }

    private void Timer()
    {
        if (_timerOn)
        {
            target = _flowerVector;
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
            }
            else
            {
                _timeLeft = Random.Range(time.x, time.y);
                _timerOn = false;
                _pollinated = true;
                target = transform.parent.position;
            }
        }
    }

    private void GetRandomFlower()
    {
        target = hive.knownFlowers[Random.Range(0, hive.knownFlowers.Count - 1)];
    }

    private void Update()
    {
        if (target == Vector2.zero && hive.knownFlowers.Count != 0) GetRandomFlower();

        if (target != Vector2.zero)
        {
            MoveTo();
            Rotation();
            CheckPosition();
            Timer();
            BeeTouchHive();
        }
    }

    private void CheckPosition()
    {
        if ((Vector2)transform.position == target && hive.knownFlowers.Count != 0 && target != (Vector2)transform.parent.position)
        {
            GetRandomFlower();
        }
    }

    private void BeeTouchHive()
    {
        if (_pollinated && target == (Vector2)transform.parent.position && (Vector2)transform.position == (Vector2)transform.parent.position)
        {
            _pollinated = false;
            hive.CheckSpawnBee();
            GetRandomFlower();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flower"))
        {
            if (hive.knownFlowers.Contains(col.transform.position) && target == (Vector2)col.transform.position)
            {
                _flowerVector = col.transform.position;
                _timerOn = true;
            }
        }
    }
}
