using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float oxygen;
    public Renderer[] oxygenIndicator;

    [SerializeField] private Gradient oxygenGradient;

    Color currentColor;
    void Start()
    {
        oxygen = 1f;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            oxygen -= 0.0001f;
            oxygen = Mathf.Clamp01(oxygen);
        }
        
        oxygenBarColor();


    }

  /*  private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other);
        }
    }*/
    private void oxygenBarColor()
    {

        currentColor = oxygenGradient.Evaluate(oxygen);


        foreach (Renderer oxyGen in oxygenIndicator)
        {
            if (oxyGen == null) continue;

            oxyGen.material.color = currentColor;
            oxyGen.material.EnableKeyword("_EMISSION");
            oxyGen.material.SetColor("_EmissionColor", currentColor);
        }
    }
}
