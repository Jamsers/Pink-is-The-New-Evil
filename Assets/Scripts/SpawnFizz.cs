using UnityEngine;
using System.Collections;

public class SpawnFizz : MonoBehaviour {

    public float fizzExpandOrDexpand;
    public float fizzLife;
    public GameObject particle;
    public GameObject damageSphere;
    public Light fizzlight;
    public float fizzlightorig;
    float spawnTime;
    Vector3 originalScale;
    Vector3 originalParticleScale;
    bool existing;
    bool countingExisting = false;
    bool spawningOut = false;

	// Use this for initialization
	void Start () {
        fizzlightorig = fizzlight.range;
        damageSphere.SetActive(false);
        originalScale = transform.localScale;
        originalParticleScale = particle.transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
        particle.transform.localScale = new Vector3(0, 0, 0);
        fizzlight.range = 0;
        spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float scalePercent = (Time.time - spawnTime) / fizzExpandOrDexpand;
        if ((scalePercent < 1) && spawningOut == false) {
            if (scalePercent > 0.25) {
                damageSphere.SetActive(true);
            }
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), originalScale, scalePercent);
            particle.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), originalParticleScale, scalePercent);
            fizzlight.range = Mathf.Lerp(0, fizzlightorig, scalePercent);
        }
        else if (countingExisting == false) {
            transform.localScale = originalScale;
            particle.transform.localScale = originalParticleScale;
            fizzlight.range = fizzlightorig;
            countingExisting = true;
            Invoke("LetsSpawnOut", fizzLife);
        }
        else if (spawningOut == true) {
            if (scalePercent > 0.75) {
                damageSphere.SetActive(false);
            }
            transform.localScale = Vector3.Lerp(originalScale, new Vector3(0,0,0), scalePercent);
            particle.transform.localScale = Vector3.Lerp(originalParticleScale, new Vector3(0,0,0), scalePercent);
            fizzlight.range = Mathf.Lerp(fizzlightorig, 0, scalePercent);
            if (scalePercent > fizzExpandOrDexpand) {
                Destroy(gameObject);
            }
        }

	}

    void LetsSpawnOut () {
        spawningOut = true;
        spawnTime = Time.time;
    }
}
