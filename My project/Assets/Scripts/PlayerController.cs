using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f; // Fuerza del salto
    public float rayLength = 0.5f; // Longitud del rayo para detectar el suelo
    public LayerMask groundLayer; // Capa del suelo para detecci�n

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        isGrounded = IsGrounded();

        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        UnityEngine.Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);

        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }

}
