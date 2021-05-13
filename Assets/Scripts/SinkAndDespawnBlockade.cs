using UnityEngine;

public class SinkAndDespawnBlockade : MonoBehaviour {
    public float shakeAmount;
    public float shakeInterval;
    public Transform objectsContainer;
    public GameObject[] smokes;

    const float sinkLength = 5;
    float initiateStartTime;
    bool initiateSink = false;
    bool hasInitialized = false;
    Vector3 moveUpperCubeorigpos;
    Vector3 beginPos;
    Vector3 endPos;

    void Update() {
        if (initiateSink == false)
            return;

        if (hasInitialized == false) {
            PinkIsTheNewEvil.PlayerSoundManager.PlaySound(13);
            beginPos = transform.position;
            endPos = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
            MoveStart();

            foreach (GameObject smoke in smokes) {
                smoke.SetActive(true);
                smoke.transform.parent = null;
                Destroy(smoke, 3);
            }

            hasInitialized = true;
            return;
        }

        float lerp = (Time.time - initiateStartTime) / sinkLength;
        float smoothedLerp = Mathf.SmoothStep(0f, 1f, lerp);

        if (lerp > 1) {
            initiateSink = false;
            hasInitialized = false;
            gameObject.SetActive(false);
        }
        else {
            transform.position = Vector3.Lerp(beginPos, endPos, smoothedLerp);
        }
    }

    public void DespawnBlockade() {
        initiateSink = true;
        initiateStartTime = Time.time;
    }

    void MoveStart() {
        moveUpperCubeorigpos = objectsContainer.localPosition;
        Invoke("MoveUpper", shakeInterval * 0);
        Invoke("MoveLower", shakeInterval * 1);
        Invoke("MoveUpper2", shakeInterval * 2);
        Invoke("MoveLower", shakeInterval * 3);
        Invoke("MoveFinish", shakeInterval * 4);
    }

    void MoveUpper() {
        objectsContainer.localPosition = new Vector3(objectsContainer.localPosition.x + shakeAmount / 2, objectsContainer.localPosition.y + shakeAmount / 2, objectsContainer.localPosition.z + shakeAmount / 2);
    }

    void MoveLower() {
        objectsContainer.localPosition = new Vector3(objectsContainer.localPosition.x - shakeAmount, objectsContainer.localPosition.y - shakeAmount, objectsContainer.localPosition.z - shakeAmount);
    }

    void MoveUpper2() {
        objectsContainer.localPosition = new Vector3(objectsContainer.localPosition.x + shakeAmount, objectsContainer.localPosition.y + shakeAmount, objectsContainer.localPosition.z + shakeAmount);
    }

    void MoveFinish() {
        objectsContainer.localPosition = moveUpperCubeorigpos;
        Invoke("MoveStart", shakeInterval);
    }
}