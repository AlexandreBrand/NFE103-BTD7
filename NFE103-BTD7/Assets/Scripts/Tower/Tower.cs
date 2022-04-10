using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    int X { get; set; }
    int Y { get; set; }
    float fireRate { get; set; }
    float fireCountDown { get; set; }
    float range { get; set; }
    float damage { get; set; }
    float zone { get; set; }
    Transform target { get; set; }
    GameObject bulletprefab { get; set; }
    Transform firePoint { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        if (fireCountDown < 0f)
        {
            Shoot();
            fireCountDown = 1 / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    //Pour ne pas gaspiller de ressources
    private void UpdateTarget()
    {
        //plus proche
        float shortestDistanceToEnemy = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in Wave.GetInstance().enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistanceToEnemy)
            {
                shortestDistanceToEnemy = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistanceToEnemy <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    public void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletprefab, transform);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
        //Target.EnemyHealth = Target.EnemyHealth - Damage;
    }

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, range);
    }
}
