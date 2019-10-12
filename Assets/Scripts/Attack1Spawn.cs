using UnityEngine;
using System.Collections;

public class Attack1Spawn : MonoBehaviour {

    PlayerAI playerai;

	// Use this for initialization
	void Start () {
        playerai = transform.parent.GetComponent<PlayerAI>();
        playerai.attack1particle = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
