using UnityEngine;

public class SpawnBlades : MonoBehaviour {
    public float fizzExpandOrDexpand;
    public Vector3 fromPos;
    public GameObject damageSphere;

    Vector3 originalPos;
    float spawnTime;
    bool countingExisting = false;
    bool spawningOut = false;

    void Start() {
        damageSphere.SetActive(false);
        originalPos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
        fromPos = transform.position;
        spawnTime = Time.time;
    }

    void Update() {
        float scalePercent = (Time.time - spawnTime) / fizzExpandOrDexpand;

        if ((scalePercent < 1) && spawningOut == false) {
            if (scalePercent > 0.75)
                damageSphere.SetActive(true);

            transform.position = Vector3.Lerp(fromPos, originalPos, scalePercent);
        }
        else if (countingExisting == false) {
            transform.position = originalPos;
            countingExisting = true;
        }
        else if (spawningOut == true) {
            if (scalePercent > 0.25)
                damageSphere.SetActive(false);

            transform.position = Vector3.Lerp(originalPos, fromPos, scalePercent);

            if (scalePercent > fizzExpandOrDexpand)
                Destroy(gameObject);
        }
    }

    public void LetsSpawnOut() {
        spawningOut = true;
        spawnTime = Time.time;
    }
}