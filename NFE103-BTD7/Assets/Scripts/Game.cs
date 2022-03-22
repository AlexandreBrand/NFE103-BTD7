using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game _instance;
    public static Wave wave;
    public static Player player;


    void Awake()
    {
        //if (_instance == null)
        //{
        //    _instance = this;
        //    //DontDestroyOnLoad(this.gameObject);
        //}
        //else { Destroy(this); }
    }

    public static Game GetInstance()
    {
        return _instance;
    }


    void Init()
    {
        //Wave wave = new Wave();
        wave = Wave.GetInstance();
        player = Player.GetInstance();

        wave.waveStarted = false;
        wave.paused = false;
        wave.waveNumber = 1;
        wave.waveLvL.text = "LvL " + wave.waveNumber.ToString();
        wave.maxObstacles = 30;
        wave.monstersAmount = 3;

        player.LifePoints = 100;
        player.GoldCoins = 300;
    }


    void Start()
    {
        Init();
    }

    public void Lost()
    {
        //TODO fin de partie
    }
}
