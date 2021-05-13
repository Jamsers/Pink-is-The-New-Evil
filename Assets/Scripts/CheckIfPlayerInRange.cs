using UnityEngine;

public class CheckIfPlayerInRange : MonoBehaviour {
    EnemyAI enemyAI;

	void Start () {
        enemyAI = transform.parent.GetComponent<EnemyAI>();
	}

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            enemyAI.isPlayerInDanger = true;
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.tag == "Player") {
            enemyAI.isPlayerInDanger = false;
        }
    }
}
