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


                if (!transform.GetChild(0).gameObject.activeSelf)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                break;

            case ButtonType.starsCount_1:

                animator.SetTrigger("reset 1stars");

                if (!transform.GetChild(0).gameObject.activeSelf)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                break;

            case ButtonType.starsCount_2:

                animator.SetTrigger("reset 2stars");

                if (!transform.GetChild(0).gameObject.activeSelf)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                break;

            case ButtonType.starsCount_3:

                animator.SetTrigger("reset 3stars");

                if (!transform.GetChild(0).gameObject.activeSelf)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                break;

            case ButtonType.locked:
                animator.SetTrigger("reset locked");
                break;
        }
    }       // при выходе из меню уровней прерывает все анимации и выставляет IDLE


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
        animator.ResetTrigger("reset 0stars");
        animator.ResetTrigger("reset 1stars");
        animator.ResetTrigger("reset 2stars");
        animator.ResetTrigger("reset 3stars");

        animator.SetFloat("speed", 1);
        animator.SetFloat("speed_1stars", 1);
        animator.SetFloat("speed_2stars", 1);
        animator.SetFloat("speed_3stars", 1);

        switch (currentType)
        {
            case ButtonType.locked:
                animator.SetTrigger("locked");
                break;

            case ButtonType.starsCount_0:
                transform.parent.GetComponent<LvlButtonsHandler>().SetprePickButton(gameObject);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (0 stars)") || (animator.GetCurrentAnimatorStateInfo(0).IsName("To number anim (0 stars)")))
                {
                    animator.ResetTrigger("0stars");
                }
                else
                {
                    animator.SetTrigger("0stars");
                }
                break;

            case ButtonType.starsCount_1:
                transform.parent.GetComponent<LvlButtonsHandler>().SetprePickButton(gameObject);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (1 stars)") || (animator.GetCurrentAnimatorStateInfo(0).IsName("To number anim (1 stars)")))
                {
                    animator.ResetTrigger("1stars");
                }
                else
                {
                    animator.SetTrigger("1stars");
                }
                break;

            case ButtonType.starsCount_2:
                transform.parent.GetComponent<LvlButtonsHandler>().SetprePickButton(gameObject);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (2 stars)") || (animator.GetCurrentAnimatorStateInfo(0).IsName("To number anim (2 stars)")))
                {
                    animator.ResetTrigger("2stars");
                }
                else
                {
                    animator.SetTrigger("2stars");
                }
                break;

            case ButtonType.starsCount_3:
                transform.parent.GetComponent<LvlButtonsHandler>().SetprePickButton(gameObject);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (3 stars)") || (animator.GetCurrentAnimatorStateInfo(0).IsName("To number anim (3 stars)")))
                {
                    animator.ResetTrigger("3stars");
                }
                else
                {
                    animator.SetTrigger("3stars");
                }
                break;
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (0 stars)"))
        {
            animator.SetFloat("speed", -1);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE Play (0 stars)"))
        {
            animator.SetTrigger("0stars reversed");
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (1 stars)"))
        {
            animator.SetFloat("speed_1stars", -1);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE Play (1 stars)"))
        {
            animator.SetTrigger("1stars reversed");
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (2 stars)"))
        {
            animator.SetFloat("speed_2stars", -1);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE Play (2 stars)"))
        {
            animator.SetTrigger("2stars reversed");
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("To play anim (3 stars)"))
        {
            animator.SetFloat("speed_3stars", -1);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE Play (3 stars)"))
        {
            animator.SetTrigger("3stars reversed");
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
