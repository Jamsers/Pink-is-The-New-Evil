using UnityEngine;

public class SetMotionVectorsToCamera : MonoBehaviour {
    void Start() {
        ParticleSystemRenderer renderer = GetComponent<ParticleSystemRenderer>();
        renderer.motionVectorGenerationMode = MotionVectorGenerationMode.Camera;
    }
}