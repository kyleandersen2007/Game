using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskTextManager : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

    public void EndObjective()
    {

    }
}
