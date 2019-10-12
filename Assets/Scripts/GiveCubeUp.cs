using UnityEngine;
using System.Collections;

public class GiveCubeUp : MonoBehaviour {

    EnemyAI enemyAI;

	// Use this for initialization
	void Start () {
        //pass transform up to enemyai
        enemyAI = transform.parent.GetComponent<EnemyAI>();
        enemyAI.cube = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
