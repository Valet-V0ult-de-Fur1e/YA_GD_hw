using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Slide : MonoBehaviour
{
    [SerializeField] private float _minGroundNormalY = .3f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed;
    [SerializeField] private float buttonTime = 0.3f;
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityScale = 5;
    [SerializeField] private float _connectDelay = 3f;
    
    private float _connectTime;
    private float velocity;

    private Rigidbody2D _rb2d;

    private Vector2 _groundNormal;
    private Vector2 _targetVelocity;
    private bool _grounded;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.01f;

    void OnEnable()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    void Update()
    {
        Vector2 alongSurface = Vector2.Perpendicular(_groundNormal);

        _targetVelocity = alongSurface * _speed;

        velocity += gravity * gravityScale * Time.deltaTime;
        if (velocity < 0)
        {
            velocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!(_connectTime + _connectDelay < Time.time))
                return;

            _connectTime = Time.time;
            velocity = jumpForce;

        }
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
    }

    void FixedUpdate()
    {

        if (_velocity.y > 0)
        {
            _speed = 0;
            _velocity.y = 0;
        }
        
        if (_groundNormal.x != 0)
        {
            _speed = -Mathf.Sign(_groundNormal.x);
        }

        else
        {
            _speed = 0;
        }

        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;
        _grounded = false;
        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
        
    }

    private void jump(Vector2 velocity, bool jump) 
    {
        
    }
    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _rb2d.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    _grounded = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rb2d.position = _rb2d.position + move.normalized * distance;
    }
}