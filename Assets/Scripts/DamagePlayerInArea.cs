using UnityEngine;
using System.Collections;

public class DamagePlayerInArea : MonoBehaviour {

    bool isPlayerInDanger;
    PlayerController playerai;
    bool specialimsoDAMAGED = false;

	// Use this for initialization
	void Start () {
        playerai = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) <= transform.lossyScale.x / 2 && specialimsoDAMAGED == false) {
            playerai.Health(-20);
            specialimsoDAMAGED = true;
            Invoke("HeiiiiImNotSoDamagedAfterAll", .5f);
        }
        else if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 1.5) {
            CancelInvoke("HeiiiiImNotSoDamagedAfterAll");
            specialimsoDAMAGED = false;
        }
        //Debug.Log(transform.lossyScale);
    }

    void HeiiiiImNotSoDamagedAfterAll () {
        specialimsoDAMAGED = false;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player Identifier")
            isPlayerInDanger = true;
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player Identifier")
            isPlayerInDanger = false;
    }
}
