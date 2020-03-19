using System;
using UnityEngine;
using UnityEngine.UI;

public class LvlButtonHandler : MonoBehaviour
{
    public enum ButtonType
    {
        starsCount_0,
        starsCount_1,
        starsCount_2,
        starsCount_3,
        locked
    }


    public Image image;
    public ButtonType currentType;
    public int levelNumber;                    // тот который сейчас отображается на кнопке
    public int levelNumberAfterPageChange;     // тот который будет после анимации смены страницы
    public Animator animator;
    public bool passed = false; // нужна для того что бы при запуске в первый раз не срабатывало
    public int count;  // сколько раз на этой кнопке начиналась анимация

    public void SetNextLvlNumber()
    {
        transform.GetChild(0).GetComponent<Text>().text = levelNumberAfterPageChange.ToString();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnEnable()
    {
        transform.GetChild(0).GetComponent<Text>().text = levelNumber.ToString();

        if (passed == true)
        {
            ResetAll();
        }
        passed = true;
    }

    public void OnDisable()  // при выходе из меню уровней восстанавливает непрозрачность
    {
        image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
    }
    public void ResetAll()
    {
        switch (currentType)
        {
            case ButtonType.starsCount_0:
                animator.SetTrigger("reset 0stars");
                ResetTextOnButton();
                break;

            case ButtonType.starsCount_1:
                animator.SetTrigger("reset 1stars");
                ResetTextOnButton();
                break;

            case ButtonType.starsCount_2:
                animator.SetTrigger("reset 2stars");
                ResetTextOnButton();
                break;

            case ButtonType.starsCount_3:
                animator.SetTrigger("reset 3stars");
                ResetTextOnButton();
                break;

            case ButtonType.locked:
                animator.SetTrigger("reset locked");
                break;
        }
    }       // при выходе из меню уровней прерывает все анимации и выставляет IDLE

    private void ResetTextOnButton()
    {
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// устанавливает новый тип кнопки и цифру уровня на ней
    /// </summary>
    /// <param name="buttonTypeIndex">0-3 открыт и число указывает кол-во звезд. 4 -- закрыт</param>
    /// <param name="level">число, которое будет отображаться на кнопке</param>
    public void SetButtonInfo(int buttonTypeIndex, int level)
    {
        Array type = Enum.GetValues(typeof(ButtonType));
        currentType = (ButtonType)type.GetValue(buttonTypeIndex);
        levelNumber = level;
    }
    public void Onclick()
    {
        animator.enabled = true;

        for (int i = 0; i <= 3; i++)
        {
            animator.ResetTrigger("reset " + i.ToString() + "stars");
            animator.SetFloat("speed_" + i.ToString() + "stars", 1);
        }

        switch (currentType)
        {
            case ButtonType.locked:
                animator.SetTrigger("locked");
                break;

            case ButtonType.starsCount_0:
                ClickButton("0");
                break;

            case ButtonType.starsCount_1:
                ClickButton("1");
                break;

            case ButtonType.starsCount_2:
                ClickButton("2");
                break;

            case ButtonType.starsCount_3:
                ClickButton("3");
                break;
        }
    }

    private void ClickButton(string type)
    {
        transform.parent.GetComponent<LvlButtonsHandler>().SetprePickButton(gameObject);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (" + type + " stars)") || (animator.GetCurrentAnimatorStateInfo(0).IsName("To number anim (" + type + " stars)")))
        {
            animator.ResetTrigger(type + "stars");
        }
        else
        {
            animator.SetTrigger(type + "stars");
        }
    }

    public void DisableText()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ReversAnim()
    {
        for (int i = 0; i <= 3; i++)
        {
            ReverseAnim(i.ToString());
        }
    }

    private void ReverseAnim(string type)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (" + type + " stars)"))
        {
            animator.SetFloat("speed_" + type + "stars", -1);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE Play (" + type + " stars)"))
        {
            animator.SetTrigger(type + "stars reversed");
        }
    }

    public void ChangeAnyAnimPlayedToFalse()
    {
        transform.GetComponentInParent<LvlButtonsHandler>().ChangeAnyAnimPlayedToFalse(count);
        count = 0;
    }
    public void ChangeAnyAnimPlayedToTrue()
    {
        transform.GetComponentInParent<LvlButtonsHandler>().ChangeAnyAnimPlayedToTrue(1);

    }

    public void SetNotinteractable()
    {
        GetComponent<Button>().interactable = false;
    }
    public void Setinteractable()
    {
        GetComponent<Button>().interactable = true;
    }
}
