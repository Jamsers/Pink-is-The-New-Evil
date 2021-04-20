using UnityEngine;
using System.Collections;

public class AdaptBloodSplatterToScreen : MonoBehaviour {

    public GameObject blood;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float x = GetComponent<RectTransform>().sizeDelta.x;
        float y = GetComponent<RectTransform>().sizeDelta.y;
        blood.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
    }
}
