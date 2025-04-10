using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OxygenEffects : MonoBehaviour
{
    private GameManager gameManager; 
    public Volume globalVolume;

    private Vignette vignette;

    [Range(0f, 1f)]
    public float minIntensity = 0.2f;
    public float maxIntensity = 0.6f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (globalVolume.profile.TryGet(out vignette))
        {
            vignette.active = true;
        }
        else
        {
            Debug.LogWarning("Виньетка не найдена в Global Volume!");
        }
    }

    void Update()
    {
        if (vignette == null) return;

        float oxygenNormalized = gameManager.oxygen / 100f;
        float targetIntensity = Mathf.Lerp(maxIntensity, minIntensity, oxygenNormalized);
        vignette.intensity.Override(targetIntensity);
    }
}
