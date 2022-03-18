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
        //Si pas de vague en cours
        if (!Game.wave.waveStarted)
        {
            //Démarrer vague
            Game.wave.waveStarted = true;
            Game.wave.waveStateText.text = "Pause";
            Game.wave.waveNumber++;
        }
        //Si vague en cours
        else if (Game.wave.waveStarted)
        {
            //Mettre en pause
            if (!Game.wave.paused)
            {
                Time.timeScale = 0;
                Game.wave.paused = true;
                Game.wave.waveStateText.text = "Resume";
            }
            //Reprendre
            else if (Game.wave.paused)
            {
                Time.timeScale = 1;
                Game.wave.paused = false;
                Game.wave.waveStateText.text = "Pause";
            }
            
        }
    }
}
