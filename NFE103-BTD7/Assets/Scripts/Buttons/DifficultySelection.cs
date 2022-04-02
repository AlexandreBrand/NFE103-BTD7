using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    public void setEasyDifficulty()
    {
        Game.GetInstance().setDifficulty(30,100,false,0,5,10,200,300, 1.05, 1.2);
    }

    public void setMediumDifficulty()
    {
        Game.GetInstance().setDifficulty(25, 75, false, 0, 6, 12, 150, 250, 1.3, 1.22);
    }

    public void setHardDifficulty()
    {
        Game.GetInstance().setDifficulty(20, 50, false, 1, 7, 14, 100, 200, 1.01, 1.24);
    }

    public void TEST()
    {
        Wave.GetInstance().endWave();
    }
}
