using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesInAttackArea : MonoBehaviour {

    public List<GameObject> withinArea = new List<GameObject>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" && withinArea.Contains(other.gameObject) == false)
            withinArea.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy" && withinArea.Contains(other.gameObject) == true)
            withinArea.Remove(other.gameObject);
    }
}
