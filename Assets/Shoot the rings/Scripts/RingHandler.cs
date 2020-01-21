﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHandler : MonoBehaviour
{


    //int accumulator = 0;
    //int counter = 0;
    //float timer = 0f;
    //GUIStyle style = new GUIStyle();

    //void Start()
    //{
    //    style.normal.textColor = Color.white;
    //    style.fontSize = 32;
    //    style.fontStyle = FontStyle.Bold;
    //}

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 10, 100, 34), "FPS: " + counter, style);
    //}

    //void Update()
    //{
    //    accumulator++;
    //    timer += Time.deltaTime;

    //    if (timer >= 1)
    //    {
    //        timer = 0;
    //        counter = accumulator;
    //        accumulator = 0;
    //    }
    //}


    public void RingsReachAbility(string color)
    {
        int ringCount = transform.Find(color).transform.childCount;

        if (ringCount > 0)
        {
            for (int i = 0; ringCount > i; i++)
            {
                transform.Find(color).transform.GetChild(i).GetComponent<Ring>().switchOnOff();
                transform.Find(color).transform.GetChild(i).GetComponent<Ring>().GetComponent<Animator>().SetTrigger(color);
            }
        }
    }
    public void DelateAllRings()
    {
        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(1).gameObject);
        Destroy(transform.GetChild(2).gameObject);
        Destroy(transform.GetChild(3).gameObject);

        GameObject red = new GameObject();
        red.name = "red rings";
        red.transform.parent = this.transform;

        GameObject blue = new GameObject();
        blue.name = "blue rings";
        blue.transform.parent = this.transform;

        GameObject green = new GameObject();
        green.name = "green rings";
        green.transform.parent = this.transform;

        GameObject white = new GameObject();
        white.name = "white rings";
        white.transform.parent = this.transform;

    }
}
