using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public int maxObstacles { get; set; }
    public bool waveStarted = true;
    public int monstersAmount { get; set; }
    public int waveNumber { get; set; }


    public void Init()
    {
        waveStarted = false;
    }

    // Start is called before the first frame update
    public void Start()
    {
        maxObstacles = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
