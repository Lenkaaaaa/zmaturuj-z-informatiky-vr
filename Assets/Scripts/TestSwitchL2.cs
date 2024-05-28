using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwitchL2 : MonoBehaviour
{
    public TheoryTwo theoryTwo;
    public FinalTest finalTestOne;
    public GameObject buttonPilar;

    public void Start()
    {
        finalTestOne.gameObject.SetActive(false);
        buttonPilar.SetActive(false);
    }

    private void Update()
    {
        theoryTwo.finalTestButton.onClick.AddListener(OnFinalTestButtonClick);
        finalTestOne.showTheoryButton.onClick.AddListener(OnShowTheoryButtonClick);

    }

    private void OnShowTheoryButtonClick()
    {
        theoryTwo.gameObject.SetActive(true);
        StartCoroutine(HideFinalTest());

        theoryTwo.finalTestButton.gameObject.SetActive(false);
        theoryTwo.panelContainerFinal.gameObject.SetActive(false);

        theoryTwo.NewStart();

    }

    private IEnumerator HideFinalTest()
    {
        yield return new WaitForSeconds(2f);

        finalTestOne.gameObject.SetActive(false);
        finalTestOne.showTheoryButton.gameObject.SetActive(false);
    }

    private void OnFinalTestButtonClick()
    {
        finalTestOne.gameObject.SetActive(true);
        finalTestOne.messagePanel.SetActive(false);
        StartCoroutine(HideTheoryPanel());
    }

    private IEnumerator HideTheoryPanel()
    {
        yield return new WaitForSeconds(2f);

        theoryTwo.gameObject.SetActive(false);
    }
}
