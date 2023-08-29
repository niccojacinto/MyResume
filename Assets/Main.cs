using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public string entryId;
    private void Start()
    {
        DialogueManager.Instance.Say(entryId);
    }
}
