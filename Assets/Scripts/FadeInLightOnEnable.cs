using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInLightOnEnable : MonoBehaviour {

	Light lightAttachedTo;
	float origRange;
	bool isFading = false;
	float fadeamount = 10f;

	// Use this for initialization
	void Start () {
		lightAttachedTo = GetComponent<Light>();
		origRange = lightAttachedTo.range;
		lightAttachedTo.range = 0;
	}

	void OnEnable()
    {
		isFading = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (isFading)
        {
			lightAttachedTo.range = Mathf.MoveTowards(lightAttachedTo.range, origRange, fadeamount * Time.deltaTime);
			if (lightAttachedTo.range >= origRange)
            {
				isFading = false;
            }
		}
		
	}
}
