using UnityEngine;

public class BeeWorker : Bee
{
    [SerializeField] float time = 3;

    private Vector2 _flowerVector;

    // timer
    private float _timeLeft = 0f;
    private bool _timerOn = false;

    private bool _pollinated = false;

    void Start()
    {
        _timeLeft = time;
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
                _timeLeft = time;
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
        if (target == new Vector2() && hive.knownFlowers.Count != 0)
        {
            GetRandomFlower();
        }

        // если опылён и цель = улью и пчела достигла улья
        if (_pollinated && target == (Vector2)transform.parent.position && (Vector2)transform.position == (Vector2)transform.parent.position)
        {
            _pollinated = false;
            hive.CheckSpawnBee();
            GetRandomFlower();
        }

        if (target != new Vector2(0, 0))
        {
            MoveTo();
            Rotation();
            CheckPosition();
            Timer();
        }
    }

    private void CheckPosition()
    {
        if ((Vector2)transform.position == target && hive.knownFlowers.Count != 0 && target != (Vector2)transform.parent.position)
        {
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
