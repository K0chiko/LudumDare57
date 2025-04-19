using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

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


    Color currentColor;

    public AudioSource uiSourceSFX;
    public AudioClip[] uiAudio;


    private int currentClipIndex = 0;

    private UIController uiController;

    void Start()
    {
        uiController = GameObject.Find("UI").GetComponent<UIController>();
        oxygenMax = oxygen;


        rope = GameObject.Find("Rope");

        uiSourceSFX.clip = uiAudio[currentClipIndex];
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

        if (oxygen <= 0)
        {
            RestartLevel();
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Killzone"))
        {
            RestartLevel();
        }

/*        if (other.gameObject.CompareTag("Bell"))
        {
            Debug.Log(" asdasd ");
            //rope.GetComponent<RopeToBase>().isRising = false;
            saleWindow.SetActive(true);
            upgradeManager.isUpgrade = true;
            oxygen = oxygenMax;
        }*/
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
