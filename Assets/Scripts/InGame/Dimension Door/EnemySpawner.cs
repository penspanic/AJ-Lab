using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public DD_Enemy[] normalEnemyPrefabs;
    public DD_Enemy bossPrefab;
    public float spawnInterval;
    public int spawnCount;

    DD_Game game;


    void Awake()
    {
        game = GameObject.FindObjectOfType<DD_Game>();
        StartCoroutine(SpawnProcess());
    }

    IEnumerator SpawnProcess()
    {
        GameObject newEnemy = null;
        while (!game.gameOver && spawnCount > 0)
        {
            newEnemy = Instantiate(normalEnemyPrefabs[Random.Range(0, normalEnemyPrefabs.Length)]).gameObject;
            newEnemy.transform.position = transform.position;

            yield return new WaitForSeconds(spawnInterval);
            spawnInterval -= 0.1f;
            spawnCount--;
        }
    }
}