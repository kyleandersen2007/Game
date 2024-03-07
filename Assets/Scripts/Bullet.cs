using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject destroyEffect;
    private void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);

        Destroy(this.gameObject);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy hit = collision.gameObject.GetComponent<Enemy>();

            if (hit != null)
            {
                hit.TakeDamage(1);
            }
            Destroy(collision.gameObject);
        }
    }
}
