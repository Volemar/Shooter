using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    DeathHandler deathHandler;

    private void Start()
    {
        deathHandler = GetComponent<DeathHandler>();
    }

    public void PlayerDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            deathHandler.HandleDeath();
        }
    }
}
