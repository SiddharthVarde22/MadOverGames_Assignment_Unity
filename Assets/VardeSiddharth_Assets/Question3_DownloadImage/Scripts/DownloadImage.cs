using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class DownloadImage : GenericSingleton<DownloadImage>
{
    public void DownloadTheImage(string url, Action<Texture2D> onDownloadSuccessfull, Action<String> onDownloadFailed)
    {
        StartCoroutine(DownloadImageCoroutine(url, onDownloadSuccessfull, onDownloadFailed));
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
    }
}