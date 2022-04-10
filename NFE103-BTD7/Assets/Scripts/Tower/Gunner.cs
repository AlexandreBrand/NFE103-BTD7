using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Tower
{
    public GameObject PrefabTower;
    public float Rate { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Range { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Damage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Zone { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int X { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Y { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Gunner()
    {
        Rate = 5;
        Range = 100;
        Damage = 10;
        Zone = 0;

        GameObject newGunner = Instantiate(PrefabTower);
        MapGenerator map = GetComponent<MapGenerator>();
        newGunner = MapGenerator.GetInstance().StartC;
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
