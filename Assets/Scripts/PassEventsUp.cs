using UnityEngine;

public class PassEventsUp : MonoBehaviour {
    public EnemyAI core;
    public SoundManager sound;

    public void SetIsSpawning() {
        core.SetIsSpawning();
    }

    public void StartDecomposing() {
        core.StartDecomposing();
    }

    public void PlaySound(int index) {
        sound.PlaySound(index);
    }

    public void StopSound(int index) {
        sound.StopSound(index);
    }
}