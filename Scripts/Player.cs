using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Destroy(GetComponent<HingeJoint2D>());
        }
    }
}
