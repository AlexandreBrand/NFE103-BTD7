using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public int maxObstacles { get; set; }
    public bool waveStarted { get; set; }
    public int monstersAmount { get; set; }
    public int waveNumber { get; set; }

    public GameObject enemyFactory;

    public void Init()
    {
        waveStarted = false;
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
