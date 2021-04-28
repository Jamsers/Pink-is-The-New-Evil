using UnityEngine;

public class CameraTargetLogic : MonoBehaviour {
	public float camMoveDistance;
	public float camMoveSpringResistance;
	public float camLinearMoveSpeed;
	
	public Transform specialCam1;
	public Transform specialCam2;

	const float SpringResistanceSpecialMultiplier = 2.0f;
	const float LinearSpeedSpecialDivider = 1.25f;

	Vector3 refoutvar = Vector3.zero;
	Transform camMovePosition;
	PlayerController playerController;
	CameraLogic cameraLogic;

	void Start () {
		playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		cameraLogic = GameObject.Find("Game Camera").GetComponent<CameraLogic>();
		transform.position = playerController.transform.position;
		camMovePosition = transform.GetChild(0);
	}
	
	void Update () {
		if (cameraLogic.isTracking) {
			transform.position = playerController.transform.position;
			transform.rotation = Quaternion.identity;
			Vector3 camMoveTarget = playerController.storedMoveDirection * camMoveDistance;
			float linearMoveStep = camLinearMoveSpeed * Time.deltaTime;

			if (playerController.isSpecialAttackUnderWay == true) {
				if (playerController.specialAttackMode == 1) {
					camMovePosition.localPosition = Vector3.MoveTowards(camMovePosition.localPosition, transform.InverseTransformPoint(specialCam1.position), linearMoveStep);
				}
				else {
					camMovePosition.localPosition = Vector3.SmoothDamp(camMovePosition.localPosition, transform.InverseTransformPoint(specialCam2.position), ref refoutvar, camMoveSpringResistance * SpringResistanceSpecialMultiplier);
				}
			}
			else if (playerController.amAttackingRightNow == true) {
				camMovePosition.localPosition = Vector3.MoveTowards(camMovePosition.localPosition, transform.InverseTransformPoint(specialCam2.position), linearMoveStep/LinearSpeedSpecialDivider);
			}
			else {
				camMovePosition.localPosition = Vector3.SmoothDamp(camMovePosition.localPosition, camMoveTarget, ref refoutvar, camMoveSpringResistance);
			}
		}
	}
}
