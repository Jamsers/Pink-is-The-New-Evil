using UnityEngine;

public class WalkToMouseCursor : MonoBehaviour {
    public Transform playerTransform;
    public Transform direction;
    public Transform moveTo;

    bool buttonDownStored = false;
    Vector3 pressPosition = Vector3.zero;
    Animator animator;
    Rigidbody rigidBody;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            if (buttonDownStored == true) {
                JoystickLogic(Input.mousePosition);
            }
            else {
                pressPosition = Input.mousePosition;
                buttonDownStored = true;
            }
        }
        else {
            buttonDownStored = false;
            animator.SetBool("isWalking", false);
        }
    }

    void JoystickLogic(Vector3 currentPosition) {
        Vector3 directionPos = pressPosition - currentPosition;
        direction.localPosition = new Vector3(-directionPos.x, 0, -directionPos.y);

        playerTransform.LookAt(direction);
        rigidBody.MovePosition(moveTo.position);

        animator.SetBool("isWalking", true);
    }
}