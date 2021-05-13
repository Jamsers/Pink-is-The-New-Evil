using UnityEngine;
using System.Collections.Generic;

public class SpecialAttackTargeting : MonoBehaviour {
    [HideInInspector] public List<GameObject> withinTargetZone = new List<GameObject>();

    public GameObject RetrieveTarget() {
        GameObject targetToReturn = null;

        foreach (GameObject target in withinTargetZone) {
            if (targetToReturn == null) {
                targetToReturn = target;
                break;
            }

            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            float distanceToCurrentTarget = Vector3.Distance(transform.position, targetToReturn.transform.position);

            if (distanceToTarget < distanceToCurrentTarget)
                targetToReturn = target;
        }

        return targetToReturn;
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