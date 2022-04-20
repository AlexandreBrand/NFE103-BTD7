using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Wave : MonoBehaviour
{
    private static Wave _instance;

    
    public bool waveStarted;
    public bool paused;
    public bool quitMenu;
    public bool spawned;

    public int waveNumber;
    public double maxObstacles;
    public int waveEndBounty;
    public bool diff_select;
    public double bountyCoef;

    public int monsterNbr;
    public int tanksNbr;
    public int tanksLeft;
    public int assassinsNbr;
    public int assassinsLeft;
    public int knightNbr;
    public int knightLeft;
    public int monstersLeft;
    public double monsterCoef;

    public bool placeTower = false;
    
    public TextMeshProUGUI waveStateText;
    public TextMeshProUGUI waveLvL;
    public float lastTimeSpawn;

    public List<GameObject> enemies;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        waveStarted = false;
        paused = false;
        quitMenu = false;
        spawned = false;

        waveNumber = 1;
        maxObstacles = 0;
        waveEndBounty = 0;
        bountyCoef = 0;
        diff_select = true;

        monstersLeft = 0;
        assassinsLeft = 0;
        knightLeft = 0;
        tanksLeft = 0;
        lastTimeSpawn = Time.time;
        waveLvL.text = "LvL 1";
        monsterNbr = tanksNbr+knightNbr+assassinsNbr;
        tanksNbr = 1;
        assassinsNbr = 1;
        knightNbr = 1;
        monsterCoef = 0;

    }

    private void Update()
    {
        endWave();
        //loseLife();

        double time = Time.time - lastTimeSpawn;
        if (waveStarted && monsterNbr > monstersLeft && 0.5f < time)
        {
            EnemySpawner();
        }  
    }

    public static Wave GetInstance() { return _instance; }


    public void EnemySpawner()
    {
        int rand = UnityEngine.Random.Range(1,4);
        
        switch (rand)
        {
            case 1:
                if(assassinsLeft < assassinsNbr)
                {
                    CreateEnemy(EnemyType.Bloodthirsty);
                    assassinsLeft++;
                }
                break;

            case 2:
                if (knightLeft < knightNbr)
                {
                    CreateEnemy(EnemyType.Knight);
                    knightLeft++;
                }
                break;
            case 3:
                if (tanksLeft < tanksNbr)
                {
                    CreateEnemy(EnemyType.Tank);
                    tanksLeft++;
                }
                break;

            default:
                if (assassinsLeft < assassinsNbr)
                {
                    CreateEnemy(EnemyType.Bloodthirsty);
                    assassinsLeft++;
                }
                break;
        }  
    }

    public void CreateEnemy(EnemyType type)
    {
        GameObject newEnemy = EnemyFactory.GetEnemy(type);
        newEnemy.transform.position = MapGenerator.GetInstance().StartC.transform.position;
        lastTimeSpawn = Time.time;
        monstersLeft++;
        spawned = true;

        enemies.Add(newEnemy);
    }

    public GameObject GetEnemies(int position)
    {
        return enemies[position];
    }
    public int GetEnemiesCount()
    {
        return enemies.Count;
    }
    public bool GetPlaceTower()
    {
        return placeTower;
    }

    public void SetPlaceTower(bool value)
    {
        placeTower = value;
    }

    public void removeEnemy(GameObject enemy) 
    {
        if (enemy.tag == "Enemy")
        {
            enemies.Remove(enemy);
            Destroy(enemy);
        }
    }

    public void ModifMonsterLeft() 
    {
        monstersLeft--;
    }

    public void StartWave()
    {
        monsterNbr = tanksNbr + knightNbr + assassinsNbr;
        Game.GetInstance().GameStarted = true;
        waveStateText.text = "PAUSE";
        waveLvL.text = "LvL " + waveNumber.ToString();
        waveStarted = true;
    }

    public void PauseOrResumeWave()
    {
        //Mettre en pause
        if (!paused)
        {
            Time.timeScale = 0;
            paused = true;
            waveStateText.text = "RESUME";
        }
        //Reprendre
        else if (paused)
        {
            Time.timeScale = 1;
            paused = false;
            waveStateText.text = "PAUSE";
        }
    }

    public void winWave()
    {
        
        waveStateText.text = "START";

        Player.GetInstance().GoldCoins += waveEndBounty;

        if (tanksNbr == 0) tanksNbr = 1;

        waveEndBounty = (int)Math.Round(waveEndBounty * bountyCoef);
        tanksNbr = (int)Math.Round(tanksNbr * monsterCoef);
        knightNbr = (int)Math.Round(knightNbr * monsterCoef);
        assassinsNbr = (int)Math.Round(assassinsNbr * monsterCoef);
        tanksLeft = 0;
        knightLeft = 0;
        assassinsLeft = 0;

        waveNumber++;
        waveStarted = false;
        spawned = false;
    }

    public void endWave()
    {
        if (spawned)
        {
            //Plus de points de vie
            if (Player.GetInstance().LifePoints <= 0) { Game.GetInstance().Lost(); }

            //Tous les ennemis battus
            else if (monstersLeft == 0)
            {
                winWave();
            }
        }

    }
}
