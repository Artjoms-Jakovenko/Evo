using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image image;

    private Health health;
    private BlobStats blobStats;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInParent<Health>();
        blobStats = GetComponentInParent<BlobStats>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        image.fillAmount = health.GetHealth() / blobStats.stats.stats[StatName.Health].value;
    }
}
