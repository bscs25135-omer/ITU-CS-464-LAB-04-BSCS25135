using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1.5f;       // in world units
    [SerializeField] private float groundCheckExtra = 0.15f; // tolerance below the feet

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private Vector2 moveInput;
    private bool jumpRequested;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        moveInput = Vector2.zero;
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.wKey.isPressed || kb.upArrowKey.isPressed)    moveInput.y += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed)  moveInput.y -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) moveInput.x += 1f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)  moveInput.x -= 1f;
        moveInput = Vector2.ClampMagnitude(moveInput, 1f);

        // wasPressedThisFrame must be read in Update, so we store it for FixedUpdate
        if (kb.spaceKey.wasPressedThisFrame)
            jumpRequested = true;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * moveSpeed;
        velocity.z = moveInput.y * moveSpeed;

        if (jumpRequested && IsGrounded())
        {
            // v = sqrt(2 * g * h) gives exactly the chosen jump height
            velocity.y = Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        }
        jumpRequested = false;

        rb.linearVelocity = velocity;
    }

    private bool IsGrounded()
    {
        // Distance from the capsule centre down to its feet
        float feetDistance = capsule.height * 0.5f * transform.lossyScale.y;
        Vector3 origin = transform.position + capsule.center;
        return Physics.Raycast(origin, Vector3.down, feetDistance + groundCheckExtra,
                               ~0, QueryTriggerInteraction.Ignore);
    }
}