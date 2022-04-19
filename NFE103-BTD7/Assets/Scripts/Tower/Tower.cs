using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tower : MonoBehaviour
{
    //int X { get; set; }
    //int Y { get; set; }
    //[SerializeField] public float fireRate { get; set; }
    //[SerializeField] public float fireCountDown { get; set; }
    //[SerializeField] public float range { get; set; }
    //[SerializeField] public float damage { get; set; }
    //[SerializeField] public float zone { get; set; }
    //[SerializeField] public Transform target { get; set; }
    //[SerializeField] public GameObject bulletprefab { get; set; }
    //[SerializeField] public Transform firePoint { get; set; }

    [SerializeField] public float fireRate;
    [SerializeField] public float fireCountDown;
    [SerializeField] public float range;
    [SerializeField] public float damage;
    [SerializeField] public float zone;
    [SerializeField] public Transform target;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject rangePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        transform.localScale = new Vector3(range, range, 0.0f);
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
        Debug.Log("UpdateTarget");
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
        Debug.Log("shoot");
        GameObject bulletGO = Instantiate(bulletPrefab, transform);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
        //Target.EnemyHealth = Target.EnemyHealth - Damage;
    }

    private void OnMouseEnter()
    {
        Debug.Log("mouse enter");
        var color = rangePrefab.GetComponent<Renderer>().sharedMaterial.color;
        color.a = 0.5f;
    }

    private void OnMouseExit()
    {
        Debug.Log("mouse exit");
        var color = rangePrefab.GetComponent<Renderer>().sharedMaterial.color;
        color.a = 0f;
    }
}
