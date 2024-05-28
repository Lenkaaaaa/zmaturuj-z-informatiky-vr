using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public Button[] passwordButtons; 
    public string correctPassword = "0101";
    public TMP_Text feedbackText;
    public GameObject buttonPilar;
    public GameObject keyboard;

    private string enteredPassword = "";

    void Start()
    {
        foreach (Button button in passwordButtons)
        {
            button.onClick.AddListener(() => OnPasswordButtonClick(button.GetComponentInChildren<TMP_Text>().text));
        }

        buttonPilar.SetActive(false);
    }

    void OnPasswordButtonClick(string character)
    {
        enteredPassword += character;

        if (enteredPassword == correctPassword)
        {
            ShowFeedbackMessage("Správne.");
            buttonPilar.SetActive(true);
            keyboard.SetActive(false);
        }
        else if (enteredPassword.Length >= correctPassword.Length)
        {
            enteredPassword = "";
            ShowFeedbackMessage("Nesprávne.");
        }
        else
        {
            ShowFeedbackMessage(enteredPassword);
        }
    }

    void ShowFeedbackMessage(string message)
    {
        feedbackText.text = message;
    }
}
