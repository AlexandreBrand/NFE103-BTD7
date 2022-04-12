using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    public GameObject PrefabGatling;
    public GameObject PrefabGunner;
    public GameObject PrefabRocket;
    public GameObject PrefabSniper;

    private static TowerFactory Instance;
    private void Awake()
    {
        Instance = this;
    }
    public static GameObject GetTower(TowerType type)
    {
        switch (type)
        {
            case TowerType.Gatling:
                GameObject gatling = Instantiate(Instance.PrefabGatling);
                return gatling;

            case TowerType.Gunner:
                GameObject gunner = Instantiate(Instance.PrefabGunner);
                return gunner;

            case TowerType.Rocket:
                GameObject rocket = Instantiate(Instance.PrefabRocket);
                return rocket;

            case TowerType.Sniper:
                GameObject sniper = Instantiate(Instance.PrefabSniper);
                return sniper;

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
