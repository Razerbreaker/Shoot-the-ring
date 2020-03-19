using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMethods : MonoBehaviour
{
    public void LvlIndicatorResetPos()
    {
        transform.position = new Vector3(transform.position.x, -7.2f, -0.64f);
    }
}
