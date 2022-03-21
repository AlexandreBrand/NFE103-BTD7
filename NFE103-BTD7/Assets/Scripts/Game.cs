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
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(this); }
    }

    public static Game GetInstance()
    {
        return _instance;
    }


    void Init()
    {
        wave = Wave.GetInstance();
        player = Player.GetInstance();

        wave.waveStarted = false;
        wave.paused = false;
        wave.waveNumber = 0;
        wave.maxObstacles = 30;

        player.LifePoints = 100;
        player.GoldCoins = 300;
    }


    void Start()
    {
        Init();
        Debug.Log(wave.waveStarted);
    }

    public void Lost()
    {
        //TODO fin de partie
    }
}
