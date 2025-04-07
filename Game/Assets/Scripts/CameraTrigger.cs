using UnityEngine;

public class CameraTrigger : MonoBehaviour
{

    public float targetZ = -40f;           // куда хотим сместить Z
    public float transitionSpeed = 5f;     // скорость перехода

    private CameraController cameraController;
    private float originalZ;
    private bool inZone = false;

    void Start()
    {
        GameObject cameraObj = Camera.main.gameObject;
        cameraController = cameraObj.GetComponent<CameraController>();
        originalZ = cameraController.cameraAdjustment.z;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = true;
            StopAllCoroutines();
            StartCoroutine(ChangeZSmooth(cameraController.cameraAdjustment.z, targetZ));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = false;
            StopAllCoroutines();
            StartCoroutine(ChangeZSmooth(cameraController.cameraAdjustment.z, originalZ));
        }
    }

    System.Collections.IEnumerator ChangeZSmooth(float fromZ, float toZ)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * transitionSpeed;
            float newZ = Mathf.Lerp(fromZ, toZ, t);
            var adjustment = cameraController.cameraAdjustment;
            adjustment.z = newZ;
            cameraController.cameraAdjustment = adjustment;
            yield return null;
        }
    }
}

