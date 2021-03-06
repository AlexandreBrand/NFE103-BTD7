using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed;
    public Tower shooter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        Destroy(gameObject);
        //A supprimer test sans vie
        var enemy = target.gameObject.GetComponent<IEnemy>();
        enemy.LooseLife(shooter.damage);
        if (enemy.EnemyHealth <= 0)
        {
            Player.GetInstance().EarnGold(enemy.KillReward);
            Destroy(target.gameObject);
            Wave.GetInstance().ModifMonsterLeft();
        }
        Wave.GetInstance().endWave();
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

}
