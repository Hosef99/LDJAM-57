using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    public AudioClip caveMusic;
    public AudioClip shopMusic;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] sfxClips;

    private void Awake()
    {
        // Singleton pattern to ensure only one SoundManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Plays a sound effect by name (must match clip name in sfxClips array)
    /// </summary>
    public void PlaySFX(string clipName)
    {
        AudioClip clip = GetClipByName(clipName);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SoundManager: Clip '{clipName}' not found!");
        }
    }

    /// <summary>
    /// Plays a sound effect directly from an AudioClip
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Returns the AudioClip from the array by name
    /// </summary>
    private AudioClip GetClipByName(string name)
    {
        foreach (AudioClip clip in sfxClips)
        {
            if (clip.name == name)
                return clip;
        }
        return null;
    }
    
    public void StopMusic(){
        StartCoroutine("FadeOutMusic", 2);
    }

    public void ToShop(){
        ChangeClip(2, shopMusic);
    }

    public void ToCave(){
        ChangeClip(2, caveMusic);
    }

    public void ChangeClip(float sec, AudioClip clip){
        StartCoroutine(ChangeToClip(sec, clip));
    }

    IEnumerator ChangeToClip(float sec, AudioClip clip){
        for (int i = 0; i < 100; i++)
        {
            musicSource.volume -= 0.01f;
            yield return new WaitForSeconds(sec/100);
        }
        musicSource.clip = clip;
        musicSource.Play();
        for (int i = 0; i < 100; i++)
        {
            musicSource.volume += 0.01f;
            yield return new WaitForSeconds(sec/100);
        }
    }
    IEnumerator FadeOutMusic(float sec)
    {
        for (int i = 0; i < 100; i++)
        {
            musicSource.volume -= 0.01f;
            yield return new WaitForSeconds(sec/100);
        }
    }

    IEnumerator FadeInMusic(float sec)
    {
        for (int i = 0; i < 100; i++)
        {
            musicSource.volume += 0.01f;
            yield return new WaitForSeconds(sec/100);
        }
    }
}
