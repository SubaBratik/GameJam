using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private TMP_Text _foodText;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius = 0.2f;
    private bool _isGrounded;

    [SerializeField] private GameObject PanelDie;

    private Rigidbody2D _rb;
    private float xInput;
    private bool _isJumping = false;
    private int _foodEated = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GameMaster.instance.SetChechPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (PanelDie.activeSelf)
            return;
        xInput = Input.GetAxis("Horizontal");

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
            Eat(other.gameObject);

        if (other.CompareTag("Spike"))
        {
            Debug.Log("Умер");
            StartCoroutine(Die());
        }
    }

    private void Eat(GameObject foodObject)
    {
        Destroy(foodObject);
        _foodEated++;

        _foodText.text = "Food: " + _foodEated;
    }

    private void Jump()
    {
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
        _rb.bodyType = RigidbodyType2D.Static;
    }

    public IEnumerator Die()
    {
        PanelDie.SetActive(true);
        DisableMovement();

        yield return new WaitForSeconds(1f);

        transform.position = GameMaster.instance.lastCheckPointPos;


        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.linearVelocity = Vector2.zero;
        PanelDie.SetActive(false);
    }
}
