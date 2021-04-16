using UnityEngine;
using System.Collections;

public class CameraAI : MonoBehaviour {

    public Transform cameraTarget;
    Vector3 cameraTargetOffset;
    public bool isOn = true;

	void Start () {
        cameraTargetOffset = transform.position - cameraTarget.position;
        GameObject.Find("Systems Process").GetComponent<SystemsProcess>().playerCamStartPos = transform.position;
        GameObject.Find("Systems Process").GetComponent<SystemsProcess>().playerCamStartRot = transform.rotation;
        GameObject.Find("Systems Process").GetComponent<SystemsProcess>().playerCamStartFOV = GetComponent<Camera>().fieldOfView;
    }

    public LayerMask myLayerMask;

    public GameObject visionTest;

    public Material transparancy;

    public Material road;
    public Material tower;
    public Material building;
    public Material spiral;

    public GameObject RoadTopTest;
    public GameObject RoadBottomTest;
    public GameObject RoadPillar1Test;
    public GameObject RoadPillar2Test;
    public GameObject Pillar1Test;
    public GameObject Pillar2Test;
    public GameObject Pillar3Test;
    public GameObject Building1Test;
    public GameObject Building2Test;
    public GameObject Building3Test;
    public GameObject SpiralTowerTopTest;
    public GameObject SpiralTowerBase1Test;
    public GameObject SpiralTowerBase2Test;
    public GameObject SpiralTowerBase3Test;
    public GameObject SpiralTowerBase4Test;
    public GameObject SpiralTowerBase5Test;

    public GameObject roadModel;
    public GameObject Pillar1Model;
    public GameObject Pillar2Model;
    public GameObject Pillar3Model;
    public GameObject Building1Model;
    public GameObject Building2Model;
    public GameObject Building3Model;
    public GameObject SpiralTowerModel;


    void Update () {
        if (isOn == true)
        {
            transform.position = cameraTarget.position + cameraTargetOffset;
        }

        RaycastHit hit;
        Ray ray = new Ray(transform.position, visionTest.transform.position - transform.position);
        Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask);

        //Debug.DrawLine(transform.position, hit.point, Color.red, 0, true);

        if (hit.transform == visionTest.transform) {
            //Debug.Log("I see the player.");
            roadModel.GetComponent<Renderer>().sharedMaterial = road;
            Pillar1Model.GetComponent<Renderer>().sharedMaterial = tower;
            Pillar2Model.GetComponent<Renderer>().sharedMaterial = tower;
            Pillar3Model.GetComponent<Renderer>().sharedMaterial = tower;
            SpiralTowerModel.GetComponent<Renderer>().sharedMaterial = spiral;
            Building1Model.GetComponent<Renderer>().sharedMaterial = building;
            Building2Model.GetComponent<Renderer>().sharedMaterial = building;
            Building3Model.GetComponent<Renderer>().sharedMaterial = building;
        }
        else if (hit.transform == RoadTopTest.transform || hit.transform == RoadBottomTest.transform || hit.transform == RoadPillar1Test.transform || hit.transform == RoadPillar2Test.transform) {
            //Debug.Log("Blocked by roadModel.");
            roadModel.GetComponent<Renderer>().sharedMaterial = transparancy;
        }
        else if (hit.transform == Pillar1Test.transform) {
            //Debug.Log("Blocked by Pillar1Model.");
            Pillar1Model.GetComponent<Renderer>().sharedMaterial = transparancy;
        }
        else if (hit.transform == Pillar2Test.transform) {
            //Debug.Log("Blocked by Pillar2Model.");
            Pillar2Model.GetComponent<Renderer>().sharedMaterial = transparancy;
        }
        else if (hit.transform == Pillar3Test.transform) {
            //Debug.Log("Blocked by Pillar3Model.");
            Pillar3Model.GetComponent<Renderer>().sharedMaterial = transparancy;
        }
        else if (hit.transform == SpiralTowerTopTest.transform || hit.transform == SpiralTowerBase1Test.transform || hit.transform == SpiralTowerBase2Test.transform || hit.transform == SpiralTowerBase3Test.transform || hit.transform == SpiralTowerBase4Test.transform || hit.transform == SpiralTowerBase5Test.transform || Building1Test.transform || Building2Test.transform || Building3Test.transform) {
            //Debug.Log("Blocked by SpiralTowerModel or Buildings.");
            SpiralTowerModel.GetComponent<Renderer>().sharedMaterial = transparancy;
            Building1Model.GetComponent<Renderer>().sharedMaterial = transparancy;
            Building2Model.GetComponent<Renderer>().sharedMaterial = transparancy;
            Building3Model.GetComponent<Renderer>().sharedMaterial = transparancy;
        }
        else {
            Debug.Log("ERROR");
        }
    }
}
