using UnityEngine;

public class BeeScout : Bee
{
    [SerializeField,Tooltip("Радиус случайного перемещения")] public float radius;
    
    private Vector2 flower;

    void Start()
    {
        SetRandomTarget();
    }
    
    void Update()
    {
        Rotation();
        CheckPosition();
        MoveTo();
        BeeTouchHive();
    }

    private void CheckPosition()
    {
        if ((Vector2)transform.position == target && flower == Vector2.zero)
        {
            SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        target = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius)) + (Vector2)hive.transform.position;
    }
    
    private void BeeTouchHive()
    {
        if (flower != Vector2.zero && target == (Vector2)transform.parent.position && (Vector2)transform.position == (Vector2)transform.parent.position)
        {
            hive.knownFlowers.Add(flower);
            flower = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flower"))
        {
            if (!hive.knownFlowers.Contains(col.transform.position))
            {
                flower = col.transform.position;
                target = transform.parent.position;
            }
        }
    }
}
