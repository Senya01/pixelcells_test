using UnityEngine;

public class BeeScout : Bee
{
    void Start()
    {
        SetRandomTarget();
    }
    
    void Update()
    {
        Rotation();
        CheckPosition();
        MoveTo();
    }

    private void CheckPosition()
    {
        if ((Vector2)transform.position == target)
        {
            SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        target = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius)) + (Vector2)hive.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flower"))
        {
            if (!hive.knownFlowers.Contains(col.transform.position))
            {
                hive.knownFlowers.Add(col.transform.position);
                target = transform.parent.position;
            }
        }
    }
}