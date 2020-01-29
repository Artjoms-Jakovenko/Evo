using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject rewardScreen;
    public GameObject blobSelector;
    public BlobSelector blobSelectorBar;

    //public GameObject objectsList;
    float roundTime = 20.0F;

    private void Update()
    {
        roundTime -= Time.deltaTime;
        var li = ObjectManager.GetInstance().GetAllWithTags(new List<ObjectTag>() { ObjectTag.Vegetarian });
        if(li.Count == 0 || roundTime <= 0.0F)
        {
            //Time.timeScale = 0;
            //Debug.Log("Game over");
            //rewardScreen.SetActive(true);
            //GameRewardsSystem.AdministerRewards(); // TODO get out of the loop with rewards
        }
    }

    public void StartRound()
    {
        List<int> selectedBlobIds = blobSelectorBar.GetSelectedBlobIds();
        blobSelector.SetActive(false);
        
        SaveData saveData = SaveSystem.Load();

        foreach (var blobId in selectedBlobIds)
        {
            GameObject blob = BlobInstantiator.GetBlobGameObject(saveData.blobData[blobId]);

            blob.transform.position = new Vector3(0.0F, 0.0F, 0.0F);

            ObjectManager.GetInstance().AddObject(blob);
        }
    }
}
