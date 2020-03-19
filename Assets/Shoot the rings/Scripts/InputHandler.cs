using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject shootTheRing;
    private ShootTheRing ShootTheRing;
    private void Start()
    {
        ShootTheRing = shootTheRing.GetComponent<ShootTheRing>();
    }

    private void Update()
    {
        if (ShootTheRing.gameState == ShootTheRing.GameStates.game || ShootTheRing.gameState == ShootTheRing.GameStates.survival)
        {
            var tapCount = Input.touchCount;
            for (var i = 0; i < tapCount; i++)
            {
                var touch = Input.GetTouch(i);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint((touch.position));
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
                switch (touch.phase)
                {

                    case TouchPhase.Ended:
                        if (hit.collider != null)
                        {
                            if (hit.collider.name == "red")
                            {
                                ShootTheRing.RedMod_Click(hit.collider.gameObject);
                            }
                            if (hit.collider.name == "green")
                            {
                                ShootTheRing.GreenMod_Click(hit.collider.gameObject);
                            }
                            if (hit.collider.name == "blue")
                            {
                                ShootTheRing.BlueMod_Click(hit.collider.gameObject);
                            }
                            if (hit.collider.name == "bow area" && ShootTheRing.BowToched == true)
                            {
                                ShootTheRing.ShootArrow();
                                ShootTheRing.BowToched = false;

                            }

                        }
                        break;

                    case TouchPhase.Began:

                        if (hit.collider != null && hit.collider.name == "bow area" && ShootTheRing.BowToched == false)
                        {
                            ShootTheRing.BowToched = true;
                            ShootTheRing.PrepareArrow(touchPos);
                        }


                        break;

                    case TouchPhase.Moved:
                        if (hit.collider != null && hit.collider.name == "bow area" && ShootTheRing.BowToched == true)
                        {

                            ShootTheRing.PrepareArrow(touchPos);




                        }
                        if (hit.collider != null && hit.collider.name == "exit bow" && ShootTheRing.BowToched == true)
                        {
                            ShootTheRing.ShootArrow();
                            ShootTheRing.BowToched = false;
                        }

                        break;

                }
            }
        }
    }
}
