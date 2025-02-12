using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
