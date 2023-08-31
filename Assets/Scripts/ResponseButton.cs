using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponseButton : MonoBehaviour
{
    [SerializeField]
    TMP_Text response;
    [SerializeField]
    string nextId;

    public void SetResponse(string next_id, string text)
    {
        nextId = next_id;
        response.text = text;
    }

    public void Next()
    {
        DialogueManager.Instance.Say(nextId);
    }
}
