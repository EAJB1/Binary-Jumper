using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Transform Camera;
    Vector3 tempVec3 = new Vector3();

    void LateUpdate()
    {
        /*tempVec3.x = Target.position.x;
        tempVec3.y = this.transform.position.y;
        tempVec3.z = this.transform.position.z;
        this.transform.position = tempVec3;*/

        if (Target != null)
        {
            tempVec3.x = Target.position.x;
            tempVec3.y = this.transform.position.y;
            tempVec3.z = this.transform.position.z;
            this.transform.position = tempVec3;
        }
        else if (Target == null)
        {
            tempVec3.x = Camera.position.x;
            tempVec3.y = Camera.transform.position.y;
            tempVec3.z = Camera.transform.position.z;
            Camera.transform.position = tempVec3;
        }
    }
}