using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTransition : MonoBehaviour
{
    public Image fadeImage; // Reference to the Image component used for fading
    public float fadeDuration = 1f; // Duration of the fade
    public string sceneToLoad; // Name of the scene to load

    bool isTransitioning = false; // Flag to prevent multiple transitions

    void Start()
    {
        fadeImage.gameObject.SetActive(false); // Deactivate the fade image on start
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        isTransitioning = true;

        fadeImage.gameObject.SetActive(true); // Activate the fade image

        // Fade Out
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha); // Black with varying alpha
            timer += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = Color.black;

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);

        // Fade In
        timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha); // Black with varying alpha
            timer += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = Color.clear;

        fadeImage.gameObject.SetActive(false); // Deactivate the fade image

        isTransitioning = false;
    }
}
