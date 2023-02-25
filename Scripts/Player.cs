using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb2D;
    [SerializeField] private float _thrust = 200f;
    [SerializeField] private float _startForce = 300f;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.AddForce(new Vector2(-_startForce, 0));
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Destroy(GetComponent<HingeJoint2D>());
        }

        if (Input.GetKeyDown(KeyCode.D)) 
        {
            _rb2D.AddForce(new Vector2(_thrust, 0));
        }

        if (Input.GetKeyDown(KeyCode.A)) 
        {
            _rb2D.AddForce(new Vector2(-_thrust, 0));
        }
    }
}
