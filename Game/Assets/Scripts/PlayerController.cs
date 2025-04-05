using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Button oxygenButton;
    public float jumpForce;
    private Vector3 moveVector;
    Quaternion lastDirection;
    public GameObject SaleWindow;
    public GameObject Rope;

    public float gravityModifier;
    private bool isOnGround = true;
    void Start()
    {
        oxygenButton.onClick.AddListener(oxygenButtonResolve);
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
            SaleWindow.SetActive(true);
        }
    }

    private void oxygenButtonResolve()
    {
        SaleWindow.SetActive(false);
    }

    
}


