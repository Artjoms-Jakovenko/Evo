using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddBlobButton : MonoBehaviour
{
    public GameObject selectedBlobView;
    public GameObject plusSignView;

    Image blobImage;
    TextMeshProUGUI blobNameText;

    private void Start()
    {
        blobImage = selectedBlobView.GetComponent<Image>();
        blobNameText = selectedBlobView.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SwitchToSelectedBlob(BlobType blobType, string blobName)
    {
        selectedBlobView.gameObject.SetActive(true);
        plusSignView.gameObject.SetActive(false);

        // TODO image
        blobNameText.text = blobName;
    }

    public void SwitchToPlusSign()
    {
        selectedBlobView.gameObject.SetActive(false);
        plusSignView.gameObject.SetActive(true);
    }
}
