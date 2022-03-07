using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour , IEnemy
{
    public GameObject PrefabEnemy;
    [SerializeField] public float EnemyHealth {get; set;}
    [SerializeField] public float MovementSpeed  {get; set;}
    [SerializeField] public int KillReward {get; set;}
    [SerializeField] public int Damage {get; set;}
    /*
    public override string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public override float EnemyHealth
    {
        get { return _enemyHealth; }
        set { _enemyHealth = value; }
    }

    public override float MovementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public override int KillReward
    {
        get { return _killReward; }
        set { _killReward = value; }
    }

    public override int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Tank(float enemyHealth, float movementspeed, int killReward, int damage)
    {
        EnemyHealth = enemyHealth;
        MovementSpeed = movementspeed;
        KillReward = killReward;
        Damage = damage;

        GameObject newTank = Instantiate(PrefabEnemy);
        MapGenerator map = GetComponent<MapGenerator>();
        newTank = MapGenerator.GetInstance().StartC;
    }

}
