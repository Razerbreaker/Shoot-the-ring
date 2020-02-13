using UnityEngine;

public class GameButtonsHandler : MonoBehaviour
{

    private GameObject[] buttons;
    private GameObject activeButton;

    private void Start()
    {
        buttons = new GameObject[3];
        buttons[0] = transform.GetChild(0).gameObject;      //red
        buttons[1] = transform.GetChild(1).gameObject;      //green
        buttons[2] = transform.GetChild(2).gameObject;      //blue
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
            button.GetComponent<GameButton>().SetButton();
            activeButton = null;
        }

        else

            if (activeButton != button)
        {
            activeButton.GetComponent<GameButton>().SetButton();
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
    public void ResetRedForLvl3()
    {
        buttons[0].GetComponent<GameButton>().ResetButton();
    }

    public void StartAppearAnimation()
    {
        buttons[0].GetComponent<Animator>().SetTrigger("red");
        buttons[1].GetComponent<Animator>().SetTrigger("green");
        buttons[2].GetComponent<Animator>().SetTrigger("blue");
    }

    public void SetONOFF(bool trueORfalse)
    {
        for (int i = 0; i < 3; i++)
        {
            buttons[i].GetComponent<SpriteRenderer>().enabled = trueORfalse;
            buttons[i].GetComponent<CircleCollider2D>().enabled = trueORfalse;
        }
    }

    public void SetOffSeparately(int index, bool trueORfalse)
    {
        buttons[index].GetComponent<SpriteRenderer>().enabled = trueORfalse;
        buttons[index].GetComponent<CircleCollider2D>().enabled = trueORfalse;
    }

    public void SetInteractable(bool trueORfalse)
    {
        for (int i = 0; i < 3; i++)
        {
            buttons[i].GetComponent<CircleCollider2D>().enabled = trueORfalse;
        }
    }

    public void StartAppearAnimationGreenOnly()
    {
        buttons[1].GetComponent<Animator>().SetTrigger("green");
    }
    public void StartAppearAnimationBlueOnly()
    {
        buttons[2].GetComponent<Animator>().SetTrigger("blue");
    }

    public void ResetGreen()
    {
        buttons[1].GetComponent<GameButton>().ResetButton();
    }
    public void ResetBlue()
    {
        buttons[2].GetComponent<GameButton>().ResetButton();
    }
}
