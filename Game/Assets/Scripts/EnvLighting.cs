using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnvLighting : MonoBehaviour
{
    public Transform player;
    public Volume volume;

    [Header("Ambient Light")]
    public Color topColor = Color.white;
    public Color bottomColor = Color.black;

    [Header("Vertical Range")]
    public float topY = 10f;
    public float bottomY = -10f;

    [Header("Directional Light")]
    public Light sun;
    public float topLight = 1f;
    public float bottomLight = 0.1f;
    public float sunFallof = 1.5f;

    [Header("Lift Start (Top) / End (Bottom)")]
    public Vector3 liftTop = Vector3.one;
    public Vector3 liftBottom = Vector3.zero;

    [Header("Gamma Start (Top) / End (Bottom)")]
    public Vector3 gammaTop = Vector3.one;
    public Vector3 gammaBottom = Vector3.zero;

    [Header("Gain Start (Top) / End (Bottom)")]
    public Vector3 gainTop = Vector3.one;
    public Vector3 gainBottom = Vector3.zero;

    private float tSmoothed = 0f;
    public float smoothSpeed = 1f; // больше Ч быстрее, меньше Ч плавнее

    private LiftGammaGain liftGammaGain;

    void Start()
    {
        if (!volume.profile.TryGet(out liftGammaGain))
        {
            Debug.LogError("LiftGammaGain не найден в профиле Volume.");
        }
    }

    void Update()
    {

        float targetT = Mathf.InverseLerp(topY, bottomY, player.position.y);
        tSmoothed = Mathf.Lerp(tSmoothed, targetT, Time.deltaTime * smoothSpeed);
        sun.intensity = Mathf.Lerp(topLight, bottomLight, targetT * sunFallof);

        // Ambient Light
        RenderSettings.ambientLight = Color.Lerp(topColor, bottomColor, tSmoothed);

        // Lift/Gamma/Gain интерпол€ци€
        Vector3 lift = Vector3.Lerp(liftTop, liftBottom, tSmoothed);
        Vector3 gamma = Vector3.Lerp(gammaTop, gammaBottom, tSmoothed);
        Vector3 gain = Vector3.Lerp(gainTop, gainBottom, tSmoothed);

        // ѕрименение
        liftGammaGain.lift.value = new Vector4(lift.x, lift.y, lift.z, 0f);
        liftGammaGain.gamma.value = new Vector4(gamma.x, gamma.y, gamma.z, 0f);
        liftGammaGain.gain.value = new Vector4(gain.x, gain.y, gain.z, 0f);
    }
}