using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hive : MonoBehaviour
{
    [SerializeField] GameObject beeScoutPrefab;
    [SerializeField] GameObject beeWorkerPrefab;
    [SerializeField] GameObject[] flowersPrefabs;
    [SerializeField, Range(1, 5)] float flowersSpawnRadius = 3;
    [SerializeField] int flowersSpawnCount = 20;
    [SerializeField] int pointsToSpawn = 5;

    private static int _points = 0;
    private static int _beeScoutCount = 0;
    
    public List<Vector2> knownFlowers;

    void Start()
    {
        _points = pointsToSpawn;
        Generator();
    }

    private void SpawnBee(GameObject prefab)
    {
        if (prefab == beeScoutPrefab) _beeScoutCount++;
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
                Random.Range(-flowersSpawnRadius, flowersSpawnRadius)) + (Vector2) transform.position;
            Instantiate(flowerPrefab, position, Quaternion.identity, gameObject.transform);
        }
    }

    public void CheckSpawnBee()
    {
        _points--;
        if (_points <= 0)
        {
            GameObject prefab = _beeScoutCount < 5
                ? Random.Range(0, 100) <= 5 ? beeScoutPrefab : beeWorkerPrefab
                : beeWorkerPrefab;

            SpawnBee(prefab);
            _points = pointsToSpawn;
        }
    }
}