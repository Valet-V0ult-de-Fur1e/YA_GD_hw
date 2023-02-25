using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_rope : MonoBehaviour
{
    private Rigidbody2D _rb2D;
    [SerializeField] private float _startForce = 500f;
    [SerializeField] private float _connectDelay = 0.5f;
    private float _connectTime;
    
    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.AddForce(new Vector2(-_startForce, 0));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Player"){
            
            if (! other.gameObject.TryGetComponent<HingeJoint2D>(out HingeJoint2D hingeJoint2D))
            {
                if (!(_connectTime + _connectDelay < Time.time)) 
                    return;

                _connectTime = Time.time;
                HingeJoint2D hingeJoint = other.gameObject.AddComponent<HingeJoint2D>();
                hingeJoint.connectedBody = _rb2D;
            }
        }
    }
}
