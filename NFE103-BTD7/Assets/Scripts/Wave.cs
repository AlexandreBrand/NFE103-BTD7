using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    private static Wave _instance;

    public int maxObstacles { get; set; }
    public bool waveStarted { get; set; }
    public int monstersAmount { get; set; }
    public int monstersLeft { get; set; }
    public int waveNumber;
    public bool paused;
    public int waveEndBounty;
    public Text waveStateText;

    public GameObject ennemy;
    public EnemyFactory enemyFactory;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(this); }
    }

    private void Start()
    {
        GameObject newEnemy = Instantiate(ennemy);
        newEnemy.transform.position = MapGenerator.GetInstance().StartC.transform.position;
        monstersLeft = 1;
    }

    private void Update()
    {
        endWave(waveEndBounty);
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
