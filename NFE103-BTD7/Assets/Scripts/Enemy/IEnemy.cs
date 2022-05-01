using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemy : MonoBehaviour
{
    [SerializeField] public float EnemyHealth;
    [SerializeField] public int KillReward;
    [SerializeField] public int Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var collider1 = gameObject.GetComponent<CircleCollider2D>();
            var collider2 = collision.gameObject.GetComponent<CircleCollider2D>();
            Physics2D.IgnoreCollision(collider1, collider2);
        }
    }
    public void LooseLife(float dmg)
    {
        EnemyHealth -= dmg;
    }
}
