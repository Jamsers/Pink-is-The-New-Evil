using UnityEngine;
using System.Collections;

public class cameralerp : MonoBehaviour {

    public GameObject objective;

    Transform start;
    Transform end;
    float starttime;
    float speed = 1;
    float distance;
    

	// Use this for initialization
	void Start () {
        start = gameObject.transform;
        end = objective.transform;
        starttime = Time.time;
        distance = Vector3.Distance(start.position, end.position);
        Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
        float distancecovered = (Time.time - starttime) * speed;
        float fracofjourney = distancecovered / distance;
        

        transform.position = Vector3.Lerp(start.position, end.position, fracofjourney);
        transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, fracofjourney);
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, objective.GetComponent<Camera>().fieldOfView, fracofjourney);
	}

    
}
