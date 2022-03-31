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
            Wave.GetInstance().StartWave();
        }
        //Si vague en cours
        else if (Wave.GetInstance().waveStarted)
        {
            Wave.GetInstance().PauseOrResumeWave();
        }
    }
}
