using UnityEngine;

public static class AudioManager
{
    public enum Sounds
    {
        ArrowRelease,
        ColourChange,
        CompleteLvl,
        MenuClick,
        RingShotted,
        SoundVibrateToggle,
        titel,
        Tutorial,
        MenuLvlClick,
    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    private static GameObject RepeatingGameObject;
    private static AudioSource RepeatingAudioSource;

    public static void PlaySound(Sounds sound)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
    }

    public static void PlayRepeatingSound(Sounds sound)
    {
        if (RepeatingGameObject == null)
        {
            RepeatingGameObject = new GameObject("BackGround Sound");
            RepeatingAudioSource = RepeatingGameObject.AddComponent<AudioSource>();
        }
        RepeatingAudioSource.PlayOneShot(GetAudioClip(sound));
    }
    public static void StopRepeatingSound()
    {
        RepeatingAudioSource.Stop();

    }


    private static AudioClip GetAudioClip(Sounds sound)
    {
        foreach (SoundsStorage.SoundAudioClip soundAudioClip in SoundsStorage.GetInstance().soundsArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " noy found!");
        return null;
    }

}


