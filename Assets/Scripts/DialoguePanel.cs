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

    private IEnumerator SayCoroutine(Dialogue d)
    {
        coroutineIsRunning = true;
        DialogueManager.Instance.ShowNextButton(false);
        DialogueManager.Instance.ShowResponses(false);
        finishDialogue = false;
        WaitForSeconds delay = new WaitForSeconds(DialogueManager.Instance.textSpeed);
        speakerName.text = d.speaker;

        if (d.GetSprite() == null)
        {
            speakerImageFrame.gameObject.SetActive(false);
        }
        else
        {
            speakerImage.sprite = d.GetSprite();
            speakerImageFrame.gameObject.SetActive(true);
        }

        speakerDialogue.text = "";

        bool markup = false;
        foreach (char c in d.dialogue)
        {
            if (finishDialogue)
            {
                speakerDialogue.text = d.dialogue;
                markup = false;
                break;
            }


            if (c == '<')
            {
                markup = true;
            }
            else if (c == '>')
            {
                markup = false;
            }

            if (!markup)
            {
                speakerDialogue.text += c;
                SoundManager.Instance.PlayBlip();
                yield return delay;
            }
            else
            {
                speakerDialogue.text += c;
            }

        }
        DialogueManager.Instance.ShowNextButton(true);

        if (d.responses.Count > 0)
        {
            DialogueManager.Instance.ShowResponses(true);
        }
        coroutineIsRunning = false;
    }

    public void EndCurrentDialogue()
    {
        finishDialogue = true;
    }

    public void Say(Dialogue _dialogue)
    {
        if (!coroutineIsRunning)
        {
            StartCoroutine(SayCoroutine(_dialogue));
        }
    }
}
