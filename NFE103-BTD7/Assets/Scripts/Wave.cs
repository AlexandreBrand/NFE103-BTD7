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
    public int waveNumber;
    public bool paused;
    public Text waveStateText;


    public GameObject enemyFactory;

    public void Init()
    {
        waveStarted = false;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(this); }
    }

    public static Wave GetInstance()
    {
        return _instance;
    }


    // Start is called before the first frame update
    public void Start()
    {
        int diff = Game.GetInstance().difficulty;
        maxObstacles = 30;

        GameObject newEnemy = Instantiate(enemyFactory);
        newEnemy.transform.position = MapGenerator.GetInstance().StartC.transform.position;

        AstarPath.active.Scan();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
