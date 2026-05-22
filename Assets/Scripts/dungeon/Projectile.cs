using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ElementType elementType;
    private Vector2 direction;
    private float speed = 15f;
    private float lifetime = 5f;
    private Rigidbody2D rb;

    public void Launch(Vector2 dir, ElementType element)
    {
        direction = dir.normalized;
        elementType = element;

        rb = GetComponent<Rigidbody2D>();

        // Set velocity to make it move
        rb.linearVelocity = direction * speed;
        rb.gravityScale = 0; // No gravity

        Debug.Log($"Projectile launched: {element} in direction {direction}");

        // Rotate to face direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Destroy after 5 seconds
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monument monument = collision.GetComponent<Monument>();
        if (monument != null)
        {
            Debug.Log($"Hit monument! Casting {elementType}");
            monument.Activate(elementType);
            Destroy(gameObject);
        }
    }
}