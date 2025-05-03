using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public struct FloatRange
{
    [Range(0f, 10f)]
    public float min;

    [Range(0f, 10f)]
    public float max;
}
public class OxygenEffects : MonoBehaviour
{
    private GameManager gameManager; 
    public Volume globalVolume;
    private Material _material;

    public float hue;
    //public float saturation;
    //public float value;

    private float _valueFrequency;
    private float _valueAmplitude;

    private Vignette vignette;

    public FloatRange saturation = new FloatRange { min = 0.9f, max = 1.15f };
    public FloatRange value = new FloatRange { min = 0.2f, max = 0.5f };
    public FloatRange amplitude = new FloatRange { min = 0.1f, max = 0.2f };
    public FloatRange vignetteRadius = new FloatRange { min = 2.0f, max = 3.5f };
    public FloatRange frequency = new FloatRange { min = 2f, max = 5f };

    void Start()
    {
        _material = GetComponent<Renderer>().sharedMaterial;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        /*     if (globalVolume.profile.TryGet(out vignette))
   {
       vignette.active = true;
   }
   else
   {
       Debug.LogWarning("Виньетка не найдена в Global Volume!");
   }*/
    }

    void Update()
    {
       //if (vignette == null) return;


        //float targetIntensity = Mathf.Lerp(maxIntensity, minIntensity, oxygenNormalized);
        /*   vignette.intensity.Override(targetIntensity);*/
        HSV();
    }

    public void HSV()
    {

        Color originalColor = _material.GetColor("_Color");

        Color.RGBToHSV(originalColor, out float h, out float s, out float v);

        float pulse = Mathf.Sin(Time.time * _valueFrequency) * _valueAmplitude;
        float oxygenNormalized = gameManager.oxygen / 100f;
        //float oxygenNormalized = 1;

        h = hue;
        s = Mathf.Lerp(saturation.max, saturation.min, oxygenNormalized);
        if   (s > 0.95f && s < 1.01f) s = 1.02f; // when s = 1 effect is invisible
        if   (oxygenNormalized > 0.8) v = 0;
        else v = Mathf.Lerp(value.max, value.min, oxygenNormalized) + pulse;

        Color newColor = Color.HSVToRGB(h, s, v);

        _material.SetColor("_Color", newColor);

        _valueAmplitude = Mathf.Lerp(amplitude.min, amplitude.max, oxygenNormalized);
        _valueFrequency = Mathf.Lerp(frequency.max, frequency.min, oxygenNormalized);
        _material.SetFloat("_VignetteRadius", Mathf.Lerp(vignetteRadius.min, vignetteRadius.max, oxygenNormalized)) ;
        Debug.Log(s);
    }
}
