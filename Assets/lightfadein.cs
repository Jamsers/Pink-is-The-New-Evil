using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightfadein : MonoBehaviour {

	Light light;
	float origBrightness;
	bool isFading = false;
	float fadeamount = 1.5f;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		origBrightness = light.intensity;
		light.intensity = 0;
	}

	void OnEnable()
    {
		isFading = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (isFading)
        {
			light.intensity = Mathf.MoveTowards(light.intensity, origBrightness, fadeamount * Time.deltaTime);
			if (light.intensity >= origBrightness)
            {
				isFading = false;
            }
		}
		
	}
}
