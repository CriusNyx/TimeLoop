using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Player : TimeBehaviour
{
    new Rigidbody rigidbody;
    new SphereCollider collider;

    public new Camera camera;
    public float mouseSensitivity = 30f;

    private Vector2 mouseDelta;

    private Vector2 mousePosition = Vector2.zero;

    private float hoverDistance = 1.5f;
    private float groundDetectionDistance = 1.6f;

    public const float groundAcceleration = 150f;
    public const float maxVelocity = 15f;

    InputBuffer buffer = new InputBuffer();

    bool groundedLastFrame = false;

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<SphereCollider>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        mouseDelta += new Vector2(mouseX, mouseY) * mouseSensitivity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            buffer.jump = true;
        }
    }

    protected override void ProtectedFixedUpdate()
    {
        PhysicsUpdate();
        UpdateCamera();

        buffer = new InputBuffer();
    }

    private void UpdateCamera()
    {
        mousePosition += mouseDelta;

        mousePosition.y = Mathf.Clamp(mousePosition.y, -90f, 90f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        mouseDelta = Vector2.zero;
    }

    private void PhysicsUpdate()
    {
        if(IsGrounded(out var hit))
        {
            GroundUpdate(hit);
            groundedLastFrame = true;
        }
        else
        {
            AirUpdate();
            groundedLastFrame = false;
        }
    }

    private bool IsGrounded(out RaycastHit hit)
    {
        if (rigidbody.velocity.y > 0f) {
            hit = default;
            return false;
        }

        float distance = groundDetectionDistance;
        if (!groundedLastFrame)
        {
            distance = hoverDistance - 0.5f;
        }

        return Physics.BoxCast(transform.position, Vector3.one * 0.5f, Vector3.down, out hit, Quaternion.identity, distance, LayerMask.GetMask("Default"));
    }

    private void GroundUpdate(RaycastHit hit)
    {
        SnapToGround(hit);
        UpdateVelocty(groundAcceleration, true, false);
        if (buffer.jump)
        {
            Vector3 v = rigidbody.velocity;
            v.y = 10f;
            rigidbody.velocity = v;
        }
    }

    private void SnapToGround(RaycastHit hit)
    {
        Vector3 pos = transform.position;
        pos.y = hit.point.y + hoverDistance;
        transform.position = pos;
    }

    private void UpdateVelocty(float acceleration, bool flatten, bool applyGravity)
    {
        Vector3 velocty = rigidbody.velocity;
        Vector3 flatVelocity = velocty;
        flatVelocity.y = 0f;

        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.z -= 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            input.z += 1f;
        }

        if(input.magnitude > 1f)
        {
            input = input.normalized;
        }

        Quaternion inputOrientation = Quaternion.Euler(0f, mousePosition.x, 0f);

        Vector3 targetVelocty = inputOrientation * input * maxVelocity;

        flatVelocity = Vector3.MoveTowards(flatVelocity, targetVelocty, acceleration * Time.fixedDeltaTime);

        if (!flatten)
        {
            flatVelocity.y = velocty.y;
        }
        if (applyGravity)
        {
            flatVelocity.y += -20f * Time.fixedDeltaTime;
        }

        rigidbody.velocity = flatVelocity;
    }

    private void LateUpdate()
    {
        Quaternion cameraOrientation = Quaternion.Euler(-mousePosition.y, mousePosition.x, 0f);
        camera.transform.rotation = cameraOrientation;
        camera.transform.position = transform.position + cameraOrientation * new Vector3(0f, 1f, -2f);
    }

    private void AirUpdate()
    {
        UpdateVelocty(groundAcceleration, false, true);
    }

    private class InputBuffer
    {
        public bool jump = false;
    }
}