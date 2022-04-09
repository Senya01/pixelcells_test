using System.Collections.Generic;
using UnityEngine;

public class GeneratorHive : MonoBehaviour
{
    [SerializeField] GameObject hivePrefab;
    [SerializeField] int radius = 100;

    [SerializeField] float _width = 142;
    [SerializeField] float _height = 119;

    public List<Vector2> hives;

    private void Awake()
    {
        Generator();
    }

    private void Generator()
    {
        for (int i = 0; i < Random.Range(2000, 2000); i++)
        {
            Vector2 position = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
            if (GetHivePosition(position))
            {
                Instantiate(hivePrefab, position, Quaternion.identity);
                hives.Add(position);
            }
        }
    }

    bool GetHivePosition(Vector2 hivePosition)
    {
        for (int i = 0; i < hives.Count; i++)
        {
            float hty = hives[i].y + _height / 100 / 2;
            float hby = hives[i].y - _height / 100 / 2;
            float pty = hivePosition.y + _height / 100 / 2;
            float pby = hivePosition.y - _height / 100 / 2;
            float htx = hives[i].x + _width / 100 / 2;
            float hbx = hives[i].x - _width / 100 / 2;
            float ptx = hivePosition.x + _width / 100 / 2;
            float pbx = hivePosition.x - _width / 100 / 2;

            if (hty <= pty && hty >= pby || hby <= pty && hby >= pby)
            {
                if (htx <= ptx && htx >= pbx || hbx <= ptx && hbx >= pbx)
                {
                    return false;
                }
            }
        }

        return true;
    }
}