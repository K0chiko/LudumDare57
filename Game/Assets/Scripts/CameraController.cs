using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Vector3 cameraPosition;
    public Vector3 cameraAdjustment;
    private Quaternion cameraRotation;
    public float rotateX;
    public float rotateY;
    public float rotateZ;
    void Start()
    {
        player = GameObject.Find("Player");
        cameraAdjustment = new(0f, 1.7f, -12f);
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = player.transform.position + cameraAdjustment;
        transform.position = new(cameraPosition.x, cameraPosition.y, cameraPosition.z / 3);
        cameraRotation = Quaternion.Euler(rotateX, rotateY, rotateZ);
        transform.rotation = cameraRotation;
    }
}
