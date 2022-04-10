using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemy : MonoBehaviour
{
    [SerializeField] public float EnemyHealth;
    [SerializeField] public int KillReward;
    [SerializeField] public int Damage;
}
