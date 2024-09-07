using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> hits = new();
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount==0)
        return;
        if(raycastManager.Raycast(Input.GetTouch(0).position,hits,TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("Raycast hit");
        }
    }
}
