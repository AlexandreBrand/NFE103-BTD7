using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game _instance;

    [SerializeField] GameObject DifficultyPanel;
    public bool GameStarted = false;

    void Awake()
    {
        _instance = this;
    }

    public static Game GetInstance()
    {
        return _instance;
    }

    public void Lost()
    {
        SceneManager.LoadScene("Menu");
    }

    public void setDifficulty(int maxObs, int endBounty, bool diffSelect, int tankNbr, int knightNbr, int assassinNbr, int lifePts, int golds, double bountyCoef, double monsterCoef)
    {
        Wave.GetInstance().maxObstacles = maxObs;
        Wave.GetInstance().waveEndBounty = endBounty;
        Wave.GetInstance().diff_select = diffSelect;
        Wave.GetInstance().tanksNbr = tankNbr;
        Wave.GetInstance().knightNbr = knightNbr;
        Wave.GetInstance().assassinsNbr = assassinNbr;
        Wave.GetInstance().bountyCoef = bountyCoef;
        Wave.GetInstance().monsterCoef = monsterCoef;
        Player.GetInstance().LifePoints = lifePts;
        Player.GetInstance().GoldCoins = golds;

        DifficultyPanel.SetActive(false);
    }
}
