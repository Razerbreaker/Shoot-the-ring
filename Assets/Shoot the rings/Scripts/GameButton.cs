using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ShootTheRing shootTheRing;
    private GameButtonsHandler gameButtonsHandler;
    public Sprite active;
    public Sprite noactive;

    //private int lastFrameCount;


    public enum GameButtonStates
    {
        blue,
        green,
        red
    }
    public GameButtonStates button;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shootTheRing = GameObject.FindGameObjectWithTag("main").GetComponent<ShootTheRing>();
        gameButtonsHandler = transform.parent.GetComponent<GameButtonsHandler>();
    }


    private void OnMouseDown()
    {
        //if ((Time.frameCount - lastFrameCount) > 20)
        //{
        //    lastFrameCount = Time.frameCount;
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
        //}

    }

    public void ResetButton()
    {
        spriteRenderer.sprite = noactive;
    }

    public void SetButton()
    {
        spriteRenderer.sprite = active;
    }
}
