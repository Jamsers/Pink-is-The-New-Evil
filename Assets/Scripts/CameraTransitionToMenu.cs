using UnityEngine;

public class CameraTransitionToMenu : MonoBehaviour {
    public GameObject objective;

    Transform start;
    Transform end;
    float startTime;
    const float Speed = 1f;
    float distance;
    
	void Start () {
        start = transform;
        end = objective.transform;
        startTime = Time.time;
        distance = Vector3.Distance(start.position, end.position);
        Destroy(gameObject, 5);
	}
	
	void Update () {
        float distanceCovered = (Time.time - startTime) * Speed;
        float distancePercentage = distanceCovered / distance;

        transform.position = Vector3.Lerp(start.position, end.position, distancePercentage);
        transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, distancePercentage);
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, objective.GetComponent<Camera>().fieldOfView, distancePercentage);
	}
}
