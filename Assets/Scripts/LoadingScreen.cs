using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {
    void Start() {
        Invoke("LoadGame", 0.1f);
    }

    void LoadGame() {
        SceneManager.LoadSceneAsync(1);
    }
}