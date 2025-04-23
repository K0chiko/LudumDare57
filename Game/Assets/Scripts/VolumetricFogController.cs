using UnityEngine;

public class VolumetricFogController : MonoBehaviour
{
    [Header("Target Material (instance)")]
    public Material volumetricFogMaterial;

    [Header("Noise Tiling")]
    public float minTiling = 0.1f;
    public float maxTiling = 3.0f;
    public float tilingSpeed = 1.0f;

    [Header("Density Threshold")]
    public float minThreshold = 0.0f;
    public float maxThreshold = 0.5f;
    public float thresholdSpeed = 0.5f;

    private float tilingTime;
    private float thresholdTime;

    void Update()
    {
        if (volumetricFogMaterial == null) return;


        tilingTime += Time.deltaTime * tilingSpeed;
        thresholdTime += Time.deltaTime * thresholdSpeed;

        float tilingValue = Mathf.Lerp(minTiling, maxTiling, (Mathf.Sin(tilingTime) + 1) * 0.5f);
        float thresholdValue = Mathf.Lerp(minThreshold, maxThreshold, (Mathf.Sin(thresholdTime + Mathf.PI / 2) + 1) * 0.5f);

        volumetricFogMaterial.SetFloat("_NoiseTiling", tilingValue);
        volumetricFogMaterial.SetFloat("_DensityThreshold", thresholdValue);
    }
}
