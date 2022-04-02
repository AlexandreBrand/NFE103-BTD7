using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    public void setEasyDifficulty()
    {
        Game.GetInstance().setDifficulty(30,150,false,0,5,10,200,300);
    }

    public void setMediumDifficulty()
    {
        Game.GetInstance().setDifficulty(25, 125, false, 0, 6, 12, 150, 250);
    }

    public void setHardDifficulty()
    {
        Game.GetInstance().setDifficulty(20, 100, false, 1, 8, 15, 100, 200);
    }
}
