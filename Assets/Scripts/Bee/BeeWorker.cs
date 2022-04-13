using UnityEngine;
using UnityEngine.Serialization;

public class BeeWorker : Bee
{
    [SerializeField, Tooltip("Время зависания над цветком")]
    Vector2 timeOverFlower;

    private Vector2 _flowerVector;
    private bool _pollinated;

    // выполняется при окончании таймера
    void TimerEndAction()
    {
        if (toHive)
        {
        }
        else
        {
            _pollinated = true;
            ReturnToHive();
        }
    }

    // делает целью случайный вектор
    private void RandomFlowerTarget()
    {
        target = hive.knownFlowers[Random.Range(0, hive.knownFlowers.Count - 1)];
    }

    private void Update()
    {
        // если нет цели, известен хотя бы 1 цветок, то установить цель на случайный цветок
        if (target == Vector2.zero && hive.knownFlowers.Count != 0) RandomFlowerTarget();

        if (target != Vector2.zero)
        {
            MoveTo();
            Rotation();
            
            if (toHive)
            {
                // таймер на улей
                Timer(transform.parent.position);
            }
            else
            {
                // таймер на цветок
                Timer(_flowerVector);
            }

            CheckPosition();
            BeeTouchHive();
        }
    }

    private void CheckPosition()
    {
        // если достиг цели, известен хотя бы 1 цветок и цель не улей
        // if ((Vector2) transform.position == target && hive.knownFlowers.Count != 0 && !toHive)
        // {
        //     RandomFlowerTarget();
        // }

        // если достиг цели и цель не улей
        if ((Vector2) transform.position == target && !toHive)
        {
            // точка для зависания = цветку
            _flowerVector = transform.position;
            // включить таймер
            turnOnTimer(timeOverFlower);
        }
    }

    private void BeeTouchHive()
    {
        // опылён, цель - улей, достиг улья
        if (_pollinated && toHive && (Vector2) transform.position == (Vector2) transform.parent.position)
        {
            // декремируется счётчик и устанавливается новый цветок
            _pollinated = false;
            hive.CheckSpawnBee();
            RandomFlowerTarget();
        }
    }
}