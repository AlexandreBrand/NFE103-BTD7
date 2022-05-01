using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour
{
    public void chooseGunner()
    {
        Wave.GetInstance().choosedTower = TowerType.Gunner;
        PlaceTower();
    }
    public void chooseGatling()
    {
        Wave.GetInstance().choosedTower = TowerType.Gatling;
        PlaceTower();
    }
    public void chooseRocket()
    {
        Wave.GetInstance().choosedTower = TowerType.Rocket;
        PlaceTower();
    }
    public void chooseSniper()
    {
        Wave.GetInstance().choosedTower = TowerType.Sniper;
        PlaceTower();
    }
    public void PlaceTower()
    {
        Wave.GetInstance().placeTower = !Wave.GetInstance().placeTower;
        if (Wave.GetInstance().placeTower) Game.GetInstance().Message.text = "Placez la tourelle";
        else Game.GetInstance().Message.text = "";
    }

    public void SellTowerOnClick()
    {
        Wave.GetInstance().selectedTower.GetComponent<Tower>().Sell();
    }
    public void UpgradeTowerOnClick()
    {
        Wave.GetInstance().selectedTower.GetComponent<Tower>().Upgrade();
    }
}
