using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject prefabTank;
    public GameObject GetEnemy (EnemyType type)
    {
        Debug.Log("start getEnemy");
        switch (type)
        {
            case EnemyType.Tank:
                Debug.Log("create tank");
                GameObject tank = Instantiate(prefabTank);
                return tank;
                
            case EnemyType.Bowman:
                //return new Bowman();


            default:
                throw new Exception();
        }
    }
  
}
