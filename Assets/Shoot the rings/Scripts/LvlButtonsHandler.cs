using UnityEngine;
using UnityEngine.UI;

public class LvlButtonsHandler : MonoBehaviour
{
    public static int lastOpenedLvl;

    public int startNum;        // первый номер на  странице уровней (определяет с какого номера начинать страницу)
    public int pageNumber;      // номер страницы с последним открыми увронем(открывается по умолчанию)
    public int buttonsCountOnPage;  // количество кнопок на странице
    public int currentPageNumber;      // номер текущей страницы
    public int anyAnimPlayed;   //  0 - анимации в покое, больше 1 - какие то анимации в движении

    public GameObject levels;

    public GameObject main;           // ссылка на главный скрипт
    public GameObject prePickButton;  // ссылка на выбраный уровень в меню
    public GameObject pagesPoints;    // ссылка на сетку точек внизу меню уровней
    public ShootTheRing mainScript;
    public Pages_points pages_Points;

    void Start()
    {
        mainScript = main.GetComponent<ShootTheRing>();
        pages_Points = pagesPoints.GetComponent<Pages_points>();
        lastOpenedLvl = mainScript.sv.stars[0];
        buttonsCountOnPage = transform.childCount;
        pageNumber = (lastOpenedLvl - 1) / buttonsCountOnPage;
        pages_Points.totalCountOfPages = pageNumber;
        pages_Points.currentPage = pageNumber;
        SetStartNum(pageNumber);

        currentPageNumber = pageNumber;
        InitLvlPage();

    }


    private void SetStartNum(int thisPageNumber)
    {
        startNum = (thisPageNumber * buttonsCountOnPage) + 1;
    }    // определяет с какого номера начинать страницу
    public void ChangeLvlPage()
    {

        for (int i = 0; i < buttonsCountOnPage; i++)
        {
            int lvlCount = (startNum + i);
            #region Rename button and change text
            transform.GetChild(i).name = "Lvl" + lvlCount.ToString();
            transform.GetChild(i).GetComponent<LvlButtonHandler>().levelNumberAfterPageChange = (lvlCount);     // новое значение будет установлено в начале след анимации
            transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().enabled = true;

            #endregion

            if (lvlCount <= lastOpenedLvl)     // когда открыт
            {
                int numOfStars = main.GetComponent<ShootTheRing>().sv.GetStarsOfLvl(lvlCount);
                transform.GetChild(i).GetComponent<Animator>().enabled = true;
                transform.GetChild(i).GetComponent<Animator>().SetTrigger("To " + numOfStars + " stars with number");
                transform.GetChild(i).GetComponent<Button>().interactable = true;
                transform.GetChild(i).GetComponent<LvlButtonHandler>().SetButtonInfo(numOfStars, lvlCount);

            }
            else
            {
                transform.GetChild(i).GetComponent<Animator>().SetTrigger("To locked");
                transform.GetChild(i).GetComponent<LvlButtonHandler>().SetButtonInfo(4, lvlCount);
                transform.GetChild(i).GetComponent<Button>().interactable = true;
            }

        }
    }
    public void InitLvlPage()
    {

        for (int i = 0; i < buttonsCountOnPage; i++)
        {
            int lvlCount = (startNum + i);
            #region Rename button and change text
            transform.GetChild(i).name = "Lvl" + lvlCount.ToString();
            transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text = (lvlCount).ToString();
            transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().enabled = true;
            transform.GetChild(i).GetComponent<Animator>().enabled = true;

            #endregion

            if (lvlCount <= lastOpenedLvl)     // когда открыт
            {
                int numOfStars = main.GetComponent<ShootTheRing>().sv.GetStarsOfLvl(lvlCount);
                transform.GetChild(i).GetComponent<Animator>().SetTrigger("reset " + numOfStars + "stars");
                transform.GetChild(i).GetComponent<Button>().interactable = true;

                transform.GetChild(i).GetComponent<LvlButtonHandler>().SetButtonInfo(numOfStars, lvlCount);

            }
            else
            {
                transform.GetChild(i).GetComponent<Animator>().SetTrigger("reset locked");

                transform.GetChild(i).GetComponent<Button>().interactable = true;
                transform.GetChild(i).GetComponent<LvlButtonHandler>().SetButtonInfo(4, lvlCount);
            }

        }
    }       // переименовывает кнопки
    public void SetTriggerWhenChangePage()
    {
        for (int i = 0; i < buttonsCountOnPage; i++)
        {
            switch (transform.GetChild(i).GetComponent<LvlButtonHandler>().currentType)
            {
                case LvlButtonHandler.ButtonType.locked:
                    transform.GetChild(i).GetComponent<Animator>().SetTrigger("From locked");
                    break;
                case LvlButtonHandler.ButtonType.starsCount_0:
                    SetAnimTrigger(i, "0");
                    break;
                case LvlButtonHandler.ButtonType.starsCount_1:
                    SetAnimTrigger(i, "1");
                    break;
                case LvlButtonHandler.ButtonType.starsCount_2:
                    SetAnimTrigger(i, "2");
                    break;
                case LvlButtonHandler.ButtonType.starsCount_3:
                    SetAnimTrigger(i, "3");
                    break;
            }
        }
    }

