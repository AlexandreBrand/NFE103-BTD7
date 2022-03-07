using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] float _enemyHealth {get; set;}
    [SerializeField] float _movementSpeed  {get; set;}
    [SerializeField] int _killReward {get; set;}
    [SerializeField] int _damage {get; set;}

    public EnemyFactory(float enemyHealth, float movementspeed, int killReward, int damage)
    {
        _enemyHealth = enemyHealth;
        _movementSpeed = movementspeed;
        _killReward = killReward;
        _damage = damage;
    }

    public IEnemy GetEnemy (EnemyType type)
    {
        switch(type)
        {
            case EnemyType.Tank:
                return new Tank(_enemyHealth, _movementSpeed, _killReward, _damage);
                
            case EnemyType.Bowman:
                //return new Bowman();
            default:
                throw new Exception();
        }
    }
  
}
