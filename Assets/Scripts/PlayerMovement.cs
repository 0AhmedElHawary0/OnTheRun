using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;
    public Animator Animator;
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;
    public Rigidbody rb;

    float horizontalInput;
    public float jumpForce = 400f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool grounded;

    private void FixedUpdate() 
    {
        if (!alive) return;
        movementSpeed += 0.1f * Time.deltaTime;
        Vector3 forwardMove = transform.forward * movementSpeed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * movementSpeed * Time.fixedDeltaTime / 2;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    // Update is called once per frame
    void Update()
    {   
        if (!alive) return;
        grounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        horizontalInput = Input.GetAxis("Horizontal");

        if(transform.position.y > 1f)
        {
            Animator.SetBool("isJumping",true);
            Animator.SetBool("isRunning",false);
        }
        else
        {
            Animator.SetBool("isJumping",false);
            Animator.SetBool("isRunning",true);
        }

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "SpawnTrigger")
        {
            spawnManager.SpawnTriggerEntered();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.tag == "Trap")
        {
            Die();
        }
    }

    private void Die()
    {
        alive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
         
}
