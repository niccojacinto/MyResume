using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    GameObject responsePrefab;
    [SerializeField]
    Transform responseContainer;
    [SerializeField]
    Button nextButton;


    [SerializeField]
    List<Sprite> listImages;

    Dictionary<string, Dialogue> dialogues;
    Dictionary<string, Sprite> dictImages;

    [SerializeField]
    DialoguePanel dialoguePanel;

    [Header("Dialogue Settings:")]
    public float textSpeed;
    public AudioClip dialogueBlipSFX;

    Dialogue currentDialogue;


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

        InitDialogues();
        InitImages();
    }

    private void InitDialogues()
    {
        dialogues = new Dictionary<string, Dialogue>();
        XMLLoader.Instance.LoadDialogueXML();
    }

    private void InitImages()
    {
        dictImages = new Dictionary<string, Sprite>();
        foreach (Sprite spr in listImages)
        {
            if (!dictImages.ContainsKey(spr.name))
            {
                dictImages.Add(spr.name, spr);
            }
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!dialoguePanel.IsReady())
            {
                dialoguePanel.EndCurrentDialogue();
                ShowNextButton(true);
            }
            else
            {
                
            }
        }
    }

    public void ShowNextButton(bool state)
    {
        nextButton.gameObject.SetActive(state);
    }

    public void Say(string id)
    {
        // Debug.Log("Saying: " + id);

        if (id == "" ) id = "introduction7"; // temp default.
        nextButton.gameObject.SetActive(false);
        currentDialogue = dialogues[id];
        dialoguePanel.Say(currentDialogue.speaker, currentDialogue.dialogue);

        // TODO: Use object pooling for responses to save resources on instantiation 
        //                      OR 
        // set max to 4 responses depending on container real estate space.

        if (currentDialogue.responses.Count == 0)
        {
            responseContainer.gameObject.SetActive(false);
        }
        else
        {
            foreach (Transform t in responseContainer)
            {
                Destroy(t.gameObject);
            }
            foreach ((string text, string next_id) responsePair in currentDialogue.responses)
            {
                GameObject g = Instantiate(responsePrefab, responseContainer);
                ResponseButton rb = g.GetComponent<ResponseButton>();
                rb.SetResponse(responsePair.text, responsePair.next_id);
            }
        }

        
    }

    public void SayNext()
    {
        Say(currentDialogue.next_id);
    }

    public void AddDialogue(string id, Dialogue dialogue)
    {
        if (!dialogues.ContainsKey(id))
        {
            dialogues.Add(id, dialogue);
            Debug.Log("Added Key: " + id + " to dialogues");
        }
        else
        {
            Debug.LogWarning("Duplicate Dialogue Keys Warning!");
        }
    }

}

public struct Dialogue
{
    public string id;
    public string speaker;
    public string next_id;
    public string image_id;
    public string image_pos;
    public string dialogue;

    // desc, nextId
    public List<(string, string)> responses;

}