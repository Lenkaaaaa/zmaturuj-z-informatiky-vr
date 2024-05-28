using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalTest : MonoBehaviour
{
    public GameObject[] questions;

    public Button submitButton;
    public Button nextButton;
    public Button againButton;
    public Button endButton;
    public Button showTheoryButton;

    public GameObject resultPanel;
    public GameObject messagePanel;
    public GameObject finalTestOne;
    public GameObject buttonPilar;
    public TMP_Text resultText;

    private int numOfRightAnswers;
    private bool testFinished;
    private bool testRight;
    private bool secondRound;

    void Start()
    {
        
        submitButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        againButton.gameObject.SetActive(false);
        endButton.gameObject.SetActive(false);
        showTheoryButton.gameObject.SetActive(false);
        resultPanel.SetActive(false);
        messagePanel.SetActive(false);
        buttonPilar.SetActive(false);

        numOfRightAnswers = 0;
        testFinished = false;
        testRight = false;
        secondRound = false;
              
        for (int i = 0; i < questions.Length; i++)
        {
            InitializeQuestion(questions[i]);
        }
    }

    void Update()
    {
        
        if (!testFinished)
        {
            bool allQuestionsAnswered = true;

            for (int i = 0; i < questions.Length; i++)
            {
                if (!IsQuestionAnswered(questions[i]))
                {
                    allQuestionsAnswered = false;
                    break;
                }
            }

            submitButton.gameObject.SetActive(allQuestionsAnswered);
        }
    }

    void InitializeQuestion(GameObject question)
    {
        Transform questionContainer = question.transform.Find("Answer Buttons Container");

        if (questionContainer == null)
        {
            Debug.LogError("Answer Buttons Container not Found in: " + question.name);
            return;
        }

        Button[] answerButtons = question.transform.Find("Answer Buttons Container").GetComponentsInChildren<Button>();

        foreach (Button button in answerButtons)
        {
            button.onClick.AddListener(delegate { OnButtonClick(button); });
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        GameObject question = clickedButton.transform.parent.parent.gameObject;

        Button[] answerButtons = question.transform.Find("Answer Buttons Container").GetComponentsInChildren<Button>();

        foreach (Button button in answerButtons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        clickedButton.GetComponent<Image>().color = Color.cyan;
    }

    bool IsQuestionAnswered(GameObject question)
    {
        Button[] odpovedButtons = question.transform.Find("Answer Buttons Container").GetComponentsInChildren<Button>();

        foreach (Button button in odpovedButtons)
        {
            if (button.GetComponent<Image>().color == Color.cyan)
            {
                return true;
            }
        }

        return false;
    }

    public void OnSubmitButtonClick()
    {
        numOfRightAnswers = 0;

        for (int i = 0; i < questions.Length; i++)
        {
            if (IsRightAnswer(questions[i]))
            {
                numOfRightAnswers++;
            }
        }

        resultText.text = $"Poèet správnych odpovedí: {numOfRightAnswers} z {questions.Length}";
        resultPanel.gameObject.SetActive(true);       
        nextButton.gameObject.SetActive(true);
        testFinished = true;

        testRight = IsEnoughRightAnswers(numOfRightAnswers, questions.Length);
    }

    bool IsRightAnswer(GameObject question)
    {
        Button[] odpovedButtons = question.transform.Find("Answer Buttons Container").GetComponentsInChildren<Button>();

        foreach (Button button in odpovedButtons)
        {
            if (button.GetComponent<Image>().color == Color.cyan && button.CompareTag("RightAnswer"))
            {
                button.GetComponent<Image>().color = Color.green;
                return true;
            }

            if (button.GetComponent<Image>().color == Color.cyan && !button.CompareTag("RightAnswer"))
            {
                button.GetComponent<Image>().color = Color.red;                
            }
        }

        return false;
    }

    bool IsEnoughRightAnswers(int x, int all)
    {
        if (x * 100 / all >= 80)
        {
            return true;
        }

        return false;
    }

    public void OnNextButtonClick()
    {
        resultPanel.SetActive(false);
        nextButton.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);

        if (testRight)
        {
            endButton.gameObject.SetActive(true);
        }
        else if (!secondRound)
        { 
            againButton.gameObject.SetActive(true);
        }
        else if (secondRound)
        {
            OnAgainButtonClick();
            messagePanel.SetActive(true);
            showTheoryButton.gameObject.SetActive(true);
            
        }
       
    }

    public void OnAgainButtonClick()
    {    
        for (int i = 0; i < questions.Length; i++)
        {
            Button[] odpovedButtons = questions[i].transform.Find("Answer Buttons Container").GetComponentsInChildren<Button>();

            foreach (Button button in odpovedButtons)
            {
                button.GetComponent<Image>().color = Color.white;
            }
        }

        secondRound = true;
        numOfRightAnswers = 0;
        testFinished = false;
        againButton.gameObject.SetActive(false);
    }

    public void OnEndButtonClick()
    {
        finalTestOne.SetActive(false);
        buttonPilar.SetActive(true);
    }


}
