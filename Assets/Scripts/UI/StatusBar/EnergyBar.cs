using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private Image image;

    private Energy energy;
    private BlobStats blobStats;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        energy = GetComponentInParent<Energy>();
        blobStats = GetComponentInParent<BlobStats>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        image.fillAmount = energy.GetEnergy() / blobStats.stats.stats[StatName.MaxEnergy].value;
    }
}
