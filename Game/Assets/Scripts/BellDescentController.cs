using UnityEngine;

public class BellDescentController : MonoBehaviour
{
    [Header("Спуск")]
    public float descentSpeed = 0.5f;
    public float targetDepth = -10f;

    [Header("Покачивание")]
    public float swayAmount = 0.05f;
    public float swayFrequency = 1f;

    [Header("Дверь")]
    public Transform door; // Объект двери
    public Vector3 doorOpenRotation = new Vector3(0, 90, 0);
    public float doorOpenSpeed = 2f;

    private Vector3 _startPos;
    private bool _doorOpening = false;
    private bool _timeToUp = false;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        // Спуск вниз до нужной глубины
        if (transform.position.y > targetDepth && !_timeToUp)
        {
            // Движение вниз
            transform.position += Vector3.down * descentSpeed * Time.deltaTime;

            // Покачивание синусоидой
            float swayX = Mathf.Sin(Time.deltaTime * swayFrequency) * swayAmount;
            float swayZ = Mathf.Cos(Time.deltaTime * swayFrequency * 0.8f) * swayAmount;
            if (Time.timeScale > 0) transform.position += new Vector3(swayX, 0, swayZ);
        }
        else if (!_doorOpening)
        {
            _doorOpening = true;
        }

        // Открытие двери
        if (_doorOpening)
        {
            Quaternion targetRotation = Quaternion.Euler(doorOpenRotation);
            door.localRotation = Quaternion.Lerp(door.localRotation, targetRotation, Time.deltaTime * doorOpenSpeed);
        }

        if (_timeToUp && transform.position.y < 15)
        {
            transform.position += Vector3.up * descentSpeed * Time.deltaTime;

            // Покачивание синусоидой
            float swayX = Mathf.Sin(Time.deltaTime * swayFrequency) * swayAmount;
            float swayZ = Mathf.Cos(Time.deltaTime * swayFrequency * 0.8f) * swayAmount;
            transform.position += new Vector3(swayX, 0, swayZ);

            Quaternion targetRotation = Quaternion.Euler(-doorOpenRotation);
            door.localRotation = Quaternion.Lerp(door.localRotation, targetRotation, Time.deltaTime * doorOpenSpeed);
        }
        if (_timeToUp && transform.position.y > 15) Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _timeToUp = true;
            Debug.Log(_timeToUp);


        }
    }
}