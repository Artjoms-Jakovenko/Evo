using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float health;
    void Start()
    {
        health = GetComponent<BlobStats>().stats.stats[StatName.Health].value;
    }

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0.0F)
        {
            ObjectManager.GetInstance().RemoveFromObjectList(gameObject);
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<AnimationController>().PlayAnimation(AnimationState.Death);
            GetComponent<BlobMovement>().Stop();
        }
    }
}
