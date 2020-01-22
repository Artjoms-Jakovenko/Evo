using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float health;
    void Awake()
    {
        health = GetComponent<BlobStats>().stats.stats[StatName.Health].value;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0.0F)
        {
            ObjectManager.GetInstance().DestroyObject(gameObject);
        }
    }
}
