using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform spawnPlayer;
    [SerializeField] GameObject player;

    [Header("Enemy")]
    [SerializeField] Transform[] spawnEnemy;
    [SerializeField] GameObject[] enemy;
    [SerializeField] int maxEnemiesPerSpawnPoint = 5;
    [SerializeField] float spawnInterval = 10f;

    private List<List<GameObject>> spawnedEnemies;

    private void Start()
    {
        spawnedEnemies = new List<List<GameObject>>();
        for (int i = 0; i < spawnEnemy.Length; i++)
        {
            spawnedEnemies.Add(new List<GameObject>());
        }

        SpawnPlayer();
        for (int i = 0; i < spawnEnemy.Length; i++)
        {
            StartCoroutine(SpawnEnemy(i));
        }
    }

    void SpawnPlayer()
    {
        if (spawnPlayer != null && player != null)
        {
            Instantiate(player, spawnPlayer.position, spawnPlayer.rotation);
        }
    }

    IEnumerator SpawnEnemy(int spawnIndex)
    {
        while (true)
        {
            if (spawnedEnemies[spawnIndex].Count < maxEnemiesPerSpawnPoint)
            {
                for (int i = 0; i < maxEnemiesPerSpawnPoint - spawnedEnemies[spawnIndex].Count; i++)
                {
                    GameObject newEnemy = Instantiate(enemy[spawnIndex], spawnEnemy[spawnIndex].position, spawnEnemy[spawnIndex].rotation);
                    spawnedEnemies[spawnIndex].Add(newEnemy);
                }
            }
            CleanUpEnemies(spawnIndex);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void CleanUpEnemies(int spawnIndex)
    {
        for (int i = spawnedEnemies[spawnIndex].Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[spawnIndex][i] == null)
            {
                spawnedEnemies[spawnIndex].RemoveAt(i);
            }
        }
    }
}
