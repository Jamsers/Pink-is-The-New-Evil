using UnityEngine;

public class AdaptBloodSplatterToScreen : MonoBehaviour {
    public RectTransform bloodRectTransform;
	
	void Update () {
        bloodRectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta;
    }
}
