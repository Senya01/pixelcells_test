using UnityEngine;

public class GeneratorHive : MonoBehaviour
{
    [SerializeField] GameObject hive;
    
    private int radius = 50;

    private void Awake()
    {
        Generator();
    }

    private void Generator()
    {
        for (int i = 0; i < Random.Range(3, 5); i++)
        {
            Vector2 position = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
            Instantiate(hive, position, Quaternion.identity);
        }
    }
}
