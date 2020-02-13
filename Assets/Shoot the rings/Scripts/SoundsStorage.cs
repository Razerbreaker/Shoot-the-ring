using UnityEngine;
using System;
public class SoundsStorage : MonoBehaviour
{
    private static SoundsStorage instance = null;
    void Start()
    {
        if (GetInstance() == null)
        {
            SetInstance(this);
        }
    }

    public static SoundsStorage GetInstance()
    {
        return instance;
    }

    public static void SetInstance(SoundsStorage value)
    {
        instance = value;
    }

    public SoundAudioClip[] soundsArray;

    [Serializable]
    public class SoundAudioClip
    {
        public AudioClip audioClip;
        public AudioManager.Sounds sound;
    }
}