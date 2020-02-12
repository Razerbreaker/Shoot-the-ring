using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] private ShootTheRing main;
    [SerializeField] private GameObject bucket;

    [SerializeField] private GameObject RingHandler;
    [SerializeField] private BoxCollider boxCollider;
    private bool shootted;
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

    public float VerticalSpeed { get; set; } = 0.0015f;

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main").GetComponent<ShootTheRing>();
        bucket = GameObject.FindGameObjectWithTag("bucket");
        animator = GetComponent<Animator>();
        RingHandler = GameObject.FindGameObjectWithTag("RingHandler");

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
            RingFalling();
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
        }
        HorizontalMove(0.001f);

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
            if (scale > 0.1)
            {
                scale -= 0.001f;
            }
            transform.localScale = new Vector3(scale, scale, 1);

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
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "arrow")
        {
            if (main.sv.sound)
            {
                AudioManager.PlaySound(AudioManager.Sounds.RingShotted);
            }
            shootted = true;
            VerticalSpeed = 0.03f;
            animator.SetFloat("speed", 3);
        }
    }
}
