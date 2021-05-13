using UnityEngine;

public class PassTransformToController : MonoBehaviour {
    void Start() {
        PlayerController playerController = transform.parent.GetComponent<PlayerController>();
        playerController.transformToShake = transform;
    }
}