using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool smoothDestroy;
    private float scale = 1;

    private void Start()
    {
        transform.parent = GameObject.FindGameObjectWithTag("RingHandler").transform.Find("bombs");
    }
    void FixedUpdate()
    {
        if (smoothDestroy)
        {
            if (scale > 0.01f)
            {
                scale -= 0.005f;
                transform.localScale = new Vector3(scale, scale, 1);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.002f, gameObject.transform.position.z);

        if (gameObject.transform.position.y < -5.5f)
        {
            smoothDestroy = true;
        }

    }
    public void Bombexplosion()
    {
        smoothDestroy = true;
    }

}
