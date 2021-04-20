using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotationWithInterval : MonoBehaviour {

	public GameObject dirlight;
	int updateframes = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (updateframes > 30)
        {
			updateframes = 0;
			transform.rotation = dirlight.transform.rotation;
		}
		else
        {
			updateframes++;
        }
	}
}
