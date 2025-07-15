using UnityEngine;
using Cinemachine;

public class BellDescentController : MonoBehaviour
{
    [Header("Ссылка на виртуальную камеру")]
    [SerializeField] private CinemachineVirtualCamera vcam;

    [Header("Тряска")]
    [SerializeField] private float shakeAmplitude = 2f;
    [SerializeField] private float shakeFrequency = 2f;
    [SerializeField] private float shakeTime = 5f;

    [Header("Покачивание")]
    public float swayAmount = 0.05f;
    public float swayFrequency = 1f;

    [Header("Дверь")]
    public Transform door;
    public Vector3 doorOpenRotation = new Vector3(0, 90, 0);
    public float doorOpenSpeed = 2f;

    [Header("Подъём")]
    public float ascentSpeed = 0.5f;
    public float stopHeight = 15f;


    CinemachineBasicMultiChannelPerlin _noise;
    float _shakeTimer;
    bool _playerInside;
    bool _doorOpening;
    bool _goingUp;

    void Start()
    {
        // получаем noise компонент камеры
        _noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _noise.m_AmplitudeGain = 0;
    }

    void Update()
    {
        // пока игрок внутри
        if (_playerInside && _shakeTimer < shakeTime)
        {
            _shakeTimer += Time.deltaTime;
            _noise.m_AmplitudeGain = shakeAmplitude;
            _noise.m_FrequencyGain = shakeFrequency;

            // лёгкое покачивание колокола для вида
            SwayBell();
        }
        // игрок вышел или время вышло 
        else if (!_goingUp)
        {
            _noise.m_AmplitudeGain = 0;          // стоп тряску
            _doorOpening = true;                 // открываем дверь
        }

        // плавное открытие двери
        if (_doorOpening)
        {
            var targetRot = Quaternion.Euler(doorOpenRotation);
            door.localRotation = Quaternion.Lerp(door.localRotation, targetRot,
                                                 Time.deltaTime * doorOpenSpeed);
        }

        // подъём колокола после выхода
        if (_goingUp && transform.position.y < stopHeight)
        {
            var targetRot = Quaternion.Euler(-doorOpenRotation);
            door.localRotation = Quaternion.Lerp(door.localRotation, targetRot,
                                                 Time.deltaTime * doorOpenSpeed);
            transform.position += Vector3.up * ascentSpeed * Time.deltaTime;
            SwayBell(); // раскачка
        }
        else if (_goingUp && transform.position.y >= stopHeight)
        {
            Destroy(gameObject); // колокол уехал
        }
    }

    
    void SwayBell()
    {
        float swayX = Mathf.Sin(Time.time * swayFrequency * 0.8f) * swayAmount;
        float swayZ = Mathf.Cos(Time.time * swayFrequency * 10f) * swayAmount;
        transform.position += new Vector3(swayX, 0, swayZ) * Time.deltaTime;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = true;   // запускаем тряску
            _shakeTimer = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = false;  // время тряски закончится
            _goingUp = true;   // начинаем подъём
        }
    }
}