using UnityEngine;

public class SinkAndDespawnBlockade : MonoBehaviour {
    public float shakeAmount;
    public float shakeInterval;
    public Transform objectsContainer;
    public GameObject[] smokes;

    float sinkLength = 5;
    float initiateStartTime;
    Vector3 beginPos;
    Vector3 endPos;
    bool initiateSink = false;
    bool hasCreatedInitalParams = false;
    Vector3 moveUpperCubeorigpos;

    void Update() {
        if (initiateSink == false)
            return;

        if (hasCreatedInitalParams == false) {
            MoveUpper();
            PinkIsTheNewEvil.PlayerSoundManager.PlaySound(13);
            beginPos = transform.position;
            endPos = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);

            for (int i = 0; i < smokes.Length; i++) {
                smokes[i].SetActive(true);
                smokes[i].transform.parent = null;
                Destroy(smokes[i], 3);
            }

            hasCreatedInitalParams = true;
            return;
        }


        float lerp = (Time.time - initiateStartTime) / sinkLength;
        if (lerp > 1) {
            initiateSink = false;
            hasCreatedInitalParams = false;
            gameObject.SetActive(false);
        }
        else {
            transform.position = Vector3.Lerp(beginPos, endPos, Mathf.SmoothStep(0f, 1f, lerp));
        }
    }

    void MoveUpper() {
        moveUpperCubeorigpos = objectsContainer.localPosition;
        objectsContainer.localPosition = new Vector3(objectsContainer.localPosition.x + shakeAmount / 2, objectsContainer.localPosition.y + shakeAmount / 2, objectsContainer.localPosition.z + shakeAmount / 2);
        Invoke("MoveLower", shakeInterval);
        Invoke("MoveUpper2", shakeInterval * 2);
        Invoke("MoveLower", shakeInterval * 3);
        Invoke("MoveFinish", shakeInterval * 4);
    }

    void MoveLower() {
        objectsContainer.localPosition = new Vector3(objectsContainer.localPosition.x - shakeAmount, objectsContainer.localPosition.y - shakeAmount, objectsContainer.localPosition.z - shakeAmount);
    }

    void MoveUpper2() {
        objectsContainer.localPosition = new Vector3(objectsContainer.localPosition.x + shakeAmount, objectsContainer.localPosition.y + shakeAmount, objectsContainer.localPosition.z + shakeAmount);
    }

    void MoveFinish() {
        objectsContainer.localPosition = moveUpperCubeorigpos;
        Invoke("MoveUpper", shakeInterval);
    }

    public void DespawnBlockade() {
        initiateSink = true;
        initiateStartTime = Time.time;
    } 
}