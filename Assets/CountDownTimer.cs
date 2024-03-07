using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text countdownText;
    public float countdownTime = 30f; // Initial countdown time in seconds
    private bool isCounting = false;

    public Image fadeImage; // Reference to the Image component used for fading
    public float fadeDuration = 1f; // Duration of the fade
    public string sceneToLoad;

    void Start()
    {
        // Set the initial text
        UpdateCountdownText();
    }

    public void StartCountdown()
    {
        if (!isCounting)
        {
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        isCounting = true;
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(0.01f); // Update every 0.01 seconds (10 milliseconds)
            countdownTime -= 0.01f;
            UpdateCountdownText();
        }

        // Countdown has reached 0
        countdownTime = 0;
        UpdateCountdownText();

        // Optionally do something when countdown ends
        Debug.Log("Countdown finished!");

        // Transition with fade effect
        yield return StartCoroutine(Transition());

        isCounting = false;
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
    }

    void UpdateCountdownText()
    {
        int seconds = Mathf.FloorToInt(countdownTime);
        int milliseconds = Mathf.FloorToInt((countdownTime * 1000) % 1000);

        countdownText.text = string.Format("{0:00}.{1:00}", seconds, milliseconds);
    }
}
