using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform posA, posB;
    [SerializeField] private float speed = 3f;

    private Vector3 _targetPos;
    private Vector3 _worldPosA;
    private Vector3 _worldPosB;

    void Start()
    {
        _worldPosA = posA.position;
        _worldPosB = posB.position;
        _targetPos = _worldPosB;
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, _targetPos) < 0.1f)
        {
            _targetPos = (_targetPos == _worldPosA) ? _worldPosB : _worldPosA;
        }

        transform.position = Vector2.MoveTowards(transform.position, _targetPos, speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    collision.transform.SetParent(this.transform);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}