using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class LastUpdate : MonoBehaviour
{
    [SerializeField]
    TMP_Text buildDateText;

    void Start()
    {
        // string buildDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string buildDate = File.GetLastWriteTime(Application.dataPath).ToString("yyyy-MM-dd HH:mm:ss");
        buildDateText.text = "Build Date: " + buildDate;
    }
}
