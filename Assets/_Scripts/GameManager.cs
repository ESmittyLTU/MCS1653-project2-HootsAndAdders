using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int startingHealth;
    public TextMeshProUGUI livesCount, moneyCount, roundCounter;
    public GameObject enemy, bundleEnemy;
    public float spawnDelay = 2f;
    public static TextMeshProUGUI livesCounter, moneyCounter;
    public GameObject deathScreen, winScreen;
    public static int health = 10;
    public int[] enemyWaves;
    public bool[] isWaveBundle;
    public float initialDelay = 15f;
    public AudioClip winSong, loseSong, dingSound;
    public static int money = 5;
    public int startingMoney = 25;
    public float timeBetweenRounds = 15f;
    public int round = 1;

    private bool betweenRounds = false;
    private bool preRound = true;
    private int wave = 0;
    private EnemyPathing[] enemies;
    private bool noMoreSpawning, wonRound = false;
    private bool runWave = false;
    private float timerBetweenRounds, initialDelayTimer = 0;

    private void Start()
    {
        //Cant set pub static vars from unity inspector, other classes have access to it, but I need to tell the pub static TMPS on start that they are TMPS I set from unity inspector
        health = startingHealth;
        money = startingMoney;
        livesCounter = livesCount;
        moneyCounter = moneyCount;
        preRound = true;

        moneyCounter.SetText($"{money}");
        livesCounter.SetText($"{health}");
        roundCounter.SetText($"Round: {round}/6");
        Debug.Log($"Player health starting at {health}"); 
    }

    void spawnEnemies()
    {
        //Starting at 0, spawn the proper enemy at the proper time until all the enemies in the wave have spawned
        for (int i = 0; i < enemyWaves[wave]; i++)
        {
            //Which enemy to spawn
            if (isWaveBundle[wave])
            {
                Invoke("spawnBundleEnemy", spawnDelay * i * 2 + Random.Range(0f, .8f));
            } 
            else
            {
                Invoke("spawnSingleEnemy", spawnDelay * i + Random.Range(0f, .6f));
            }

            Debug.Log("Current index of wave is " + i);
            Debug.Log("Wave enemy amount is currently " + enemyWaves[wave]);
            //If the wave is finished, begin a new wave of the right type, and delay it
            if (i == enemyWaves[wave] - 1)
            {
                Debug.Log("Wave finished");
                Debug.Log("Moving on to wave index #" + wave);
                //Check for endRound conditons
                if (wave != enemyWaves.Length - 1 && enemyWaves[wave+1] >= 500)
                {
                    Invoke("roundEnd", spawnDelay * i);
                }
                else if (isWaveBundle[wave])
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

    void roundEnd() //Create a pause and update the round counter
    {
        wave++; // SKip 500 enemies
        betweenRounds = true;
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
            initialDelayTimer += Time.deltaTime;
            if (initialDelayTimer >= initialDelay)
            {
                runWave = true;
                preRound = false;
            }
            else return;
        }

        if (betweenRounds)
        {
            timerBetweenRounds += Time.deltaTime;
            if (timerBetweenRounds >= timeBetweenRounds)
            {
                if (round == 3 || round == 4 || round == 5 || round == 6)
                {
                    spawnDelay -= .2f;
                }
                betweenRounds = false; // Reset timer and bool
                timerBetweenRounds = 0;
                round++; // Update round count
                AudioSource.PlayClipAtPoint(dingSound, transform.position);
                roundCounter.SetText("Round: " + round + "/6");
                beginNewWave();
            }
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
            Time.timeScale = 0;
        }
    }
}
