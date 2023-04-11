using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed = 300f;
    public float JumpForce = 50f;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
    }

    private void MovementLogic()
    {
        float moveHorizontal = -Input.GetAxis("Horizontal");

        float moveVertical = -Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        _rb.AddForce(movement * Speed);
    }
    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            _rb.AddForce(Vector3.up * JumpForce);
        }
        if (Input.GetAxis("Jump") < 0)
        {
            _rb.AddForce(Vector3.up * -JumpForce);
        }
    }
}
