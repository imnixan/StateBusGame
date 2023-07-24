using System.Collections;
using UnityEngine;

public class EnemySpawner : AbstractManager
{
    private const int MinEnemiesCount = 2;

    [SerializeField]
    private Enemy[] enemyPrefabs;

    private WaitForSeconds waitForSeconds;
    private Transform spawnerTransform;
    private Vector2 minBorders,
        maxBorders;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;

        spawnerTransform = transform;
        waitForSeconds = new WaitForSeconds(0.5f);
    }

    public override void OnGameStarted()
    {
        SetBorders();

        StartCoroutine(SpawnEnemy());
    }

    private void SetBorders()
    {
        minBorders = camera.ViewportToWorldPoint(Vector2.zero);
        maxBorders = camera.ViewportToWorldPoint(new Vector2(1, 1));
        if (minBorders.x > maxBorders.x)
        {
            Vector2 timed = minBorders;
            minBorders = maxBorders;
            maxBorders = timed;
        }
    }

    public override void OnMenu()
    {
        StopAllCoroutines();
    }

    public override void OnGameEnded()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (transform.childCount < MinEnemiesCount)
            {
                SpawnRandomEnemy();
            }
            yield return waitForSeconds;
        }
    }

    private void SpawnRandomEnemy()
    {
        Instantiate(
                enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                GetRandomCoord(),
                new Quaternion(),
                spawnerTransform
            )
            .Init();
    }

    private Vector2 GetRandomCoord()
    {
        float side = Random.Range(0, 2) > 0 ? 1 : -1;
        Vector2 spawnPosition;
        spawnPosition.y = Random.Range(maxBorders.y, maxBorders.y / 2 - 2);
        spawnPosition.x = (maxBorders.x + 2) * side;
        return spawnPosition;
    }
}
