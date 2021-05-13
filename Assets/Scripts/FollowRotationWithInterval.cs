using UnityEngine;

public class FollowRotationWithInterval : MonoBehaviour {
    public GameObject mainDirectionalLight;
    int updateframes = 0;

    void Update() {
        if (updateframes > 30) {
            updateframes = 0;
            transform.rotation = mainDirectionalLight.transform.rotation;
        }
        else {
            updateframes++;
        }
    }
}
