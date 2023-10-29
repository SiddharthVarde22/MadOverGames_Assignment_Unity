using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = (T)this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
