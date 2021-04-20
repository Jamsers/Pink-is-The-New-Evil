using UnityEngine;
using System.Collections;

public class PassEventsUp : MonoBehaviour {

    public EnemyAI core;
    public SoundManager sound;

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

	public void PlaySound(int index)
    {
        sound.PlaySound(index);
    }

	public void StopSound(int index)
    {
        sound.StopSound(index);
    }
}
