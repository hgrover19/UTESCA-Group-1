using UnityEngine;

namespace NameSpaceCopyPaste
{
public class CopyPaste : MonoBehaviour
{
    private GameObject copiedObject;
    
    // Copy the selected object
    public void CopyObject(GameObject selectedObject)
    {
        if (selectedObject != null)
        {
            copiedObject = Instantiate(selectedObject, selectedObject.transform.position, selectedObject.transform.rotation);
        }
    }

    // Paste the copied object
    public void PasteObject()
    {
        if (copiedObject != null)
        {
            GameObject pastedObject = Instantiate(copiedObject, copiedObject.transform.position + Vector3.right, copiedObject.transform.rotation);
            pastedObject.name = "Copied Object";
            pastedObject.SetActive(true);
        }
    }
}
}
