using UnityEngine;
using System.Collections;

public class CheckIfPlayerInRange : MonoBehaviour {

    EnemyAI parent;

	// Use this for initialization
	void Start () {
        parent = transform.parent.GetComponent<EnemyAI>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
            parent.isPlayerInDanger = true;
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
            parent.isPlayerInDanger = false;
    }
}
