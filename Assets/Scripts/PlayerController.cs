using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using TMPro;
using UnityEngine.SceneManagement;

public class Player1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private TMP_Text _foodText;

    private Rigidbody2D _rb;
    private float xInput;
    private bool _isJumping = false;
    private int _foodEated = 0;
    private bool _canJump = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
            _isJumping = true;
    }

    private void FixedUpdate()
    {
        Move();
        if (_isJumping)
        {
            Jump();
            _isJumping = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    if (contact.normal.y > 0.5f)
                    {
                        _canJump = true;
                        _isJumping = false;
                        return;
                    }
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
            Eat(other.gameObject);

        if (other.CompareTag("Spike"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Eat(GameObject foodObject)
    {
        Destroy(foodObject);
        _foodEated++;

        _foodText.text = "Food: " + _foodEated;
    }

    private void Jump()
    {
        _canJump = false;
        _isJumping = true;
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
    }

    private void Move()
    {
        _rb.linearVelocity = new Vector2(xInput * _speed, _rb.linearVelocity.y);
    }


    public void DisableMovement()
    {
        _rb.linearVelocity = Vector2.zero;
        this.enabled = false; //выключаем скрипт чтобы кнопки не работали
    }
}
