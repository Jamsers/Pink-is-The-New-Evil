using UnityEngine;

public class UnpauseOnEscape : MonoBehaviour {
    void Update() {
        if (Input.GetButtonDown("Cancel") == true)
            PinkIsTheNewEvil.MainSystems.OpenPrompt(8);
    }
}