using UnityEngine;

public class KeepWorldRotation : MonoBehaviour {
    Quaternion origRotation;

    void Start() {
        origRotation = GetComponent<Transform>().rotation;
    }

    void Update() {
        GetComponent<Transform>().rotation = origRotation;
    }
}
