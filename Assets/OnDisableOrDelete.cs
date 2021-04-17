﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableOrDelete : MonoBehaviour {

	PlayerAI playerAI;

	// Use this for initialization
	void Start () {
		playerAI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
    {
		playerAI.setisMouseOverButton(false);
    }

	void OnDisable()
    {
		playerAI.setisMouseOverButton(false);
	}
}
