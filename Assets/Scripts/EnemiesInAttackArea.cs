using UnityEngine;
using System.Collections.Generic;

public class EnemiesInAttackArea : MonoBehaviour {
    public List<GameObject> enemiesWithinArea = new List<GameObject>();

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" && enemiesWithinArea.Contains(other.gameObject) == false)
            enemiesWithinArea.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy" && enemiesWithinArea.Contains(other.gameObject) == true)
            enemiesWithinArea.Remove(other.gameObject);
    }
}
