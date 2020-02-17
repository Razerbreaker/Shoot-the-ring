using UnityEngine;

public class RingHandler : MonoBehaviour
{
    public void RingsReachAbility(string color)
    {
        int ringCount = transform.Find(color).transform.childCount;

        if (ringCount > 0)
        {
            for (int i = 0; ringCount > i; i++)
            {
                transform.Find(color).transform.GetChild(i).GetComponent<Ring>().SwitchOnOff();
                transform.Find(color).transform.GetChild(i).GetComponent<Ring>().GetComponent<Animator>().SetTrigger(color);
            }
        }
    }
    public void DelateAllRings()
    {
        for (int i = 0; i <= 3; i++)
        {
            var ringCount = transform.GetChild(i).transform.childCount;
            if (ringCount > 0)
            {
                for (int g = 0; g < ringCount; g++)
                {
                    transform.GetChild(i).transform.GetChild(g).GetComponent<Ring>().smoothDestroy = true;
                }
            }
        }
    }



}
