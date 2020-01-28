using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public GameObject bow;
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


    private IEnumerator Lvl1()
    {
        SetRingsCountOnlvl(6);

        shootTheRing.DisableColorButton(0, false);  // - red button blocked
        shootTheRing.DisableColorButton(1, false);  // - green button blocked
        shootTheRing.DisableColorButton(2, false);  // - blue button blocked

        if (!shootTheRing.sv.tutorialLvl1Passed)
        {
            shootTheRing.AllowToShoot = false;
        }   // блокируем стрелбу из лука до прохождения туториала
        yield return new WaitForSeconds(1f);

        shootTheRing.createRing(-2, Ring.RingStates.white);

        yield return new WaitForSeconds(2f);

        if (!shootTheRing.sv.tutorialLvl1Passed)
        {
            shootTheRing.StartTutorial(1);
        }       // если туториал не пройден - показываем

        yield return new WaitForSeconds(1f);
        shootTheRing.createRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.createRing(4, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.createRing(-1, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.createRing(3, Ring.RingStates.white);
        shootTheRing.createRing(0, Ring.RingStates.white);


    }
    private IEnumerator Lvl2()
    {
        shootTheRing.DisableColorButton(0, false);  // - red button blocked
        shootTheRing.DisableColorButton(1, false);  // - green button blocked
        shootTheRing.DisableColorButton(2, false);  // - blue button blocked

        SetRingsCountOnlvl(9);
        yield return new WaitForSeconds(1f);

        shootTheRing.createRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.createRing(1, Ring.RingStates.white);
        shootTheRing.createRing(-1, Ring.RingStates.white);
        yield return new WaitForSeconds(4f);
        shootTheRing.createRing(-1, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.createRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.createRing(1, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.createRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.createRing(-3, Ring.RingStates.white);
        shootTheRing.createRing(-2, Ring.RingStates.white);
    }
    private IEnumerator Lvl3()
    {
        shootTheRing.DisableColorButton(1, false);  // - green button blocked
        shootTheRing.DisableColorButton(2, false);  // - blue button blocked

        if (!shootTheRing.sv.tutorialLvl3Passed)
        {
            shootTheRing.AllowToShoot = false;
        }   // блокируем стрелбу из лука до прохождения туториала

        SetRingsCountOnlvl(1);
        yield return new WaitForSeconds(0.2f);

        shootTheRing.createRing(-2, Ring.RingStates.red);
        yield return new WaitForSeconds(2f);
        if (!shootTheRing.sv.tutorialLvl1Passed)
        {
            shootTheRing.StartTutorial(3);
        }       // если туториал не пройден - показываем
        shootTheRing.StartTutorial(3);
        yield return new WaitForSeconds(0.1f);



    }
    private IEnumerator Lvl4()
    {
        shootTheRing.DisableColorButton(1, false);  // - green button blocked
        shootTheRing.DisableColorButton(2, false);  // - blue button blocked

        SetRingsCountOnlvl(5);
        yield return new WaitForSeconds(0.1f);

        shootTheRing.createRing(5, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.createRing(0, Ring.RingStates.red);
        yield return new WaitForSeconds(3f);
        shootTheRing.createRing(4, Ring.RingStates.blue);
        yield return new WaitForSeconds(0.1f);
        shootTheRing.createRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.createRing(1, Ring.RingStates.green);


    }
    private IEnumerator Lvl5()
    {
        shootTheRing.DisableColorButton(2, false);  // - blue button blocked

        SetRingsCountOnlvl(9);
        yield return new WaitForSeconds(0.1f);

        shootTheRing.createRing(-4, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.createRing(-3, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.createRing(-2, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.createRing(-1, Ring.RingStates.white);
        yield return new WaitForSeconds(4f);
        shootTheRing.createRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(5f);
        shootTheRing.createRing(1, Ring.RingStates.white);
        yield return new WaitForSeconds(6f);
        shootTheRing.createRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(7f);
        shootTheRing.createRing(3, Ring.RingStates.white);
        yield return new WaitForSeconds(8f);
        shootTheRing.createRing(4, Ring.RingStates.white);
    }
    private IEnumerator Lvl6()
    {
        shootTheRing.DisableColorButton(2, false);  // - blue button blocked

        SetRingsCountOnlvl(6);
        yield return new WaitForSeconds(0.2f);

        shootTheRing.createRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(1, Ring.RingStates.white);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-1, Ring.RingStates.white);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-2, Ring.RingStates.white);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-3, Ring.RingStates.white);
    }
    private IEnumerator Lvl7()
    {
        SetRingsCountOnlvl(6);
        yield return new WaitForSeconds(0.2f);

        shootTheRing.createRing(2, Ring.RingStates.white);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(1, Ring.RingStates.red);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(0, Ring.RingStates.blue);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-1, Ring.RingStates.green);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-2, Ring.RingStates.white);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-3, Ring.RingStates.white);
    }
    private IEnumerator Lvl8()
    {
        SetRingsCountOnlvl(2);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-3, Ring.RingStates.white);
        shootTheRing.createRing(0, Ring.RingStates.white);


    }
    private IEnumerator Lvl9()
    {
        SetRingsCountOnlvl(2);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-3, Ring.RingStates.white);
        shootTheRing.createRing(0, Ring.RingStates.white);


    }
    private IEnumerator Lvl10()
    {
        SetRingsCountOnlvl(2);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-3, Ring.RingStates.white);
        shootTheRing.createRing(0, Ring.RingStates.white);
    }

    private IEnumerator Lvl18()
    {
        SetRingsCountOnlvl(2);
        yield return new WaitForSeconds(0.2f);
        shootTheRing.createRing(-3, Ring.RingStates.white);
        shootTheRing.createRing(0, Ring.RingStates.white);
    }
}
