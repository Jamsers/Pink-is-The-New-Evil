using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel") == true)
		{
			GameObject.FindGameObjectWithTag("Systems Process").GetComponent<SystemsProcess>().OpenPrompt(7);
		}
	}
}
