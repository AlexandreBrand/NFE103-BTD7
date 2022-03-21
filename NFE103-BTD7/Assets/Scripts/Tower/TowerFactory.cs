using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    public TowerFactory()
    {

    }

    public ITower GetTower(TowerType type)
    {
        switch (type)
        {
            case TowerType.Gatling:
                return new Gatling();

            case TowerType.Gunner:
                return new Gunner();

            case TowerType.Rocket:
                return new Rocket();

            default:
                throw new Exception();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
