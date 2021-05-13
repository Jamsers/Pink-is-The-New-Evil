using UnityEngine;
using UnityEngine.UI;

public class SelectByDefault : MonoBehaviour {
    void Start() {
        GetComponent<Button>().Select();
    }

    void OnEnable() {
        GetComponent<Button>().Select();
    }

    void OnAwake() {
        GetComponent<Button>().Select();
    }
}