using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game _instance;

    [SerializeField] GameObject DifficultyPanel;
    [SerializeField] GameObject QuitPanel;
    [SerializeField] GameObject TowerManagementPanel;
    public TextMeshProUGUI Message;
    public bool GameStarted = false;

    void Awake()
    {
        _instance = this;
    }


    public void Start()
    {
        TowerManagementPanel.SetActive(false);
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
        Player.GetInstance().SetLife(lifePts);
        Player.GetInstance().SetGold(golds);
        DifficultyPanel.SetActive(false);
        GetComponent<ObstaclesCreation>().obs_restants.text = "Obstacles restants : " + Wave.GetInstance().maxObstacles.ToString();
    }

    public void Quit()
    {
        Wave.GetInstance().quitMenu = true;
        Time.timeScale = 0;
        QuitPanel.SetActive(true);
    }

    public void ConfirmQuit()
    {
        Wave.GetInstance().endWave();
        GameStarted = false;
        DifficultyPanel.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void CancelQuit()
    {
        Wave.GetInstance().quitMenu = false;
        if (Wave.GetInstance().paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        QuitPanel.SetActive(false);
    }
}
