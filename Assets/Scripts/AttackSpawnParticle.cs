using UnityEngine;

public class AttackSpawnParticle : MonoBehaviour {
	void Start () {
        PinkIsTheNewEvil.PlayerController.bloodParticleTransform = transform;
	}
}
