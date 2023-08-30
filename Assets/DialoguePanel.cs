using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField]
    TMP_Text speakerName;
    [SerializeField]
    TMP_Text speakerDialogue;
    [SerializeField] 
    Image speakerImage;
    [SerializeField]
    Transform speakerImageFrame;

    bool coroutineIsRunning = false;
    private bool finishDialogue;


    public bool IsReady()
    {
        return !coroutineIsRunning;
    }

    private IEnumerator SayCoroutine(string speaker, string dialogue, Sprite sprite=null)
    {
        coroutineIsRunning = true;
        DialogueManager.Instance.ShowNextButton(false);
        finishDialogue = false;
        WaitForSeconds delay = new WaitForSeconds(DialogueManager.Instance.textSpeed);
        speakerName.text = speaker;

        if (!sprite)
        {
            speakerImageFrame.gameObject.SetActive(false);
        }
        else
        {
            speakerImage.sprite = sprite;
            speakerImageFrame.gameObject.SetActive(true);
        }

        speakerDialogue.text = "";


        foreach (char c in dialogue)
        {
            if (finishDialogue)
            {
                speakerDialogue.text = dialogue;
                break;
            }

            speakerDialogue.text += c;
            SoundManager.Instance.PlayBlip();
            yield return delay;
        }
        DialogueManager.Instance.ShowNextButton(true);
        coroutineIsRunning = false;
    }

    public void EndCurrentDialogue()
    {
        finishDialogue = true;
    }

    public void Say(string speaker, string dialogue, Sprite sprite=null)
    {
        if (!coroutineIsRunning)
        {
            StartCoroutine(SayCoroutine(speaker, dialogue, sprite));
        }
    }
}
