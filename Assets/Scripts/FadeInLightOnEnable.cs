using UnityEngine;

public class FadeInLightOnEnable : MonoBehaviour {
    Light lightAttachedTo;
    float origRange;
    bool isFading = false;
    const float fadeamount = 10f;

    void Start() {
        lightAttachedTo = GetComponent<Light>();
        origRange = lightAttachedTo.range;
        lightAttachedTo.range = 0;
    }

    void OnEnable() {
        isFading = true;
    }

    void Update() {
        if (isFading == false)
            return;

        lightAttachedTo.range = Mathf.MoveTowards(lightAttachedTo.range, origRange, fadeamount * Time.deltaTime);

        if (lightAttachedTo.range >= origRange) {
            isFading = false;
            lightAttachedTo.range = origRange;
        }
    }
}