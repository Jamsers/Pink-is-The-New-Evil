using UnityEngine;
using System.Collections;

public class PassEventsToCore : MonoBehaviour {

    public EnemyAI core;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetIsSpawning () {
        core.SetIsSpawning();
    }

    public void StartDecomposing () {
        core.StartDecomposing();
    }
}
