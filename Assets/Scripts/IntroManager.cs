using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
	For the time being just enables objects that on runtime so that they'd can be disabled when using the editor
*/
public class IntroManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> activateOnStart;
    void Start()
    {
        foreach (GameObject g in activateOnStart)
        {
            g.SetActive(true);
        }
    }

}
