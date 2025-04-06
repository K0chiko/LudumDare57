using StarterAssets;
using UnityEngine;
using System.Collections;

public class RopeToBase : MonoBehaviour
{
    public GameObject player;
    public float riseSpeed = 1f;

    public bool isRising = false;

    private GameManager gameManager;
    private BoxCollider boxCollider;

    void Start()
    {
        gameManager = GameObject.Find("Player").GetComponent<GameManager>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        Debug.Log(player.GetComponent<ThirdPersonController>().controlEnabled);
        if (isRising)
        {
            player.GetComponent<ThirdPersonController>().transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Return) && gameManager.isUpgrade)
        {
            player.GetComponent<ThirdPersonController>().transform.Translate(Vector3.up * -riseSpeed * Time.deltaTime);
        }

        if (!gameManager.isUpgrade)
        {
            isRising = false;
            player.GetComponent<ThirdPersonController>().controlEnabled = true;
            StartCoroutine(DisableColliderTemporarily(5f));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        isRising = true;
        player.GetComponent<ThirdPersonController>().controlEnabled = false;
/*
        if (!gameManager.isUpgrade)
        {

        }*/

 
    }

    IEnumerator DisableColliderTemporarily(float duration)
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(duration);
        boxCollider.enabled = true;
    }
}
