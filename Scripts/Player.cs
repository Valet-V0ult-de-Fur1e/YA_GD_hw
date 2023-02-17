using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float thrust = 20f;
    public float start_force = 1500f;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(new Vector2(-start_force, 0));
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Destroy(GetComponent<HingeJoint2D>());
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rb2D.AddForce(new Vector2(thrust, 0));
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            rb2D.AddForce(new Vector2(-thrust, 0));
        }
    }
}
