using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }
}
