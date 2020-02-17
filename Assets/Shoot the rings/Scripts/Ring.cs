using System.Collections;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private ShootTheRing main;
    private GameObject bucket;

    private GameObject RingHandler;
    [SerializeField] private BoxCollider boxCollider;

    private bool shootted;
    private bool missUP;
    private bool missDown;
    public bool smoothDestroy;
    private bool startMoveToBucket;
    private float scale = 1;
    private Vector3 destination;
    private Vector3 destinationFinal;

    private Animator animator;

    public enum RingStates
    {
        red,
        green,
        blue,
        white,
    };
    public RingStates ringstate;

    public float startPosX;
    public float NumberOfBounds = 0f;
    public float VerticalSpeed = 0.0015f;
    public float HorizontalSpeed = 0f;
    public float WindRange = 0f;
    public float leftMax;
    public float rightMax;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main").GetComponent<ShootTheRing>();
        bucket = GameObject.FindGameObjectWithTag("bucket");
        animator = GetComponent<Animator>();
        RingHandler = GameObject.FindGameObjectWithTag("RingHandler");

        destination = bucket.transform.GetChild(0).transform.position;
        destinationFinal = bucket.transform.GetChild(3).transform.position;
        Ringstate(ringstate);
        startPosX = transform.position.x;
        leftMax = startPosX - WindRange;
        rightMax = startPosX + WindRange;
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
            RingFalling();
        }
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
    }


    public void ScaleToZero()
    {
        if (scale > 0.1)
        {
            scale -= 0.001f;
        }
        transform.localScale = new Vector3(scale, scale, 1);
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
                    SwitchOnOff();
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
                    SwitchOnOff();
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
                    SwitchOnOff();
                    animator.SetTrigger("green rings");
                }
                break;

            case RingStates.white:
                transform.parent = RingHandler.transform.Find("white rings");
                SwitchOnOff();
                break;
        }
    }

    public void SwitchOnOff()
    {
        boxCollider.enabled = !boxCollider.enabled;
    }
    /// <summary>
    /// отвечает за вертикальную скорость падения, направления в карзину и удаление кольца
    /// </summary>
    private void RingFalling()
    {



        if (!startMoveToBucket)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - VerticalSpeed, gameObject.transform.position.z);
            if (HorizontalSpeed != 0)
            {
                HorizontalMove(HorizontalSpeed);
                if (NumberOfBounds > 0)
                {
                    if ((transform.position.x > rightMax) || (transform.position.x < leftMax))
                    {
                        HorizontalSpeed = -HorizontalSpeed;
                        NumberOfBounds--;
                    }
                }
            }
        }

        if (transform.position.y < -5.6f)
        {
            if (!shootted)
            {
                main.TurnLanternOff();
            }
            main.RingCountDec();
            Destroy(gameObject);
        }




        if (shootted && transform.position.y < -1)
        {
            if (!startMoveToBucket)
            {
                bucket.GetComponent<Bucket>().OpenCap();
            }

            startMoveToBucket = true;
            ScaleToZero();

            if (transform.position.x > -6.5)
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, destination, 0.01f);
            }
            else
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, destinationFinal, 0.005f);
                if (transform.position.y < -4)
                {
                    main.RingCountDec();
                    Destroy(gameObject);

                }
            }
        }

    }
    private void HorizontalMove(float horizontalSpeed)
    {
        transform.position = new Vector3(transform.position.x + horizontalSpeed, transform.position.y, transform.position.z);
    }

    public void Hit()
    {
        shootted = true;
        if (main.sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.RingShotted);
        }
        VerticalSpeed = 0.03f;
        animator.SetFloat("speed", 3);
    }

    public void MissHandler()
    {
        if ((missDown || missUP) && !shootted)
        {
            if (main.sv.sound)
            {
                AudioManager.PlaySound(AudioManager.Sounds.miss);
            }
            transform.GetChild(2).GetComponent<Animator>().SetTrigger("miss");
        }
    }
    public void MissUp()
    {
        missUP = true;
        MissHandler();
    }

    public void MissDown()
    {
        missDown = true;
        MissHandler();
    }

}
