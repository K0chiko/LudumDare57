using StarterAssets;
using UnityEngine;

public class RopeToBase : MonoBehaviour
{
    public GameObject player;
    public float riseSpeed = 1f;

    public bool isRising = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.GetComponent<ThirdPersonController>().controlEnabled);
        if (isRising)
        {
            player.GetComponent<ThirdPersonController>().transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<ThirdPersonController>().controlEnabled = false;
        isRising = true;


    }
}
