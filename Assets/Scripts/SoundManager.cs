using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioClip[] sfxClips;

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
}
