using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    float EnemyHealth {get; set;}
    float MovementSpeed  {get; set;}
    int KillReward {get; set;}
    int Damage {get; set;}
}
