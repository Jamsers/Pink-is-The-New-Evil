using UnityEngine;
using UnityEngine.UI;

public class InvokeOnClickOnButtonPress : MonoBehaviour {
    public string ButtonDown;

    void Update() {
        if (Input.GetButtonDown(ButtonDown) == true)
            GetComponent<Button>().onClick.Invoke();
    }
}