using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public ShootTheRing main;
    public GameObject bucket;

    public GameObject RingHandler;
    public BoxCollider boxCollider;
    public bool shootted;
    public bool startMoveToBucket;
    public float verticalSpeed;
    public float scale;
    public Vector3 destination;
    public Vector3 destinationFinal;

    Animator animator;

    public enum RingStates
    {
        red,
        green,
        blue,
        white,
    };
    public RingStates ringstate;

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main").GetComponent<ShootTheRing>();
        bucket = GameObject.FindGameObjectWithTag("bucket");
        animator = GetComponent<Animator>();
        RingHandler = GameObject.FindGameObjectWithTag("RingHandler");
        verticalSpeed = 0.0015f;
        scale = 1;

        destination = bucket.transform.GetChild(0).transform.position;
        destinationFinal = bucket.transform.GetChild(3).transform.position;
        Ringstate(ringstate);


    }

    private void SetRandomRingstate()
    {
        System.Array enumValues = System.Enum.GetValues(typeof(RingStates));
        ringstate = (RingStates)enumValues.GetValue(Random.Range(0, enumValues.Length));
        Ringstate(ringstate);
    }

    private void FixedUpdate()
    {
        if (ShootTheRing.gameState == ShootTheRing.GameStates.game)
        {
            ringFalling();
        }
    }

    public void Ringstate(RingStates ringstate)
    {
        switch (ringstate)
        {
            case RingStates.red:
                transform.parent = RingHandler.transform.Find("red rings");
                if (main.LightState == ShootTheRing.LightStates.green ||
                    main.LightState == ShootTheRing.LightStates.blue ||
                    main.LightState == ShootTheRing.LightStates.white)
                {
                    animator.SetTrigger("red not active");
                }
                else
                {
                    switchOnOff();
                    animator.SetTrigger("red rings");
                }
                break;

            case RingStates.blue:
                transform.parent = RingHandler.transform.Find("blue rings");
                if (main.LightState == ShootTheRing.LightStates.green ||
                    main.LightState == ShootTheRing.LightStates.red ||
                    main.LightState == ShootTheRing.LightStates.white)
                {
                    animator.SetTrigger("blue not active");
                }
                else
                {
                    switchOnOff();
                    animator.SetTrigger("blue rings");
                }
                break;

            case RingStates.green:
                transform.parent = RingHandler.transform.Find("green rings");
                if (main.LightState == ShootTheRing.LightStates.red ||
                    main.LightState == ShootTheRing.LightStates.blue ||
                    main.LightState == ShootTheRing.LightStates.white)
                {
                    animator.SetTrigger("green not active");
                }
                else
                {
                    switchOnOff();
                    animator.SetTrigger("green rings");
                }
                break;

            case RingStates.white:
                transform.parent = RingHandler.transform.Find("white rings");
                switchOnOff();
                break;
        }
    }

    public void switchOnOff()
    {
        boxCollider.enabled = !boxCollider.enabled;
    }

    void ringFalling()
    {

        if (!startMoveToBucket)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - verticalSpeed, gameObject.transform.position.z);
        }


        if (transform.position.y < -5.6f)
        {
            if (!shootted)
            {
                main.TurnLanternOff();
            }
            Destroy(gameObject);
            main.RingCountDec();
        }

        if (shootted && transform.position.y < -4)
        {
            Destroy(gameObject);
            main.RingCountDec();

        }


        if (shootted && transform.position.y < -1)
        {
            if (!startMoveToBucket)
            {
                bucket.GetComponent<Bucket>().OpenCap();
            }

            startMoveToBucket = true;
            if (scale > 0.1)
            {
                scale -= 0.001f;
            }
            transform.localScale = new Vector3(scale, scale, 1);

            if (transform.position.x > -6.5)
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, destination, 0.005f);
            }
            else
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, destinationFinal, 0.005f);

            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "arrow")
        {
            shootted = true;
            verticalSpeed = 0.03f;
            animator.SetFloat("speed", 3);

        }
    }
}
