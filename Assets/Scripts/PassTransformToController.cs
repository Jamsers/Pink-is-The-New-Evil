using UnityEngine;
using System.Collections;

public class PassTransformToController : MonoBehaviour {

    PlayerController playerai;

    // Use this for initialization
    void Start()
    {
        playerai = transform.parent.GetComponent<PlayerController>();
        playerai.transformToShake = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
