using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wave : MonoBehaviour
{
    private static Wave _instance;

    public int maxObstacles;
    public bool waveStarted;
    public int monstersAmount;
    public int monstersLeft;
    public int waveNumber;
    public bool paused;
    public int waveEndBounty;
    public TextMeshProUGUI waveStateText;
    public TextMeshProUGUI waveLvL;
    private float lastTimeSpawn;

    void Awake()
    {
        _instance = this;
        //if (_instance == null)
        //{
        //    _instance = this;
        //    //DontDestroyOnLoad(this.gameObject);
        //}
        //else { Destroy(this); }
    }

    private void Start()
    {
        waveStarted = false;
        //GameObject newEnemy = Instantiate(ennemy);
        monstersLeft = 0;
        lastTimeSpawn = Time.time;
    }

    private void Update()
    {
        endWave(waveEndBounty);

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
        else if(monstersLeft == -1)
        {
            waveStarted = false;
            waveStateText.text = "START";
            Player.GetInstance().GoldCoins += WaveEndBounty;
        }
    }

    public void createEnemy()
    {
        GameObject newEnemy = EnemyFactory.GetEnemy(EnemyType.Bloodthirsty);
        newEnemy.transform.position = MapGenerator.GetInstance().StartC.transform.position;
    }
}
