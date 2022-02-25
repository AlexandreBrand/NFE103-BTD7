using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour, IEnemy
{

    public float enemyHealth {get; set;}
    public float movementSpeed  {get; set;}
    public int killReward {get; set;}
    public int damage {get; set;}


    // Start is called before the first frame update
    void Start()
    {
        
    }

}
