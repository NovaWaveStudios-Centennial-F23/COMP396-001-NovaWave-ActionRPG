using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
    }

    IEnumerator Initalize()
    {
        while(AudioController.Instance == null)
        {
            yield return new WaitForEndOfFrame();
        }

        volumeSlider.value = AudioController.Instance.GetVolume();
    }

    private void Start()
    {
        StartCoroutine(nameof(Initalize));
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(Initalize));
    }

    public void OnSliderChanged()
    {
        AudioController.Instance.SetVolume(volumeSlider.value);
    }
}
