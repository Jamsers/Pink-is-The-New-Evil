using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvokeOnClickOnButtonPress : MonoBehaviour {

	public string ButtonDown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(ButtonDown) == true)
		{
			//GameObject.FindGameObjectWithTag("Systems Process").GetComponent<SystemsProcess>().OpenPrompt(7);
			GetComponent<Button>().onClick.Invoke();
		}
	}
}
