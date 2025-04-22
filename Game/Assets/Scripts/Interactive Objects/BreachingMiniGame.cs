using System.Linq;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BreachingMiniGame : MonoBehaviour, IInteractable, IShowPrompt, IDestroyable, IBreachable
{
    public bool isBreaching = false;
    private int increment;
    private int mistakeCount = 0;
    private char currentLetter;
    private int maxLetterHits;
    private int currentLetterHits;
    private float _timeSinceLastDecay = 0f;

    public GameObject breachingUIPrefab;
    public Slider slider;
    public TextMeshProUGUI targetLetterText;

    public GameObject pressEPrefab;
    public PressEPrompts promptData;
    private GameObject _promptInstance;

    public bool CanShowPrompt() => !isBreaching;

    void Update()
    {
        if (isBreaching)
        {
            HandleBreachingInput();

            _timeSinceLastDecay += Time.deltaTime;
            if (_timeSinceLastDecay >= 1f)
            {
                float decayAmount = 3f; 
                slider.value -= decayAmount;
                slider.value = Mathf.Max(0f, slider.value);

                _timeSinceLastDecay = 0f;
            }
        }
    }

    public void Interact(GameObject interactor)
    {
        // Здесь пока ничего
    }

    public void ShowPrompt()
    {
        if (_promptInstance == null)
        {
            GameObject canvas = GameObject.Find("UI");
            if (canvas != null)
            {
                _promptInstance = Instantiate(pressEPrefab, canvas.transform);
                TextMeshProUGUI tmp = _promptInstance.GetComponentInChildren<TextMeshProUGUI>();
                tmp.text = promptData.promptText[2];
            }
            else
            {
                Debug.Log("Canvas 'UI' not found!");
            }
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

    }

    public void Breach()
    {
        Debug.Log("Started breaching!");
        breachingUIPrefab.SetActive(true);
        isBreaching = true;
        slider.value = 0;
        mistakeCount = 0;
        PickNewLetter();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<ThirdPersonController>();
            if (controller != null)
                controller.controlEnabled = false;
        }
    }

    private static readonly char[] allowedLetters = { 'T', 'Y', 'U', 'G', 'H', 'J', 'B', 'N', 'M' };

    private void PickNewLetter()
    {
        currentLetter = allowedLetters[Random.Range(0, allowedLetters.Length)];
        maxLetterHits = Random.Range(1, 5);
        currentLetterHits = 0;

        targetLetterText.text = $"PRESS: <color=red>{currentLetter}</color>";
    }

    private void HandleBreachingInput()
    {
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                char inputChar = char.ToUpper(c);

                if (!allowedLetters.Contains(inputChar))
                    continue; // игнорируем неподходящие символы

                if (inputChar == currentLetter)
                {
                    currentLetterHits++;
                    int gain = Random.Range(5, 11);
                    slider.value += gain;

                    if (slider.value >= 100)
                    {
                        Debug.Log("Success!");
                        EndBreaching();
                    }
                    else if (currentLetterHits >= maxLetterHits)
                    {
                        PickNewLetter();
                    }
                }
                else
                {
                    mistakeCount++;
                    float penalty = mistakeCount switch
                    {
                        1 => 5f,
                        2 => 15f,
                        3 => 25f,
                        4 => 35f,
                        _ => 50f
                    };

                    slider.value -= penalty;
                    slider.value = Mathf.Max(0, slider.value);

                    if (mistakeCount >= 5)
                    {
                        Debug.Log("Breaching locked! Too many mistakes.");
                        EndBreaching();
                    }
                }

                break; // читаем только одну клавишу за кадр
            }
        }
    }


    private void EndBreaching()
    {
        //_isBreached = true;
        breachingUIPrefab.SetActive(false);
        isBreaching = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<ThirdPersonController>();
            if (controller != null)
                controller.controlEnabled = true;
        }

        Destroy(gameObject);
    }
}
