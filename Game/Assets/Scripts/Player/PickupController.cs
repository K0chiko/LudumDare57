using UnityEngine;

public class PickupController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] private Transform _holdArea;
    private GameObject _heldObj;
    private Rigidbody _heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float _pickupRange = 5f;
    [SerializeField] private float _pickupForce = 150f;
    [SerializeField] private float _rotationSmoothSpeed = 5f;

    void Start()
    {
        
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }

            }
            else
            {
                DropObject();
            }

        }
        if (_heldObj != null)
        {
            MoveObject();
        }

    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            _heldObjRB = pickObj.GetComponent<Rigidbody>();
            _heldObjRB.useGravity = false;
            _heldObjRB.linearDamping = 10;
            _heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            _heldObjRB.transform.parent = _holdArea;

            _heldObj = pickObj;
        }
    }

    void DropObject()
    {
        _heldObjRB.useGravity = true;
        _heldObjRB.linearDamping = 1;
        _heldObjRB.constraints = RigidbodyConstraints.None;

        _heldObjRB.transform.parent = null;
        _heldObj = null;
    }

    void MoveObject()
    {
        Vector3 directionToHold = _holdArea.position - _heldObj.transform.position;

        _heldObjRB.linearVelocity = directionToHold * _pickupForce * Time.deltaTime;

        Quaternion targetRotation = Quaternion.identity;
        _heldObjRB.transform.localRotation = Quaternion.Slerp(
            _heldObjRB.transform.localRotation,
            targetRotation,
            Time.deltaTime * _rotationSmoothSpeed);
    }
}
