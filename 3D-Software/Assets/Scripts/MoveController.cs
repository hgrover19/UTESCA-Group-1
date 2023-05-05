using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour
{
    public GameObject selectedObject;
	float dirx, diry, dirz;
	[Range(1f, 100f)]
	public float moveSpeed = 70f;
	public int status = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

		if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.collider.gameObject;

                    if (hitObject.GetComponent<RectTransform>() == null && hitObject.name != "Plane")
                    {
                        SelectObject(hitObject);
                    }
                    else
                    {
                        Debug.Log("UI Element selected: " + hitObject.name);
                    }
                }
                else
                {
                    ClearSelection();
                }
            }
        }


		if(selectedObject != null) {
			if (status == 0)
			{
				PositionModifier();
			}
			else if (status == 1)
			{
				RotationModifier();
			}
			else if (status == 2)
			{
				ScaleModifier();
			}
		}
    }

    

    void SelectObject(GameObject obj) {
		if(selectedObject != null) {
			if(obj == selectedObject)
				return;

			ClearSelection();
		}

		selectedObject = obj;

		Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rs) {
			Material m = r.material;
			m.color = Color.green;
			r.material = m;
		}
	}

	void ClearSelection() {
		if(selectedObject == null)
			return;

		Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rs) {
			Material m = r.material;
			m.color = Color.white;
			r.material = m;
		}


		selectedObject = null;
	}
	
	void PositionModifier() {
		dirx = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		diry = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		if (Input.GetKey(KeyCode.E)){
			dirz = moveSpeed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.Q)){
			dirz = -moveSpeed * Time.deltaTime;
		}
		selectedObject.transform.position += new Vector3(dirx, diry, dirz);
		dirz = 0f;
	}

	void RotationModifier() {
		dirx = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		diry = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		if (Input.GetKey(KeyCode.E)){
			dirz = moveSpeed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.Q)){
			dirz = -moveSpeed * Time.deltaTime;
		}
		selectedObject.transform.Rotate(dirx, diry, dirz);
		dirz = 0f;
	}

	void ScaleModifier() {
		dirx = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		diry = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		if (Input.GetKey(KeyCode.E)){
			dirz = moveSpeed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.Q)){
			dirz = -moveSpeed * Time.deltaTime;
		}
		selectedObject.transform.localScale += new Vector3(dirx, diry, dirz);
		dirz = 0f;
	}

	public void HandleInputDataObjectController(int val)
	{
		status = val;
		Debug.Log("Status: " + status);
	}

	public void ResetOrigin()
	{
		if (selectedObject == null)
		{
			return;
		}
			
		selectedObject.transform.position = new Vector3(0, 77, 0);
		selectedObject.transform.rotation = Quaternion.identity;
		selectedObject.transform.localScale = new Vector3(1, 1, 1);
	}
}   
