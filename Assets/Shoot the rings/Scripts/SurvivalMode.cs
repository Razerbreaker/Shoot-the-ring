using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalMode : MonoBehaviour
{
    public GameObject main;
    private ShootTheRing shootTheRing;

    public int score;

    public float TimeFromStart;
    private int timeInSec;
    private int complexityLvl;

    public float TimeFromLastRing;
    public float ringInterval;
    public float bombInterval;
    public float maxRingsOnScene;

    private bool isActive;

    public int СomplexityOverTime
    {
        get { return complexityLvl; }
        set
        {
            complexityLvl = value;
            switch (complexityLvl)
            {
                case 10:
                    ringInterval = 1;
                    maxRingsOnScene = 7;
                    break;
                case 15:
                    maxRingsOnScene = 10;
                    break;

                default:
                    break;
            }
        }
    }

    void Start()
    {
        shootTheRing = main.GetComponent<ShootTheRing>();
        SetStartValues();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShootTheRing.gameState == ShootTheRing.GameStates.survival)
        {
            TimeFromStart += Time.deltaTime;
            timeInSec = (int)TimeFromStart;
            if (timeInSec > complexityLvl)
            {
                СomplexityOverTime = timeInSec;
            }
            TimeFromLastRing += Time.deltaTime;
            if (TimeFromLastRing > ringInterval)
            {
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    if (shootTheRing.totalRingsCounter < maxRingsOnScene)
                    {
                        shootTheRing.CreateRing(111, Ring.RingStates.random);
                    }
                }
                TimeFromLastRing = TimeFromLastRing - ringInterval;
            }
            bombInterval += Time.deltaTime;
            if (bombInterval > 30)
            {
                shootTheRing.CreateBomb(111);
                bombInterval = 0;
            }

        }
    }

    public void SetStartValues()
    {
        bombInterval = 1f;
        TimeFromStart = 0f;
        TimeFromLastRing = 2;
        ringInterval = 2;
        maxRingsOnScene = 5;
        complexityLvl = 0;
        timeInSec = 0;
    }


    public void StartSurvival()
    {
        isActive = true;
    }

}
