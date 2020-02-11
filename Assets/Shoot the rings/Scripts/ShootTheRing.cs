using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShootTheRing : MonoBehaviour
{
    private float timerToInvokeMethod = 1;
    private float counter = 1;

    public GameObject InputHandler;
    public GameObject adManager;
    public GameObject gameButtonsHandler;
    public GameObject LvlButtonsHandler;
    public GameObject levels;
    public GameObject light;
    public GameObject RingHandler;
    public Animator animator;

    public int numberOfLanterns = 3;

    private Ray mouseRay1;
    private bool bowToched;

    private RaycastHit rayHit;
    // position of the raycast on the screen
    private float posX;
    private float posY;

    // References to the gameobjects / prefabs
    public GameObject bowString;
    public GameObject arrow;
    public GameObject ring;

    public GameObject lvlProgressIndicator;
    public GameObject bottonCube;

    public GameObject lanternLight1;
    public GameObject lanternLight2;
    public GameObject lanternLight3;

    public GameObject arrowPrefab;
    public GameObject ringPrefab;
    public GameObject ringHandlerPrefab;

    public GameObject gameManager;
    public GameObject risingText;
    public GameObject TutorialAnimations;


    // Sound effects
    public AudioClip stringPull;
    public AudioClip stringRelease;
    public AudioClip arrowSwoosh;

    // has sound already be played
    bool stringPullSoundPlayed;
    bool stringReleaseSoundPlayed;
    bool arrowSwooshSoundPlayed;

    // the bowstring is a line renderer
    private List<Vector3> bowStringPosition;
    LineRenderer bowStringLinerenderer;

    // to determine the string pullout
    float arrowStartX;
    float length;

    // some status vars
    public bool timeScaleNeedToUp;   // когда активно, срабатывает плавное увеличение до 1
    public bool arrowShot;
    public bool arrowPrepared;
    float partsOfLvlIndicator; // длинна деления шкалы уровня

    // position of the line renderers middle part 
    Vector3 stringPullout;
    Vector3 stringRestPosition = new Vector3(-1f, -0f, 2f);

    // game states
    public enum GameStates
    {
        menu,
        instructions,
        game,
        over,
        lvlComplete,
        levels,
        pause,
        tutorial,
    };

    // store the actual game state
    public static GameStates gameState = GameStates.menu;

    //public static GameStates gameStateStat = GameStates.menu;

    public enum LightStates
    {
        white,
        red,
        blue,
        green,
    };
    public LightStates LightState = LightStates.white;


    // references to main objects for the UI screens
    public Canvas menuCanvas;
    public Canvas CanvasLvlComplete;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;
    public Canvas LevelsCanvas;
    public Canvas PauseCanvas;
    //public Canvas tutorialCanvas;


    public string currentLvl;
    public int currentLvlNumber;

    public float lvlProgressDestination;
    public bool lvlProgress;

    public Save sv = new Save();

    public float timerCounter;
    public int ringsCreatedCounter;
    public int totalCountRingsOnScene;
    private bool allowToShoot = true;
    public bool AllowToShoot { get => allowToShoot; set => allowToShoot = value; }
    public bool BowToched { get => bowToched; set => bowToched = value; }

    void Awake()
    {
        lvlProgressDestination = -6.4f;

        Application.targetFrameRate = 60;
        //create the PlayerPref
        //PlayerPrefs.DeleteAll();
        SaveInitialization();

        if (!sv.vibrate)
        {
            menuCanvas.transform.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[4];   //неактив
            PauseCanvas.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[4];   //неактив
        }
        if (!sv.sound)
        {
            menuCanvas.transform.GetChild(1).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[2];   //неактив
            PauseCanvas.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[2];   //неактив
        }

        //sv.stars[0] = 6;

        if (sv.stars[0] < 10)
        {

            SpriteState sprState = new SpriteState
            {
                pressedSprite = Resources.LoadAll<Sprite>("Textures/Main menu/buttons big")[4]
            };
            menuCanvas.gameObject.transform.GetChild(3).GetComponent<Button>().spriteState = sprState;

            menuCanvas.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/buttons big")[3];
        }

        //sv.stars[1] = 2;
        //sv.stars[2] = 1;
        //sv.stars[3] = 3;
        //sv.stars[4] = 2;


        animator = GetComponent<Animator>();
        // set the UI screens
        menuCanvas.enabled = true;
        CanvasLvlComplete.enabled = false;
        gameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        LevelsCanvas.enabled = false;
        PauseCanvas.enabled = false;
        //tutorialCanvas.enabled = false;


        // create an arrow to shoot
        // use true to set the target
        CreateArrow();

        // setup the line renderer representing the bowstring
        bowStringLinerenderer = bowString.AddComponent<LineRenderer>();
        bowStringLinerenderer.SetVertexCount(3);
        bowStringLinerenderer.SetWidth(0.05F, 0.05F);
        bowStringLinerenderer.useWorldSpace = false;
        bowStringLinerenderer.material = Resources.Load("Materials/bowStringMaterial") as Material;
        bowStringPosition = new List<Vector3>();
        bowStringPosition.Add(new Vector3(-1f, 0.85f, 2f));  //0.66
        bowStringPosition.Add(new Vector3(-1f, 0f, 2f));
        bowStringPosition.Add(new Vector3(-1f, -0.85f, 2f));

        bowStringLinerenderer.SetPosition(0, bowStringPosition[0]);
        bowStringLinerenderer.SetPosition(1, bowStringPosition[1]);
        bowStringLinerenderer.SetPosition(2, bowStringPosition[2]);
        arrowStartX = 0.7f;

        stringPullout = stringRestPosition;



    }


    void Update()
    {
        switch (gameState)
        {
            case GameStates.menu:

                // leave the game when back key is pressed (android)
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
#if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
#endif
                    Application.Quit();
                }
                break;

            case GameStates.game:

                if (lvlProgress)
                {
                    ChangeLvlProgressIndicator();
                }

                if (timeScaleNeedToUp)
                {
                    Time.timeScale += 0.01f;
                    if (Time.timeScale >= 1)
                    {
                        Time.timeScale = 1;
                        timeScaleNeedToUp = false;
                    }
                }



                timerCounter += Time.deltaTime;

                #region CHECK IS LVL COMPLETED?
                if (numberOfLanterns < 1)
                {

                    ShowLvlFailedScreen();
                }

                if (totalCountRingsOnScene == 0 && ringsCreatedCounter == levels.GetComponent<Levels>().RingAmountOnLvl && numberOfLanterns > 0)
                {
                    if (sv.GetStarsOfLvl(currentLvlNumber) < numberOfLanterns)
                    {
                        Debug.Log("уровень перезаписан");
                        sv.SetStarsToLvl(currentLvlNumber, numberOfLanterns);
                    }  // прошли на больше звезд чсем было?

                    if (sv.stars[currentLvlNumber] <= numberOfLanterns)
                    {
                        //Debug.Log("обновлено");
                        LvlButtonsHandler.GetComponent<LvlButtonsHandler>().UpdateLvlButton(currentLvlNumber, numberOfLanterns);   // обновляем иконку текущего уровня  // TODO нужно что бы при проходе на меньшее колво звезд не даунгрейдило иконку
                    }
                    if (sv.stars[0] == currentLvlNumber)
                    {
                        //Debug.Log("открыт новый");
                        sv.stars[0]++;
                        LvlButtonsHandler.GetComponent<LvlButtonsHandler>().UpdateLvlButton(currentLvlNumber + 1, 0);   // открываем след уровень и ставим на него 0 звезд
                    }
                    ShowLvlCompleteScreen(numberOfLanterns);
                }

                #endregion



                // return to main menu when back key is pressed (android)
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    StartPause();
                }


                // in any case: update the bowstring line renderer
                DrawBowString();
                break;

            case GameStates.tutorial:
                var tapCount = Input.touchCount;

                if (tapCount > 0)
                {
                    var touch = Input.GetTouch(0);
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint((touch.position));
                    RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

                    if (hit.collider != null && hit.collider.name == "bow area" && BowToched == false)
                    {
                        BowToched = true;
                        PrepareArrow(touchPos);
                        CloseTutorial(1);
                    }
                    if (hit.collider.name == "red")
                    {
                        CloseTutorial(3);

                    }
                }

                break;
        }
    }


    // операции с луком и стрелой
    public void CreateArrow()
    {
        this.transform.localRotation = Quaternion.identity;
        arrow = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        arrow.name = "arrow";
        //arrow.transform.localScale = this.transform.localScale;
        arrow.transform.localPosition = this.transform.position + new Vector3(0f, 0f, 0);
        arrow.transform.localRotation = this.transform.localRotation;
        arrow.transform.parent = this.transform;
        // transmit a reference to the arrow script
        arrow.GetComponent<rotateArrow>().setBow(gameObject);
        arrowShot = false;
        arrowPrepared = false;
    }
    public void ShootArrow()
    {
        if (arrow.GetComponent<Rigidbody>() == null && arrowPrepared)
        {
            if (sv.sound)
            {
                AudioManager.PlaySound(AudioManager.Sounds.ArrowRelease);
            }
            arrowShot = true;
            arrow.AddComponent<Rigidbody>();
            arrow.transform.parent = gameManager.transform;
            arrow.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)) * new Vector3(25f * length, 0, 0), ForceMode.VelocityChange);
        }
        arrowPrepared = false;
        stringPullout = stringRestPosition;
    }
    public void PrepareArrow(Vector3 touchPos)
    {
        if (!arrowShot && allowToShoot)
        {
            // get the touch point on the screen
            //mouseRay1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(mouseRay1, out rayHit, 500f))
            //{
            //    // determine the position on the screen
            posX = touchPos.x;
            posY = touchPos.y;

            //if (posX > -9f && posX < -3)
            //{

            // set the bows angle to the arrow
            Vector2 mousePos = new Vector2(transform.position.x - posX, transform.position.y - posY);
            float angleZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angleZ);
            // determine the arrow pullout
            length = mousePos.magnitude / 3f;
            length = Mathf.Clamp(length, 0, 1);
            // set the bowstrings line renderer
            stringPullout = new Vector3(-(0.6f + length), 0f, 2f);
            //stringPullout = new Vector3(-(1.1f + length), -0.06f, 2f);

            // set the arrows position
            Vector3 arrowPosition = arrow.transform.localPosition;
            arrowPosition.x = (arrowStartX - length);
            arrow.transform.localPosition = arrowPosition;
            arrowPrepared = true;

        }
        //    }
        //}
    }
    public void DrawBowString()
    {
        bowStringLinerenderer = bowString.GetComponent<LineRenderer>();
        bowStringLinerenderer.SetPosition(0, bowStringPosition[0]);
        bowStringLinerenderer.SetPosition(1, stringPullout);
        bowStringLinerenderer.SetPosition(2, bowStringPosition[2]);
    }


    public void StartTutorial(int number)
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.Tutorial);
        }

        gameCanvas.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;        // делаем кнопку паузы неактивной
        if (number == 1)
        {
            light.GetComponent<Lighthandler>().TutorialLightLvl1("tutorial");
            TutorialAnimations.transform.GetChild(0).GetComponent<Animator>().SetTrigger("tutorial lvl" + number);

        }
        else
        {
            light.GetComponent<Lighthandler>().TutorialLightLvl3("tutorial");
            SwitchOnOffColorButton(0, true);  // - red button unblocked
            gameButtonsHandler.GetComponent<GameButtonsHandler>().StartAppearAnimation();

            TutorialAnimations.transform.GetChild(1).GetComponent<Animator>().SetTrigger("tutorial lvl" + number);


        }
        //tutorialCanvas.transform.GetChild(0).GetComponent<Animator>().SetTrigger("tutorial lvl" + number);
        AllowToShoot = true;
        gameState = GameStates.tutorial;
        //tutorialCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void CloseTutorial(int number)
    {
        gameCanvas.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;        // делаем кнопку паузы активной
        if (number == 1)
        {
            sv.tutorialLvl1Passed = true;
            light.GetComponent<Lighthandler>().TutorialLightLvl1("tutorial");
        }
        else
        {
            sv.tutorialLvl3Passed = true;

            light.GetComponent<Lighthandler>().TutorialLightLvl3("tutorial");

        }
        TutorialAnimations.transform.GetChild(0).GetComponent<Animator>().SetTrigger("reset");
        TutorialAnimations.transform.GetChild(1).GetComponent<Animator>().SetTrigger("reset");
        gameState = GameStates.game;
        //tutorialCanvas.enabled = false;
        timeScaleNeedToUp = true;
    }


    public void BackToMenu()
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
        }

        arrow.GetComponent<rotateArrow>().ReCreate();
        light.GetComponent<Lighthandler>().ResetState("Reset");
        showMenu();

        lvlProgressIndicator.transform.position = new Vector3(lvlProgressIndicator.transform.position.x, -6.4f, lvlProgressIndicator.transform.position.z);

        PauseCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        CanvasLvlComplete.enabled = false;
        RingHandler.GetComponent<RingHandler>().DelateAllRings();
        levels.GetComponent<Levels>().Stoplvl(currentLvl);
    }
    public void GoToLevelsFromLoseScreen()
    {
        gameOverCanvas.enabled = false;
        LevelsCanvas.enabled = true;
        LevelsCanvas.gameObject.transform.Find("Grid").gameObject.SetActive(true);

        RingHandler.GetComponent<RingHandler>().DelateAllRings();
        ResetLanterns();
        light.GetComponent<Lighthandler>().ResetState("Reset");
        LightState = LightStates.white;
        gameState = GameStates.levels;
    }
    public void GoToLevelsFromLvlCompleteScreen()
    {
        light.GetComponent<Lighthandler>().ResetState("Reset");

        CanvasLvlComplete.enabled = false;
        LevelsCanvas.enabled = true;
        LevelsCanvas.gameObject.transform.Find("Grid").gameObject.SetActive(true);
        gameState = GameStates.levels;
    }
    public void ShowLvlCompleteScreen(int count_stars)
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.CompleteLvl);
        }
        CanvasLvlComplete.transform.GetChild(0).GetComponent<Animator>().SetTrigger(count_stars.ToString() + " stars");
        CanvasLvlComplete.transform.GetChild(1).GetComponent<Animator>().SetTrigger(count_stars.ToString() + " stars");

        light.GetComponent<Lighthandler>().ChangeLightBrightness("game inactive");
        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetInteractable(false);                     // делаем игровые кнопки неактивными
        gameCanvas.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;        // делаем кнопку паузы неактивной


        CanvasLvlComplete.enabled = true;
        gameState = GameStates.lvlComplete;
    }

    public void ShowLvlFailedScreen()
    {
        light.GetComponent<Lighthandler>().ChangeLightBrightness("game inactive");
        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetInteractable(false);                    // делаем игровые кнопки неактивными
        gameCanvas.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;        // делаем кнопку паузы неактивной

        gameOverCanvas.enabled = true;
        gameState = GameStates.over;
        gameOverCanvas.transform.GetChild(0).GetComponent<Animator>().SetTrigger("failed");
    }
    public void RestartCurrentLevel()
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
        }
        arrow.GetComponent<rotateArrow>().ReCreate();

        lvlProgressDestination = -6.4f;

        gameOverCanvas.enabled = false;
        PauseCanvas.enabled = false;
        CanvasLvlComplete.enabled = false;
        ResetLanterns();
        gameState = GameStates.game;
        LightState = LightStates.white;

        RingHandler.GetComponent<RingHandler>().DelateAllRings();
        levels.GetComponent<Levels>().Stoplvl(currentLvl);
        levels.GetComponent<Levels>().Startlvl(currentLvl);
    }
    public void StartNextLvl()
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
        }
        CanvasLvlComplete.enabled = false;
        levels.GetComponent<Levels>().Stoplvl(currentLvl);
        GetNextLvlString();
        levels.GetComponent<Levels>().Startlvl(currentLvl);

    }
    public void GetNextLvlString()
    {
        int num = GetCurrentLvlNumber();
        num++;
        currentLvl = "Lvl" + num;

    }

    public void showMenu()
    {
        lvlProgressDestination = -6.4f;
        menuCanvas.enabled = true;
        gameState = GameStates.menu;

        if (sv.stars[0] >= 10)
        {

            SpriteState sprState = new SpriteState
            {
                pressedSprite = Resources.LoadAll<Sprite>("Textures/Main menu/buttons big")[5]
            };
            menuCanvas.gameObject.transform.GetChild(3).GetComponent<Button>().spriteState = sprState;

            menuCanvas.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/buttons big")[2];
        }

    }       // показывает меню и  Gamestate = menu
    public void showLevels()
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
        }
        Time.timeScale = 1;
        menuCanvas.enabled = false;
        LevelsCanvas.enabled = true;
        Input.multiTouchEnabled = false;
        LevelsCanvas.gameObject.transform.Find("Grid").gameObject.SetActive(true);

        gameState = GameStates.levels;



    }
    public void hideLevels()
    {

        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
        }
        LevelsCanvas.enabled = false;
        LevelsCanvas.gameObject.transform.Find("Grid").gameObject.SetActive(false);
        LevelsCanvas.gameObject.transform.Find("Grid").GetComponent<LvlButtonsHandler>().anyAnimPlayed = 0;

    }
    public void PrepareLvl()
    {
        if (gameState == GameStates.levels)
        {
            hideLevels();
        }
        if (gameState == GameStates.menu)
        {
            menuCanvas.enabled = false;
        }
        lvlProgressDestination = -6.4f;

        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetONOFF(true);

        gameButtonsHandler.GetComponent<GameButtonsHandler>().ResetAll();

        Input.multiTouchEnabled = true;
        gameCanvas.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;        // делаем кнопку паузы активной
        gameOverCanvas.transform.GetChild(0).GetComponent<Animator>().SetTrigger("reset");      // ресет анимации проигранного уровня
        CanvasLvlComplete.transform.GetChild(0).GetComponent<Animator>().SetTrigger("reset");
        CanvasLvlComplete.transform.GetChild(1).GetComponent<Animator>().SetTrigger("reset");
        ResetLanterns();
        ringsCreatedCounter = 0;
        totalCountRingsOnScene = 0;
        gameCanvas.enabled = true;
        Time.timeScale = 1;
        gameButtonsHandler.GetComponent<GameButtonsHandler>().StartAppearAnimation();
        light.GetComponent<Lighthandler>().ChangeLightBrightness("game active");
        //light.GetComponent<Lighthandler>().ResetState("Reset");
        LightState = LightStates.white;
        gameState = GameStates.game;

    }


    public void Breaklvl()
    {
        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetONOFF(false);
        light.GetComponent<Lighthandler>().ResetState("Reset");
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("white"))
        {
            animator.SetTrigger("reset");
        }
        gameCanvas.enabled = false;

    }
    public void SwitchOnOffColorButton(int index, bool trueORfalse)
    {
        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetOffSeparately(index, trueORfalse);

    }


    public void createRings()
    {
        float x = UnityEngine.Random.Range(-3f, 8f);
        Vector3 position = new Vector3(x, 5.5f, -1);
        ring = Instantiate(ringPrefab, position, Quaternion.identity) as GameObject;
        ring.name = "ring";
    }       // TODO рандом для survival
    public void fallingRings()
    {
        counter += Time.deltaTime;
        if (counter > timerToInvokeMethod)
        {
            counter = 0;
            UnityEngine.Random.Range(0, 1);
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                createRings();
            }
        }
    }       // TODO рандом для survival

    // Отработчики UI элементов
    // PAUSE
    public void StartPause()
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.MenuLvlClick);
        }
        gameCanvas.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;        // делаем кнопку паузы неактивной
        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetInteractable(false);   // делаем игровые кнопки неактивными
        light.GetComponent<Lighthandler>().ChangeLightBrightness("game inactive");
        Time.timeScale = 0;


        gameState = GameStates.pause;
        PauseCanvas.enabled = true;
        PauseCanvas.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("pause");
        PauseCanvas.gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("pause");

    }
    public void EndPause()
    {
        gameCanvas.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;        // делаем кнопку паузы активной
        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetInteractable(true);   // делаем игровые кнопки активными

        light.GetComponent<Lighthandler>().ChangeLightBrightness("game active");

        PauseCanvas.enabled = false;
        Time.timeScale = 1;
        gameState = GameStates.game;
    }

    //Прочее

    public void Survival()
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
        }
    }
    public void RateUs()
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
        }
    }

    public void Highscore()
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.MenuClick);
        }
    }





    public float CalculatelengthOfpartLvlIndicator()
    {

        return 6.2f / levels.GetComponent<Levels>().RingAmountOnLvl;
    }  // подсчитывает, на сколько нужно поднимать шкалу прогресса уровня за каждое брошенное кольцо
    public void ChangeLvlProgressIndicator() // поднимает шкалу прогресса уровня
    {
        lvlProgressIndicator.transform.position = Vector3.Lerp(lvlProgressIndicator.transform.position, new Vector3(lvlProgressIndicator.transform.position.x, lvlProgressDestination, lvlProgressIndicator.transform.position.z), Time.deltaTime);
    }
    public void VibrateToggle()      //переключатель вибрации
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.SoundVibrateToggle);
        }

        if (sv.vibrate == true)
        {
            sv.vibrate = false;
            menuCanvas.transform.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[4];   //неактив
            PauseCanvas.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[4];   //неактив
        }
        else
        {
            sv.vibrate = true;
            menuCanvas.transform.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[5];   //актив
            PauseCanvas.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[5];   //неактив

        }
    }
    public void SoundToggle()      //переключатель звука
    {
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.SoundVibrateToggle);
        }
        if (sv.sound == true)
        {
            sv.sound = false;
            menuCanvas.transform.GetChild(1).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[2];   //неактив
            PauseCanvas.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[2];   //неактив
        }
        else
        {
            sv.sound = true;
            menuCanvas.transform.GetChild(1).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[3];   //актив
            PauseCanvas.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Textures/Main menu/icons")[3];   //неактив

        }
    }
    public int GetCurrentLvlNumber()
    {
        string lvlNum = currentLvl.Substring(3);
        int num = Int32.Parse(lvlNum);
        return num;
    }           // парсит номер уровня из строки уровня
    public void WriteOpenedLvl(string lvlInfo)
    {
        currentLvl = lvlInfo;
        currentLvlNumber = Int32.Parse(lvlInfo.Substring(3));
        partsOfLvlIndicator = CalculatelengthOfpartLvlIndicator();
    }       // Сохраняет  номер и строку текущего уровня

    public void CreateRing(float positionX, Ring.RingStates ringState)
    {
        Vector3 position = new Vector3(positionX, 5.7f, -1);
        ring = Instantiate(ringPrefab, position, Quaternion.identity) as GameObject;
        ring.name = "ring";
        ring.GetComponent<Ring>().ringstate = ringState;
        timerCounter = 0;
        ringsCreatedCounter++;
        totalCountRingsOnScene++;

        lvlProgress = true;
        lvlProgressDestination += partsOfLvlIndicator;

    }       // создает кольцо заданного цвета на определенной позиции
    public void CreateRing(float positionX, Ring.RingStates ringState, float verticalSpeed)
    {
        Vector3 position = new Vector3(positionX, 5.7f, -1);
        ring = Instantiate(ringPrefab, position, Quaternion.identity) as GameObject;
        ring.name = "ring";
        ring.GetComponent<Ring>().ringstate = ringState;
        ring.GetComponent<Ring>().VerticalSpeed = verticalSpeed;
        timerCounter = 0;

        ringsCreatedCounter++;
        totalCountRingsOnScene++;

        lvlProgress = true;
        lvlProgressDestination += partsOfLvlIndicator;

    }       // создает кольцо заданного цвета на определенной позиции c заданной начальной скоростью
    public void RingCountDec()
    {
        totalCountRingsOnScene--;
    }       // уменьшает счетчик totalCountRingsOnScene
    public void TurnLanternOff()
    {
        if (sv.vibrate)
        {
            Handheld.Vibrate();
        }
        numberOfLanterns--;

        if (numberOfLanterns == 2)
        {
            SwitchLantern(lanternLight1, false);
        }
        if (numberOfLanterns == 1)
        {
            SwitchLantern(lanternLight2, false);
        }
        else if (numberOfLanterns == 0)
        {
            SwitchLantern(lanternLight3, false);
        }
    }       // выключает фонарь при падении кольца
    public void ResetLanterns()
    {
        numberOfLanterns = 3;
        SwitchLantern(lanternLight1, true);
        SwitchLantern(lanternLight2, true);
        SwitchLantern(lanternLight3, true);

    }       // включает все фонари
    private void SwitchLantern(GameObject lantern, Boolean state)
    {
        lantern.GetComponentInChildren<Transform>().Find("on").gameObject.SetActive(state);
        lantern.GetComponentInChildren<Transform>().Find("light").gameObject.SetActive(state);
        lantern.GetComponentInChildren<Transform>().Find("off").gameObject.SetActive(!state);
        lantern.GetComponentInChildren<Transform>().Find("flare").gameObject.SetActive(state);

    }       // переключает фонарь On\off
    public void redMod_Click(GameObject button)
    {
        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetActiveButton(button);
        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.ColourChange);
        }
        if (LightState == LightStates.green)
        {
            RingHandler.GetComponent<RingHandler>().RingsReachAbility("green rings");
            animator.SetTrigger("red");
        }
        if (LightState == LightStates.blue)
        {
            RingHandler.GetComponent<RingHandler>().RingsReachAbility("blue rings");
            animator.SetTrigger("red");
        }
        if (LightState != LightStates.red)
        {
            animator.SetTrigger("red");
            LightState = LightStates.red;
        }
        else
        {
            LightState = LightStates.white;
            animator.SetTrigger("white");
        }
        light.GetComponent<Lighthandler>().Button_pressed("red");
        RingHandler.GetComponent<RingHandler>().RingsReachAbility("red rings");
    }      // обрабатывает нажатие красной кнопки
    public void GreenMod_Click(GameObject button)
    {
        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetActiveButton(button);

        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.ColourChange);
        }
        if (LightState == LightStates.red)
        {
            animator.SetTrigger("green");
            RingHandler.GetComponent<RingHandler>().RingsReachAbility("red rings");
        }
        if (LightState == LightStates.blue)
        {
            animator.SetTrigger("green");
            RingHandler.GetComponent<RingHandler>().RingsReachAbility("blue rings");
        }
        if (LightState != LightStates.green)
        {
            animator.SetTrigger("green");
            LightState = LightStates.green;
        }
        else
        {
            LightState = LightStates.white;
            animator.SetTrigger("white");
        }
        light.GetComponent<Lighthandler>().Button_pressed("green");
        RingHandler.GetComponent<RingHandler>().RingsReachAbility("green rings");
    }      // обрабатывает нажатие зеленой кнопки
    public void BlueMod_Click(GameObject button)
    {
        gameButtonsHandler.GetComponent<GameButtonsHandler>().SetActiveButton(button);

        if (sv.sound)
        {
            AudioManager.PlaySound(AudioManager.Sounds.ColourChange);
        }
        if (LightState == LightStates.red)
        {
            animator.SetTrigger("blue");
            RingHandler.GetComponent<RingHandler>().RingsReachAbility("red rings");
        }
        if (LightState == LightStates.green)
        {
            animator.SetTrigger("blue");
            RingHandler.GetComponent<RingHandler>().RingsReachAbility("green rings");
        }

        if (LightState != LightStates.blue)
        {
            animator.SetTrigger("blue");
            LightState = LightStates.blue;
        }
        else
        {
            LightState = LightStates.white;
            animator.SetTrigger("white");
        }
        light.GetComponent<Lighthandler>().Button_pressed("blue");
        RingHandler.GetComponent<RingHandler>().RingsReachAbility("blue rings");
    }      // обрабатывает нажатие синей кнопки
    public void SaveInitialization()
    {
        if (!PlayerPrefs.HasKey("Save"))
        {
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
            Debug.Log("создали сейв");
            sv.stars[0] = 1;    // устанавливаем первый открытый уровень
            sv.sound = true;
            sv.vibrate = true;
        }
        else
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
        }
    }       // загружает сохранения из PlayerPrefs или создает и записывает новый сейв
}

public class Save
{
    public int[] stars = new int[91];    // [0] - колво открытых уровней. далее колво звезд за уровень

    public bool tutorialLvl1Passed;
    public bool tutorialLvl3Passed;
    public bool vibrate;
    public bool sound;

    public void SetStarsToLvl(int lvl, int numOfStars)
    {
        stars[lvl] = numOfStars;
    }

    public int GetStarsOfLvl(int lvl)
    {
        return stars[lvl];
    }
}