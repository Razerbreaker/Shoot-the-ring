using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthandler : MonoBehaviour
{
    public GameObject[] lights;
    public GameObject globalLight;
    public GameObject symbolslight;
    public GameObject lvlindicator;
    public GameObject colorButtons;
    public GameObject tutorialLight;

    public void Button_pressed(string trigger)
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<Animator>().SetTrigger(trigger);
        }
    }

    public void ChangeLightBrightness(string trigger)
    {
        globalLight.GetComponent<Animator>().SetTrigger(trigger);
        symbolslight.GetComponent<Animator>().SetTrigger(trigger);
        lvlindicator.GetComponent<Animator>().SetTrigger(trigger);
        colorButtons.transform.GetChild(0).GetComponent<Animator>().SetTrigger(trigger);
        colorButtons.transform.GetChild(1).GetComponent<Animator>().SetTrigger(trigger);
        colorButtons.transform.GetChild(2).GetComponent<Animator>().SetTrigger(trigger);
    }

    public void ChangeOnlyGlobalLight(string trigger)
    {
        globalLight.GetComponent<Animator>().SetTrigger(trigger);
    }

    public void TutorialLight(string trigger)
    {
        globalLight.GetComponent<Animator>().SetTrigger(trigger);
        tutorialLight.GetComponent<Animator>().SetTrigger(trigger);
    }


}
