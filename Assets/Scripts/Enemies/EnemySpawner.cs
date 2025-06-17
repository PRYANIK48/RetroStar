using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public GameObject prefab;
    [Range(0f, 1f)] public float spawnChance;
}

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Тип врага и спавнрейт")]
    public EnemyType[] zombieTypes;

    private void Start()
    {
        SpawnRandomZombie();
    }

    private void SpawnRandomZombie()
    {
        float roll = Random.value;
        float cumulative = 0f;

        foreach (var zombie in zombieTypes)
        {
            cumulative += zombie.spawnChance;

            if (roll <= cumulative)
            {
                Instantiate(zombie.prefab, transform.position, Quaternion.identity);
                return;
            }
        }
    }
}
