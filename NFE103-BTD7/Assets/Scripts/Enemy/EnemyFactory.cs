using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] EnemyType _name { get; set; } 
    [SerializeField] float _enemyHealth {get; set;}
    [SerializeField] float _movementSpeed  {get; set;}
    [SerializeField] int _killReward {get; set;}
    [SerializeField] int _damage {get; set;}

    public EnemyFactory(EnemyType name, float enemyHealth, float movementspeed, int killReward, int damage)
    {
        _name = name;
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
                return new Tank();
                
            case EnemyType.Bowman:
                return new Bowman();
            default:
                throw new Exception();
        }
    }
  
}
