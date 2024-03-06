using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    private int jumpCount = 0; 
    private int maxJump = 2;
    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;
    
    private int platformCount = 0;
    public TextMeshProUGUI scoreText;

    public int collectiblesCount = 0;
    public TextMeshProUGUI collectiblesText;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && (jumpCount < maxJump))
        {
            rb.velocity = Vector3.up * jumpForce;
            jumpCount++;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            platformCount++;
            scoreText.text = "Score: " + platformCount;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    public void AddCollectible()
    {
        collectiblesCount++;
        if (collectiblesText != null)
        {
            collectiblesText.text = "Collectibles: " + collectiblesCount;
        }
    }
}
