using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_rope : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float start_force = 500f;
    public float connectDelay = 0.5f;
    public float connectTime;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(new Vector2(-start_force, 0));
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Player"){
            if (! other.gameObject.TryGetComponent<HingeJoint2D>(out HingeJoint2D hingeJoint2D)){
                if (!(connectTime + connectDelay < Time.time)) return;
                connectTime = Time.time;
                HingeJoint2D hingeJoint = other.gameObject.AddComponent<HingeJoint2D>();
                hingeJoint.connectedBody = rb2D;
            }
        }
    }
}
