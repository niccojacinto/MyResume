using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

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

    public void LoadDialogueXML()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Assets/MyResume.xml");


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
