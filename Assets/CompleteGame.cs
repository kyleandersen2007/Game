using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteGame : MonoBehaviour
{

    public Image fadeImage; // Reference to the Image component used for fading
    public float fadeDuration = 1f; // Duration of the fade
    public string sceneToLoad;

    public void Finish()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
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
        SceneManager.LoadScene("VICTORY");

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
    }
}
