using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialAttackTargeting : MonoBehaviour {

    public List<GameObject> withinTargetZone = new List<GameObject>();
    GameObject target = null;

	void Start () {
	
	}
	
	void Update () {
        //Debug.Log(RetrieveTarget());
        //Debug.Log(withinTargetZone.Count);
    }

    public GameObject RetrieveTarget () {
        for (int i = 0; i < withinTargetZone.Count; i++) {
            if (target == null) {
                target = withinTargetZone[i];
            }
            else if (Vector3.Distance(transform.position, withinTargetZone[i].transform.position) < Vector3.Distance(transform.position, target.transform.position)) {
                target = withinTargetZone[i];
            }
        }
        return target;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" && withinTargetZone.Contains(other.gameObject) == false)
            withinTargetZone.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy" && withinTargetZone.Contains(other.gameObject) == true)
            withinTargetZone.Remove(other.gameObject);
    }
}
