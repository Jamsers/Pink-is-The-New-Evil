#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ChangeVersionWindows : EditorWindow {
    string version;
    string lastVersion;
    bool changed;

    [MenuItem("Window/Version Helper")]
    public static void ShowWindow() {
        GetWindow(typeof(ChangeVersionWindows));
    }

    void OnGUI() {
        if (changed == false) {
            version = PlayerSettings.bundleVersion;
            lastVersion = version;
        }

        EditorGUIUtility.labelWidth = 80;
        version = EditorGUILayout.TextField("Version", version);

        if (lastVersion != version) {
            changed = true;
        }

        if (changed == true) {
            if (GUILayout.Button("Set Version")) {
                PlayerSettings.bundleVersion = version;
                changed = false;
            }
        }
    }
}
#endif