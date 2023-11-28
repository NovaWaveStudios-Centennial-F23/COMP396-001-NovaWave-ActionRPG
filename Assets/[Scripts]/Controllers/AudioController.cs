/**Created by: Han Bi
 * Used to play sounds
 * Allows for adjustable volume
 */
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Singleton class responsible for handling sound 
/// </summary>
public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    private readonly string VOLUME_KEY = "Volume";

    float volume=1.0f;

    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance != null & Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }

        
    }

    private void Initialize()
    {
        volume = PlayerPrefs.GetFloat(VOLUME_KEY, 1.0f);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }


    public void SetVolume(float value)
    {
        volume = value;
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        audioSource.volume = volume;
    }

    /// <summary>
    /// Plays oneshot audio at specified position
    /// </summary>
    /// <param name="audio"></param>
    /// <param name="pos"></param>
    public void PlayAtLocation(AudioClip audio, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(audio, pos, volume);
    }

    public void PlayOneShot(AudioClip audio)
    {
        audioSource.PlayOneShot(audio, volume);
    }

    /// <summary>
    /// Plays audioclip on loop from audiosource. For oneshot, use PlayAtLocation.
    /// </summary>
    /// <param name="audio"></param>
    public void PlayAudio(AudioClip audio)
    {
        audioSource.Stop();
        audioSource.clip = audio;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }


    public float GetVolume()
    {
        return volume;
    }
}
