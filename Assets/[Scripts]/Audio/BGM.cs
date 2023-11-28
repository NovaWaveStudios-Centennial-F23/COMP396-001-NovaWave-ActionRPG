/**Created by: Han Bi
 * Simple script that plays a BGM using the AudioController
 */

using System.Collections;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField]
    AudioClip bgmClip;

    private void Start()
    {
        StartCoroutine(nameof(PlayBGM));
        
    }

    IEnumerator PlayBGM()
    {
        while (AudioController.Instance == null)
        {
            yield return new WaitForEndOfFrame();
        }
        AudioController.Instance.PlayAudio(bgmClip);
    }


}
