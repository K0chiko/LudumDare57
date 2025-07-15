using System;
using System.Collections;
using UnityEngine;


public class SnappableObject : MonoBehaviour
{
    [Header("Socket (гнездо)")]
    public Transform socket;

    [Header("Параметры снапа")]
    public float snapDistance = 0.3f;
    public float snapLerpSpeed = 10f;
    public bool lockAfterSnap = true;

    public bool IsSnapped { get; private set; }  // <- сюда из других скриптов
    public event Action OnSnapped;               // <- событие

    Rigidbody _rb;

    void Awake() => _rb = GetComponent<Rigidbody>();

    void Update()
    {
        if (IsSnapped) return;                   // уже вставлен
        if (transform.parent != null) return;    // ещё в руках

        if (Vector3.Distance(transform.position, socket.position) <= snapDistance)
            StartCoroutine(SnapToSocket());
    }

    IEnumerator SnapToSocket()
    {
        _rb.isKinematic = true;

        Vector3 p0 = transform.position;
        Quaternion r0 = transform.rotation;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * snapLerpSpeed;
            transform.position = Vector3.Lerp(p0, socket.position, t);
            transform.rotation = Quaternion.Slerp(r0, socket.rotation, t);
            yield return null;
        }

        transform.SetPositionAndRotation(socket.position, socket.rotation);
        transform.SetParent(socket);

        if (lockAfterSnap) Destroy(_rb);
        else _rb.isKinematic = false;

        IsSnapped = true;
        OnSnapped?.Invoke();         // сообщаем консоли

    }
}
