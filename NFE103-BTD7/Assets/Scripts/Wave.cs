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
    public Text waveStateText;
    public TextMeshProUGUI waveLvL;

    public GameObject ennemy;
    public EnemyFactory enemyFactory;
    private float lastTimeSpawn;

    void Awake()
    {
        //if (_instance == null)
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else { Destroy(this); }
        _instance = this;
    }

    private void Start()
    {
        //GameObject newEnemy = Instantiate(ennemy);


        //GameObject newEnemy = EnemyFactory.GetEnemy(EnemyType.Tank);
        //newEnemy.transform.position = MapGenerator.GetInstance().StartC.transform.position;
        //monstersLeft = 1;
        lastTimeSpawn = Time.time;
    }

    private void Update()
    {
        endWave(waveEndBounty);

        double time = Time.time - lastTimeSpawn;
        Debug.Log(time);
        if (0.2f < time)
        {
                GameObject newEnemy = EnemyFactory.GetEnemy(EnemyType.Tank);
                newEnemy.transform.position = MapGenerator.GetInstance().StartC.transform.position;
                Debug.Log(newEnemy);
                lastTimeSpawn = Time.time;
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

    public void createEnemies()
    {
        for (int i = 0; i < monstersAmount; i++)
        {
            //instancier monstres
        }
    }
}
