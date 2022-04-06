using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeeScout : Bee
{
    private void SetRandomTarget()
    {
        _target = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius)) + (Vector2)transform.position;
    }

    private void CheckPosition()
    {
        if ((Vector2)transform.position == _target)
        {
            SetRandomTarget();
        }
    }

    private void Rotation()
    {
        transform.localScale = _target.x > transform.position.x
            ? new Vector3(-1.0f, 1.0f, 1.0f)
            : new Vector3(1.0f, 1.0f, 1.0f);
    }
    
    void Start()
    {
        SetRandomTarget();
    }

    void Update()
    {
        Rotation();
        CheckPosition();
        transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Flower")) return;
        // if (transform.parent.gameObject.knownFlowers.Contains(col)) return;

        _target = transform.parent.position;
    }
}
