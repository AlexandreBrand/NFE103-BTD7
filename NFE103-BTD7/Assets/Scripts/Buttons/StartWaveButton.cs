using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaveButton : MonoBehaviour
{
    public void StartWave()
    {
        Wave wave = Wave.GetInstance();

        //Si pas de vague en cours
        if (!wave.waveStarted)
        {
            //Démarrer vague
            wave.waveStarted = true;
            wave.waveStateText.text = "PAUSE";
            wave.waveNumber++;
            
        }
        //Si vague en cours
        else if (wave.waveStarted)
        {
            //Mettre en pause
            if (!wave.paused)
            {
                Time.timeScale = 0;
                wave.paused = true;
                wave.waveStateText.text = "RESUME";
            }
            //Reprendre
            else if (Wave.GetInstance().paused)
            {
                Time.timeScale = 1;
                wave.paused = false;
                wave.waveStateText.text = "PAUSE";
            }
            
        }
    }
}
