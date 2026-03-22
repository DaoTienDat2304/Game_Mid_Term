using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] enemyWaves;

    private int currentWave;

    void Start()
    {
        if (enemyWaves == null || enemyWaves.Length == 0)
        {
            Debug.LogWarning("No enemy waves assigned!");
            return;
        }

        StartCoroutine(SpawnWaveRoutine());
    }

    private IEnumerator SpawnWaveRoutine()
    {
        while (currentWave < enemyWaves.Length)
        {
            yield return StartCoroutine(SpawnEnemyWave(enemyWaves[currentWave]));
            yield return new WaitForSeconds(enemyWaves[currentWave].nextWaveDelay);
            currentWave++;
        }
    }

    // ✅ đổi sang coroutine để spawn có delay
    private IEnumerator SpawnEnemyWave(EnemyWave waveInfo)
    {
        if (waveInfo.flyPath == null || waveInfo.flyPath.waypoints.Length == 0)
        {
            Debug.LogWarning("FlyPath is missing!");
            yield break;
        }

        Vector3 startPosition = waveInfo.flyPath[0];

        for (int i = 0; i < waveInfo.numberOfEnemy; i++)
        {
            SpawnSingleEnemy(waveInfo, startPosition);
            startPosition += waveInfo.formationOffset;

            // ✅ delay giữa từng enemy → không bay sync
            yield return new WaitForSeconds(0.9f);
        }
    }

    private void SpawnSingleEnemy(EnemyWave waveInfo, Vector3 position)
    {
        var enemy = Instantiate(
            waveInfo.enemyPrefab,
            position,
            Quaternion.identity
        );

        var agent = enemy.GetComponent<FlyPathAgent>();

        if (agent == null)
        {
            Debug.LogWarning("Enemy prefab missing FlyPathAgent!");
            return;
        }

        agent.flyPath = waveInfo.flyPath;

        // ✅ random speed nhẹ → tránh chết cùng lúc
        agent.flySpeed = waveInfo.speed + Random.Range(-0.05f, 0.05f);
    }
}