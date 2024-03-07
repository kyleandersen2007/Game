using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBatteryTrigger : MonoBehaviour
{
    public GameEvent OnBatteryPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnBatteryPickUp.Raise();
            Destroy(gameObject);
        }
    }
}
