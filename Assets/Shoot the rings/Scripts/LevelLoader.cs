using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    AsyncOperation async;

    void Start()
    {
        async = Application.LoadLevelAsync("Shoot the ring");
        async.allowSceneActivation = false;
    }

    public void AllowLoad()
    {
        async.allowSceneActivation = true;
    }
}