using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoins = 100;
    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(int damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoins -= damage;
        if (hitPoins <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        GetComponent<Animator>().SetTrigger("Death");
    }
}
