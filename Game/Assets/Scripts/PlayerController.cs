using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Vector3 moveVector;
    Quaternion lastDirection;
    private Vector3 horizontalInput;
    private Rigidbody playerRB;

    public float gravityModifier;
    private bool isOnGround = true;
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    private void MovePlayer()
    {
        horizontalInput = new(Input.GetAxis("Horizontal"), 0f, 0f);
        Vector3 moveVector = horizontalInput * speed * Time.deltaTime;


       

        if (horizontalInput.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 0.1f);
            lastDirection = transform.rotation;
        }
        else if(horizontalInput.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 180f, 0f), 0.1f);
            lastDirection = transform.rotation;
        }
        else
        {
            transform.rotation = lastDirection;
        }
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVector), 0.1f);
            playerRB.linearVelocity = new(moveVector.x, 0f, 0f);
    }
}


