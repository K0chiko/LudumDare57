using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerVFXManager : MonoBehaviour
{
    public ParticleSystem jetPackBubbles;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            EnableEmission(true);
        }
        else
        {
            EnableEmission(false);
        }
    }

    void EnableEmission(bool enable)
    {
        var emission = jetPackBubbles.emission;
        emission.enabled = enable;
    }
}
