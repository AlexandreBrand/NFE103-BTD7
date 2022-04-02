using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Wave : MonoBehaviour
{
    private static Wave _instance;

    
    public bool waveStarted;
    public bool paused;
    public bool quitMenu;

    public int waveNumber;
    public double maxObstacles;
    public int waveEndBounty;
    public bool diff_select;

    public int monsterNbr;
    public int tanksNbr;
    public int tanksLeft;
    public int assassinsNbr;
    public int assassinsLeft;
    public int knightNbr;
    public int knightLeft;
    public int monstersLeft;
    
    
    public TextMeshProUGUI waveStateText;
    public TextMeshProUGUI waveLvL;
    public float lastTimeSpawn;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        waveStarted = false;
        paused = false;
        quitMenu = false;

        waveNumber = 0;
        maxObstacles = 0;
        waveEndBounty = 0;
        diff_select = true;

        monstersLeft = 0;
        assassinsLeft = 0;
        knightLeft = 0;
        tanksLeft = 0;
        lastTimeSpawn = Time.time;
        waveLvL.text = "LvL 0";
        monsterNbr = tanksNbr+knightNbr+assassinsNbr;
        tanksNbr = 1;
        assassinsNbr = 1;
        knightNbr = 1;

    }

    private void Update()
    {
        waveLvL.text = "LvL " + waveNumber.ToString();
        monsterNbr = tanksNbr + knightNbr + assassinsNbr;
        //A appeler quand un ennemy meurs ou arrive à la cellule de fin
        //endWave(waveEndBounty);

        double time = Time.time - lastTimeSpawn;

        if (waveStarted && monsterNbr > monstersLeft && 0.6f < time)
        {
            EnemySpawner();
        }
        
    }

    public static Wave GetInstance() { return _instance; }


    public void EnemySpawner()
    {
        int rand = Random.Range(1,4);

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
    }

    public void StartWave()
    {
        waveStarted = true;
        waveStateText.text = "PAUSE";
        waveNumber++;
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
        else if (Wave.GetInstance().paused)
        {
            Time.timeScale = 1;
            paused = false;
            waveStateText.text = "PAUSE";
        }
    }


    public void endWave(int WaveEndBounty)
    {
        //Plus de points de vie
        if (Player.GetInstance().LifePoints == 0) { Game.GetInstance().Lost(); }

        //Tous les ennemis battus
        else if (monstersLeft == 0)
        {
            waveStarted = false;
            waveStateText.text = "START";
            Player.GetInstance().GoldCoins += WaveEndBounty;
        }
    }
}
