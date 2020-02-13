using UnityEngine;


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