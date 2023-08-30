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


        foreach (char c in d.dialogue)
        {
            if (finishDialogue)
            {
                speakerDialogue.text = d.dialogue;
                break;
            }

            speakerDialogue.text += c;
            SoundManager.Instance.PlayBlip();
            yield return delay;
        }
        DialogueManager.Instance.ShowNextButton(true);

        if (d.responses.Count > 0)
        {
            DialogueManager.Instance.ShowResponses();
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
