using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class WobbleStone : MonoBehaviour
{
    [Tooltip("Сила дрожания камня")]
    public float wobbleStrength = 0.01f;

    [Tooltip("Задержка перед снятием ограничений")]
    public float unfreezeDelay = 2.5f;

    private Rigidbody rb;
    private AudioSource audioSource;
    private bool triggered = false;
    private Vector3 originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        originalPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (triggered) return;
        if (!collision.collider.CompareTag("Player")) return;

        triggered = true;
        audioSource.Play();

        StartCoroutine(Wobble());
        Invoke(nameof(UnfreezeRigidbody), unfreezeDelay);
    }

    private System.Collections.IEnumerator Wobble()
    {
        yield return new WaitForSeconds(1f);
        float timer = 0f;
        float duration = unfreezeDelay;

        while (timer < duration)
        {
            float offsetX = Random.Range(-wobbleStrength, wobbleStrength);
            float offsetY = Random.Range(-wobbleStrength, wobbleStrength);
            float offsetZ = Random.Range(-wobbleStrength, wobbleStrength);
            transform.position = originalPosition + new Vector3(offsetX, offsetY, offsetZ);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }

    private void UnfreezeRigidbody()
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}
