using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public int maxObstacles { get; set; }
    public bool waveStarted { get; set; }
    public int monstersAmount { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        waveStarted = false;
        maxObstacles = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
