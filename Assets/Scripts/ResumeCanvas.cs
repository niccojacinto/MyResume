using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeCanvas : MonoBehaviour
{
    [SerializeField]
    AudioClip pageFlipSFX;

    [SerializeField]
    List<Transform> pages;

    [SerializeField]
    List<Transform> tabs;

    public void ShowPage(int pageNumber)
    {
        SoundManager.Instance.PlayOneShot(pageFlipSFX);

        for (int i=0; i<pages.Count; i++)
        {
            if (i != pageNumber) 
            {
                pages[i].gameObject.SetActive(false);
            } 
        }

        pages[pageNumber].gameObject.SetActive(true);

        tabs[pageNumber].SetAsLastSibling();
    }
}
