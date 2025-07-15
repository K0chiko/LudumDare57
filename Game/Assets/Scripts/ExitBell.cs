using UnityEngine;
using Cinemachine;

public class ExitBell : MonoBehaviour
{
    [Header("Ссылка на виртуальную камеру")]
    [SerializeField] private CinemachineVirtualCamera vcam;

    [Header("Тряска")]
    [SerializeField] private float shakeAmplitude = 2f;
    [SerializeField] private float shakeFrequency = 2f;
    [SerializeField] private float shakeTime = 5f;

    [Header("Покачивание")]
    [SerializeField] private float swayAmount = 0.05f;
    [SerializeField] private float swayFrequency = 1f;

    [Header("Дверь")]
    [SerializeField] private Transform door;
    [SerializeField] private Vector3 doorOpenRotation = new Vector3(0, 90, 0);
    [SerializeField] private float doorOpenSpeed = 2f;

    [Header("Подъём")]
    [SerializeField] private float ascentSpeed = 0.5f;
    [SerializeField] private Transform destinationPivot;
    private float _stopHeight = 15f;


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
        _stopHeight = destinationPivot.transform.position.y;
    }

    void Update()
    {
        // пока игрок внутри
        if (_playerInside)
        {
            Debug.Log("Win!");
        }
        

        // плавное открытие двери
        if (_doorOpening)
        {
            var targetRot = Quaternion.Euler(doorOpenRotation);
            door.localRotation = Quaternion.Lerp(door.localRotation, targetRot,
                                                 Time.deltaTime * doorOpenSpeed);
        }

        // опускание колокола
        if (/*_goingUp &&*/ transform.position.y > _stopHeight)
        {
            transform.position -= Vector3.up * ascentSpeed * Time.deltaTime;
            //SwayBell(); // раскачка
        }
        else if (transform.position.y >= _stopHeight)
        {
            _doorOpening = true;
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
            _playerInside = true;

            var targetRot = Quaternion.Euler(-doorOpenRotation);
            door.localRotation = Quaternion.Lerp(door.localRotation, targetRot,
                                                 Time.deltaTime * doorOpenSpeed);
        }
    }

}