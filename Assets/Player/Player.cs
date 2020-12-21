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
    public const float grappleHookDistance = 70f;

    public const float grappleHookTimeout = 2f;
    public const float grappleHookCooldown = 2f;

    public const float jumpSpeed = 20f;
    public const float highJumpSpeed = 40f;

    public const int maxAirDashes = 2;

    public PlayerState state;
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
        rigidbody.freezeRotation = true;
        GetComponent<Inventory>().AddItem(new GrappleItem());
    }

    private void Update()
    {
        gameObject.transform.Find("Player Model/Char Base Unity 1/Gun").GetComponent<SkinnedMeshRenderer>().enabled = PlayerPowerupState.hasGrappleUnlocked;
        gameObject.transform.Find("Player Model/Char Base Unity 1/Booster").GetComponent<SkinnedMeshRenderer>().enabled = PlayerPowerupState.hasSuperJump;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        mouseDelta += new Vector2(mouseX, mouseY) * mouseSensitivity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            state.buffer.jump = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Inventory>().UseSelectedItem();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state.buffer.airDashPressed = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            state.buffer.superJumpPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            state.buffer.superJumpReleased = true;
        }
    }

    protected override void ProtectedFixedUpdate()
    {
        state.velocity = rigidbody.velocity;

        behaviour.Update(state);
        UpdateCamera();

        rigidbody.velocity = state.velocity;

        Vector3 flatVelocity = state.velocity;

        if (flatVelocity.magnitude > 0.1f)
        {
            flatVelocity.y = 0f;
            flatVelocity = Vector3.Normalize(flatVelocity);
            float theta = -Mathf.Atan2(flatVelocity.z, flatVelocity.x) * Mathf.Rad2Deg + 90f;
            Quaternion rot = Quaternion.Euler(0f, theta, 0f);
            gameObject.transform.Find("Player Model").transform.rotation = rot;
        }

        state.buffer = new InputBuffer(state.buffer);
    }

    private void UpdateCamera()
    {
        Vector2 mousePos = mousePosition;

        mousePos += mouseDelta;

        mousePos.y = Mathf.Clamp(mousePos.y, -90f, 90f);

        mousePosition = mousePos;

        mouseDelta = Vector2.zero;
    }

    private void LateUpdate()
    {
        Quaternion cameraOrientation = Quaternion.Euler(-mousePosition.y, mousePosition.x, 0f);
        camera.transform.rotation = cameraOrientation;
        camera.transform.position = transform.position + cameraOrientation * new Vector3(0f, 1f, -5f);
    }
}
