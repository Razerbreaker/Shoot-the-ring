using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    private ShootTheRing shootTheRing;
    private GameButtonsHandler gameButtonsHandler;


    public enum GameButtonStates
    {
        blue,
        green,
        red
    }
    public GameButtonStates button;

    private void Start()
    {
        shootTheRing = GameObject.FindGameObjectWithTag("main").GetComponent<ShootTheRing>();
        gameButtonsHandler = transform.parent.GetComponent<GameButtonsHandler>();
    }

    private void OnMouseDown()
    {
        gameButtonsHandler.SetActiveButton(gameObject);

        switch (button)
        {
            case GameButtonStates.blue:
                shootTheRing.BlueMod_Click();
                break;

            case GameButtonStates.green:
                shootTheRing.GreenMod_Click();

                break;
            case GameButtonStates.red:
                shootTheRing.redMod_Click();
                break;
        }

    }

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
