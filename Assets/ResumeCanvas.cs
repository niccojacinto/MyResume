using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeCanvas : MonoBehaviour
{
    [SerializeField]
    AudioClip pageFlipSFX;

    [SerializeField]
    List<Canvas> pages;

    [SerializeField]
    List<Canvas> tabs;

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

        for (int i = 0; i < tabs.Count; i++)
        {
            if (i != pageNumber)
            {
                tabs[i].sortingOrder = 1;
            }
        }

        tabs[pageNumber].sortingOrder = 2;
    }
}
