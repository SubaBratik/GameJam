using UnityEditor.Tilemaps;
using UnityEngine;

public class BugEnemy : MonoBehaviour
{
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] private float bounceForce = 10f;
    public float speed = 3f;

    private Vector3 _target;
    private Vector3 _worldPosA;
    private Vector3 _worldPosB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _worldPosA = pointA.position;
        _worldPosB = pointB.position;
        _target = _worldPosB;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target) < 0.1f)
        {
            _target = (_target == _worldPosA) ? _worldPosB : _worldPosA;    
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();
            bool jumpedTop = false;

            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    jumpedTop = true;

                    Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                    pushDirection += Vector2.up * 0.5f;

                    player.AddForce(pushDirection * bounceForce, ForceMode2D.Impulse);
                    Destroy(gameObject);
                    break;
                }
            }

            if (!jumpedTop)
            {
                PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
                controller.StartCoroutine(controller.Die());
            }

        }
    }
}
