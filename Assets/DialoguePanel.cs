using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField]
    TMP_Text speakerName;
    [SerializeField]
    TMP_Text speakerDialogue;

    public bool coroutineIsRunning = false;
    private bool finishDialogue;


    public bool IsReady()
    {
        return !coroutineIsRunning;
    }

    private IEnumerator SayCoroutine(string speaker, string dialogue)
    {
        coroutineIsRunning = true;
        DialogueManager.Instance.ShowNextButton(false);
        finishDialogue = false;
        WaitForSeconds delay = new WaitForSeconds(DialogueManager.Instance.textSpeed);
        speakerName.text = speaker;

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

    public void Say(string speaker, string dialogue)
    {
        if (!coroutineIsRunning)
        {
            StartCoroutine(SayCoroutine(speaker, dialogue));
        }
    }
}
