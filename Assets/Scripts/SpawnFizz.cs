using UnityEngine;

public class SpawnFizz : MonoBehaviour {
    public float fizzExpandOrDexpand;
    public float fizzLife;
    public float fizzlightorig;
    public Light fizzlight;
    public GameObject particle;
    public GameObject damageSphere;    

    float spawnTime;
    Vector3 originalScale;
    Vector3 originalParticleScale;
    bool countingExisting = false;
    bool spawningOut = false;

    void Start() {
        damageSphere.SetActive(false);
        fizzlightorig = fizzlight.range;
        originalScale = transform.localScale;
        originalParticleScale = particle.transform.localScale;

        fizzlight.range = 0;
        transform.localScale = Vector3.zero;
        particle.transform.localScale = Vector3.zero;
        
        spawnTime = Time.time;
    }

    void Update() {
        float scalePercent = (Time.time - spawnTime) / fizzExpandOrDexpand;

        if ((scalePercent < 1) && spawningOut == false) {
            if (scalePercent > 0.25)
                damageSphere.SetActive(true);

            fizzlight.range = Mathf.Lerp(0, fizzlightorig, scalePercent);
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, scalePercent);
            particle.transform.localScale = Vector3.Lerp(Vector3.zero, originalParticleScale, scalePercent);
        }
        else if (countingExisting == false) {
            fizzlight.range = fizzlightorig;
            transform.localScale = originalScale;
            particle.transform.localScale = originalParticleScale;
            
            countingExisting = true;
            Invoke("LetsSpawnOut", fizzLife);
        }
        else if (spawningOut == true) {
            if (scalePercent > 0.75) 
                damageSphere.SetActive(false);

            fizzlight.range = Mathf.Lerp(fizzlightorig, 0, scalePercent);
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, scalePercent);
            particle.transform.localScale = Vector3.Lerp(originalParticleScale, Vector3.zero, scalePercent);

            if (scalePercent > fizzExpandOrDexpand)
                Destroy(gameObject);
        }
    }

    void LetsSpawnOut() {
        spawningOut = true;
        spawnTime = Time.time;
    }
}