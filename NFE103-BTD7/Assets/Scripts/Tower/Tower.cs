using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] public float fireRate;
    [SerializeField] public float fireCountDown;
    [SerializeField] public float range;
    [SerializeField] public float damage;
    [SerializeField] public int price;
    [SerializeField] public float zone;
    [SerializeField] public bool isPrefabRangeCreated = false;
    [SerializeField] public Transform target;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject rangePrefab;
    [SerializeField] public Color32 rangePrefabColor;
    private Animator animator;

    public int level;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        createPrefabRange();
        rangePrefabColor = rangePrefab.GetComponent<Renderer>().sharedMaterial.color;
        
        level = 1;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //TowerClicked();
        UpdateTarget();
        if (fireCountDown < 0f && target != null)
        {
            Shoot();
            fireCountDown = 1 / fireRate;
        }

        fireCountDown -= Time.deltaTime;

        if (Wave.GetInstance().selectedTower != null)
        {
            PlaceTower.GetInstance().UpdateTowerPanel(level, price);
        }
    }

    //Pour ne pas gaspiller de ressources
    private void UpdateTarget()
    {
        //plus proche
        float shortestDistanceToEnemy = Mathf.Infinity;
        GameObject nearestEnemy = null;

        for(int i=0; i< Wave.GetInstance().GetEnemiesCount(); i++)
        {
            var enemy = Wave.GetInstance().GetEnemies(i);
            if (enemy != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistanceToEnemy)
                {
                    shortestDistanceToEnemy = distanceToEnemy;
                    nearestEnemy = enemy;
                }
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

    public int GetPrice()
    {
        return price;
    }
    public void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            animator.SetTrigger("TowerShootTrigger");
            bullet.Seek(target);
        }
        //Target.EnemyHealth = Target.EnemyHealth - Damage;
    }

    public void createPrefabRange()
    {
        GameObject newTowerRange = Instantiate(rangePrefab);
        newTowerRange.transform.position = transform.position;
        newTowerRange.GetComponent<Renderer>().enabled = false;
                                                        //largeur, hauteur, prodondeur
        newTowerRange.transform.localScale = new Vector3(range*2, range*2, range);
        rangePrefab = newTowerRange;
    }

    public void Upgrade()
    {
        if (Player.GetInstance().SpendGold(Wave.GetInstance().selectedTower.GetComponent<Tower>().GetPrice()))
        {
            level++;
            price = Convert.ToInt32(price * 1.05);
            range = range * 1.05f;
            damage = damage * 1.05f;
            rangePrefab.transform.localScale = new Vector3(range * 2, range * 2, range);
        }
    }

    public void Sell()
    {
        Player.GetInstance().EarnGold(Convert.ToInt32(Wave.GetInstance().selectedTower.GetComponent<Tower>().GetPrice()*0.8));
        PlaceTower.towerTiles.Remove(Wave.GetInstance().selectedTower);
        Destroy(Wave.GetInstance().selectedTower);
        Destroy(rangePrefab);
        Wave.GetInstance().selectedTower = null;
        PlaceTower.GetInstance().towerPanel.SetActive(false);
    }
    public GameObject GetRangePrefab()
    {
        return rangePrefab;
    }
}
