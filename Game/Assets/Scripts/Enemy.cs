using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public float moveSpeed = 3f;
    public float stopDistance = 1f; 
    public bool isAttack = false;
    public bool hasReachedPlayer = false;

    private bool isEnd = false;

    private Vector3 direction;
    void Update()
    {
        if (isAttack && enemy != null)
        {
            direction = (player.transform.position - enemy.transform.position).normalized;
            enemy.transform.position += direction * moveSpeed * Time.deltaTime;
        }

        if (isEnd)
        {
            Debug.Log("YOU DEAD");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.gameObject.SetActive(true);
            isAttack = true;
            StartCoroutine(DelayedEnd());
        }
    }

    IEnumerator DelayedEnd()
{
    yield return new WaitForSeconds(3f);
    isEnd = true;
}
}
