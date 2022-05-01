using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    //public static GameObject getTowerPanelInstance()
    //{
    //    if (towerPanel == null)
    //    {
    //        return GameObject.FindGameObjectWithTag("TowerPanel");
    //    }
    //    else
    //    {
    //        return towerPanel;
    //    }
    //}

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

        //foreach (GameObject enemy in Wave.GetInstance().enemies)
        //{
        //    if (enemy != null)
        //    {
        //        float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
        //        if (distanceToEnemy < shortestDistanceToEnemy)
        //        {
        //            shortestDistanceToEnemy = distanceToEnemy;
        //            nearestEnemy = enemy;
        //        }
        //    }
        //}

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
        //Color32 color1 = rangePrefabColor;
        //color1.a = 0;
        //newTowerRange.GetComponent<Renderer>().sharedMaterial.color = color1;

        //newTowerRange.GetComponent<Renderer>().sharedMaterial.color.a = 0;

        //Color32 color = new Color32(rangePrefabColor.r, rangePrefabColor.g, rangePrefabColor.b, 0);
        //newTowerRange.GetComponent<Renderer>().sharedMaterial.color = color;

        //newTowerRange.transform.localScale = new Vector3(10f, 10f, 10f);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !Wave.GetInstance().placeTower)
        {
            Vector2 clickPos;
            clickPos = new Vector2(
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));

            Vector2 towerPos = transform.position;

            var towerGO = PlaceTower.towerTiles.Where(t => t.GetComponent<Tower>() == this).FirstOrDefault();
            //var towerGO = PlaceTower.towerTiles.Where(t => t.transform.position == transform.position).FirstOrDefault();

            if (clickPos == towerPos)
            {
                if (Wave.GetInstance().selectedTower == towerGO)
                {
                    Wave.GetInstance().selectedTower = null;
                    PlaceTower.GetInstance().towerPanel.SetActive(false);
                    rangePrefab.GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    Wave.GetInstance().selectedTower = towerGO;
                    PlaceTower.GetInstance().towerPanel.SetActive(true);
                    rangePrefab.GetComponent<Renderer>().enabled = true;
                }
            }
            else
            {
                Wave.GetInstance().selectedTower = null;
                PlaceTower.GetInstance().towerPanel.SetActive(false);
                rangePrefab.GetComponent<Renderer>().enabled = false;
            }
        }
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
        Player.GetInstance().EarnGold(Wave.GetInstance().selectedTower.GetComponent<Tower>().GetPrice());
        PlaceTower.towerTiles.Remove(Wave.GetInstance().selectedTower);
        Destroy(Wave.GetInstance().selectedTower);
        Destroy(rangePrefab);
        Wave.GetInstance().selectedTower = null;
        PlaceTower.GetInstance().towerPanel.SetActive(false);
    }

    private void OnMouseEnter()
    {
        //rangePrefab.GetComponent<Renderer>().enabled = true;
    }

    private void OnMouseExit()
    {
        //rangePrefab.GetComponent<Renderer>().enabled = false;
    }

    public GameObject GetRangePrefab()
    {
        return rangePrefab;
    }
}
