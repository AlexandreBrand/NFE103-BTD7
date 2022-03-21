using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject prefabTank;
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
                
            case EnemyType.Bowman:
                //return new Bowman();


            default:
                throw new Exception();
        }
    }
  
}
