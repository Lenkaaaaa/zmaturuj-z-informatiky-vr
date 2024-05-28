using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Credit to @ValemTutorials a youtube channel focusing on learning about Immersive Technologies
public class SetTurnTypeFromPlayerPref : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    // Start is called before the first frame update
    void Start()
    {
        ApplyPlayerPref();
    }

    public void ApplyPlayerPref()
    {
        if(PlayerPrefs.HasKey("turn"))
        {
            int value = PlayerPrefs.GetInt("turn");
            if(value == 0)
            {
                snapTurn.rightHandSnapTurnAction.action.Enable();
                continuousTurn.rightHandTurnAction.action.Disable();
            }
            else if(value == 1)
            {
                snapTurn.rightHandSnapTurnAction.action.Disable();
                continuousTurn.rightHandTurnAction.action.Enable();
            }
        }
    }
}
