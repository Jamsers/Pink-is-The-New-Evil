using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveaccordingtojoystick : MonoBehaviour {
	public PlayerAI playerAI;
	public CameraAI cameraAI;
	Vector3 virtualJoystickStored;
	public float distanceFromPlayer;
	public float moveCameraSpringResistance;
	public float powerCameraMoveSpeed;
	Transform childActualTarget;
	Vector3 refoutvar = Vector3.zero;
	public Transform powertarget;
	public Transform powertarget2;

	// Use this for initialization
	void Start () {
		GetComponent<Transform>().position = playerAI.GetComponent<Transform>().position;
		childActualTarget = this.gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (cameraAI.isOn)
        {
			GetComponent<Transform>().position = playerAI.GetComponent<Transform>().position;

			virtualJoystickStored = playerAI.virtualJoystickStored;
			Vector3 movetopos = (virtualJoystickStored * distanceFromPlayer);

			//float step = moveCameraSpeed * Time.deltaTime;
			if (playerAI.isSpecialAttackUnderWay == true)
			{
				float step = (powerCameraMoveSpeed * Time.deltaTime);
				if (playerAI.specialAttackMode == 1)
				{
					childActualTarget.localPosition = Vector3.MoveTowards(childActualTarget.localPosition, transform.InverseTransformPoint(powertarget.position), step);
					//childActualTarget.localPosition = Vector3.SmoothDamp(childActualTarget.localPosition, transform.InverseTransformPoint(powertarget.position), ref refoutvar, moveCameraSpringResistance * 2);
				}
				else
				{
					//childActualTarget.localPosition = Vector3.MoveTowards(childActualTarget.localPosition, transform.InverseTransformPoint(powertarget2.position), step);
					childActualTarget.localPosition = Vector3.SmoothDamp(childActualTarget.localPosition, transform.InverseTransformPoint(powertarget2.position), ref refoutvar, moveCameraSpringResistance * 2);
				}
			}
			else
			{
				childActualTarget.localPosition = Vector3.SmoothDamp(childActualTarget.localPosition, (movetopos), ref refoutvar, moveCameraSpringResistance);
			}


			//GetComponent<Transform>().position = movetopos;
			//GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
			GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		}
		
	}
}
