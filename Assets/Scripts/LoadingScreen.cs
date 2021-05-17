using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {
    void Start() {
        QualitySettings.vSyncCount = 0;
        Invoke("LoadGame", 0.1f);
    }

    void LoadGame() {
        SceneManager.LoadSceneAsync(1);
    }
}