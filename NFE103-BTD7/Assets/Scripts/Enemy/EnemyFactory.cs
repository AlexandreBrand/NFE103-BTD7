using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject prefabTank; 
    public GameObject PrefabBloodthirsty;
    private static EnemyFactory Instance;
    private void Awake()
    {
        Instance = this;
    }
    public static GameObject GetEnemy(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Tank:
                GameObject tank = Instantiate(Instance.prefabTank);
                return tank;

            case EnemyType.Bloodthirsty:
                GameObject bloodthirsty = Instantiate(Instance.PrefabBloodthirsty);
                return bloodthirsty;



            default:
                throw new Exception();
        }
    }

}
