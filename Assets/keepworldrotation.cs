using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepworldrotation : MonoBehaviour {

	Quaternion origRotation;

	// Use this for initialization
	void Start () {
		origRotation = GetComponent<Transform>().rotation;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Transform>().rotation = origRotation;
	}
}
