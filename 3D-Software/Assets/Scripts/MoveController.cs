using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public GameObject selectedObject;
	float dirx, diry, dirz;
	[Range(1f, 100f)]
	public float moveSpeed = 70f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        
		RaycastHit hitInfo;
		
		if( Physics.Raycast( ray, out hitInfo ) & Input.GetMouseButtonDown(0)) {
			Debug.Log("Mouse is over: " + hitInfo.collider.name );

			// The collider we hit may not be the "root" of the object
			// You can grab the most "root-est" gameobject using
			// transform.root, though if your objects are nested within
			// a larger parent GameObject (like "All Units") then this might
			// not work.  An alternative is to move up the transform.parent
			// hierarchy until you find something with a particular component.

			GameObject hitObject = hitInfo.transform.root.gameObject;

			SelectObject(hitObject);
		}
		else if(!Physics.Raycast( ray, out hitInfo ) & Input.GetMouseButtonDown(0)){
			ClearSelection();
		}

		if(selectedObject != null) {
			PositionModifier();
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
}   
