using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonsHandler : MonoBehaviour
{

    private GameObject[] buttons;
    private GameObject activeButton;
    private Sprite[] sprites;

    private void Start()
    {
        buttons = new GameObject[3];
        buttons[0] = transform.GetChild(0).gameObject;      //red
        buttons[1] = transform.GetChild(1).gameObject;      //green
        buttons[2] = transform.GetChild(2).gameObject;      //blue

        sprites = new Sprite[6];

        sprites[0] = Resources.Load<Sprite>("Textures/Game buttons/active_r");
        sprites[1] = Resources.Load<Sprite>("Textures/Game buttons/noactive_r");

        sprites[2] = Resources.Load<Sprite>("Textures/Game buttons/active_g");
        sprites[3] = Resources.Load<Sprite>("Textures/Game buttons/noactive_g");

        sprites[4] = Resources.Load<Sprite>("Textures/Game buttons/active_b");
        sprites[5] = Resources.Load<Sprite>("Textures/Game buttons/noactive_b");


        buttons[0].GetComponent<GameButton>().active = sprites[0];
        buttons[0].GetComponent<GameButton>().noactive = sprites[1];

        buttons[1].GetComponent<GameButton>().active = sprites[2];
        buttons[1].GetComponent<GameButton>().noactive = sprites[3];

        buttons[2].GetComponent<GameButton>().active = sprites[4];
        buttons[2].GetComponent<GameButton>().noactive = sprites[5];
    }

    public void SetActiveButton(GameObject button)
    {
        if (activeButton == null)
        {
            activeButton = button;
            button.GetComponent<GameButton>().SetButton();
        }
        else

            if (activeButton == button)
        {
            button.GetComponent<GameButton>().ResetButton();
            activeButton = null;
        }

        else

            if (activeButton != button)
        {
            buttons[0].GetComponent<GameButton>().ResetButton();
            buttons[1].GetComponent<GameButton>().ResetButton();
            buttons[2].GetComponent<GameButton>().ResetButton();

            activeButton = button;
            button.GetComponent<GameButton>().SetButton();

        }


    }


    public void ResetAll()
    {
        activeButton = null;
        buttons[0].GetComponent<GameButton>().ResetButton();
        buttons[1].GetComponent<GameButton>().ResetButton();
        buttons[2].GetComponent<GameButton>().ResetButton();
    }

    public void SetONOFF(bool trueORfalse)
    {
        for (int i = 0; i < 3; i++)
        {
            buttons[i].GetComponent<SpriteRenderer>().enabled = trueORfalse;
            buttons[i].GetComponent<PolygonCollider2D>().enabled = trueORfalse;
        }
    }

    public void setOffSeparately(int index, bool trueORfalse)
    {
        buttons[index].GetComponent<SpriteRenderer>().enabled = trueORfalse;
        buttons[index].GetComponent<PolygonCollider2D>().enabled = trueORfalse;
    }

    public void SetInteractable(bool trueORfalse)
    {
        for (int i = 0; i < 3; i++)
        {
            buttons[i].GetComponent<PolygonCollider2D>().enabled = trueORfalse;
        }
    }

}
