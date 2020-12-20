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

    public Vector2 mousePosition { get; private set; } = Vector2.zero;

    public const float hoverDistance = 1.0f;
    public const float groundDetectionDistance = 1.1f;

    public const float groundAcceleration = 150f;
    public const float airAcceleration = 20f;
    public const float maxVelocity = 15f;
    public const float gravityAcceleration = 20f;
    public const float airDashSpeed = 100f;

    public const float jumpSpeed = 20f;

    public const int maxAirDashes = 2;

    PlayerState state;
    public PlayerBehaviour behaviour = new WalkingBehaviour();

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<SphereCollider>();

        this.state = new PlayerState(this, rigidbody);
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
            state.buffer.jump = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            state.buffer.grappelHook = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state.buffer.airDashPressed = true;
        }
    }

    protected override void ProtectedFixedUpdate()
    {
        state.velocity = rigidbody.velocity;

        behaviour.Update(state);
        UpdateCamera();

        rigidbody.velocity = state.velocity;

        state.buffer = new InputBuffer();
    }

    private void UpdateCamera()
    {
        Vector2 mousePos = mousePosition;

        mousePos += mouseDelta;

        mousePos.y = Mathf.Clamp(mousePos.y, -90f, 90f);

        mousePosition = mousePos;

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

    private void LateUpdate()
    {
        Quaternion cameraOrientation = Quaternion.Euler(-mousePosition.y, mousePosition.x, 0f);
        camera.transform.rotation = cameraOrientation;
        camera.transform.position = transform.position + cameraOrientation * new Vector3(0f, 1f, -2f);
    }
}
