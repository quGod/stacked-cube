using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBuildingSystem : MonoBehaviour
{
    public Transform ObjToMove; //define thhe coordinates of object
    public GameObject ObjToPlace;//define the object
    public LayerMask mask;//define the layers
    int LastPosX, LastPosY, LastPosZ;
    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            /*float PosX = hit.point.x;
            float PosZ = hit.point.z;
            Debug.Log("X : " + PosX + " Z : " + PosZ);*/

            int PosX = (int)Mathf.Round(hit.point.x);
            int PosY = (int)Mathf.Round(hit.point.y);
            int PosZ = (int)Mathf.Round(hit.point.z);

            if (PosX != LastPosX || PosY != LastPosY || PosZ != LastPosZ)
            {
                LastPosX = PosX;
                LastPosY = PosY;
                LastPosZ = PosZ;
                //Debug.Log("X : " + LastPosX + " Y : " + LastPosY + "Z : " + LastPosZ);
                ObjToMove.position = new Vector3(PosX, PosY+.75f, PosZ);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(ObjToPlace, ObjToMove.position, Quaternion.identity);
            }
        }
    }
}
