using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OxygenController : MonoBehaviour
{

    public static OxygenController Instance { get; private set; }

    [Tooltip("Количество кислорода в баллоне")]
    public float oxygen = 100f;
    [Tooltip("Расход кислорода базовый. Используется даже при отсутствии действий.")]
    public float baseOxygenConsumption = 0.01f;
    [Tooltip("Расход кислорода при ходьбе и прыжках без ранца.")]
    public float activeOxygenConsuption = 1.5f;
    [Tooltip("Расход кислорода при прыжке с ранцем.")]
    public float jetPackOxygenConsuption = 50000.0f;



    [Space(50)]
    public Renderer[] oxygenIndicator;
    [Space(10)]

    public int value = 0;
    public int jetPackModifier;

    [SerializeField] private Gradient oxygenGradient;

    private float oxygenNormalized;
    public float oxygenMax;


    Color currentColor;



    private UIController uiController;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        uiController = GameObject.Find("UI").GetComponent<UIController>();
        oxygenMax = oxygen;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            oxygen -= baseOxygenConsumption * activeOxygenConsuption * Time.deltaTime;
        }
        else
        {
            oxygen -= baseOxygenConsumption * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            oxygen -= baseOxygenConsumption * jetPackOxygenConsuption * Time.deltaTime;

        }

        oxygen = Mathf.Clamp(oxygen, 0f, oxygenMax);
        oxygenNormalized = oxygen / oxygenMax;

        OxygenBarColor();
    }

       
    
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    private void OxygenBarColor()
    {

        currentColor = oxygenGradient.Evaluate(oxygenNormalized);
        float emissionIntensity = 3f;

        foreach (Renderer oxyGen in oxygenIndicator)
        {
            if (oxyGen == null) continue;

            Color emissionColor = currentColor * emissionIntensity;
            oxyGen.material.color = currentColor;
            oxyGen.material.EnableKeyword("_EMISSION");
            oxyGen.material.SetColor("_EmissionColor", emissionColor);
        }
    }

    

}
