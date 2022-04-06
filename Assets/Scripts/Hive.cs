using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hive : MonoBehaviour
{
    public GameObject beeScoutPrefab;
    public GameObject beeWorkerPrefab;
    public GameObject[] flowersPrefabs;
    public float flowersSpawnRadius = 5;
    public int flowersSpawnCount = 10;
    public int pointsToSpawn = 5;

    public static int Points = 0;
    public Vector3[] knownFlowers;

    void Start()
    {
        Points = pointsToSpawn;
        
        var beesCount = Random.Range(2, 4);

        for (var i = 1; i <= beesCount; i++)
        {
            GameObject bee = Instantiate(i <= beesCount / 2 ? beeScoutPrefab : beeWorkerPrefab, transform.position, Quaternion.identity, gameObject.transform);
            bee.GetComponent<Bee>().hive = this;
        }

        for (var i = 0; i < flowersSpawnCount; i++)
        {
            GameObject flowerPrefab = flowersPrefabs[Random.Range(0, flowersPrefabs.Length)];
            Vector2 position = new Vector2(Random.Range(-flowersSpawnRadius, flowersSpawnRadius),
                Random.Range(-flowersSpawnRadius, flowersSpawnRadius)) + (Vector2)transform.position;
            Instantiate(flowerPrefab, position, Quaternion.identity, gameObject.transform);
        }
    }

    private void Update()
    {
        if (Points > 0) return;
        Instantiate(Random.Range(1, 100) <= 5 ? beeScoutPrefab : beeWorkerPrefab, transform.position, Quaternion.identity, gameObject.transform);
        Points = pointsToSpawn;
    }
}
