using UnityEngine;
using UnityEngine.Serialization;

public class BeeWorker : Bee
{
    [SerializeField, Tooltip("Время зависания над цветком")]
    Vector2 timeOverFlower;

    private Vector2 _flowerVector;
    private bool _pollinated = false;

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

            CheckPosition();
            BeeTouchHive();
            
            // таймер на улей
            if (toHive && Timer(transform.parent.position) && _pollinated)
            {
                // декремируется счётчик и устанавливается новый цветок
                _pollinated = false;
                hive.CheckSpawnBee();
                RandomFlowerTarget();
            }
            // таймер на цветок
            else if (!toHive && Timer(_flowerVector))
            {
                _pollinated = true;
                ReturnToHive();
            }
        }
    }

    private void CheckPosition()
    {
        // если достиг цели и цель не улей
        if ((Vector2) transform.position == target && !toHive && timerOn == false)
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
            turnOnTimer(timeInHive);
        }
    }
}