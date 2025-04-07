using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("Количество кислорода в баллоне")]
    public float oxygen = 100f;
    [Tooltip("Расход кислорода базовый. Используется даже при отсутствии действий.")]
    public float baseOxygenConsumption = 0.01f;
    [Tooltip("Расход кислорода при ходьбе и прыжках без ранца.")]
    public float activeOxygenConsuption = 1.5f;
    [Tooltip("Расход кислорода при прыжке с ранцем.")]
    public float jetPackOxygenConsuption = 50000.0f;

    [Space(10)]
    [Tooltip("Стоимость прокачки балона.")]
    public int valueToOxygenTank;
    [Tooltip("Стоимость прокачки ранца.")]
    public int valueToJetPackConsumption;
    [Tooltip("Число, на которое увеличивается балон при прокачке.")]
    public int oxygenIncrement;
    [Tooltip("Число, на которое уменьшается расход кислорода ранца при прокачке.")]
    public int jetPackDecrement;

    [Space(50)]
    public Renderer[] oxygenIndicator;
    [Space(10)]
    public GameObject saleWindow;
    private GameObject rope;
    public int value = 0;
    public int jetPackModifier;

    [SerializeField] private Gradient oxygenGradient;

    private float oxygenNormalized;
    public float oxygenMax;

    public bool isUpgrade = true;
    Color currentColor;

    private TextMeshProUGUI textValue;


    void Start()
    {
        //oxygen = 100f;
        oxygenMax = oxygen;
        textValue = GameObject.Find("Value").GetComponent<TextMeshProUGUI>();
        textValue.text = "Value: " + value;

        rope = GameObject.Find("Rope");
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
        Debug.Log("Нормализованный кислород: " + oxygenNormalized);
        Debug.Log("Кислород: " + oxygen);
        oxygenBarColor();
        UpgradeWindows();

        if (oxygen <= 0)
        {
            RestartLevel();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Pickup pickup = other.GetComponent<Pickup>();
            if (pickup != null)
            {
                value += pickup.pickupValue;
                textValue.text = "Value: " + value;
            }
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Killzone"))
        {
            RestartLevel();
        }

        if (other.gameObject.CompareTag("Bell"))
        {
            Debug.Log(" asdasd ");
            //rope.GetComponent<RopeToBase>().isRising = false;
            saleWindow.SetActive(true);
            isUpgrade = true;
            oxygen = oxygenMax;
        }
     }
       
    

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    private void oxygenBarColor()
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

    private void UpgradeWindows()
    {
        /*        if (Input.GetKeyDown(KeyCode.Alpha1) && isUpgrade)
                {
                    value -= valueToOxygenTank;
                    oxygen += oxygenIncrement;
                    textValue.text = "Value: " + value;
                }*/

        if (Input.GetKeyDown(KeyCode.Alpha1) && isUpgrade)
        {
            if (value >= valueToOxygenTank)
            {
                value -= valueToOxygenTank;
                oxygenMax += oxygenIncrement;
                oxygen += oxygenIncrement;
                oxygen = Mathf.Clamp(oxygen, 0f, oxygenMax);
                textValue.text = "Value: " + value;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && isUpgrade)
        {
            value -= valueToJetPackConsumption;
            jetPackOxygenConsuption -= jetPackDecrement;
            textValue.text = "Value: " + value;
        }

        if (Input.GetKeyDown(KeyCode.Return) && isUpgrade)
        {
            saleWindow.SetActive(false);
            isUpgrade = false;
        }
    }

}
