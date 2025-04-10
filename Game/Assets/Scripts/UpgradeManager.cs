using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UpgradeManager : MonoBehaviour
{
    public bool isUpgrade = true;
    private GameManager gameManager;
    private UIController uiController;

    [Space(10)]
    [Tooltip("Стоимость прокачки балона.")]
    public int valueToOxygenTank;
    [Tooltip("Стоимость прокачки ранца.")]
    public int valueToJetPackConsumption;
    [Tooltip("Число, на которое увеличивается балон при прокачке.")]
    public int oxygenIncrement;
    [Tooltip("Число, на которое уменьшается расход кислорода ранца при прокачке.")]
    public int jetPackDecrement;

    [Tooltip("Стоимость прокачки кислородного баллона.")]
    public int oxygenUpgradeUpcost = 20;
    [Tooltip("Стоимость прокачки реактивного ранца")]
    public int jetPackUpgradeUpcost = 20;

    [SerializeField] private TextMeshProUGUI upgradeInfoText;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiController = GameObject.Find("UI").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpgradeWindows();

        if (isUpgrade)
        {
            UpgradeSaleWindow();
        }
    }

    private void UpgradeWindows()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && isUpgrade)
        {
            if (gameManager.value >= valueToOxygenTank)
            {
                gameManager.value -= valueToOxygenTank;
                valueToOxygenTank += oxygenUpgradeUpcost;
                gameManager.oxygenMax += oxygenIncrement;
                gameManager.oxygen += oxygenIncrement;
                gameManager.oxygen = Mathf.Clamp(gameManager.oxygen, 0f, gameManager.oxygenMax);
                uiController.textValue.text = "Value: " + gameManager.value;

                gameManager.uiSourceSFX.clip = gameManager.uiAudio[1];
                gameManager.uiSourceSFX.Play();
            }
            else
            {
                gameManager.uiSourceSFX.clip = gameManager.uiAudio[0];
                gameManager.uiSourceSFX.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && isUpgrade)
        {
            if (gameManager.value >= valueToJetPackConsumption && gameManager.jetPackOxygenConsuption >= 1000)
            {
                gameManager.value -= valueToJetPackConsumption;
                valueToJetPackConsumption += jetPackUpgradeUpcost;
                gameManager.jetPackOxygenConsuption -= jetPackDecrement;
                uiController.textValue.text = "Value: " + gameManager.value;

                gameManager.uiSourceSFX.clip = gameManager.uiAudio[1];
                gameManager.uiSourceSFX.Play();
            }
            else
            {
                gameManager.uiSourceSFX.clip = gameManager.uiAudio[0];
                gameManager.uiSourceSFX.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && isUpgrade)
        {
            gameManager.saleWindow.SetActive(false);
            isUpgrade = false;
        }
    }


    void UpgradeSaleWindow()
    {
        upgradeInfoText.text =
    $"<b>Press \"1\" to upgrade Oxygen Tank</b>\n" +
    //$"(more capacity)\n" +
    $"<color=yellow>Cost: {valueToOxygenTank}</color>\n" +
    $"Capacity: <color=green>{(int)gameManager.oxygenMax} → {gameManager.oxygenMax + oxygenIncrement}</color>\n\n" +

    $"<b>Press \"2\" to upgrade Jetpack</b>\n" +
    //$"(lower oxygen consumption while jumping)\n" +
    $"<color=yellow>Cost: {valueToJetPackConsumption}</color>\n" +
    $"Oxygen Consumption: <color=green>{(int)gameManager.jetPackOxygenConsuption} → {gameManager.jetPackOxygenConsuption - jetPackDecrement}</color>\n\n" +

    $"<b>Press \"Enter\" to exit the menu</b>";
    }
}
