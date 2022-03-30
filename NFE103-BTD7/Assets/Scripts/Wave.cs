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

    public int monstersAmount;
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
        lastTimeSpawn = Time.time;
        waveLvL.text = "LvL " + waveNumber.ToString();
        monstersAmount = 3;

    }

    private void Update()
    {
        //A appeler quand un ennemy meurs ou arrive à la cellule de fin
        //endWave(waveEndBounty);

        double time = Time.time - lastTimeSpawn;
        if (waveStarted == true && monstersAmount > monstersLeft && 0.2f < time)
        {
            createEnemy();    
            lastTimeSpawn = Time.time;
            monstersLeft ++;
        }
    }

    public static Wave GetInstance() { return _instance; }


    public void loseLife(int dmg) { Player.GetInstance().LifePoints -= dmg; }


    public void endWave(int WaveEndBounty)
    {
        //Plus de points de vie
        if(Player.GetInstance().LifePoints == 0)
        {
            Game.GetInstance().Lost();
        }

        //Tous les ennemis battus
        else if(monstersLeft == 0)
        {
            waveStarted = false;
            waveStateText.text = "START";
            Player.GetInstance().GoldCoins += WaveEndBounty;
        }
    }

    public void createEnemy()
    {
        GameObject newEnemy = EnemyFactory.GetEnemy(EnemyType.Knight);
        newEnemy.transform.position = MapGenerator.GetInstance().StartC.transform.position;
    }
}
