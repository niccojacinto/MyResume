using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

using System.IO;

public class BuildDate : MonoBehaviour

#if UNITY_EDITOR
    , IPreprocessBuildWithReport
#endif
{

    public TextAsset BuildDateTextAsset;

    public string s_BuildDate
    {
        get
        {
            return BuildDateTextAsset.text;
        }
    }

#if UNITY_EDITOR
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        string builddate = System.DateTime.Now.ToString();
        Debug.Log("builddate:" + builddate);

        string outfile = Path.Combine(Application.dataPath, "Resources", "BuildDateTextFile.txt");

        Debug.Log("path = '" + outfile + "'");

        System.IO.File.WriteAllText(outfile, builddate + "\n");

        AssetDatabase.Refresh();
    }
#endif
}
