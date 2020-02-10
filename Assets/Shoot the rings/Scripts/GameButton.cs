using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    private ShootTheRing shootTheRing;
    private GameButtonsHandler gameButtonsHandler;

    public LayerMask mask;
    private Ray mouseRay1;
    private RaycastHit rayHit;
    // position of the raycast on the screen
    public float posX;
    public float posY;

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



    //private void checkTouch(Vector3 pos)
    //{
    //    Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
    //    Vector2 touchPos = new Vector2(wp.x, wp.y);
    //    Collider2D hit = Physics2D.OverlapPoint(touchPos);

    //    if (hit && hit == gameObject.GetComponent<Collider2D>())
    //    {

    //        // do something
    //    }
    //private void Update()
    //{
    //    if (Input.touchCount == 1)
    //    {
    //        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    //        Vector2 touchPos = new Vector2(wp.x, wp.y);
    //        if (collider2D == Physics2D.OverlapPoint(touchPos))
    //        {
    //        }

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.name);

                    
                       
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
                }
            }
        }
    }

    //private void OnMouseDown()
    //{
    //    gameButtonsHandler.SetActiveButton(gameObject);

    //    switch (button)
    //    {
    //        case GameButtonStates.blue:
    //            shootTheRing.BlueMod_Click();
    //            break;

    //        case GameButtonStates.green:
    //            shootTheRing.GreenMod_Click();

    //            break;
    //        case GameButtonStates.red:
    //            shootTheRing.redMod_Click();
    //            break;
    //    }

    //}

    public void ButtonClick()
    {

        Debug.Log("test");
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
