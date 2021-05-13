using UnityEngine;

public class PassTransformToAI : MonoBehaviour {
    void Start() {
        EnemyAI enemyAI = transform.parent.GetComponent<EnemyAI>();
        enemyAI.enemyModel = transform;
    }
}