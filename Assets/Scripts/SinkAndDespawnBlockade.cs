using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkAndDespawnBlockade : MonoBehaviour {

    Vector3 moveUpperCubeorigpos;
    public float shakeAmount;
    public float shakeInterval;
    public Transform objectsContainer;

    void Start () {
        
        
        
    }
	
	void Update () {
        Sink();

	}

    void MoveUpper()
    {
        moveUpperCubeorigpos = objectsContainer.localPosition;
        objectsContainer.localPosition = new Vector3(objectsContainer.localPosition.x + shakeAmount / 2, objectsContainer.localPosition.y + shakeAmount / 2, objectsContainer.localPosition.z + shakeAmount / 2);
        Invoke("MoveLower", shakeInterval);
        Invoke("MoveUpper2", shakeInterval*2);
        Invoke("MoveLower", shakeInterval*3);
        Invoke("MoveFinish", shakeInterval*4);
        //Debug.Log("moveup");
    }

    void MoveLower()
    {
        objectsContainer.localPosition = new Vector3(objectsContainer.localPosition.x - shakeAmount, objectsContainer.localPosition.y - shakeAmount, objectsContainer.localPosition.z - shakeAmount);
        //Debug.Log("movelow");
    }

    void MoveUpper2()
    {
        objectsContainer.localPosition = new Vector3(objectsContainer.localPosition.x + shakeAmount, objectsContainer.localPosition.y + shakeAmount, objectsContainer.localPosition.z + shakeAmount);
        //Debug.Log("moveup");
    }

    void MoveFinish()
    {
        //cube.position = new Vector3(cube.position.x + .05f, cube.position.y + .05f, cube.position.z + .05f);
        objectsContainer.localPosition = moveUpperCubeorigpos;
        Invoke("MoveUpper", shakeInterval);
        //Debug.Log("moveup");
    }

    public GameObject[] smokes;

    public void DespawnBlockade () {
        initiateSink = true;
        initiateStartTime = Time.time;
    }

    bool initiateSink = false;
    float initiateStartTime;
    bool hasCreatedInitalParams = false;

    Vector3 beginPos;
    Vector3 endPos;

    float sinkLength = 5;

    void Sink() {
        if (initiateSink == true) {
            if (hasCreatedInitalParams == false) {
                MoveUpper();
                GameObject.FindWithTag("Player").GetComponent<PlaySoundEffect>().PlaySound(13);
                beginPos = transform.position;
                endPos = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);

                for (int i = 0; i < smokes.Length; i++) {
                    smokes[i].SetActive(true);
                    smokes[i].transform.parent = null;
                    Destroy(smokes[i], 3);
                }

                hasCreatedInitalParams = true;
            }
            else {
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
        }
    }
}
