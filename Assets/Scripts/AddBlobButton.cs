using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddBlobButton : MonoBehaviour
{
    public GameObject selectedBlobView;
    public GameObject plusSignView;
    
    public int buttonBlobId;

    Image blobImage;
    TextMeshProUGUI blobNameText;

    private void Start()
    {
        blobImage = selectedBlobView.GetComponent<Image>();
        blobNameText = selectedBlobView.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SwitchToSelectedBlob(BlobType blobType, int buttonBlobId)
    {
        this.buttonBlobId = buttonBlobId;

        selectedBlobView.gameObject.SetActive(true);
        plusSignView.gameObject.SetActive(false);

        // TODO image
        blobNameText.text = buttonBlobId.ToString();
    }

    public void SwitchToPlusSign()
    {
        selectedBlobView.gameObject.SetActive(false);
        plusSignView.gameObject.SetActive(true);
    }
}
