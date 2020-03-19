using System.Collections;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject attentionMark;
    private ShootTheRing shootTheRing;
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
        shootTheRing.Breaklvl();
    }

    //                                     Создание уровня

    //      SetRingsCountOnlvl(5);       нужно указать сколько общее колво колец на уровень
    //      yield return new WaitForSeconds(0.2f)       нужно указать паузу в секундах для задержки появления колец
    //      ShootTheRing.createRing(-2, Ring.RingStates.white);      первое число - позиция X на экране(x от -2 до 5),  второе - тип кольца Ring.RingStates.   white, blue, red, green

    //      attentionMark.GetComponent<Attention>().CallAttention(2);
    //      shootTheRing.CreateRing(2, Ring.RingStates.green, 0.003f);

    private IEnumerator Lvl1()
    {

        shootTheRing.SetRingCountToWinLvl(6);

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

        shootTheRing.SetRingCountToWinLvl(9);

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
        shootTheRing.SetRingCountToWinLvl(8);

        shootTheRing.SwitchOnOffColorButton(1, false);  // - green button blocked
        shootTheRing.SwitchOnOffColorButton(2, false);  // - blue button blocked

        if (!shootTheRing.sv.tutorialLvl3Passed)
        {
            shootTheRing.SwitchOnOffColorButton(0, false);  // - red button blocked
            shootTheRing.AllowToShoot = false;
        }   // блокируем стрелбу из лука до прохождения туториала и выключаем красную кнопку

        yield return new WaitForSeconds(1f);
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
        shootTheRing.SetRingCountToWinLvl(10);
        yield return new WaitForSeconds(1f);


        shootTheRing.CreateRing(4, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);

        shootTheRing.CreateRing(0, Ring.RingStates.red);

        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(4, Ring.RingStates.white);

        yield return new WaitForSeconds(0.1f);
        attentionMark.GetComponent<Attention>().CallAttention(2);
        shootTheRing.CreateRing(2, Ring.RingStates.green, 0, 0, 0, 0.002f);

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

        shootTheRing.SetRingCountToWinLvl(12);

        yield return new WaitForSeconds(1f);

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
        shootTheRing.CreateRing(3, Ring.RingStates.red, 0, 0, 0, 0.003f);
        shootTheRing.CreateRing(4, Ring.RingStates.white);
        shootTheRing.CreateRing(-2, Ring.RingStates.white);
        yield return new WaitForSeconds(4f);
        shootTheRing.CreateRing(3, Ring.RingStates.green);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(0, Ring.RingStates.white);

    }
    private IEnumerator Lvl6()
    {
        shootTheRing.SetRingCountToWinLvl(13);

        shootTheRing.SwitchOnOffColorButton(2, false);  // - blue button blocked

        yield return new WaitForSeconds(1f);


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
    }
    private IEnumerator Lvl7()
    {
        shootTheRing.SetRingCountToWinLvl(11);
        yield return new WaitForSeconds(1f);

        shootTheRing.CreateRing(-3, Ring.RingStates.white);
        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(4, Ring.RingStates.green);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(0.5f, Ring.RingStates.white);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(0.5f, Ring.RingStates.red);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(0.5f, Ring.RingStates.blue);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(0.5f, Ring.RingStates.green);
        yield return new WaitForSeconds(4f);
        shootTheRing.CreateRing(-2, Ring.RingStates.green);
        shootTheRing.CreateRing(2, Ring.RingStates.white, 4, 6, -0.0015f);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(2, Ring.RingStates.white, 4, 6, -0.0015f);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(2, Ring.RingStates.white, 4, 6, -0.0015f);
        shootTheRing.CreateRing(5, Ring.RingStates.blue);
    }
    private IEnumerator Lvl8()
    {
        shootTheRing.SetRingCountToWinLvl(8);
        yield return new WaitForSeconds(1f);

        shootTheRing.CreateRing(-3, Ring.RingStates.white);
        shootTheRing.CreateRing(0, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);

        shootTheRing.CreateRing(1, Ring.RingStates.green);
        shootTheRing.CreateRing(2, Ring.RingStates.red);
        shootTheRing.CreateRing(3, Ring.RingStates.white);
        yield return new WaitForSeconds(5f);

        shootTheRing.CreateRing(1, Ring.RingStates.red, 2, 3, 0.0015f);
        shootTheRing.CreateRing(1, Ring.RingStates.green, 3, 3, 0.0015f);
        shootTheRing.CreateRing(1, Ring.RingStates.blue, 4, 3, 0.0015f);
    }
    private IEnumerator Lvl9()
    {
        shootTheRing.SetRingCountToWinLvl(12);

        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(-4, Ring.RingStates.white);
        shootTheRing.CreateRing(4, Ring.RingStates.white);
        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(-4, Ring.RingStates.white);
        shootTheRing.CreateRing(4, Ring.RingStates.white);

        shootTheRing.CreateRing(-1, Ring.RingStates.red);
        shootTheRing.CreateRing(1, Ring.RingStates.red);



        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(-4, Ring.RingStates.white);
        shootTheRing.CreateRing(4, Ring.RingStates.white);
        shootTheRing.CreateRing(0, Ring.RingStates.green);

        yield return new WaitForSeconds(3f);
        shootTheRing.CreateRing(-4, Ring.RingStates.white);
        shootTheRing.CreateRing(4, Ring.RingStates.white);
        shootTheRing.CreateRing(0, Ring.RingStates.blue);


    }
    private IEnumerator Lvl10()
    {
        shootTheRing.SetRingCountToWinLvl(12);

        yield return new WaitForSeconds(1f);
        shootTheRing.CreateRing(-5, Ring.RingStates.red, 8, 3, 0.0015f);
        shootTheRing.CreateRing(5, Ring.RingStates.red, 8, 3, -0.0015f);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(-5, Ring.RingStates.green, 8, 3, 0.0015f);
        shootTheRing.CreateRing(5, Ring.RingStates.green, 8, 3, -0.0015f);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(-5, Ring.RingStates.blue, 8, 3, 0.0015f);
        shootTheRing.CreateRing(5, Ring.RingStates.blue, 8, 3, -0.0015f);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(-3, Ring.RingStates.white, 2, 3, 0.0015f);
        shootTheRing.CreateRing(3, Ring.RingStates.white, 2, 3, -0.0015f);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(0, Ring.RingStates.white, 1, 3, -0.0015f);
        shootTheRing.CreateRing(0, Ring.RingStates.white, 1, 3, 0.0015f);
        yield return new WaitForSeconds(2f);
        shootTheRing.CreateRing(0, Ring.RingStates.white, 1, 3, -0.0015f);
        shootTheRing.CreateRing(0, Ring.RingStates.white, 1, 3, 0.0015f);
    }        //done

    private IEnumerator Lvl11()
    {
        shootTheRing.SetRingCountToWinLvl(3);

        yield return new WaitForSeconds(1f);
        shootTheRing.CreateBomb(-2);
        yield return new WaitForSeconds(3f);

        shootTheRing.CreateRing(-2, Ring.RingStates.white);
        shootTheRing.CreateRing(-0, Ring.RingStates.white);
        shootTheRing.CreateRing(-1, Ring.RingStates.white);


    }        //done
}
