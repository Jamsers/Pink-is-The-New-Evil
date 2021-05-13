using UnityEngine;

public class SetMouseStateOnDisable : MonoBehaviour {
    void OnDestroy() {
        PinkIsTheNewEvil.PlayerController.setisMouseOverButton(false);
    }

    void OnDisable() {
        PinkIsTheNewEvil.PlayerController.setisMouseOverButton(false);
    }
}