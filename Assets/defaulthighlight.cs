using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class defaulthighlight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().Select();
	}

	void OnEnable()
    {
		GetComponent<Button>().Select();
	}

	void OnAwake()
    {
		GetComponent<Button>().Select();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
