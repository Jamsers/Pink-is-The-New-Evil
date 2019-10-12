using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkAndDespawnBlockade : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        Sink();
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
