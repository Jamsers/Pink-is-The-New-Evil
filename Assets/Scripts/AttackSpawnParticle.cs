using UnityEngine;

public class AttackSpawnParticle : MonoBehaviour {
	void Start () {
        PlayerController playerController = transform.parent.GetComponent<PlayerController>();
        playerController.bloodParticleTransform = transform;
	}
}
