using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject prefabTank;
    public GameObject GetEnemy (EnemyType type)
    {
        switch(type)
        {
            case EnemyType.Tank:
                GameObject tank = Instantiate(prefabTank);
                return tank;
                
            case EnemyType.Bowman:
                //return new Bowman();


            default:
                throw new Exception();
        }
    }
  
}
