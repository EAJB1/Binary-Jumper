using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    public Transform Target;
    public Transform Self;
    Vector3 tempVec3 = new Vector3();

    void LateUpdate()
    {
        if (Target != null)
        {
            tempVec3.x = Target.position.x;
            tempVec3.y = Target.position.y;
            tempVec3.z = this.transform.position.z;
            this.transform.position = tempVec3;
        }
        else if (Target == null)
        {
            tempVec3.x = Self.position.x;
            tempVec3.y = Self.transform.position.y;
            tempVec3.z = Self.transform.position.z;
            Self.transform.position = tempVec3;
        }
    }
}
