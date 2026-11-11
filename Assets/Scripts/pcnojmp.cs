// using UnityEngine;
// using UnityEngine.InputSystem;

// [RequireComponent(typeof(Rigidbody))]
// public class PlayerController : MonoBehaviour
// {
//     [SerializeField] private float moveSpeed = 6f;

//     private Rigidbody rb;
//     private Vector2 moveInput;

//     private void Awake()
//     {
//         rb = GetComponent<Rigidbody>();
//         rb.freezeRotation = true;
//     }

//     private void Update()
//     {
//         moveInput = Vector2.zero;
//         Keyboard kb = Keyboard.current;
//         if (kb == null) return;

//         if (kb.wKey.isPressed || kb.upArrowKey.isPressed)    moveInput.y += 1f;
//         if (kb.sKey.isPressed || kb.downArrowKey.isPressed)  moveInput.y -= 1f;
//         if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) moveInput.x += 1f;
//         if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)  moveInput.x -= 1f;

//         moveInput = Vector2.ClampMagnitude(moveInput, 1f);
//     }

//     private void FixedUpdate()
//     {
//         Vector3 velocity = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed;
//         rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
//     }
// }