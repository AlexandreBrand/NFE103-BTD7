using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncCellScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemy = collision.gameObject;
        if (enemy.tag == "Enemy")
        {
            Player.GetInstance().LoseLife(enemy.GetComponent<IEnemy>().Damage); 
            Wave.GetInstance().removeEnemy(enemy);
            //Destroy(enemy);
            Wave.GetInstance().ModifMonsterLeft();
        }
    }
}
