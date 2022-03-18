using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game _instance;

    public static Wave wave;

    [SerializeField] public int difficulty;


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
        wave.paused = false;
        wave.waveNumber = 0;
    }


    void Start()
    {
        Init();
    }
}
