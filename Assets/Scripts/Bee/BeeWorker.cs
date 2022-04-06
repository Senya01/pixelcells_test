using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeWorker : Bee
{
    void Start()
    {
        
    }

    void GetRandomFlower()
    {
        target = hive.knownFlowers[Random.Range(0, hive.knownFlowers.Count - 1)];
    }
    
    private void CheckPosition()
    {
        if ((Vector2) transform.position != target) return;
        
        if (hive.knownFlowers.Count != 0)
        {
            GetRandomFlower();
        }
    }
    
    void Update()
    {
        if (target == new Vector2())
        {
            GetRandomFlower();
        }
        
        MoveTo();
        Rotation();
        CheckPosition();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flower"))
        {
            if (hive.knownFlowers.Contains(col.transform.position))
            {
                target = transform.parent.position;
                hive.CheckSpawnBee();
            }
        }
    }
}
