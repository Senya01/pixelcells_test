using UnityEngine;

public class Bee : MonoBehaviour
{
    public Vector2 target;
    [SerializeField] public float radius = 5;
    [SerializeField] public float speed = 1.5f;

    public Hive hive;

    protected void SetRandomTarget()
    {
        target = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius)) + (Vector2)transform.position;
    }

    protected void MoveTo()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    protected void Rotation()
    {
        transform.localScale = target.x > transform.position.x
            ? new Vector3(-1.0f, 1.0f, 1.0f)
            : new Vector3(1.0f, 1.0f, 1.0f);
    }
}