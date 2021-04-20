using UnityEngine;
using System.Collections;

public class DestroyAfterStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
