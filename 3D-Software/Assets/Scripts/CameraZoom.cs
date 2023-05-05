using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float cam_move = (float)0.5;
    void Update()
    {
        CamZoom();
    }

    public void CamZoom()
    {
        // -------------------Code for Zooming Out------------
    if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView<=125)
            {
                Camera.main.fieldOfView +=2;
            }
                
            if (Camera.main.orthographicSize<=20)
            {
                Camera.main.orthographicSize += cam_move;
            }
                                
        }
    // ---------------Code for Zooming In------------------------
     if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView>2)
            {
                Camera.main.fieldOfView -=2;
            }
            if (Camera.main.orthographicSize>=1)
            {
                Camera.main.orthographicSize -= cam_move;
            }
                                
        }
       
    // -------Code to switch camera between Perspective and Orthographic--------
     if (Input.GetKeyUp(KeyCode.B ))
        {
        if (Camera.main.orthographic==true)
        {
            Camera.main.orthographic=false;
        }
        else
        {
            Camera.main.orthographic=true;
        }
            
        }
    }
}
