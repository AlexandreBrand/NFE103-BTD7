using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.VersionControl;

public class Wave : MonoBehaviour
{
    private static Wave _instance;

    public bool waveStarted = false;
    public bool paused = false;
    public bool quitMenu = false;
    public bool placeTower = false;
    public bool diff_select = true;

    public double maxObstacles = 0;
    public int waveEndBounty = 0;
    public double bountyCoef = 0;
    public double monsterCoef = 0;

    public int tanksNbr = 0;
    public int assassinsNbr = 0;
    public int knightNbr = 0;

    private int waveNumber;
    private int monsterNbr;
    private int tanksLeft;
    private int assassinsLeft = 0;
    private int knightLeft = 0;
    private int monstersLeft = 0;
    private int spawnedMonsters = 0;

    public GameObject selectedTower;
    
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
        waveNumber = 1;
        waveLvL.text = "LvL 1";
        lastTimeSpawn = Time.time;

        monsterNbr = tanksNbr+knightNbr+assassinsNbr;

    }

    private void Update()
    {
        double time = Time.time - lastTimeSpawn;
        if (waveStarted && monsterNbr > monstersLeft && 0.5f < time)
        {
            EnemySpawner();
        }
    }

    public static Wave GetInstance() { return _instance; }


    public void EnemySpawner()
    {
        int types_nbr = Enum.GetNames(typeof(EnemyType)).Length;
        int rand = UnityEngine.Random.Range(1,types_nbr+1);
        
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
                break;
        }  
    }

    public void CreateEnemy(EnemyType type)
    {
        GameObject newEnemy = EnemyFactory.GetEnemy(type);
        newEnemy.transform.position = MapGenerator.GetInstance().StartC.transform.position;
        lastTimeSpawn = Time.time;
        monstersLeft++;
        spawnedMonsters++;

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
        AstarPath.active.Scan();
        monsterNbr = tanksNbr + knightNbr + assassinsNbr;
        Game.GetInstance().GameStarted = true;
        waveStateText.text = "PAUSE";
        waveLvL.text = "LvL " + waveNumber.ToString();
        waveStarted = true;
        GetComponent<ObstaclesCreation>().obs_restants.text = "";
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
        spawnedMonsters = 0;

        waveNumber++;
        waveLvL.text = "LvL " + waveNumber.ToString();
        waveStarted = false;
        Game.GetInstance().Message.text = "";
    }

    public void endWave()
    {
        if (Game.GetInstance().GameStarted)
        {
            //Plus de points de vie
            if (Player.GetInstance().LifePoints <= 0) Game.GetInstance().Lost();

            //Tous les ennemis battus
            else if (monstersLeft == 0 && spawnedMonsters == monsterNbr) winWave();

        }
    }
}
