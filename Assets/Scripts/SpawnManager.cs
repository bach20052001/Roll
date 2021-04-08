using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    private float range = 10;
    private int enemyCount;
    private int nEnemy = 1;
    public GameObject powerUp;
    public static SpawnManager instace;

    public static SpawnManager Instance()
    {
        if (instace == null)
            return new SpawnManager();
        return instace;
    }

    // Start is called before the first frame update
    void Start()
    {
        instace = GetComponent<SpawnManager>();
        SpawnEnemyWave(nEnemy);
        Instantiate(powerUp, GenerateSpawnPoint(), Quaternion.identity);
    }
    private void SpawnEnemyWave(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Instantiate(enemy, GenerateSpawnPoint(), Quaternion.identity);
        }
    }

    private Vector3 GenerateSpawnPoint()
    {
        float posX = Random.Range(-range, range);
        float posZ = Random.Range(-range, range);
        return new Vector3(posX, 0, posZ);

    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(++nEnemy);
            Instantiate(powerUp, GenerateSpawnPoint(), Quaternion.identity);
        }
    }
}
