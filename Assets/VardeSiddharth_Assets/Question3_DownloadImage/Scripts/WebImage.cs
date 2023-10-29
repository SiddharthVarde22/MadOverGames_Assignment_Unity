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
    [SerializeField]
    bool shouldCacheImage = true;

    private void Start()
    {
        downloadImageButton.interactable = false;
        ImageSaver.Instance.ReadTextureFile(imageName, OnImageReaded, OnImageReadFailed);
    }

    public void OnDownloadImageButtonPressed()
    {
        if (DownloadImage.Instance.DownloadTheImage(imageURL, OnImageDownloaded, OnImageDownloadFailed))
        {
            downloadImageButton.interactable = false;
            downloadButtonText.text = "Downloading";
        }
        
    }

    public void OnImageDownloaded(Texture2D downloadedTexture)
    {
        if(shouldCacheImage)
        {
            //save the image
            ImageSaver.Instance.SaveTextureFile(downloadedTexture, imageName);
        }
        ShowImage(downloadedTexture);
    }

    private void ShowImage(Texture2D downloadedTexture)
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

    public void OnImageReaded(Texture2D readedTexture)
    {
        ShowImage(readedTexture);
    }

    public void OnImageReadFailed()
    {
        downloadImageButton.onClick.AddListener(OnDownloadImageButtonPressed);
        downloadImageButton.interactable = true;
    }
}
