using TMPro;
using UnityEngine;

public class UpgradeTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upgradeInfoText;
    public GameManager gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isUpgrade)
        {
            upgradeUI();
        }
    }

    void upgradeUI()
    {
        upgradeInfoText.text =
    $"<b>Press \"1\" to upgrade Oxygen Tank</b>\n" +
    //$"(more capacity)\n" +
    $"<color=yellow>Cost: {gameManager.valueToOxygenTank}</color>\n" +
    $"Capacity: <color=green>{(int)gameManager.oxygenMax} → {gameManager.oxygenMax + gameManager.oxygenIncrement}</color>\n\n" +

    $"<b>Press \"2\" to upgrade Jetpack</b>\n" +
    //$"(lower oxygen consumption while jumping)\n" +
    $"<color=yellow>Cost: {gameManager.valueToJetPackConsumption}</color>\n" +
    $"Oxygen Consumption: <color=green>{(int)gameManager.jetPackOxygenConsuption} → {gameManager.jetPackOxygenConsuption - gameManager.jetPackDecrement}</color>\n\n" +

    $"<b>Press \"Esc\" to exit the menu</b>";
    }
}
