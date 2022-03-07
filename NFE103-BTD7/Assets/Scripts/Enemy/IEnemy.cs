using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    GameObject PrefabEnemy {get;set;}
    string Name { get; set; } 
    float EnemyHealth {get; set;}
    float MovementSpeed  {get; set;}
    int KillReward {get; set;}
    int Damage {get; set;}
}
