using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInLightOnEnable : MonoBehaviour {

	Light light;
	float origRange;
	bool isFading = false;
	float fadeamount = 10f;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		origRange = light.range;
		light.range = 0;
	}

	void OnEnable()
    {
		isFading = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (isFading)
        {
			light.range = Mathf.MoveTowards(light.range, origRange, fadeamount * Time.deltaTime);
			if (light.range >= origRange)
            {
				isFading = false;
            }
		}
		
	}
}
