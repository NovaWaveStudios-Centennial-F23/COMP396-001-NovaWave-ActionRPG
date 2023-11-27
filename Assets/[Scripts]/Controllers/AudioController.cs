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
    }


    public void SetVolume(float value)
    {
        volume = value;
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
    }


    public void PlayAudio(AudioClip audio)
    {
        audioSource.PlayOneShot(audio, volume);
    }


    public float GetVolume()
    {
        return volume;
    }
}
