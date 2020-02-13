using UnityEngine;

public class GameButton : MonoBehaviour
{
    public enum GameButtonStates
    {
        blue,
        green,
        red
    }
    public GameButtonStates button;


    public void ResetButton()
    {
        GetComponent<Animator>().SetTrigger("reset");
    }

    public void SetButton()
    {
        switch (button)
        {
            case GameButtonStates.blue:
                GetComponent<Animator>().SetTrigger("blue");
                break;

            case GameButtonStates.green:
                GetComponent<Animator>().SetTrigger("green");
                break;
            case GameButtonStates.red:
                GetComponent<Animator>().SetTrigger("red");
                break;
        }
    }
}