    private void SetAnimTrigger(int i, string type)
    {
        if (transform.GetChild(i).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IDLE Play (" + type + " stars)"))
        {
            transform.GetChild(i).GetComponent<Animator>().SetTrigger("From_" + type + "_stars with play");
        }
        else
        {
            transform.GetChild(i).GetComponent<Animator>().SetTrigger("From " + type + " stars with number");
        }
    }

    public void UpdateLvlButton(int lvl, int numberOfStars)     // обновляет колво звезд на пройденом уровне
    {

        if (!(lvl > (startNum - 1 + buttonsCountOnPage)))        //проверка, что уровень на котором нужно обновить иконку на текущей странице
        {
            int buttonIndex = lvl;


            if (buttonIndex > buttonsCountOnPage - 1)
            {
                buttonIndex -= (currentPageNumber * buttonsCountOnPage);
            }
            buttonIndex--;
            Debug.Log(buttonIndex);

            transform.GetChild(buttonIndex).GetComponent<LvlButtonHandler>().SetButtonInfo(numberOfStars, lvl);
            transform.GetChild(buttonIndex).transform.GetChild(0).GetComponent<Text>().enabled = true;

        }
        else
        {
            Pages_points pages_Points = pagesPoints.GetComponent<Pages_points>();
            ShootTheRing mainScript = main.GetComponent<ShootTheRing>();

            lastOpenedLvl = mainScript.sv.stars[0];

            pageNumber = (lastOpenedLvl - 1) / buttonsCountOnPage;
            pages_Points.totalCountOfPages = pageNumber;
            pages_Points.currentPage = pageNumber;
            pages_Points.Init();
            SetStartNum(pageNumber);

            currentPageNumber = pageNumber;
            InitLvlPage();
        }
    }
    public void OnDisable() // снимает выбор уровня при выходе с меню уровней
    {
        prePickButton = null;
    }
    public void SetprePickButton(GameObject button) // устанавливает предварительный выбор уровня, а по второму нажатию на него - запускает уровень
    {
        if (prePickButton == null && button != null)
        {
            if (mainScript.sv.sound)
            {
                AudioManager.PlaySound(AudioManager.Sounds.MenuLvlClick);
            }
            //Debug.Log("было нал стало не нал");
            prePickButton = button;
        }
        else if (prePickButton != button && button != null)  // выбран другой уровень
        {
            if (mainScript.sv.sound)
            {
                AudioManager.PlaySound(AudioManager.Sounds.MenuLvlClick);
            }
            //Debug.Log("было не нал стало тоже не нал");
            prePickButton.GetComponent<LvlButtonHandler>().ReversAnim();
            prePickButton = button;

        }
        else if (prePickButton != null && button != null)
        {
            if (mainScript.sv.sound)
            {
                AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
            }
            //Debug.Log("было не нал выбран тот же заново - уровень запущен");

            levels.GetComponent<Levels>().Startlvl(button.name);
        }
        else
        {
            prePickButton = button;
        }
    }
    public void ChangeAnyAnimPlayedToFalse(int count)
    {
        anyAnimPlayed = -count;
    }
    public void ChangeAnyAnimPlayedToTrue(int count)
    {
        anyAnimPlayed = +count;
    }
    public void OpenNextPage()
    {

        if (currentPageNumber != pageNumber && anyAnimPlayed == 0) // проверка что мы не на последней странице и что анимации в покое
        {
            if (mainScript.sv.sound)
            {
                AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
            }
            lastOpenedLvl = mainScript.sv.stars[0];
            pagesPoints.GetComponent<Pages_points>().NextPage();
            SetprePickButton(null);
            SetTriggerWhenChangePage();
            currentPageNumber++;
            SetStartNum(currentPageNumber);
            ChangeLvlPage();
        }
    }
    public void OpenPreviousPage()
    {

        if (currentPageNumber != 0 && anyAnimPlayed == 0)     // проверка что мы не на первой странице и что анимации в покое
        {
            if (mainScript.sv.sound)
            {
                AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
            }
            lastOpenedLvl = mainScript.sv.stars[0];
            pagesPoints.GetComponent<Pages_points>().PreviousPage();
            SetprePickButton(null);
            SetTriggerWhenChangePage();
            currentPageNumber--;
            SetStartNum(currentPageNumber);
            ChangeLvlPage();
        }

    }
}

