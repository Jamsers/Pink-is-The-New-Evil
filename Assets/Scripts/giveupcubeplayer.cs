using UnityEngine;
using System.Collections;

public class giveupcubeplayer : MonoBehaviour {

    PlayerAI playerai;

    // Use this for initialization
    void Start()
    {
        playerai = transform.parent.GetComponent<PlayerAI>();
        playerai.cube = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
