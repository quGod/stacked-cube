using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movable : MonoBehaviour
{
    public float Velocity = 2;
    public int tabSize = 100;
    // 
    private Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private float intensity = 0;
    // for rotation of camera
    private float mouseX;
    private float mouseY;
    private bool VRotationEnable = true;
    private float VRotationMin = 0f;//in degree value
    private float VRotationMzx = 65f;//in degree value
    // Update is called once per frame
    void Start()
    {

    }
    //Handles the mouse rotation vertically and horizontally
    public void HandleMouseRotation()
    {
        var easeFactor = 10f;
        //horizontal rotation
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl))
        {
            if(Input.mousePosition.x != mouseX)
            {
                var cameraRotationY = (Input.mousePosition.x - mouseX) * easeFactor * Time.deltaTime;
                this.transform.Rotate(0, cameraRotationY, 0);
            }
            //vertical rotation
            if (VRotationEnable && Input.mousePosition.y != mouseY)
            {
                GameObject MainCamera = this.gameObject.transform.Find("MainCamera").gameObject;
                var cameraRotationX = (mouseY - Input.mousePosition.y) * easeFactor * Time.deltaTime;
                var desiredRotationX = MainCamera.transform.eulerAngles.x + cameraRotationX;

                if(desiredRotationX >= VRotationMin && desiredRotationX <= VRotationMzx)
                {
                    MainCamera.transform.Rotate(cameraRotationX, 0, 0);
                }
            }
        }

    }
    void LateUpdate()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 dir = mouse - center;
        dir.z = dir.y;
        dir.y = 0;
        Vector3 scroll = Input.mouseScrollDelta;
        Vector3.Normalize(dir);
        HandleMouseRotation();
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        // only update if the mousebutton is held down
        if (Input.GetMouseButtonDown(2))
        {
            this.transform.position = new Vector3 (0, 8, 0);
        }
        if (scroll.y == 0)
            {
                if (Screen.width - tabSize < mouse.x)
                    intensity -= ((Screen.width - mouse.x) - tabSize);
                if (tabSize > mouse.x)
                    intensity += (tabSize - mouse.x);
                if (Screen.height - tabSize < mouse.y)
                    intensity -= ((Screen.height - mouse.y) - tabSize);
                if (tabSize > mouse.y)
                    intensity += (tabSize - mouse.y);

                intensity /= tabSize * 10;
                transform.Translate(Velocity * dir * intensity * Time.deltaTime, Space.World);
            }
            if (scroll.y != 0)
            {
                Vector3.Normalize(scroll);
                intensity += tabSize;
                intensity = -1 * intensity;
                transform.Translate(Velocity * scroll * intensity * Time.deltaTime, Space.World);
            }
        
        
    }
}

