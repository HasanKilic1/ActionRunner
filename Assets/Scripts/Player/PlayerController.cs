using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    Vector2 movement;
    [SerializeField] private float movementSpeed;
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [SerializeField] float zMin;
    [SerializeField] float zMax;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            MovePlayer();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition = currentPosition + moveDirection * (Time.fixedDeltaTime * movementSpeed);
        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.z = Mathf.Clamp(newPosition.z, zMin, zMax);
        rb.MovePosition(newPosition);
    }
}
