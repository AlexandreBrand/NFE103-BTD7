using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game _instance;

    [SerializeField] GameObject DifficultyPanel;
    [SerializeField] GameObject TowerManagementPanel;
    public TextMeshProUGUI Message;
    public bool GameStarted = false;

    void Awake()
    {
        _instance = this;
    }

    public void Update()
    {
        Click();
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
        Wave.GetInstance().endWave();
        GameStarted = false;
        DifficultyPanel.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    private void Click()
    {
        if (Input.GetMouseButtonDown(0) && !Wave.GetInstance().placeTower)
        {
            Vector2 clickPos;
            clickPos = new Vector2(
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));

            var hitTab = Physics2D.RaycastAll(clickPos, Vector2.zero);

            var isTowerPanel = hitTab.Any(h => h.collider.gameObject.tag == "TowerPanel");

            if (hitTab.Any(h => h.collider.gameObject.GetComponent<Tower>()) && !isTowerPanel)
            {
                foreach (var curHit in hitTab)
                {
                    if (curHit.collider != null && curHit.collider.gameObject.tag == "Tower")
                    {
                        GameObject towerGO = curHit.collider.gameObject;
                        Vector2 posGO = new Vector2(towerGO.transform.position.x, towerGO.transform.position.y);
                        if (clickPos == posGO)
                        {
                            if (Wave.GetInstance().selectedTower != null)
                            {
                                Wave.GetInstance().selectedTower.GetComponent<Tower>().rangePrefab.GetComponent<Renderer>().enabled = false;
                            }
                            Wave.GetInstance().selectedTower = towerGO;
                            PlaceTower.GetInstance().towerPanel.SetActive(true);
                            Wave.GetInstance().selectedTower.GetComponent<Tower>().rangePrefab.GetComponent<Renderer>().enabled = true;
                            PlaceTower.GetInstance().UpdateTowerPanel(Wave.GetInstance().selectedTower.GetComponent<Tower>().level, Wave.GetInstance().selectedTower.GetComponent<Tower>().price);
                        }
                        else
                        {
                            PlaceTower.GetInstance().towerPanel.SetActive(false);
                            Wave.GetInstance().selectedTower.GetComponent<Tower>().rangePrefab.GetComponent<Renderer>().enabled = false;
                            Wave.GetInstance().selectedTower = null;
                        }
                    }
                }
            }
            else
            {
                if (!isTowerPanel)
                {
                    if (Wave.GetInstance().selectedTower != null)
                    {
                        Wave.GetInstance().selectedTower.GetComponent<Tower>().rangePrefab.GetComponent<Renderer>().enabled = false;
                        Wave.GetInstance().selectedTower = null;
                    }
                    PlaceTower.GetInstance().towerPanel.SetActive(false);
                }
            }
        }
    }
}
