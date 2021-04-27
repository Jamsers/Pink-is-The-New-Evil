using UnityEngine;
using System;

public class CameraLogic : MonoBehaviour {
    public Transform cameraTarget;
    public bool isTracking = true;
    public LayerMask layerMask;
    public Transform visionTestPlane;

    [Header("Materials")]
    public Material transparancyMaterial;
    public Material roadMaterial;
    public Material towerMaterial;
    public Material buildingMaterial;
    public Material spiralMaterial;

    [Header("Models")]
    public Renderer roadModel;
    public Renderer pillar1Model;
    public Renderer pillar2Model;
    public Renderer pillar3Model;
    public Renderer building1Model;
    public Renderer building2Model;
    public Renderer building3Model;
    public Renderer spiralTowerModel;

    [Header("Vision Blockers")]
    public Transform pillar1Blocker;
    public Transform pillar2Blocker;
    public Transform pillar3Blocker;
    public Transform[] roadBlockers;
    public Transform[] buildingsSpiralBlockers;

    Vector3 cameraTargetOffset;
    RaycastHit hitOut;
    Ray visionTest;

    void Start() {
        cameraTargetOffset = transform.position - cameraTarget.position;
        MainSystems mainSystems = GameObject.Find("Main Systems").GetComponent<MainSystems>();
        mainSystems.cameraStartPosition = transform.position;
        mainSystems.cameraStartRotation = transform.rotation;
        mainSystems.cameraStartFOV = GetComponent<Camera>().fieldOfView;
    }

    void Update() {
        if (isTracking == true) {
            transform.position = cameraTarget.position + cameraTargetOffset;
        }

        visionTest = new Ray(transform.position, visionTestPlane.position - transform.position);
        Physics.Raycast(visionTest, out hitOut, Mathf.Infinity, layerMask);
        HideBlockersHit(hitOut.transform);
    }

    void HideBlockersHit(Transform hitTransform) {
        if (hitTransform == visionTestPlane) {
            roadModel.sharedMaterial = roadMaterial;
            pillar1Model.sharedMaterial = towerMaterial;
            pillar2Model.sharedMaterial = towerMaterial;
            pillar3Model.sharedMaterial = towerMaterial;
            spiralTowerModel.sharedMaterial = spiralMaterial;
            building1Model.sharedMaterial = buildingMaterial;
            building2Model.sharedMaterial = buildingMaterial;
            building3Model.sharedMaterial = buildingMaterial;
        }
        else {
            if (hitTransform == pillar1Blocker) {
                pillar1Model.sharedMaterial = transparancyMaterial;
            }
            else if (hitTransform == pillar2Blocker) {
                pillar2Model.sharedMaterial = transparancyMaterial;
            }
            else if (hitTransform == pillar3Blocker) {
                pillar3Model.sharedMaterial = transparancyMaterial;
            }
            else if (Array.Exists(roadBlockers, blocker => blocker == hitTransform)) {
                roadModel.sharedMaterial = transparancyMaterial;
            }
            else if (Array.Exists(buildingsSpiralBlockers, blocker => blocker == hitTransform)) {
                spiralTowerModel.sharedMaterial = transparancyMaterial;
                building1Model.sharedMaterial = transparancyMaterial;
                building2Model.sharedMaterial = transparancyMaterial;
                building3Model.sharedMaterial = transparancyMaterial;
            }
        }
    }
}
