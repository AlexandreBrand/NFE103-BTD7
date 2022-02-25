using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    float enemyHealth {get; set;}
    float movementSpeed  {get; set;}
    int killReward {get; set;}
    int damage {get; set;}
}
