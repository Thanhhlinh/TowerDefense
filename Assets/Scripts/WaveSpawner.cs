using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform spawnPoint;

    public static int EnemiesAlive = 0;

    public Text waveCountDownText;
    public float timeBetweenWaves = 4f;
    private float countDown = 2f;
    private int waveIndex = 0;

    public GameManager gameManager;

    private void Update()
    {
        if(EnemiesAlive > 0)
        {
            return;
        }
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }
        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f , Mathf.Infinity);
        waveCountDownText.text =string.Format("{0:00.00}", countDown);
    }

    IEnumerator SpawnWave()
    {
        PlayerState.rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for(int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f/ wave.rate);
        }
        waveIndex++;

        
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
       
    }
}
