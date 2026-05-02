using UnityEngine;

public class Tramploline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _boostForce = 9f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

                    if (rb != null)
                    {
                        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                        rb.AddForce(Vector2.up * _boostForce, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }
}
