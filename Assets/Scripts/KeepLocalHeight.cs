using UnityEngine;

public class KeepLocalHeight : MonoBehaviour {
    public Transform player;

    void Update() {
        Vector3 goalpos = transform.position;
        goalpos.y = player.position.y + 0.95f;
        transform.position = goalpos;
    }
}
