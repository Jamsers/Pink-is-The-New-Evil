#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ChangeVersionWindows : EditorWindow
{
    string versionNum;
    string lastVersion;
    bool changed;

    [MenuItem("Window/Version Helper")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ChangeVersionWindows));
    }

    void OnGUI()
    {
        if (changed == false)
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

        if (changed == true)
        {
            if (GUILayout.Button("Set Version"))
            {
                PlayerSettings.bundleVersion = versionNum;
                changed = false;
            }
        }
    }
}
#endif