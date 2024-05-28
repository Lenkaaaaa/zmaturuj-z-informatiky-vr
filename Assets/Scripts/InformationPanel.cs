using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationPanel : MonoBehaviour
{
    public Button prevButton;
    public Button nextButton;

    public Transform panelContainer;
    public GameObject instructionPanelPrefab;
    public TMP_Text instructionText;

    private Transform currentPanelContainer;
    private GameObject currentInstructionPanel;

    private int currentInstructionIndex;
    private TMP_Text textComponent;

    void Start()
    {
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(false);
        prevButton.onClick.AddListener(OnPrevButtonClick);
        nextButton.onClick.AddListener(OnNextButtonClick);

        currentInstructionIndex = 0;
        currentPanelContainer = panelContainer;

        ShowInstructionContent();
    }

    public void OnPrevButtonClick()
    {
        currentInstructionIndex--;
        ShowInstructionContent();
    }

    public void OnNextButtonClick()
    {
        currentInstructionIndex++;
        ShowInstructionContent();
    }

    private void ShowInstructionContent()
    {
        currentInstructionIndex = Mathf.Clamp(currentInstructionIndex, 0, currentPanelContainer.childCount - 1);


        for (int i = 0; i < currentPanelContainer.childCount; i++)
        {
            currentPanelContainer.GetChild(i).gameObject.SetActive(false);
        }

        currentInstructionPanel = currentPanelContainer.GetChild(currentInstructionIndex).gameObject;
        currentInstructionPanel.SetActive(true);

        textComponent = currentInstructionPanel.GetComponentInChildren<TMP_Text>();

        if (textComponent != null)
        {
            instructionText.text = textComponent.text;
        }


        if (currentInstructionIndex == currentPanelContainer.childCount - 1)
        {
            nextButton.gameObject.SetActive(false);
        } else
        {
            nextButton.gameObject.SetActive(true);
        }

        if (currentInstructionIndex == 0)
        {
            prevButton.gameObject.SetActive(false);
        } else
        {
            prevButton.gameObject.SetActive(true);
        }

    }
}
