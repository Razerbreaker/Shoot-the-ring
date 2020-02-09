using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{

    public bool isOpen;
    public float timeWait = 2f;
    public float counter;
    private void FixedUpdate()
    {
        if (isOpen)
        {
            counter += Time.fixedDeltaTime;
            if (counter > timeWait)
            {
                CloseCap();
                counter = 0;
                isOpen = false;
            }
        }
    }

    public void OpenCap()
    {
        if (!isOpen)
        {
            gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("open");
            isOpen = true;
        }
        else
        {
            counter = 0;
        }

    }
    public void CloseCap()
    {
        gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("close");
    }

    public void SetFront()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void SetBack()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }

}
