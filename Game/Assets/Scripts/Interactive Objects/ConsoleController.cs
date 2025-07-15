using Unity.VisualScripting;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    [Header("Ссылки")]
    public SnappableObject requiredItem;          // та самая батарея
    public MeshRenderer consoleRenderer;       // рендерер консоли
    public Material readyMaterial;         // материал «готово»

    [Header("Колокол")]
    public GameObject bellPrefab;
    public Transform bellSpawnPoint;
    public KeyCode callKey = KeyCode.U;     // «У» на латинской раскладке

    bool _ready;
    bool _bellCalled;

    void Start()
    {
        // подписываемся на событие, чтобы не опрашивать каждый кадр
        requiredItem.OnSnapped += HandleItemSnapped;
    }

    void Update()
    {
        if (_ready && Input.GetKeyDown(callKey))
            CallBell();
    }

    void HandleItemSnapped()
    {
        Debug.Log("Console");
        _ready = true;
        if (consoleRenderer && readyMaterial)
            consoleRenderer.material = readyMaterial;
    }

    void CallBell()
    {
        bellPrefab.SetActive(true);
        //Instantiate(bellPrefab, bellSpawnPoint.position, bellSpawnPoint.rotation);
        _bellCalled = true;
        // здесь можно запустить звук, VFX и т.д.
    }
}
