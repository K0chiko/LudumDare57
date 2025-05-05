using StarterAssets;
using UnityEngine;

public class KillzoneCheck : MonoBehaviour
{
    public bool isDead = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Killzone"))
        {
            isDead = true;
        }
    }

    public static KillzoneCheck Instance { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
