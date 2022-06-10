using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;
    
    WaveConfig currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfig GetCurerntWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do{
            foreach (WaveConfig wave in waveConfigs)
            {
                currentWave = wave;
                int enemyCount = currentWave.GetEnemyCount();

                for (int index = 0; index < enemyCount; index++)
                {
                    CreateEnemy(index);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while(isLooping);
    }

    void CreateEnemy(int index)
    {
        GameObject enemyPrefab = currentWave.GetEnemyPrefab(index);
        Transform startWavePoint = currentWave.GetStartingWavePoint();

        Instantiate(enemyPrefab, startWavePoint.position, Quaternion.Euler(0, 0, 180), transform);
    }
}
