using UnityEngine;
using UnityEngine.AI;

public class SetEnemyDestination : MonoBehaviour {
    NavMeshAgent navMeshAgent;

    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        navMeshAgent.SetDestination(PinkIsTheNewEvil.PlayerController.transform.position);
    }
}