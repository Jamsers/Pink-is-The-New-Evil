using UnityEngine;

public class DestroyAfterStart : MonoBehaviour {
	public float destroyAfterTime;

	void Start () {
        Destroy(gameObject, destroyAfterTime);
	}
}
