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
    public AudioClip airTankFillUpClip;
    public ThirdPersonController controller;
    public Inputs inputs;

    public bool isUpgrade = false;

    private GameManager _gameManager;
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
        GameObject gm = GameObject.Find("GameManager");
        _gameManager = gm.GetComponent<GameManager>();

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

        _gameManager.saleWindow.SetActive(true);
        _gameManager.oxygen = _gameManager.oxygenMax;

        // Play air tank fill-up sound
        AudioSource sfx = player.AddComponent<AudioSource>();
        sfx.clip = airTankFillUpClip;
        sfx.Play();
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
        _gameManager.saleWindow.SetActive(false);
        ThirdPersonController.Instance.controlEnabled = true;
        Inputs.Instance.cursorLocked = true;
        Inputs.Instance.ApplyCursorState();
        inputs.ApplyCursorState();
        Time.timeScale = 1f;
    }

    public void ButtonOxygenUpgrade()
    {
        if (_gameManager.value >= valueToOxygenTank)
        {
            _gameManager.value -= valueToOxygenTank;
            valueToOxygenTank += oxygenUpgradeUpcost;
            _gameManager.oxygenMax += oxygenIncrement;
            _gameManager.oxygen += oxygenIncrement;
            _gameManager.oxygen = Mathf.Clamp(_gameManager.oxygen, 0f, _gameManager.oxygenMax);
            _uiController.textValue.text = "Value: " + _gameManager.value;

            UpdateOxygenTankText();

            _gameManager.uiSourceSFX.clip = _gameManager.uiAudio[1];
            _gameManager.uiSourceSFX.Play();
        }
        else
        {
            _gameManager.uiSourceSFX.clip = _gameManager.uiAudio[0];
            _gameManager.uiSourceSFX.Play();
        }
    }
    public void ButtonJetpackUpgrade()
    {
        if (_gameManager.value >= valueToJetPackConsumption && _gameManager.jetPackOxygenConsuption >= 1000)
        {
            _gameManager.value -= valueToJetPackConsumption;
            valueToJetPackConsumption += jetPackUpgradeUpcost;
            _gameManager.jetPackOxygenConsuption -= jetPackDecrement;
            _uiController.textValue.text = "Value: " + _gameManager.value;

            UpdateJetpackText();

            _gameManager.uiSourceSFX.clip = _gameManager.uiAudio[1];
            _gameManager.uiSourceSFX.Play();
        }
        else
        {
            _gameManager.uiSourceSFX.clip = _gameManager.uiAudio[0];
            _gameManager.uiSourceSFX.Play();
        }
    }


    void UpdateOxygenTankText()
    {
        oxygenTankText.text =
            "Oxygen Tank\n" +
            $"Cost: <color=yellow>{valueToOxygenTank}</color>\n" +
            $"Capacity: <color=green>{(int)_gameManager.oxygenMax} → {_gameManager.oxygenMax + oxygenIncrement}</color>";
    }

    void UpdateJetpackText()
    {
        jetpackText.text =
            "Jetpack\n" +
            $"Cost: <color=yellow>{valueToJetPackConsumption}</color>\n" +
            $"Oxygen Consumption: <color=green>{(int)_gameManager.jetPackOxygenConsuption} → {_gameManager.jetPackOxygenConsuption - jetPackDecrement}</color>";
    }


}
