using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject[] enemies;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        // Pick a random index from our spawnPoints array
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        // Pick a random enemy to spawn
        int enemyIndex = Random.Range(0, enemies.Length);
        GameObject enemy = enemies[enemyIndex];

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
