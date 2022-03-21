using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    float EnemyHealth {get;}
    float MovementSpeed  {get; }
    int KillReward {get;}
    int Damage {get;}
}
