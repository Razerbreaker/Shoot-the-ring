using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public GameObject bow;
    public GameObject attentionMark;
    ShootTheRing shootTheRing;
    public int currentLevel;

    private int ringAmountOnLvl; //общее кол-во колец на уровень

    public int RingAmountOnLvl { get => ringAmountOnLvl; set => ringAmountOnLvl = value; }

    public void Start()
    {
        shootTheRing = GameObject.FindGameObjectWithTag("main").GetComponent<ShootTheRing>();
    }

    public void Startlvl(string lvlNumber)
    {
        shootTheRing.PrepareLvl();
        StartCoroutine(lvlNumber);
        shootTheRing.WriteOpenedLvl(lvlNumber);
    }

    public void Stoplvl(string lvlNumber)
    {
        StopCoroutine(lvlNumber);
        RingAmountOnLvl = 0;
        shootTheRing.Breaklvl();
    }

    public void RingsCounterDec()
    {
        RingAmountOnLvl--;
    }

    public void SetRingsCountOnlvl(int count)
    {
        RingAmountOnLvl = count;
    }

    //                                     Создание уровня

    //      SetRingsCountOnlvl(5);       нужно указать сколько общее колво колец на уровень
    //      yield return new WaitForSeconds(0.2f)       нужно указать паузу в секундах для задержки появления колец
    //      ShootTheRing.createRing(-2, Ring.RingStates.white);      первое число - позиция X на экране(x от -2 до 5),  второе - тип кольца Ring.RingStates.   white, blue, red, green

    //      attentionMark.GetComponent<Attention>().CallAttention(2);
    //      shootTheRing.CreateRing(2, Ring.RingStates.green, 0.003f);

    private IEnumerator Lvl1()
    {
        SetRingsCountOnlvl(6);

        shootTheRing.SwitchOnOffColorButton(0, false);  // - red button blocked
        shootTheRing.SwitchOnOffColorButton(1, false);  // - green button blocked
        shootTheRing.SwitchOnOffColorButton(2, false);  // - blue button blocked

        if (!shootTheRing.sv.tutorialLvl1Passed)
        {
            shootTheRing.AllowToShoot = false;
        }   // блокируем стрелбу из лука до прохождения туториала
        yield return new WaitForSeconds(1f);

        shootTheRing.CreateRing(-2, Ring.RingStates.white);

        yield return new WaitForSeconds(2f);

        if (!shootTheRing.sv.tutorialLvl1Passed)
        {
            shootTheRing.StartTutorial(1);
        }       // если туториал не пройден - показываем

        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(4, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(-1, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(3, Ring.RingStates.white);
        shootTheRing.CreateRing(0, Ring.RingStates.white);


    }
    private IEnumerator Lvl2()
    {
        shootTheRing.SwitchOnOffColorButton(0, false);  // - red button blocked
        shootTheRing.SwitchOnOffColorButton(1, false);  // - green button blocked
        shootTheRing.SwitchOnOffColorButton(2, false);  // - blue button blocked

        SetRingsCountOnlvl(9);
        yield return new WaitForSeconds(1f);

        shootTheRing.CreateRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(1, Ring.RingStates.white);
        shootTheRing.CreateRing(-1, Ring.RingStates.white);
        yield return new WaitForSeconds(4f);
        shootTheRing.CreateRing(-1, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(1, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(-3, Ring.RingStates.white);
        shootTheRing.CreateRing(-2, Ring.RingStates.white);
    }
    private IEnumerator Lvl3()
    {
        SetRingsCountOnlvl(8);

        shootTheRing.SwitchOnOffColorButton(1, false);  // - green button blocked
        shootTheRing.SwitchOnOffColorButton(2, false);  // - blue button blocked

        if (!shootTheRing.sv.tutorialLvl3Passed)
        {
            shootTheRing.SwitchOnOffColorButton(0, false);  // - red button blocked
            shootTheRing.AllowToShoot = false;
        }   // блокируем стрелбу из лука до прохождения туториала и выключаем красную кнопку

        yield return new WaitForSeconds(0.2f);
        if (!shootTheRing.sv.tutorialLvl3Passed)
        {
            shootTheRing.gameButtonsHandler.GetComponent<GameButtonsHandler>().ResetAll();

        } // костыль, после небольшой паузы ресетим анимацию появления красной кнопки

        shootTheRing.CreateRing(-2, Ring.RingStates.red);
        yield return new WaitForSeconds(2f);
        if (!shootTheRing.sv.tutorialLvl3Passed)
        {
            shootTheRing.StartTutorial(3);

        }       // если туториал не пройден - показываем
        yield return new WaitForSeconds(0.1f);

        shootTheRing.CreateRing(4, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(4, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(-3, Ring.RingStates.green);

        yield return new WaitForSeconds(3f);
        shootTheRing.gameButtonsHandler.GetComponent<GameButtonsHandler>().ResetGreen();
        yield return new WaitForSeconds(0.2f);
        shootTheRing.SwitchOnOffColorButton(1, true);  // - green button unblocked
        shootTheRing.gameButtonsHandler.GetComponent<GameButtonsHandler>().StartAppearAnimationGreenOnly();

        shootTheRing.CreateRing(0, Ring.RingStates.red);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(3, Ring.RingStates.white);


    }
    private IEnumerator Lvl4()
    {
        shootTheRing.SwitchOnOffColorButton(2, false);  // - blue button blocked

        SetRingsCountOnlvl(10);
        yield return new WaitForSeconds(0.1f);

        shootTheRing.CreateRing(4, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);

        shootTheRing.CreateRing(0, Ring.RingStates.red);

        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(4, Ring.RingStates.white);

        yield return new WaitForSeconds(0.1f);
        attentionMark.GetComponent<Attention>().CallAttention(2);
        shootTheRing.CreateRing(2, Ring.RingStates.green, 0.002f);

        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(1, Ring.RingStates.white);

        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(-2, Ring.RingStates.red);

        shootTheRing.CreateRing(2, Ring.RingStates.green);

        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(0, Ring.RingStates.white);
        shootTheRing.CreateRing(1, Ring.RingStates.white);
        shootTheRing.CreateRing(2, Ring.RingStates.white);

    }       
    private IEnumerator Lvl5()
    {
        shootTheRing.SwitchOnOffColorButton(2, false);  // - blue button blocked

        SetRingsCountOnlvl(12);
        yield return new WaitForSeconds(0.1f);

        shootTheRing.CreateRing(-4, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(-3, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(-1, Ring.RingStates.red);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(-3, Ring.RingStates.green);
        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(1, Ring.RingStates.red);
        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(2, Ring.RingStates.white);
        attentionMark.GetComponent<Attention>().CallAttention(3);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(3, Ring.RingStates.red,0.003f);
        shootTheRing.CreateRing(4, Ring.RingStates.white);
        shootTheRing.CreateRing(-2, Ring.RingStates.white);
        yield return new WaitForSeconds(4f);
        shootTheRing.CreateRing(3, Ring.RingStates.green);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(0, Ring.RingStates.white);

    } 
    private IEnumerator Lvl6()
    {
        SetRingsCountOnlvl(13);
        shootTheRing.SwitchOnOffColorButton(2, false);  // - blue button blocked

        yield return new WaitForSeconds(0.2f);


        shootTheRing.CreateRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(0, Ring.RingStates.red);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(2, Ring.RingStates.green);

        shootTheRing.CreateRing(-3, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(0, Ring.RingStates.red);
        yield return new WaitForSeconds(2);
        shootTheRing.CreateRing(3, Ring.RingStates.white);

        shootTheRing.CreateRing(-3, Ring.RingStates.blue);
        yield return new WaitForSeconds(3f);
        shootTheRing.gameButtonsHandler.GetComponent<GameButtonsHandler>().ResetBlue();
        yield return new WaitForSeconds(0.2f);
        shootTheRing.SwitchOnOffColorButton(2, true);
        shootTheRing.gameButtonsHandler.GetComponent<GameButtonsHandler>().StartAppearAnimationBlueOnly();
        yield return new WaitForSeconds(2f);

        shootTheRing.CreateRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(0.5f);

        shootTheRing.CreateRing(1, Ring.RingStates.red);
        yield return new WaitForSeconds(0.5f);

        shootTheRing.CreateRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(0.5f);

        shootTheRing.CreateRing(-1, Ring.RingStates.blue);
        yield return new WaitForSeconds(0.5f);

        shootTheRing.CreateRing(-2, Ring.RingStates.green);
        yield return new WaitForSeconds(0.5f);

        shootTheRing.CreateRing(-3, Ring.RingStates.white);
    }      //done
    private IEnumerator Lvl7()
    {
        SetRingsCountOnlvl(10);
        yield return new WaitForSeconds(0.2f);

        shootTheRing.CreateRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.CreateRing(1, Ring.RingStates.red);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.CreateRing(0, Ring.RingStates.blue);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.CreateRing(-1, Ring.RingStates.green);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.CreateRing(-2, Ring.RingStates.white);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.CreateRing(-3, Ring.RingStates.white);
    }
    private IEnumerator Lvl8()
    {
        SetRingsCountOnlvl(2);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.CreateRing(-3, Ring.RingStates.white);
        shootTheRing.CreateRing(0, Ring.RingStates.white);


    }
    private IEnumerator Lvl9()
    {
        SetRingsCountOnlvl(2);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.CreateRing(-3, Ring.RingStates.white);
        shootTheRing.CreateRing(0, Ring.RingStates.white);


    }
    private IEnumerator Lvl10()
    {
        SetRingsCountOnlvl(2);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.CreateRing(-3, Ring.RingStates.white);
        shootTheRing.CreateRing(0, Ring.RingStates.white);
    }

    private IEnumerator Lvl18()
    {
        SetRingsCountOnlvl(2);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.CreateRing(-3, Ring.RingStates.white);
        shootTheRing.CreateRing(0, Ring.RingStates.white);
    }
}
