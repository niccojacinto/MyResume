using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;
using System.IO;

public class XMLLoader : MonoBehaviour
{
    public static XMLLoader Instance;

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
    }

    private IEnumerator LoadXML(string path)
    {
        string xmlPath = Path.Combine(Application.streamingAssetsPath, "MyResume.xml");

        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string xmlData = www.downloadHandler.text;
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(xmlData);
                    // Now xmlDoc contains the parsed XML data
                }
                catch (XmlException e)
                {
                    Debug.LogError("Error parsing XML: " + e.Message);
                }
                XmlNode root = xmlDoc.DocumentElement; // Root element
                XmlNodeList dialogues = root.SelectNodes("Dialogue"); // Select nodes by tag name

                foreach (XmlNode dialogue in dialogues)
                {
                    Dialogue d = new Dialogue();
                    d.id = dialogue.Attributes["id"].Value;
                    d.speaker = dialogue.Attributes["speaker"].Value;
                    d.next_id = dialogue.Attributes["next_id"].Value;
                    d.image_id = dialogue.Attributes["image_id"].Value;
                    d.image_pos = dialogue.Attributes["image_pos"].Value;
                    d.dialogue = dialogue.InnerText;

                    XmlNodeList responses = dialogue.SelectNodes("Response");
                    d.responses = new List<(string, string)>();
                    foreach (XmlNode response in responses)
                    {
                        d.responses.Add((response.Attributes["next_id"].Value, response.Attributes["text"].Value));
                    }

                    // Debug.Log($"Convo ID: {d.id}, Speaker: {d.speaker}, Dialogue: {d.dialogue }");

                    DialogueManager.Instance.AddDialogue(d.id, d);
                }
            }
            else
            {
                Debug.LogError("Failed to load XML: " + www.error);
            }
        }
    }

    public void LoadDialogueXML()
    {
        string xmlPath = Path.Combine(Application.streamingAssetsPath, "MyResume.xml");

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // Handle WebGL-specific loading (e.g., using UnityWebRequest)
            // You might need to adjust how you load the file due to WebGL security restrictions.
            StartCoroutine(LoadXML(xmlPath));
        }
        else
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Get the XML content as a string
            string xmlString = xmlDoc.DocumentElement.OuterXml;

            // Now you can use the xmlString as needed
            Debug.Log(xmlString);

            XmlNode root = xmlDoc.DocumentElement; // Root element
            XmlNodeList dialogues = root.SelectNodes("Dialogue"); // Select nodes by tag name

            foreach (XmlNode dialogue in dialogues)
            {
                Dialogue d = new Dialogue();
                d.id = dialogue.Attributes["id"].Value;
                d.speaker = dialogue.Attributes["speaker"].Value;
                d.next_id = dialogue.Attributes["next_id"].Value;
                d.image_id = dialogue.Attributes["image_id"].Value;
                d.image_pos = dialogue.Attributes["image_pos"].Value;
                d.dialogue = dialogue.InnerText;

                XmlNodeList responses = dialogue.SelectNodes("Response");
                d.responses = new List<(string, string)>();
                foreach (XmlNode response in responses)
                {
                    d.responses.Add((response.Attributes["next_id"].Value, response.Attributes["text"].Value));
                }

                // Debug.Log($"Convo ID: {d.id}, Speaker: {d.speaker}, Dialogue: {d.dialogue }");

                DialogueManager.Instance.AddDialogue(d.id, d);
            }
        }
    }
}
