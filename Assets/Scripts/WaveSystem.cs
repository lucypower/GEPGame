using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public float m_timeBetweenSpawn;
    public GameObject m_enemies;

    private void Start()
    {
        StartCoroutine(SpawnTimer(m_timeBetweenSpawn));
    }

    public void SpawnWave()
    {
        for (int i = 0; i < Random.Range(1, 5); i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-8, 10), Random.Range(-3, 6));
            Instantiate(m_enemies, spawnPosition, Quaternion.identity);
        }

        StartCoroutine(SpawnTimer(m_timeBetweenSpawn));
    }

    public IEnumerator SpawnTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SpawnWave();
    }













    //private Wave m_currentWave;
    //private float m_currentWaveNumber;
    //public Transform m_spawnPosition;



    //public class Wave
    //{
    //    public int m_enemiesToSpawn;
    //    public float m_timeBetweenSpawn;
    //}
}
