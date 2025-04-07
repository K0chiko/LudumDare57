using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform enemy;
    public float moveSpeed = 3f;
    public float stopDistance = 1f; 
    public bool isAttack = false;
    public bool hasReachedPlayer = false;

    void Update()
    {
        if (isAttack && enemy != null && !hasReachedPlayer)
        {
            Vector3 direction = (transform.position - enemy.position);
            float distance = direction.magnitude;

            if (distance > stopDistance)
            {
                direction.Normalize();
                enemy.position += direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                hasReachedPlayer = true;
                Debug.Log("Игра окончена!");
            }
        }
    }
}
