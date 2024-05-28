using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPushOpenDoor : MonoBehaviour
{
    public Animator animator;
    public string boolName = "Open";

    void Start()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => OpenTheDoor());
    }

    public void OpenTheDoor()
    {
        bool isOpen = animator.GetBool(boolName);
        animator.SetBool(boolName, !isOpen);
    }
}
