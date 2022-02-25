using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{

    public IEnemy GetEnemy (EnemyType type)
    {
        switch(type)
        {
            case EnemyType.Tank:
                return new Tank();
            default:
                throw new NotSupportedException();
        }
    }
  
}
