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
    [SerializeField,Tooltip("Кол-во пчёл")] Vector2 beeSpawnCount;
    [SerializeField,Tooltip("Количество \"очков\" для появления новой пчелы")] int pointsToSpawn;

    [Header("Новый улей")]
    [SerializeField, Tooltip("Кол-во пчел для нового улья")] int beesForNewHive;
    [SerializeField, Tooltip("Расстояние нового улья")] int radiusNewHive;
    
    [Header("Разведчики")]
    [Header("Генерация пчёл")]
    [SerializeField,Tooltip("Максимальное количество")] int maxBeeScoutCount;
    [SerializeField,Tooltip("Вероятность появления, если их меньше максимального числа (%)")] int beeScoutSpawnChance;
    [Header("Защитники")]
    [SerializeField,Tooltip("Максимальное количество")] int maxBeeDefenderCount;
    [SerializeField,Tooltip("Спавнить, если в улье больше пчел, чем")] int beeDefenderSpawnIfMoreThen;
    [SerializeField,Tooltip("Вероятность появления, если их меньше максимального числа (%)")] int beeDefenderSpawnChance;
    
    
    private static int _points = 0;
    
    private int _beeScoutCount = 0;
    private int _beeWorkerCount = 0;
    private int _beeDefenderCount = 0;

    [HideInInspector] public List<Vector2> knownFlowers;

    private int TotalBeeCount()
    {
        return _beeScoutCount + _beeDefenderCount + _beeWorkerCount;
    } 
    
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
        var beesCount = Random.Range(beeSpawnCount.x, beeSpawnCount.y);
        for (int i = 1; i <= beesCount; i++)
        {
            GameObject prefab = i <= beesCount / 2 ? beeScoutPrefab : beeWorkerPrefab;
            SpawnBee(prefab);
        }
    }

    private void Awake()
    {
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
        List<int> chances = new List<int>();
        List<GameObject> prefabs = new List<GameObject>();

        if (_beeScoutCount < maxBeeScoutCount)
        {
            chances.Add(beeScoutSpawnChance);
            prefabs.Add(beeScoutPrefab);
        }

        if (_beeDefenderCount < maxBeeDefenderCount && TotalBeeCount() > beeDefenderSpawnIfMoreThen)
        {
            if (chances.Count > 0)
            {
                chances.Add(beeDefenderSpawnChance + chances[chances.Count - 1]);
            }
            else
            {
                chances.Add(beeDefenderSpawnChance);
            }
            prefabs.Add(beeDefenderPrefab);
        }
        
        chances.Add(100);
        prefabs.Add(beeWorkerPrefab);
        int randomIdx = SpawnChance(chances);
        return prefabs[randomIdx];
    }
    
    public void CheckSpawnBee()
    {
        _points--;
        if (_points <= 0)
        {
            // GameObject prefab = _beeScoutCount < 5
            //     ? Random.Range(0, 100) <= 5 ? beeScoutPrefab : beeWorkerPrefab
            //     : beeWorkerPrefab;

            GameObject prefab = RandomPrefab();

            SpawnBee(prefab);
            _points = pointsToSpawn;
        }
    }
}