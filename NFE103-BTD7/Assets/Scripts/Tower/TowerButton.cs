using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour
{
    public void PlaceTower()
    {
        Wave.GetInstance().placeTower = !Wave.GetInstance().placeTower;
    }
}
