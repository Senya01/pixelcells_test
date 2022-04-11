using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hive : MonoBehaviour
{
    [Header("Префабы")]
    [SerializeField] GameObject beeScoutPrefab;
    [SerializeField] GameObject beeWorkerPrefab;
    [SerializeField] GameObject beeDefenderPrefab;
    [SerializeField] GameObject[] flowersPrefabs;

    [Header("Настройки")]
    [SerializeField,Tooltip("Радиус спавна цветов")] float flowersSpawnRadius;
    [SerializeField,Tooltip("Кол-во цветов")] int flowersSpawnCount;
    [SerializeField,Tooltip("Количество \"очков\" для появления новой пчелы")] int pointsToSpawn;

    [Header("Разведчики")]
    [Header("Генерация пчёл")]
    [SerializeField,Tooltip("Максимальное количество")] int maxBeeScoutCount;
    [SerializeField,Tooltip("Вероятность появления, если их меньше максимального числа (%)")] int beeScoutSpawnChance;
    [Header("Защитники")]
    [SerializeField,Tooltip("Максимальное количество")] int maxBeeDefenderCount;
    [SerializeField,Tooltip("Спавнить, если в улье больше пчел, чем")] int beeDefenderSpawnIfMoreThen;
    [SerializeField,Tooltip("Вероятность появления, если их меньше максимального числа (%)")] int beeDefenderSpawnChance;
    
    private static int _points = 0;
    
    private static int _beeScoutCount = 0;
    private static int _beeWorkerCount = 0;
    private static int _beeDefenderCount = 0;
    private static readonly int TotalBeeCount = _beeScoutCount + _beeDefenderCount + _beeWorkerCount;

    [HideInInspector] public List<Vector2> knownFlowers;

    void Start()
    {
        _points = pointsToSpawn;
        Generator();
    }

    private void SpawnBee(GameObject prefab)
    {
        if (prefab == beeScoutPrefab) _beeScoutCount++;
        if (prefab == beeWorkerPrefab) _beeWorkerCount++;
        if (prefab == beeDefenderPrefab) _beeDefenderCount++;
        GameObject bee = Instantiate(prefab, transform.position, Quaternion.identity, gameObject.transform);
        bee.GetComponent<Bee>().hive = this;
    }

    private void Generator()
    {
        var beesCount = Random.Range(2, 4);
        for (int i = 1; i <= beesCount; i++)
        {
            GameObject prefab = i <= beesCount / 2 ? beeScoutPrefab : beeWorkerPrefab;
            SpawnBee(prefab);
        }
    }

    private void Awake()
    {
        RandomPrefab();
        
        for (int i = 0; i < flowersSpawnCount; i++)
        {
            GameObject flowerPrefab = flowersPrefabs[Random.Range(0, flowersPrefabs.Length)];
            Vector2 position = new Vector2(Random.Range(-flowersSpawnRadius, flowersSpawnRadius),
                Random.Range(-flowersSpawnRadius, flowersSpawnRadius)) + (Vector2) transform.position;
            Instantiate(flowerPrefab, position, Quaternion.identity, gameObject.transform);
        }
    }

    private int SpawnChance(List<int> chances)
    {
        int chance = Random.Range(0, 100) + 1;
        for (int i = 0; i < chances.Count; i++)
        {
            var ch = chances[i];
            if (chance <= ch) return i;
        }

        return 0;
    }

    private GameObject RandomPrefab()
    {
        // переписать... что-то странное
        int beeWorkerSpawnChance = 100;
        List<int> chances = new List<int>();

        if (_beeScoutCount < maxBeeScoutCount)
        {
            beeWorkerSpawnChance -= beeScoutSpawnChance;
            chances.Add(beeScoutSpawnChance);
        }

        if (_beeDefenderCount < maxBeeDefenderCount && TotalBeeCount > beeDefenderSpawnIfMoreThen)
        {
            beeWorkerSpawnChance -= beeDefenderSpawnChance;
            chances.Add(beeDefenderSpawnChance);
        }
        
        chances.Add(beeWorkerSpawnChance);
        int randomIdx = SpawnChance(chances);
        return beeWorkerPrefab;
    }
    
    public void CheckSpawnBee()
    {
        _points--;
        if (_points <= 0)
        {
            GameObject prefab = _beeScoutCount < 5
                ? Random.Range(0, 100) <= 5 ? beeScoutPrefab : beeWorkerPrefab
                : beeWorkerPrefab;

            RandomPrefab();
            
            SpawnBee(prefab);
            _points = pointsToSpawn;
        }
    }
}