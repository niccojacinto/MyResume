using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroSequence : MonoBehaviour
{
    [SerializeField]
    AudioClip ambientNoise;
    [SerializeField]
    AudioClip knockSFX;
    [SerializeField]
    AudioClip doorOpenSFX;
    [SerializeField]
    AudioClip pageflipSFX;

    [SerializeField]
    Image blackFadeBackground;
    [SerializeField]
    TMP_Text title;


    [SerializeField]
    Transform resumeTransform;

    bool proceed = false;

    private void Start()
    {
        StartSequence();
    }

    public void Continue()
    {
        proceed = true;
    }

    private void StartSequence()
    {
        SoundManager.Instance.PlayOneShot(ambientNoise);
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {

        yield return new WaitForSeconds(2f);
        title.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        float fadeDuration = 10f;
        float fadeElapsed = 0f;
        float t = 0;
        Color bgfadeStart = blackFadeBackground.color;
        Color titlefadeStart = title.color;
        while (t < 1)
        {
            t = (fadeElapsed / fadeDuration);
            fadeElapsed += Time.deltaTime;
            blackFadeBackground.color = Color.Lerp(bgfadeStart, new Color(bgfadeStart.r, bgfadeStart.g, bgfadeStart.b, 0f), t);
            title.color = Color.Lerp(titlefadeStart, new Color(title.color.r, title.color.g, title.color.b, 0f), t);
            yield return null;
        }
        blackFadeBackground.gameObject.SetActive(false);
        title.gameObject.SetActive(false);

        SoundManager.Instance.PlayOneShot(knockSFX);

        yield return new WaitForSeconds(1f);

        DialogueManager.Instance.Say("comein", Continue);

        while (!proceed)
        {
            yield return null;
        }    

        SoundManager.Instance.PlayOneShot(doorOpenSFX);

        yield return new WaitForSeconds(2f);

        DialogueManager.Instance.Say("introduction1");
    }

    public IEnumerator TutorialCoroutine()
    {
        resumeTransform.gameObject.SetActive(true);
        SoundManager.Instance.PlayOneShot(pageflipSFX);
        DialogueManager.Instance.Say("tutorial1");
        yield return null;
    }
}
