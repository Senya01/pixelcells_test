using System.Collections.Generic;
using UnityEngine;

public class GeneratorHive : MonoBehaviour
{
    [SerializeField] GameObject hivePrefab;
    [SerializeField] int spawnRadius;
    [SerializeField] Vector2Int count;
    [SerializeField] Vector2 prefabSize;

    [HideInInspector] public List<Vector2> hives;

    private void Awake()
    {
        Generator();
        // Debug.Log((int)((((spawnRadius * 2) / (prefabSize.x / 100)) * ((spawnRadius * 2) / (prefabSize.y / 100))) / 1.5));
    }

    private void Generator()
    {
        for (int i = 0; i < Random.Range(count.x, count.y); i++)
        {
            Vector2 position = new Vector2(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
            if (GetHivePosition(position))
            {
                Instantiate(hivePrefab, position, Quaternion.identity);
                hives.Add(position);
            }
        }
    }

    public bool GetHivePosition(Vector2 hivePosition)
    {
        for (int i = 0; i < hives.Count; i++)
        {
            /*
             * h - улей из списка ульев
             * n - новый улей
             * t - top
             * b - bottom
            */
            float hty = hives[i].y + prefabSize.y / 100 / 2;
            float hby = hives[i].y - prefabSize.y / 100 / 2;
            float nty = hivePosition.y + prefabSize.y / 100 / 2;
            float nby = hivePosition.y - prefabSize.y / 100 / 2;
            float htx = hives[i].x + prefabSize.x / 100 / 2;
            float hbx = hives[i].x - prefabSize.x / 100 / 2;
            float ntx = hivePosition.x + prefabSize.x / 100 / 2;
            float nbx = hivePosition.x - prefabSize.x / 100 / 2;

            if (hty <= nty && hty >= nby || hby <= nty && hby >= nby)
            {
                if (htx <= ntx && htx >= nbx || hbx <= ntx && hbx >= nbx)
                {
                    return false;
                }
            }
        }

        return true;
    }
}