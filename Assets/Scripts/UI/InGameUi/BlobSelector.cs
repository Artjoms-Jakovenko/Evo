using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSelector : SliderSelector
{
    public GameObject leftArrow;
    public GameObject rightArrow;

    int blobCount = 0;
    public override GameObject GetObjectAt(int position)
    {
        throw new System.NotImplementedException();
    }

    public override int GetObjectCount()
    {
        return blobCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveData saveData = SaveSystem.Load();
        blobCount = saveData.blobData.Count;

        //LinearUiSpacing linearUiSpacing = new LinearUiSpacing(parentRectTransform.rect.width, 80.0F, statBackgroundRectTransform.rect.width, 20.0F);
        //base.Initialize(leftArrow, rightArrow, linearUiSpacing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
