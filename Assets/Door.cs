using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float meltThreshold = 30; // Time in seconds before door melts
    public float temperatureIncreaseRate = 1.0f; // How much temperature increases per second when hit
    public Sprite[] doorSprites; // Array of sprites representing the door's states
    SpriteRenderer spriteRenderer;
    float currentTemperature = 0.0f;
    bool isMelted = false;

    public GameEvent DoorMelted;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = doorSprites[0]; // Start with the first sprite

        // Set the number of sprites per threshold (assuming evenly spaced thresholds)
        int spritesPerThreshold = doorSprites.Length / 3;
    }

    public void IncreaseTemperature()
    {
        if (!isMelted)
        {
            currentTemperature += temperatureIncreaseRate * Time.deltaTime;

            // Calculate which sprite to display based on the current temperature
            int spriteIndex = Mathf.FloorToInt((currentTemperature / meltThreshold) * doorSprites.Length);

            // Ensure the sprite index is within bounds
            spriteIndex = Mathf.Clamp(spriteIndex, 0, doorSprites.Length - 1);

            // Set the sprite based on the calculated index
            spriteRenderer.sprite = doorSprites[spriteIndex];

            // Check if door has melted
            if (currentTemperature >= meltThreshold * 3)
            {
                MeltDoor();
            }
        }
    }

    void MeltDoor()
    {
        isMelted = true;
        DoorMelted.Raise();
        Destroy(gameObject);
    }
}
