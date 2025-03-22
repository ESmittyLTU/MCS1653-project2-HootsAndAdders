using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int startingHealth;
    public TextMeshProUGUI livesCount;
    public GameObject enemy, bundleEnemy;
    public float spawnDelay = 2f;
    public static TextMeshProUGUI livesCounter, moneyCounter;
    public GameObject deathScreen, winScreen;
    public static int health = 10;
    public int[] enemyWaves;
    public bool[] isWaveBundle;
    public float initialDelay = 15f;
    public AudioClip winSong, loseSong;
    public static int money;

    private bool preRound = true;
    private int wave = 0;
    private EnemyPathing[] enemies;
    private bool noMoreSpawning, wonRound = false;
    private bool runWave = false;

    private void Start()
    {
        health = startingHealth;
        livesCounter = livesCount;

        livesCounter.SetText($"{health}");
        Debug.Log($"Player health starting at {health}"); 
    }

    void spawnEnemies()
    {
        for (int i = 0; i < enemyWaves[wave]; i++)
        {
            if (isWaveBundle[wave])
            {
                Invoke("spawnBundleEnemy", spawnDelay * i * 2);
            } 
            else
            {
                Invoke("spawnSingleEnemy", spawnDelay * i);
            }

            Debug.Log("Current index of wave is " + i);
            Debug.Log("Wave enemy amount is currently " + enemyWaves[wave]);
            if (i == enemyWaves[wave] - 1)
            {
                Debug.Log("Wave finished");
                Debug.Log("Moving on to wave index #" + wave);
                if (isWaveBundle[wave])
                {
                    //extra delay for bundle waves
                    Invoke("beginNewWave", spawnDelay * i * 2);
                } else
                {
                    Invoke("beginNewWave", spawnDelay * i );
                }
            }
        }

    }

    void beginNewWave()
    {
        //After running wave, check if it was the last wave. If so, stop spawning stuff
        if (wave == enemyWaves.Length - 1)
        {
            runWave = false;
            noMoreSpawning = true;
        } else
        {
            wave++;
            runWave = true;
        }
    }


    void spawnSingleEnemy()
    {
        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(-10.5f, -3.9f, 0), Quaternion.identity);
    }

    void spawnBundleEnemy()
    {
        GameObject spawnedEnemy = Instantiate(bundleEnemy, new Vector3(-10.5f, -3.9f, 0), Quaternion.identity);
    }

    private void Update()
    {
        if (preRound)
        {
            if (Time.time >= initialDelay)
            {
                runWave = true;
                preRound = false;
            }
            else return;
        }

        if (runWave)
        {
            Debug.Log("running new wave");
            spawnEnemies();
            runWave = false;
        }
        
        //Win condition
        if (noMoreSpawning && health > 0 && !wonRound)
        {
            enemies = FindObjectsOfType<EnemyPathing>();
            if (enemies.Length <= 0)
            {
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
                AudioSource.PlayClipAtPoint(winSong, transform.position);
                Debug.Log("YOU WIN");
                wonRound = true;
                winScreen.SetActive(true);
            }
        }

        //Lose condition
        if (health <= 0 && !wonRound)
        {
            wonRound = true;
            AudioSource.PlayClipAtPoint(loseSong, transform.position);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            Debug.Log("YOU LOSE");
            deathScreen.SetActive(true);
        }
    }
}
