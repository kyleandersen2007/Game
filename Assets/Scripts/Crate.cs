using System.Collections;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveAmount = 0.1f;
    public GameEvent onBoxMoved;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Vector3 hitDirection = collision.GetContact(0).point - (Vector2)transform.position;

            float moveX = Mathf.Sign(-hitDirection.x) * moveAmount;

            Vector3 targetPosition = transform.position + Vector3.right * moveX;

            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (transform.position.x <= 1f && transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (transform.position.x > 1f)
        {
            onBoxMoved.Raise();
        }
    }
}
