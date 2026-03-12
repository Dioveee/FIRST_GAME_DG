using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 1f;
    private Vector3 startingPosition;
    public Vector3 rightPosition;
    public Vector3 leftPosition;
    public Vector3 currentPosition;
    public float currentDistance;

    private bool movingRight = true;

    void Start()
    {
        currentPosition = transform.position;
        startingPosition = transform.position;
        rightPosition = startingPosition + Vector3.right * distance;
        leftPosition = startingPosition + Vector3.left * distance;
    }

    void Update()
    {
        currentDistance = Vector2.Distance(transform.position, startingPosition);
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, startingPosition) >= distance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, startingPosition) >= distance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
