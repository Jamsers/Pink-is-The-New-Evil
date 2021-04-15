using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveaccordingtojoystick : MonoBehaviour {

	Vector3 virtualJoystickStored;
	public float distanceFromPlayer;
	public float moveCameraSpringResistance;
	Transform childActualTarget;
	Vector3 refoutvar = Vector3.zero;

	// Use this for initialization
	void Start () {
		GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
		childActualTarget = this.gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

		virtualJoystickStored = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().virtualJoystickStored;
		Vector3 movetopos = (-virtualJoystickStored * distanceFromPlayer);

		//float step = moveCameraSpeed * Time.deltaTime;
		childActualTarget.localPosition = Vector3.SmoothDamp(childActualTarget.localPosition, (movetopos), ref refoutvar, moveCameraSpringResistance);

		//GetComponent<Transform>().position = movetopos;
		//GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
		GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
	}
}
