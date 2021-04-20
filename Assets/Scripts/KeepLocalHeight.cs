using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLocalHeight : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 goalpos = transform.position;
		goalpos.y = player.position.y + 0.95f;
		transform.position = goalpos;
	}
}
