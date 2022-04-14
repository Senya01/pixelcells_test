using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField, Tooltip("Скорость пчелы")]
    float speed = 1.5f;

    [SerializeField, Tooltip("Диапазон секунд, на который пчела будет возвращаться в улей")]
    protected Vector2 timeInHive;

    [HideInInspector] public Vector2 target;
    [HideInInspector] public Hive hive;

    // является ли целью улей
    [HideInInspector] public bool toHive = false;

    protected void MoveTo()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    // вернуться к улью
    protected void ReturnToHive()
    {
        toHive = true;
        target = transform.parent.position;
    }

    protected void Rotation()
    {
        transform.localScale = target.x > transform.position.x
            ? new Vector3(-1.0f, 1.0f, 1.0f)
            : new Vector3(1.0f, 1.0f, 1.0f);
    }
}