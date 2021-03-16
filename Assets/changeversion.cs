using UnityEngine;
using UnityEditor;

public class changeversion : EditorWindow
{
    string versionNum;
    string lastVersion;
    bool changed;
    [MenuItem("Window/Version Helper")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(changeversion));
    }

    void OnGUI()
    {
        if (!changed)
        {
            versionNum = PlayerSettings.bundleVersion;
            lastVersion = versionNum;
        }
        EditorGUIUtility.labelWidth = 80;
        versionNum = EditorGUILayout.TextField("Version", versionNum);
        if (lastVersion != versionNum)
        {
            changed = true;
        }
        if (changed)
        {
            if (GUILayout.Button("Set Version"))
            {
                PlayerSettings.bundleVersion = versionNum;
                changed = false;
            }
        }
    }
}