using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    [SerializeField] GameObject DifficultyPanel;

    public void setEasyDifficulty()
    {
        Wave.GetInstance().maxObstacles = 30;
        Wave.GetInstance().waveEndBounty = 150;
        Wave.GetInstance().diff_select = false;

        Player.GetInstance().LifePoints = 200;
        Player.GetInstance().GoldCoins = 300;

        DifficultyPanel.SetActive(false);
    }

    public void setMediumDifficulty()
    {
        Wave.GetInstance().maxObstacles = 25;
        Wave.GetInstance().waveEndBounty = 125;
        Wave.GetInstance().diff_select = false;

        Player.GetInstance().LifePoints = 150;
        Player.GetInstance().GoldCoins = 250;

        DifficultyPanel.SetActive(false);
    }

    public void setHardDifficulty()
    {
        Wave.GetInstance().maxObstacles = 20;
        Wave.GetInstance().waveEndBounty = 100;
        Wave.GetInstance().diff_select = false;

        Player.GetInstance().LifePoints = 100;
        Player.GetInstance().GoldCoins = 200;

        DifficultyPanel.SetActive(false);
    }
}
