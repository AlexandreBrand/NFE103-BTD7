using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    int X { get; set; }
    int Y { get; set; }
    float Rate { get; set; }
    float Range { get; set; }
    float Damage { get; set; }
    float Zone { get; set; }
    IEnemy Target { get; set; }

    public void Shoot(IEnemy enemy)
    {
        enemy.EnemyHealth = enemy.EnemyHealth - Damage;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            foreach (IEnemy mob in Game.wave.mobList)
            {
                if (mob.p)
                {

                }
            }
        }
    }
}
