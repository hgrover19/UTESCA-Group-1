using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{   
    public GameObject parentModel;

    private float rotationSpeed = 500.0f;
    private Vector3 mouseWorldPosStart;
    private float zoomScale = 5.0f;
    public float minZoomDistance = 1.0f;
    public float maxZoomDistance = 500.0f;
    private float defaultFieldOfView = 60.0f;
    public float zoomSpeed = 50.0f;
    public MoveController moveController;
    

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Mouse2))
        {
            CamOrbit();
        }

        if(Input.GetKey(KeyCode.LeftShift) &&  Input.GetKey(KeyCode.F))
        {
            //FitToScreen();
        }
        if(Input.GetMouseButtonDown(2) && !Input.GetKey(KeyCode.LeftShift))
        {
            mouseWorldPosStart = GetPerspectivePos();
        }
        if(Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftShift))
        {
            Pan();
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));

        if (moveController.selectedObject != null)
        {
            
        }

    }


    private void CamOrbit()
    {
        if(Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, -verticalInput);
            transform.Rotate(Vector3.up, -horizontalInput, Space.World);
        }
    }

    // private Bounds GetBound(GameObject parentGameObj)
    // {
    //     Bounds bound = new Bounds(parentGameObj.transform.position, Vector3.zero);
    //     var rList = parentGameObj.GetComponentInChildren(typeof(Renderer));
    //     foreach (Renderer r in rList)
    //     {
    //         bound.Encapsulate(r.bounds);
    //     }
    //     return bound;
    // }

    // private void FitToScreen()
    // {
    //     Camera.main.fieldOfView = defaultFieldOfView;
    //     Bounds bound = GetBound(parentModel);
    //     Vector3 boundSize = bound.size;
    //     float boundDiagonal = Mathf.Sqrt((boundSize.x * boundSize.x) + (boundSize.y * boundSize.y) + (boundSize.z * boundSize.z));
    //     float camDistanceToBoundCentre = boundDiagonal / 2.0f / (Mathf.Tan(Camera.main.fieldOfView / 2.0f * Mathf.Deg2Rad));
    //     float camDistanceToBoundWithOffset = camDistanceToBoundCentre + boundDiagonal / 2.0f - (Camera.main.transform.position - transform.position).magnitude;
    //     transform.position = bound.center + (-transform.forward * camDistanceToBoundWithOffset);
    // }

    private void Pan()
    {
        if(Input.GetAxis("Mouse Y") !=0 || Input.GetAxis("Mouse X") !=0)
        {
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - GetPerspectivePos();
            transform.position += mouseWorldPosDiff;
        }
    }

    private void Zoom(float zoomDiff)
    {
        if(zoomDiff != 0)
        {   
            
            Vector3 zoomDirection = transform.forward;
            transform.position += zoomDirection * zoomDiff * zoomSpeed;
            float clampedDistance = Mathf.Clamp(transform.position.magnitude, minZoomDistance, maxZoomDistance);
            transform.position = transform.position.normalized * clampedDistance;
            
        }
    }

    private Vector3 GetPerspectivePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(transform.forward, 0.0f);
        float dist;
        plane.Raycast(ray, out dist);
        return ray.GetPoint(dist);
    }

    public void HandleInputData(int val)
    {
       if (moveController.selectedObject != null)
       {
        CameraView(val, moveController.selectedObject);
       }
    }

    public void CameraView(int view, GameObject selectedObject)
    {
        Renderer renderer = selectedObject.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;
        float width = bounds.size.x;
        float height = bounds.size.y;
        float depth = bounds.size.z;

        if (view == 0) // Top View
        {
            transform.position =  selectedObject.transform.position + new Vector3(0, height/2 + 150, 0);
            transform.LookAt(new Vector3(1,0,0));
        }

        else if (view == 1) // Bottom View
        {
            transform.position =  selectedObject.transform.position + new Vector3(0, -height/2 - 150, 0);
            transform.LookAt(new Vector3(1,0,0));
        }

        else if (view == 2) // Left View
        {
            transform.position =  selectedObject.transform.position + new Vector3(-width/2 - 150, 0, 0);
            transform.LookAt(new Vector3(0,0,1));
        }

        else if (view == 3) // Right View
        {
            transform.position =  selectedObject.transform.position + new Vector3(width/2 + 150, 0, 0);
            transform.LookAt(new Vector3(0,0,1));
        }

        else if (view == 4) // Front View
        {
            transform.position =  selectedObject.transform.position + new Vector3(0, 0, depth/2 + 150);
            transform.LookAt(new Vector3(0,1,0));
        }

        else if (view == 5) // Back View
        {
            transform.position =  selectedObject.transform.position + new Vector3(0, 0, -depth/2 - 150);
            transform.LookAt(new Vector3(0,1,0));
        }
        
    }
}