using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class PlaceARObject : MonoBehaviour
{
    // Start is called before the first frame update
    ARControls controls;
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    bool _isPressed = false;
    [SerializeField]
    GameObject ARObject;
    GameObject SpawnedObject = null;
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        controls = new ARControls();
        controls.Controls.Touch.performed += ctx => _isPressed = true;
        controls.Controls.Touch.canceled += ctx =>_isPressed = false;
        SpawnedObject = null;
    }
    void OnEnable()
    {
        controls.Controls.Enable();
    }
    void OnDisable()
    {
        controls.Controls.Disable();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Pointer.current == null && !_isPressed)
            return;
        var touchPosition = Pointer.current.position.ReadValue();
        if(raycastManager.Raycast(touchPosition,hits,TrackableType.PlaneWithinPolygon))
        {
            var hitpose = hits[0].pose;
            if(SpawnedObject == null)
            {
                SpawnedObject = Instantiate(ARObject,hitpose.position,hitpose.rotation);
                Debug.Log("Placed gameObject");
            }
            else
            {
                SpawnedObject.transform.position = hitpose.position;
                SpawnedObject.transform.rotation = hitpose.rotation;
            }
        }

    }
}
