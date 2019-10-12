using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private bool buttonDownStored = false;
    private Vector3 pressPosition = new Vector3(0,0,0);
    public Transform playerTransform;
    public Transform direction;
    public Transform moveTo;
    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
        /*float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		Animator anim = GetComponent<Animator> ();

		if (horizontal != 0f || vertical != 0f) {
			anim.SetBool ("isWalking", true);
		}
		else {
			anim.SetBool ("isWalking", false);
		}

		Rigidbody rigidbody = GetComponent<Rigidbody> ();

		Vector3 movement = (new Vector3(horizontal, 0f, vertical)) * Time.deltaTime * 5;

		rigidbody.MovePosition (transform.position + movement);*/

        if (Input.GetMouseButton(0))
        {
            if (buttonDownStored)
            {
                JoystickLogic(Input.mousePosition);
            }
            else
            {
                pressPosition = Input.mousePosition;
                buttonDownStored = true;
            }
        }
        else
        {
            buttonDownStored = false;
            anim.SetBool("isWalking", false);
        }
	}

    void JoystickLogic (Vector3 currentPosition)
    {
        Vector3 directionPos = pressPosition - currentPosition;
        
        direction.localPosition = new Vector3(-directionPos.x, 0, -directionPos.y);

        playerTransform.LookAt(direction);

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.MovePosition(moveTo.position);

        anim.SetBool("isWalking", true);
    }
}
