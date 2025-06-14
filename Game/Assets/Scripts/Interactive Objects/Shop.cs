using StarterAssets;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class Shop : MonoBehaviour, IInteractable, IShowPrompt
{
    [Header("References")]
    public GameObject player;
    public GameObject pressEPrefab;
    public PressEPrompts promptData;
    public ThirdPersonController controller;
    public Inputs inputs;
    public GameObject saleWindow;
    public BreathAudioController sfxController;

    [Space]

    public bool isUpgrade = false;

    private OxygenController _oxygenController;
    private UIController _uiController;
    private GameObject _promptInstance;

    public TextMeshProUGUI oxygenTankText;
    public TextMeshProUGUI jetpackText;


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


    private void Start()
    {
        GameObject gm = GameObject.Find("Oxygen&saveloadController");
        _oxygenController = gm.GetComponent<OxygenController>();

        _uiController = GameObject.Find("UI").GetComponent<UIController>();

        UpdateOxygenTankText();
        UpdateJetpackText();
    }

    public void Interact(GameObject interactor)
    {
        ThirdPersonController.Instance.controlEnabled = false;
        Inputs.Instance.cursorLocked = false;
        Inputs.Instance.ApplyCursorState();
        Time.timeScale = 0f;

        isUpgrade = true;
        HidePrompt();

        saleWindow.SetActive(true);
        _oxygenController.oxygen = _oxygenController.oxygenMax;

        SoundManager.PlaySound(SoundType.JETPACK);
    }

    public bool CanShowPrompt() => !isUpgrade;

    public void ShowPrompt()
    {
        if (_promptInstance != null) return;

        GameObject canvas = GameObject.Find("UI");
        if (canvas != null)
        {
            _promptInstance = Instantiate(pressEPrefab, canvas.transform);
            var tmp = _promptInstance.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = promptData.promptText[3];
        }
        else
        {
            Debug.LogWarning("Canvas 'UI' not found!");
        }
    }

    public void HidePrompt()
    {
        if (_promptInstance != null)
        {
            Destroy(_promptInstance);
            _promptInstance = null;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void ButtonResume()
    {
        isUpgrade = false;
        saleWindow.SetActive(false);
        ThirdPersonController.Instance.controlEnabled = true;
        Inputs.Instance.cursorLocked = true;
        Inputs.Instance.ApplyCursorState();
        inputs.ApplyCursorState();
        Time.timeScale = 1f;
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
    }

    public bool OxygenUpgrade()
    {
        if (_oxygenController.value >= valueToOxygenTank)
        {
            _oxygenController.value -= valueToOxygenTank;
            valueToOxygenTank += oxygenUpgradeUpcost;
            _oxygenController.oxygenMax += oxygenIncrement;
            _oxygenController.oxygen += oxygenIncrement;
            _oxygenController.oxygen = Mathf.Clamp(_oxygenController.oxygen, 0f, _oxygenController.oxygenMax);
            _uiController.textValue.text = "Value: " + _oxygenController.value;

            UpdateOxygenTankText();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void ButtonOxygenUpgrade()
    {
        bool isUpgrade = OxygenUpgrade();
        sfxController.UpgradeAudio(isUpgrade);
    }

    public bool JetpackUpgrade()
    {
        if (_oxygenController.value >= valueToJetPackConsumption && _oxygenController.jetPackOxygenConsuption >= 1000)
        {
            _oxygenController.value -= valueToJetPackConsumption;
            valueToJetPackConsumption += jetPackUpgradeUpcost;
            _oxygenController.jetPackOxygenConsuption -= jetPackDecrement;
            _uiController.textValue.text = "Value: " + _oxygenController.value;

            UpdateJetpackText();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void ButtonJetpackUpgrade()
    {
        bool isUpgrade = JetpackUpgrade();
        sfxController.UpgradeAudio(isUpgrade);
    }


    void UpdateOxygenTankText()
    {
        oxygenTankText.text =
            "Oxygen Tank\n" +
            $"Cost: <color=yellow>{valueToOxygenTank}</color>\n" +
            $"Capacity: <color=green>{(int)_oxygenController.oxygenMax} → {_oxygenController.oxygenMax + oxygenIncrement}</color>";
    }

    void UpdateJetpackText()
    {
        jetpackText.text =
            "Jetpack\n" +
            $"Cost: <color=yellow>{valueToJetPackConsumption}</color>\n" +
            $"Oxygen Consumption: <color=green>{(int)_oxygenController.jetPackOxygenConsuption} → {_oxygenController.jetPackOxygenConsuption - jetPackDecrement}</color>";
    }


}
