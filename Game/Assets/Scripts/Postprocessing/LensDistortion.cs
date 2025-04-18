using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WaterDistortionEffect : MonoBehaviour
{
    public Volume globalVolume;

    private LensDistortion lensDistortion;

    [Header("��������� ���������")]
    public float baseDistortion = -0.2f;
    public float distortionAmplitude = 0.05f;
    public float distortionFrequency = 1f;

    [Header("�������� ������ ���������")]
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
            Debug.LogWarning("Lens Distortion �� ������ � Global Volume.");
        }
    }

    void Update()
    {
        if (lensDistortion == null) return;

        // ��������� ���������
        float pulse = Mathf.Sin(Time.time * distortionFrequency) * distortionAmplitude;
        lensDistortion.intensity.Override(baseDistortion + pulse);

        // �������� ������ (��� ��� �����)
        float x = 0.5f + Mathf.Sin(Time.time * centerShakeSpeed) * centerShakeAmount;
        float y = 0.5f + Mathf.Cos(Time.time * centerShakeSpeed) * centerShakeAmount;
        lensDistortion.center.Override(new Vector2(x, y));
    }
}

