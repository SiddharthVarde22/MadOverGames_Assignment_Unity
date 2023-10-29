using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class DownloadImage : GenericSingleton<DownloadImage>
{
    [SerializeField]
    int maxAllowedImagesToDownload = 3;
    [SerializeField]
    float timeToWaitToDownload = 10;

    int numberOfDownloadingImages = 0;

    public bool DownloadTheImage(string url, Action<Texture2D> onDownloadSuccessfull, Action<String> onDownloadFailed)
    {
        if (numberOfDownloadingImages < maxAllowedImagesToDownload)
        {
            StartCoroutine(DownloadImageCoroutine(url, onDownloadSuccessfull, onDownloadFailed));
            numberOfDownloadingImages++;
            return true;
        }

        return false;
    }
    
    private IEnumerator DownloadImageCoroutine(string url, Action<Texture2D> onSuccessFull, Action<String> onImageDownloadFailed)
    {
        UnityWebRequest downloadImageRequest = UnityWebRequestTexture.GetTexture(url);

        yield return downloadImageRequest.SendWebRequest();

        if(downloadImageRequest.result == UnityWebRequest.Result.Success)
        {
            DownloadHandlerTexture downloadHendlerTexture = (DownloadHandlerTexture)downloadImageRequest.downloadHandler;
            Texture2D downloadedTexture = downloadHendlerTexture.texture;
            
            onSuccessFull?.Invoke(downloadedTexture);
        }
        else
        {
            onImageDownloadFailed?.Invoke(downloadImageRequest.error);
        }
        numberOfDownloadingImages--;
    }
}