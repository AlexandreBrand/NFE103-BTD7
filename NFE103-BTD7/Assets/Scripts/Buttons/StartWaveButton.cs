using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaveButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave()
    {
        if (!Game.wave.waveStarted)
        {
            Game.wave.waveStarted = true;
        }
    }
}
