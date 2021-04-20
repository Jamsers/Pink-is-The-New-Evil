using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMotionVectorsToCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParticleSystemRenderer renderer = GetComponent<ParticleSystemRenderer>();
		renderer.motionVectorGenerationMode = MotionVectorGenerationMode.Camera;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
