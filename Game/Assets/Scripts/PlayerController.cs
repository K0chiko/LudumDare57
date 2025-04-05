using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Vector3 moveVector;
    Quaternion lastDirection;
    private Vector3 horizontalInput;
    public GameObject Rope;

    public float gravityModifier;
    private bool isOnGround = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      

    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bell"))
        {
            Debug.Log(" asdasd ");
            Rope.GetComponent<RopeToBase>().isRising = false;

        }
    }
}


