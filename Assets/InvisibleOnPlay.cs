using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InvisibleOnPlay : MonoBehaviour
{

    private void Start()
    {
        // Set a button's normal color to invisible after playing.
        Button b = GetComponent<Button>();

        ColorBlock cb = b.colors;

        cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0f);

        b.colors = cb;
    }
}
