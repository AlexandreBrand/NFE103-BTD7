using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaveButton : MonoBehaviour
{
    public void StartWave()
    {
        //Si pas de vague en cours
        if (!Wave.GetInstance().waveStarted)
        {
            //Démarrer vague
            Wave.GetInstance().waveStarted = true;
            Wave.GetInstance().waveStateText.text = "PAUSE";
            Wave.GetInstance().waveNumber++;


        }
        //Si vague en cours
        else if (Wave.GetInstance().waveStarted)
        {
            //Mettre en pause
            if (!Wave.GetInstance().paused)
            {
                Time.timeScale = 0;
                Wave.GetInstance().paused = true;
                Wave.GetInstance().waveStateText.text = "RESUME";
            }
            //Reprendre
            else if (Wave.GetInstance().paused)
            {
                Time.timeScale = 1;
                Wave.GetInstance().paused = false;
                Wave.GetInstance().waveStateText.text = "PAUSE";
            }
            
        }
    }
}
