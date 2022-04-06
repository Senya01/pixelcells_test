using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hive : MonoBehaviour
{
    [SerializeField] GameObject beeScoutPrefab;
    [SerializeField] GameObject beeWorkerPrefab;
    [SerializeField] GameObject[] flowersPrefabs;
    [SerializeField,Range(1, 5)] float flowersSpawnRadius = 3;
    [SerializeField] int flowersSpawnCount = 20;
    [SerializeField] int pointsToSpawn = 5;

    public static int Points = 0;
    public List<Vector2> knownFlowers;

    void Start()
    {
        Points = pointsToSpawn;
        Generator();
    }

    private void SpawnBee(GameObject prefab)
    {
        GameObject bee = Instantiate(prefab, transform.position, Quaternion.identity, gameObject.transform);
        bee.GetComponent<Bee>().hive = this;
    }
    
    private void Generator()
    {
        var beesCount = Random.Range(2, 4);
        for (var i = 1; i <= beesCount; i++)
        {
            GameObject prefab = i <= beesCount / 2 ? beeScoutPrefab : beeWorkerPrefab;
            SpawnBee(prefab);
        }
    }

    private void Awake()
    {
        for (var i = 0; i < flowersSpawnCount; i++)
        {
            GameObject flowerPrefab = flowersPrefabs[Random.Range(0, flowersPrefabs.Length)];
            Vector2 position = new Vector2(Random.Range(-flowersSpawnRadius, flowersSpawnRadius),
                Random.Range(-flowersSpawnRadius, flowersSpawnRadius)) + (Vector2)transform.position;
            Instantiate(flowerPrefab, position, Quaternion.identity, gameObject.transform);
        }
    }

    public void CheckSpawnBee()
    {
        // нет ограничений по спавну
        Points--;
        if (Points <= 0)
        {
            GameObject prefab = Random.Range(0, 100) <= 5 ? beeScoutPrefab : beeWorkerPrefab;
            SpawnBee(prefab);
            Points = pointsToSpawn;
        }
    }
}
