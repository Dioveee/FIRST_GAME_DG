using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float rayLength = 0.7f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    public bool isGrounded;
    private Animator animator;
    private bool facingRight = true;

    public TextMeshProUGUI objectCounterText;

   private Dictionary<string, int> collectedObjects = new Dictionary<string, int>()
    {
        { "Escudo", 0 },
        { "Diamante", 0 },
        { "Moneda", 0 }
    };

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UpdateObjectCounterUI();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        isGrounded = IsGrounded();

        animator.SetBool("IsJumping", !isGrounded);
        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);
        animator.SetFloat("HorizontalVelocity", rb.linearVelocity.x);
        animator.SetFloat("Speed", isGrounded ? Mathf.Abs(moveInput) : 0f);


        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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

    

void OnTriggerEnter2D(Collider2D collision)
{
    if (!collision.CompareTag("Collectible"))
        return;

    string objectName = collision.gameObject.name;

    Debug.Log("Nombre detectado: [" + objectName + "]");

    foreach (var key in collectedObjects.Keys)
    {
        Debug.Log("Clave diccionario: [" + key + "]");
    }

    if (collectedObjects.ContainsKey(objectName))
    {
        collectedObjects[objectName]++;
        Debug.Log("SÍ encontró la clave y sumó");
        UpdateObjectCounterUI();
    }
    else
    {
        Debug.Log("NO encontró la clave");
    }

    Destroy(collision.gameObject);
}


  void UpdateObjectCounterUI()
    {
        objectCounterText.text = $"Escudos: {collectedObjects["Escudo"]} | " +
                                 $"Diamantes: {collectedObjects["Diamante"]} | " +
                                 $"Monedas: {collectedObjects["Moneda"]}";
    }

  void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡Has muerto!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDrawGizmos()
    { 
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }

}
