using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwitchL1 : MonoBehaviour
{
    public TheoryOne theoryOne;
    public FinalTest finalTestOne;
    public GameObject buttonPilar;

    public void Start()
    {
        finalTestOne.gameObject.SetActive(false);
        buttonPilar.SetActive(false);        
    }

    private void Update()
    {
        theoryOne.finalTestButton.onClick.AddListener(OnFinalTestButtonClick);
        finalTestOne.showTheoryButton.onClick.AddListener(OnShowTheoryButtonClick);       
    }

    private void OnShowTheoryButtonClick()
    {
        theoryOne.gameObject.SetActive(true);
        StartCoroutine(HideFinalTest());

        theoryOne.finalTestButton.gameObject.SetActive(false);
        theoryOne.panelContainerFinal.gameObject.SetActive(false);

        theoryOne.NewStart();
        
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

        theoryOne.gameObject.SetActive(false);
    }
}
