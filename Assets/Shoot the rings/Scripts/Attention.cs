using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attention : MonoBehaviour
{
    public void CallAttention(int posX)
    {

        transform.position = new Vector3(posX, 4.25f,-1);
        GetComponent<Animator>().SetTrigger("attention");
    }
}
