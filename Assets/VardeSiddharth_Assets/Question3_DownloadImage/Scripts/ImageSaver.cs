using UnityEngine;
using System.IO;
using System;

public class ImageSaver : GenericSingleton<ImageSaver>
{
    string filePath;

    protected override void Awake()
    {
        base.Awake();
        filePath = Application.persistentDataPath + "/";
    }

    public void SaveTextureFile(Texture2D texture2D, string fileName)
    {
        try
        {
            File.WriteAllBytes(filePath + fileName, texture2D.EncodeToJPG());
        }
        catch(Exception error)
        {
            Debug.Log("While saving file " + error);
        }
    }

    public void ReadTextureFile(string fileName, Action<Texture2D> onTextureReaded, Action onImageReadFailed)
    {
        Texture2D texture2D = null;
        byte[] textureData = null;
        if (File.Exists(filePath + fileName))
        {
            try
            {
                if(IsOlderThen7Days(filePath + fileName))
                {
                    File.Delete(filePath + fileName);
                    onImageReadFailed?.Invoke();
                }
                
                textureData = File.ReadAllBytes(filePath + fileName);
            }
            catch (Exception error)
            {
                Debug.Log("While reading file " + error);
                onImageReadFailed?.Invoke();
                return;
            }
            texture2D = new Texture2D(1, 1);
            texture2D.LoadImage(textureData);
        }
        else
        {
            onImageReadFailed?.Invoke();
            return;
        }

        onTextureReaded?.Invoke(texture2D);
    }

    private bool IsOlderThen7Days(string fullFilePath)
    {
        DateTime creationTime = File.GetCreationTime(fullFilePath);
        DateTime currentTime = System.DateTime.Now;

        TimeSpan timePassed = currentTime - creationTime;

        return (timePassed.Days >= 7);
    }
}
