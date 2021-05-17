using UnityEngine;

public class RotateLoadingCircle : MonoBehaviour {
    public RectTransform rectTransform;
    const float RotationSpeed = 750f;
    Vector3 currentRotation;

    void Update() {
        currentRotation = rectTransform.rotation.eulerAngles;
        currentRotation.z -= RotationSpeed * Time.deltaTime;
        rectTransform.rotation = Quaternion.Euler(currentRotation);
    }
}