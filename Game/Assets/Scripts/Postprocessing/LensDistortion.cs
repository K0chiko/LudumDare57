using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WaterDistortionEffect : MonoBehaviour
{
    public Volume globalVolume;

    private LensDistortion lensDistortion;

    [Header("Настройки искажения")]
    public float baseDistortion = -0.2f;
    public float distortionAmplitude = 0.05f;
    public float distortionFrequency = 1f;

    [Header("Движение центра искажения")]
    public float centerShakeAmount = 0.02f;
    public float centerShakeSpeed = 1f;

    void Start()
    {
        if (globalVolume.profile.TryGet(out lensDistortion))
        {
            lensDistortion.active = true;
        }
        else
        {
            Debug.LogWarning("Lens Distortion не найден в Global Volume.");
        }
    }

    void Update()
    {
        if (lensDistortion == null) return;

        // Пульсация искажения
        float pulse = Mathf.Sin(Time.time * distortionFrequency) * distortionAmplitude;
        lensDistortion.intensity.Override(baseDistortion + pulse);

        // Движение центра (как под водой)
        float x = 0.5f + Mathf.Sin(Time.time * centerShakeSpeed) * centerShakeAmount;
        float y = 0.5f + Mathf.Cos(Time.time * centerShakeSpeed) * centerShakeAmount;
        lensDistortion.center.Override(new Vector2(x, y));
    }
}

