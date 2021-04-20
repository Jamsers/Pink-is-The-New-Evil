using UnityEngine;
using System.Collections;

public class PassTransformToAI : MonoBehaviour {

    EnemyAI enemyAI;

	// Use this for initialization
	void Start () {
        //pass transform up to enemyai
        enemyAI = transform.parent.GetComponent<EnemyAI>();
        enemyAI.transformToShake = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
