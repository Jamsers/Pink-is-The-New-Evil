using UnityEngine;
using System.Collections;

public class AttackSpawnParticle : MonoBehaviour {

    PlayerController playerai;

	// Use this for initialization
	void Start () {
        playerai = transform.parent.GetComponent<PlayerController>();
        playerai.bloodParticleEffect = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
