using UnityEngine;
using System.Collections;

public class spinnybaldes : MonoBehaviour {

    public float fizzExpandOrDexpand;
    public float fizzLife;
    public Vector3 fromPos;
    public GameObject damageSphere;
    float spawnTime;
    Vector3 originalPos;
    Vector3 originalParticleScale;
    bool existing;
    bool countingExisting = false;
    bool spawningOut = false;

    // Use this for initialization
    void Start() {
        damageSphere.SetActive(false);
        originalPos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
        fromPos = transform.position;
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        float scalePercent = (Time.time - spawnTime) / fizzExpandOrDexpand;
        if ((scalePercent < 1) && spawningOut == false) {
            if (scalePercent > 0.75) {
                damageSphere.SetActive(true);
            }
            transform.position = Vector3.Lerp(fromPos, originalPos, scalePercent);
        }
        else if (countingExisting == false) {
            transform.position = originalPos;
            countingExisting = true;
            //Invoke("LetsSpawnOut", fizzLife);
        }
        else if (spawningOut == true) {
            if (scalePercent > 0.25) {
                damageSphere.SetActive(false);
            }
            transform.position = Vector3.Lerp(originalPos, fromPos, scalePercent);
            if (scalePercent > fizzExpandOrDexpand) {
                Destroy(gameObject);
            }
        }

    }

    public void LetsSpawnOut() {
        spawningOut = true;
        spawnTime = Time.time;
    }
}
