using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour , IEnemy
{
    public GameObject _prefabEnemy {get;set;}
    [SerializeField] public string _name {get;set;} 
    [SerializeField] public float _enemyHealth {get; set;}
    [SerializeField] public float _movementSpeed  {get; set;}
    [SerializeField] public int _killReward {get; set;}
    [SerializeField] public int _damage {get; set;}

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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Tank(string name, float enemyHealth, float movementspeed, int killReward, int damage)
    {
        _name = name;
        _enemyHealth = enemyHealth;
        _movementSpeed = movementspeed;
        _killReward = killReward;
        _damage = damage;

        GameObject newTank = Instantiate(_prefabEnemy);
        MapGenerator map = GetComponent<MapGenerator>();
        newTank = MapGenerator.GetInstance().StartC;
    }

}
