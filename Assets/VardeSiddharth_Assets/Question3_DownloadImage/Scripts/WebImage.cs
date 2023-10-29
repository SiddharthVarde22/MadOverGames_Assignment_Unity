using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WebImage : MonoBehaviour
{
    [SerializeField]
    string imageURL;
    [SerializeField]
    Button downloadImageButton;
    [SerializeField]
    Image imageHolder;
    [SerializeField]
    TextMeshProUGUI downloadButtonText;
    [SerializeField]
    string imageName;

    private void Start()
    {
        downloadImageButton.onClick.AddListener(OnDownloadImageButtonPressed);
    }

    public void OnDownloadImageButtonPressed()
    {
        downloadImageButton.interactable = false;
        downloadButtonText.text = "Downloading";
        DownloadImage.Instance.DownloadTheImage(imageURL, OnImageDownloaded, OnImageDownloadFailed);
    }

    public void OnImageDownloaded(Texture2D downloadedTexture)
    {
        downloadImageButton.interactable = false;
        downloadButtonText.text = "Downloaded";

        imageHolder.overrideSprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.height, downloadedTexture.width)
                , new Vector2(0.5f, 0.5f));
    }

    public void OnImageDownloadFailed(string message)
    {
        downloadButtonText.text = "Re Try";
        downloadImageButton.interactable = true;
        Debug.Log(message);
    }
}
