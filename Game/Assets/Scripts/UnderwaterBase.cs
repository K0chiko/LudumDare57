using UnityEngine;

public class UnderwaterBase : MonoBehaviour
{
    [Tooltip("Объект, представляющий ноги дома (child)")]
    public Transform legs;

    [Tooltip("Насколько опустить дом")]
    public float dropAmount = 2f;

    [Tooltip("Время движения в секундах")]
    public float duration = 5f;

    [Tooltip("Тег объекта игрока")]
    public string playerTag = "Player";

    private bool triggered = false;

    private Vector3 initialHousePos;
    private Vector3 targetHousePos;
    private Vector3 initialLegsPos;
    private Vector3 targetLegsPos;

    private float timer = 0f;
    private bool isMoving = false;

    void Start()
    {
        initialHousePos = transform.position;

        if (legs != null)
            initialLegsPos = legs.localPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);

            transform.position = Vector3.Lerp(initialHousePos, targetHousePos, t);
            //if (legs != null)
                legs.localPosition = Vector3.Lerp(initialLegsPos, targetLegsPos, t);

            if (t >= 1f)
                isMoving = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag(playerTag)) return;

        triggered = true;
        isMoving = true;
        timer = 0f;

        // Устанавливаем начальные и целевые позиции
        initialHousePos = transform.position;
        targetHousePos = transform.position - new Vector3(0, dropAmount, 0);

        if (legs != null)
        {
            initialLegsPos = legs.localPosition;
            targetLegsPos = legs.localPosition + new Vector3(0, dropAmount, 0);
        }
    }
}