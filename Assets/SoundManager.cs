using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource audioSource;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBlip()
    {
        audioSource.PlayOneShot(DialogueManager.Instance.dialogueBlipSFX);
    }
}
