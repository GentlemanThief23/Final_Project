using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    private CapsuleCollider _col;

    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;

    private float _vInput;
    private float _hInput;

    private Rigidbody _rb;

    
    public float JumpVelocity = 5f;
    private bool _isJumping;

    



    void Start()
    {
        
        _rb = GetComponent<Rigidbody>();

        _col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _isJumping = true;
        }

        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        /*
        this.transform.Translate(Vector3.forward * _vInput *
        Time.deltaTime);
        
        this.transform.Rotate(Vector3.up * _hInput *
        Time.deltaTime);
        */
    }

    void FixedUpdate()
    {
        if (IsGrounded() && _isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity,
            ForceMode.Impulse);
        }

        if (_isJumping)
        {
            // 4
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        // 5
        _isJumping = false;

        // 2
        Vector3 rotation = Vector3.up * _hInput;
        // 3
        Quaternion angleRot = Quaternion.Euler(rotation *
        Time.fixedDeltaTime);
        // 4
        _rb.MovePosition(this.transform.position +
        this.transform.forward * _vInput * Time.fixedDeltaTime);
        // 5
        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    private bool IsGrounded()
    {
        // 7
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
        _col.bounds.min.y, _col.bounds.center.z);

        // 8
        bool grounded = Physics.CheckCapsule(_col.bounds.center,
        capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);

        // 9
        return grounded;
    }
}
