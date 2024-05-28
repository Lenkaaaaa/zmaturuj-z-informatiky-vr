using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TheoryTwo : MonoBehaviour
{
    public Button prevButton;
    public Button nextButton;
    public Button quizButton;
    public Button startButton;
    public Button finalTestButton;

    public Transform panelContainerOne;
    public Transform panelContainerTwo;
    public Transform panelContainerThree;
    public Transform panelContainerFinal;
    public Transform panelContainerStart;

    public Transform Neuman;
    public Transform Harvard;

    public GameObject instructionPanelPrefab;
    public TMP_Text instructionText;

    public Transform questionsPanelContainer;
    public GameObject questionPanelPrefab;
    public TMP_Text questionText;

    public TMP_Dropdown answersDropdown;
    public Button submitButton;

    public Transform feedbackPanelContainer;
    public TMP_Text feedbackText;

    private Transform currentPanelContainer;
    private GameObject currentFeedbackPanel;
    private GameObject currentInstructionPanel;
    private GameObject currentQuestionPanel;
    private TMP_Text textComponent;
    private int currentInstructionIndex;
    private int round;
    private int choosenAnswerIndex;
    private int rightAnswerIndex;
    private bool isRight;
    private bool finalRound;

    private void Start()
    {
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        quizButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(false);
        finalTestButton.gameObject.SetActive(false);
        answersDropdown.gameObject.SetActive(false);
        questionText.gameObject.SetActive(false);
        questionsPanelContainer.gameObject.SetActive(false);
        Neuman.gameObject.SetActive(false);
        Harvard.gameObject.SetActive(false);

        prevButton.onClick.AddListener(OnPrevButtonClick);
        nextButton.onClick.AddListener(OnNextButtonClick);
        quizButton.onClick.AddListener(OnQuizButtonClick);
        startButton.onClick.AddListener(OnStartButtonClick);
        submitButton.onClick.AddListener(OnSubmitButtonClick);

        currentInstructionIndex = 0;
        round = 0;
        choosenAnswerIndex = 0;
        rightAnswerIndex = 0;
        isRight = false;
        finalRound = false;

        SetCurrentPanelContainer(round);
        ShowInstructionContent();

    }

    public void OnStartButtonClick()
    {
        round++;
        SetCurrentPanelContainer(round);
        ShowInstructionContent();
    }

    public void OnNextButtonClick()
    {
        currentInstructionIndex++;
        ShowInstructionContent();
    }

    public void OnPrevButtonClick()
    {
        currentInstructionIndex--;
        ShowInstructionContent();
    }

    public void OnQuizButtonClick()
    {
        ShowQuizQuestion(round);
    }

    public void OnSubmitButtonClick()
    {
        CheckAnswer();
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


        if (round != 0)
        {
            nextButton.gameObject.SetActive(true);
            quizButton.gameObject.SetActive(false);
            prevButton.gameObject.SetActive(true);


            if (currentInstructionIndex == currentPanelContainer.childCount - 1)
            {
                nextButton.gameObject.SetActive(false);
                quizButton.gameObject.SetActive(true);

            }

            if (currentInstructionIndex == 0)
            {
                prevButton.gameObject.SetActive(false);
            }
        }

        if(round == 1)
        {
            if (currentInstructionIndex == currentPanelContainer.childCount - 1)
            {
                Harvard.gameObject.SetActive(true);
                Neuman.gameObject.SetActive(false);
            }
            else
            {
                Harvard.gameObject.SetActive(false);
                Neuman.gameObject.SetActive(true);
            }
            
        }

    }

    private void ShowQuizQuestion(int roundIndex)
    {

        prevButton.gameObject.SetActive(false);
        quizButton.gameObject.SetActive(false);

        Neuman.gameObject.SetActive(false);
        Harvard.gameObject.SetActive(false);

        currentInstructionIndex = 0;
        submitButton.gameObject.SetActive(true);
        answersDropdown.gameObject.SetActive(true);
        questionsPanelContainer.gameObject.SetActive(true);

        for (int i = 0; i < currentPanelContainer.childCount; i++)
        {
            currentPanelContainer.GetChild(i).gameObject.SetActive(false);
        }

        currentInstructionPanel.SetActive(false);

        for (int i = 0; i < questionsPanelContainer.childCount; i++)
        {
            questionsPanelContainer.GetChild(i).gameObject.SetActive(false);
        }

        currentQuestionPanel = questionsPanelContainer.GetChild(roundIndex).gameObject;
        currentQuestionPanel.SetActive(true);

        textComponent = currentQuestionPanel.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            questionText.text = textComponent.text;
        }

    }

    private void CheckAnswer()
    {
        choosenAnswerIndex = answersDropdown.value;
        rightAnswerIndex = GetRightAnswerIndexForRound(round);
        isRight = choosenAnswerIndex == rightAnswerIndex;
        UpdateAnswerFeedback(isRight);
        StartCoroutine(SetNewRound());
        questionsPanelContainer.gameObject.SetActive(false);
    }

    private int GetRightAnswerIndexForRound(int roundIndex)
    {
        switch (roundIndex)
        {
            case 1: return 0;
            case 2: return 2;
            case 3: return 0;
            default:
                Debug.LogError("Neplatný index otázky.");
                return -1;
        }
    }

    private void UpdateAnswerFeedback(bool isAnswrRight)
    {
        answersDropdown.image.color = isAnswrRight ? Color.green : Color.red;


        if (answersDropdown.image.color == Color.green)
        {
            currentFeedbackPanel = feedbackPanelContainer.GetChild(0).gameObject;

        }
        else
        {
            currentFeedbackPanel = feedbackPanelContainer.GetChild(2).gameObject;

        }

        currentFeedbackPanel.SetActive(true);

        textComponent = currentFeedbackPanel.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            feedbackText.text = textComponent.text;
        }


    }

    private IEnumerator SetNewRound()
    {
        yield return new WaitForSeconds(1f);

        if (isRight)
        {
            round++;
            isRight = false;
        }

        SetCurrentPanelContainer(round);
        ShowInstructionContent();

        answersDropdown.image.color = Color.white;
        questionText.gameObject.SetActive(false);
        answersDropdown.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        quizButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        currentFeedbackPanel.SetActive(false);

        if (finalRound)
        {
            nextButton.gameObject.SetActive(false);
            finalTestButton.gameObject.SetActive(true);
        }
    }

    private void SetCurrentPanelContainer(int roundIndex)
    {
        if (roundIndex == 0)
        {
            currentPanelContainer = panelContainerStart;
            panelContainerStart.gameObject.SetActive(true);
            panelContainerFinal.gameObject.SetActive(false);
            panelContainerOne.gameObject.SetActive(false);
            panelContainerTwo.gameObject.SetActive(false);
            panelContainerThree.gameObject.SetActive(false);

        }
        else if (roundIndex == 1)
        {
            currentPanelContainer = panelContainerOne;
            panelContainerOne.gameObject.SetActive(true);
            panelContainerFinal.gameObject.SetActive(false);
            panelContainerTwo.gameObject.SetActive(false);
            panelContainerThree.gameObject.SetActive(false);
            panelContainerStart.gameObject.SetActive(false);
            startButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
            Neuman.gameObject.SetActive(true);
        }
        else if (roundIndex == 2)
        {
            currentPanelContainer = panelContainerTwo;
            panelContainerFinal.gameObject.SetActive(false);
            panelContainerOne.gameObject.SetActive(false);
            panelContainerTwo.gameObject.SetActive(true);
            panelContainerThree.gameObject.SetActive(false);
            panelContainerStart.gameObject.SetActive(false);

        }
        else if (roundIndex == 3)
        {
            currentPanelContainer = panelContainerThree;
            panelContainerFinal.gameObject.SetActive(false);
            panelContainerOne.gameObject.SetActive(false);
            panelContainerTwo.gameObject.SetActive(false);
            panelContainerThree.gameObject.SetActive(true);
            panelContainerStart.gameObject.SetActive(false);
        }
        else
        {
            currentPanelContainer = panelContainerFinal;
            panelContainerFinal.gameObject.SetActive(true);
            panelContainerOne.gameObject.SetActive(false);
            panelContainerTwo.gameObject.SetActive(false);
            panelContainerThree.gameObject.SetActive(false);
            panelContainerStart.gameObject.SetActive(false);
            finalRound = true;
        }

        for (int i = 0; i < feedbackPanelContainer.childCount; i++)
        {
            feedbackPanelContainer.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void NewStart()
    {
        currentInstructionIndex = 0;
        round = 1;
        choosenAnswerIndex = 0;
        rightAnswerIndex = 0;
        isRight = false;
        finalRound = false;
        SetCurrentPanelContainer(1);
        ShowInstructionContent();
    }
}
