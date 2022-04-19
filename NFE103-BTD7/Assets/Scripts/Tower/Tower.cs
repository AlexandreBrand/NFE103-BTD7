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
    [SerializeField] public bool isPrefabRangeCreated = false;
    [SerializeField] public Transform target;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject rangePrefab;
    [SerializeField] public Color32 rangePrefabColor;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        createPrefabRange();
        rangePrefabColor = rangePrefab.GetComponent<Renderer>().sharedMaterial.color;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateTarget();
        if (fireCountDown < 0f && target != null)
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
            Debug.Log(shortestDistanceToEnemy + " tower range : "+ range);
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    public void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
        //Target.EnemyHealth = Target.EnemyHealth - Damage;
    }

    public void createPrefabRange()
    {
        GameObject newTowerRange = Instantiate(rangePrefab);
        newTowerRange.transform.position = transform.position;
        newTowerRange.GetComponent<Renderer>().enabled = false;
        newTowerRange.transform.localScale = new Vector3(range, range, range);
        rangePrefab = newTowerRange;
        //Color32 color1 = rangePrefabColor;
        //color1.a = 0;
        //newTowerRange.GetComponent<Renderer>().sharedMaterial.color = color1;

        //newTowerRange.GetComponent<Renderer>().sharedMaterial.color.a = 0;

        //Color32 color = new Color32(rangePrefabColor.r, rangePrefabColor.g, rangePrefabColor.b, 0);
        //newTowerRange.GetComponent<Renderer>().sharedMaterial.color = color;

        //newTowerRange.transform.localScale = new Vector3(10f, 10f, 10f);
    }

    private void OnMouseEnter()
    {
        Debug.Log("mouse enter");
        rangePrefab.GetComponent<Renderer>().enabled = true;
        //Color32 color = new Color32(rangePrefabColor.r, rangePrefabColor.g, rangePrefabColor.b, 125);
        //rangePrefab.GetComponent<Renderer>().sharedMaterial.color = color;
    }

    private void OnMouseExit()
    {
        Debug.Log("mouse exit");
        rangePrefab.GetComponent<Renderer>().enabled = false;
        //Color32 color = new Color32(rangePrefabColor.r, rangePrefabColor.g, rangePrefabColor.b, 0);
        //rangePrefab.GetComponent<Renderer>().sharedMaterial.color = color;
    }
}
