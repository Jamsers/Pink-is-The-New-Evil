using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.Advertisements;
//using UnityEngine.Purchasing;
using UnityEngine.PostProcessing;

public class SystemsProcess : MonoBehaviour/*, IStoreListener*/ {

    public bool IsDebugMenuOn = false;
    public GameObject DebugMenu;

    public GameObject blockAllInput;

    public GameObject[] highScoreName;

    public GameObject[] highScore;

    public bool saveHighScore = false;
    public int highscorepalce;

    public GameObject leburhighsc;
    public GameObject newname;
    public GameObject leburhighscnum;

    public Button highsetting;
    public Button lowsetting;
    public Light[] lightsforscalability;
    public PostProcessingBehaviour[] camerasForScalability;
    public PostProcessingProfile lowsettings;
    public PostProcessingProfile highsettings;
    public ReflectionProbe reflectionprobe;

    public GameObject resetProgress;
    public GameObject progressReset;
    public GameObject removeAds;
    public GameObject removeAdsFailed;
    public GameObject resetScoreConfirm;
    public GameObject loadingScreen;

    public GameObject removeAdsButton;
    public GameObject removeAdsText;

    public GameObject gamePause;
    public GameObject hud;
    public GameObject gameOver;

    public float switchToGameLength;

    public GameObject logo;

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject highScoremenu;

    public GameObject tutorialScreen1;
    public GameObject tutorialScreen2;
    public GameObject tutorialScreen3;
    public GameObject tutorialScreen4;
    public GameObject tutorialScreen5;
    public GameObject tutorialScreen6;

    public GameObject mainMenuCamera;
    public GameObject settingsMenuCamera;
    public GameObject creditsMenuCamera;
    public GameObject highscorecamera;

    public GameObject weapon1Cam;
    public GameObject weapon2Cam;
    public GameObject weapon3Cam;
    public GameObject weapon4Cam;
    public GameObject weapon5Cam;
    public GameObject weapon6Cam;
    public GameObject weapon7Cam;
    public GameObject weapon8Cam;
    public GameObject weapon9Cam;
    public GameObject weapon10Cam;

    public GameObject blockade1Cam;
    public GameObject blockade2Cam;
    public GameObject blockade3Cam;
    public GameObject blockade4Cam;
    public GameObject blockade5Cam;
    public GameObject blockade6Cam;
    public GameObject blockade7Cam;
    public GameObject blockade8Cam;

    public GameObject gameCamera;

    //SwitchToMenu()
    GameObject fromMenuCam;
    GameObject toMenuCam;
    Vector3 fromMenuCamPos;
    Quaternion fromMenuCamRot;
    float fromMenuCamFOV;

    bool switchingToGame = false;

    bool isSwitching = false;

    public float switchLength;

    float switchTime;

    bool switchBackMain = false;

    //Vector3 playerStartPos;
    //Quaternion playerStartRot;

    public Vector3 playerCamStartPos;
    public Quaternion playerCamStartRot;
    public float playerCamStartFOV;

    //IStoreController storeController;

    void togglescalability()
    {
        if (PlayerPrefs.GetInt("LowQuality") == 1)
        {
            PlayerPrefs.SetInt("LowQuality", 0);
        }
        else
        {
            PlayerPrefs.SetInt("LowQuality", 1);
        }
        setscalability();
    }

    void setscalability()
    {
        if (PlayerPrefs.GetInt("LowQuality") == 1)
        {
            QualitySettings.vSyncCount = 0;
            reflectionprobe.resolution = 16;
            highsetting.interactable = true;
            lowsetting.interactable = false;
            foreach (Light light in lightsforscalability)
            {
                light.shadowResolution = UnityEngine.Rendering.LightShadowResolution.Low;
            }
            foreach (PostProcessingBehaviour cameraobject in camerasForScalability)
            {
                cameraobject.profile = lowsettings;
            }
        }
        else
        {
            QualitySettings.vSyncCount = 1;
            reflectionprobe.resolution = 128;
            highsetting.interactable = false;
            lowsetting.interactable = true;
            foreach (Light light in lightsforscalability)
            {
                light.shadowResolution = UnityEngine.Rendering.LightShadowResolution.Medium;
            }
            foreach (PostProcessingBehaviour cameraobject in camerasForScalability)
            {
                cameraobject.profile = highsettings;
            }
        }
        reflectionprobe.RenderProbe();
    }

    void Start () {
        //ShowAd();
        

        if (IsDebugMenuOn == true)
        {
            DebugMenu.SetActive(true);
        }
        logo.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
        mainMenu.gameObject.tag = "Active Menu";
        mainMenuCamera.gameObject.SetActive(true);
        mainMenuCamera.gameObject.tag = "MainCamera";
        setscalability();
        GameObject.Find("Player").GetComponent<PlaySoundEffect>().MusicManager(PlaySoundEffect.MusicMood.MainMenu);

            //ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            //builder.AddProduct("com.jamsers.projectone.removeads", ProductType.NonConsumable);
            //UnityPurchasing.Initialize(this, builder);

        //playerStartPos = GameObject.FindWithTag("Player").transform.position;
        //playerStartRot = GameObject.FindWithTag("Player").transform.rotation;

        //playerCamStartPos = GameObject.Find("Game Camera").transform.position;
        //playerCamStartRot = GameObject.Find("Game Camera").transform.rotation;
        //playerCamStartFOV = GameObject.Find("Game Camera").GetComponent<Camera>().fieldOfView;
    }
	
	void Update () {
        if (isSwitching == true)
            SwitchToMenu();
	}

    /*public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs arguments) {
        if (string.Equals(arguments.purchasedProduct.definition.id, "com.jamsers.projectone.removeads", System.StringComparison.Ordinal)) {
            removeAds.gameObject.SetActive(true);
            removeAdsText.GetComponent<Text>().text = "Ads removed";
            removeAdsButton.GetComponent<Button>().interactable = false;
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason) {
        removeAdsFailed.gameObject.SetActive(true);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
        storeController = controller;
        if (storeController.products.WithID("com.jamsers.projectone.removeads").hasReceipt == true) {
            removeAdsText.GetComponent<Text>().text = "Ads removed";
        }
        else {
            removeAdsButton.GetComponent<Button>().interactable = true;
        }
    }

    public void OnInitializeFailed(InitializationFailureReason reason) {
        Debug.Log("IAP INITIALIZATION FAILED!!!!!! REASON: " + reason);
    }*/

    public Text debuglevel;
    public Text debugweapon;
    public Text debugpoints;

    public void OpenPrompt (int mode)
    {
        GameObject.Find("Player").GetComponent<PlaySoundEffect>().PlaySound(16);
        if (mode == 1)
        {
            resetProgress.gameObject.SetActive(true);
        }
        else if (mode == 2)
        {
            loadingScreen.SetActive(true);
            PlayerPrefs.DeleteAll();
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //resetProgress.gameObject.SetActive(false);
            //progressReset.gameObject.SetActive(true);
        }
        else if (mode == 3)
        {
            progressReset.gameObject.SetActive(false);
        }
        else if (mode == 4)
        {
            resetProgress.gameObject.SetActive(false);
        }
        else if (mode == 5)
        {
            removeAds.gameObject.SetActive(false);
        }
        else if (mode == 6)
        {
            //removeAds.gameObject.SetActive(true);
            /*if (storeController != null) {
                //TODO: check if there's reciept for product, then tell user if there is "you've already removed ads"
                if (storeController.products.WithID("com.jamsers.projectone.removeads") != null && storeController.products.WithID("com.jamsers.projectone.removeads").availableToPurchase) {
                    storeController.InitiatePurchase(storeController.products.WithID("com.jamsers.projectone.removeads"));
                }
            }*/
            //TODO: ELSE iap not initializeD yet pop up bla bla


            GetComponent<EnemySpawner>().ToggleLighting();
        }
        else if (mode == 7)
        {
            if (Time.timeScale != 0)
            {
                hud.SetActive(false);
                gamePause.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else if (mode == 8)
        {
            if (Time.timeScale == 0)
            {
                hud.SetActive(true);
                gamePause.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else if (mode == 9)
        {
            hud.SetActive(false);
            gamePause.SetActive(false);
            gameOver.SetActive(true);
        }
        else if (mode == 10) {
            resetScoreConfirm.SetActive(false);
        }
        else if (mode == 11) {
            loadingScreen.SetActive(true);
            PlayerPrefs.DeleteKey("High Score 1");
            PlayerPrefs.DeleteKey("High Score Name 1");
            PlayerPrefs.DeleteKey("High Score 2");
            PlayerPrefs.DeleteKey("High Score Name 2");
            PlayerPrefs.DeleteKey("High Score 3");
            PlayerPrefs.DeleteKey("High Score Name 3");
            PlayerPrefs.DeleteKey("High Score 4");
            PlayerPrefs.DeleteKey("High Score Name 4");
            PlayerPrefs.DeleteKey("High Score 5");
            PlayerPrefs.DeleteKey("High Score Name 5");
            PlayerPrefs.DeleteKey("High Score 6");
            PlayerPrefs.DeleteKey("High Score Name 6");
            PlayerPrefs.DeleteKey("High Score 7");
            PlayerPrefs.DeleteKey("High Score Name 7");
            PlayerPrefs.DeleteKey("High Score 8");
            PlayerPrefs.DeleteKey("High Score Name 8");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (mode == 12) {
            removeAdsFailed.SetActive(false);
        }
        else if (mode == 13) {
            tutorialScreen1.SetActive(false);
            Time.timeScale = 1;
        }
        else if (mode == 14) {
            tutorialScreen2.SetActive(false);
            Time.timeScale = 1;
        }
        else if (mode == 15) {
            tutorialScreen3.SetActive(false);
            Time.timeScale = 1;
        }
        else if (mode == 16) {
            tutorialScreen4.SetActive(false);
            Time.timeScale = 1;
        }
        else if (mode == 17) {
            tutorialScreen5.SetActive(false);
            Time.timeScale = 1;
        }
        else if (mode == 18) {
            tutorialScreen6.SetActive(false);
            //Time.timeScale = 1;
            GoToSettings(4);
        }
        else if (mode == 19)
        {
            togglescalability();
        }
        else if (mode == 20)
        {
            int leveltoswitchto = 0;
            bool isParseSucc = int.TryParse(debuglevel.text, out leveltoswitchto);
            if (leveltoswitchto > 29)
            {
                leveltoswitchto = 29;
            }
            else if (leveltoswitchto < 1)
            {
                leveltoswitchto = 1;
            }

            if (isParseSucc == true)
            {
                PlayerPrefs.SetInt("Level", leveltoswitchto);
            }
        }
        else if (mode == 21)
        {
            int weapontoswitchto = 0;
            bool isParseSucc = int.TryParse(debugweapon.text, out weapontoswitchto);
            if (weapontoswitchto > 8)
            {
                weapontoswitchto = 8;
            }
            else if (weapontoswitchto < 1)
            {
                weapontoswitchto = 1;
            }

            if (isParseSucc == true)
            {
                PlayerPrefs.SetInt("Weapon", weapontoswitchto);
            }
        }
        else if (mode == 22)
        {
            int pointstoswitchto = 0;
            bool isParseSucc = int.TryParse(debugpoints.text, out pointstoswitchto);
            if (pointstoswitchto > 1000000)
            {
                pointstoswitchto = 1000000;
            }
            else if (pointstoswitchto < 0)
            {
                pointstoswitchto = 0;
            }

            if (isParseSucc == true)
            {
                PlayerPrefs.SetInt("Upgrade Points", pointstoswitchto);
            }
        }
        else if (mode == 23)
        {
            Application.Quit();
        }
        else if (mode == 24)
        {
            if (Screen.fullScreen == true)
            {
                Screen.SetResolution(1024, 768, false);
            }
            else
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            }
            setscalability();
        }
        else
        {
            Debug.Log(mode);
        }
    }

    public bool lookMomImIngame = false;

    void SwitchToMenu ()
    {
        float switchTimePercent;
        if (lookMomImIngame == true) {
            switchTimePercent = (Time.time - switchTime) / switchLength;
        }
        else if (switchingToGame == true)
        {
            switchTimePercent = (Time.time - switchTime) / switchToGameLength;
        }
        else
            switchTimePercent = (Time.time - switchTime) / switchLength;

        if (switchTimePercent > 1 || switchBackMain == true)
        {
            isSwitching = false;
            fromMenuCam.gameObject.tag = "Untagged";
            fromMenuCam.gameObject.SetActive(false);
            toMenuCam.gameObject.tag = "MainCamera";
            toMenuCam.gameObject.SetActive(true);

            fromMenuCam.transform.position = fromMenuCamPos;
            fromMenuCam.transform.rotation = fromMenuCamRot;
            fromMenuCam.gameObject.GetComponent<Camera>().fieldOfView = fromMenuCamFOV;

            if (switchingToGame == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = false;
                //Debug.Log("dff");
                logo.SetActive(false);
                hud.SetActive(true);
                hud.tag = "Active Menu";
                GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = true;
                if (GetComponent<EnemySpawner>().transitionMode == 3) {
                    GetComponent<EnemySpawner>().level = 3;
                }
                else if (GetComponent<EnemySpawner>().transitionMode == 10) {
                    GetComponent<EnemySpawner>().level = 7;
                }
                else if (GetComponent<EnemySpawner>().transitionMode == 15) {
                    GetComponent<EnemySpawner>().level = 12;
                }
                else if (GetComponent<EnemySpawner>().transitionMode == 20) {
                    GetComponent<EnemySpawner>().level = 16;
                }
                else if (GetComponent<EnemySpawner>().transitionMode == 27) {
                    GetComponent<EnemySpawner>().level = 20;
                }
                else if (GetComponent<EnemySpawner>().transitionMode == 40) {
                    GetComponent<EnemySpawner>().level = 28;
                }
                GetComponent<EnemySpawner>().Level1();
                lookMomImIngame = true;
                pinkSuit.SetActive(true);
                //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().ResumeSkyfall();
                //Debug.Log("bop2");
            }

            //if (lookMomImIngame == true) {
                gameObject.GetComponent<EnemySpawner>().isTransitionDone = true;
            //}
            switchBackMain = false;
            //Debug.Log(toMenuCam);
            isAllInputEnabled(true);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().ResumeSkyfall();
        }
        else
        {
            fromMenuCam.transform.position = Vector3.Lerp(fromMenuCamPos, toMenuCam.transform.position, Mathf.SmoothStep(0f, 1f, switchTimePercent));
            fromMenuCam.transform.rotation = Quaternion.Lerp(fromMenuCamRot, toMenuCam.transform.rotation, Mathf.SmoothStep(0f, 1f, switchTimePercent));
            fromMenuCam.gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(fromMenuCamFOV, toMenuCam.gameObject.GetComponent<Camera>().fieldOfView, Mathf.SmoothStep(0f, 1f, switchTimePercent));
            isAllInputEnabled(false);
            //remove all
        }

    }

    public GameObject pinkSuit;

    public void isAllInputEnabled (bool isIt) {
        if (isIt == true) {
            blockAllInput.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOn = true;
        }
        else if (isIt == false) {
            blockAllInput.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOn = false;
        }
    }

    bool firstObjective4 = true;

    bool level29TutStop = true;
    bool level29TutStopCamOverride = false;

    public void GoToSettings (int objective)
    {
        GameObject.Find("Player").GetComponent<PlaySoundEffect>().PlaySound(16);
        GameObject objectiveCamera;
        GameObject objectiveMenu;
        //Debug.Log(objective);

        if (objective == 1)
        {
            objectiveCamera = mainMenuCamera;
            objectiveMenu = mainMenu;
        }
        else if (objective == 2)
        {
            objectiveCamera = settingsMenuCamera;
            objectiveMenu = settingsMenu;
        }
        else if (objective == 3)
        {
            objectiveCamera = creditsMenuCamera;
            objectiveMenu = creditsMenu;
        }
        else if (objective == 4)
        {
            if (GetComponent<EnemySpawner>().level == 29 && level29TutStop == true)
            {
                GetComponent<EnemySpawner>().ShowTutorial();
                level29TutStop = false;
                level29TutStopCamOverride = true;
                objectiveCamera = mainMenuCamera;
                objectiveMenu = mainMenu;
                mainMenu.transform.position = mainMenu.transform.position + new Vector3(0,-9999,0);
                GetComponent<EnemySpawner>().ShowTutorial();
            }
            else
            {
                if (firstObjective4 == true)
                {
                    //Debug.Log("Transition to current level lighting");
                    GetComponent<EnemySpawner>().isTransitioningBigCockTranny = true;
                    GetComponent<EnemySpawner>().TrannyStart = Time.time;
                    GetComponent<EnemySpawner>().TransitionLevelObjective = GetComponent<EnemySpawner>().level;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().ResumeSkyfall();
                }
                //Debug.Log("if you can't figure out how to make objective 4 work, make a new objective (sigh)");
                objectiveCamera = gameCamera;
                objectiveMenu = null;
                switchingToGame = true;
                //switchLength = switchToGameLength;
                //gamePause.tag = "Active Menu";
                //logo.SetActive(false);
                firstObjective4 = false;
            }
                
            
        }
        else if (objective == 5)
        {
            loadingScreen.SetActive(true);
            /*if (Advertisement.isInitialized == true && Advertisement.isShowing == false && Advertisement.IsReady() == true) {
                if (storeController != null) {
                    if (storeController.products.WithID("com.jamsers.projectone.removeads").hasReceipt == true) {
                        ExitAfterAd(new ShowResult());
                    }
                    else {
                        Advertisement.Show(null, new ShowOptions { resultCallback = ExitAfterAd });
                    }
                }
                else {
                    ExitAfterAd(new ShowResult());
                }
            }
            else {*/
                ExitAfterAd(/*new ShowResult()*/);
            //}

            objectiveCamera = mainMenuCamera;
            objectiveMenu = mainMenu;
        }
        else if (objective == 6) {
            objectiveCamera = highscorecamera;
            objectiveMenu = highScoremenu;
        }
        else if (objective == 7) {
            objectiveCamera = highscorecamera;
            objectiveMenu = highScoremenu;
            resetScoreConfirm.SetActive(true);
        }
        else if (objective == 8) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = false;
            objectiveCamera = blockade1Cam;
            objectiveMenu = null;
            //switchBackMain = false;
            switchingToGame = false;
        }
        else if (objective == 9) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            //GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = false;
            objectiveCamera = weapon1Cam;
            objectiveMenu = null;
            //switchBackMain = false;
            switchingToGame = false;
        }
        else if (objective == 10) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = false;
            objectiveCamera = blockade2Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 11) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            objectiveCamera = weapon2Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 12) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            objectiveCamera = weapon3Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 13) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = false;
            objectiveCamera = blockade3Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 14) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            objectiveCamera = weapon4Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 15) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = false;
            objectiveCamera = blockade4Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 16) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            objectiveCamera = weapon5Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 17) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = false;
            objectiveCamera = blockade5Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 18) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            objectiveCamera = weapon6Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 19) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            objectiveCamera = weapon7Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 20) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = false;
            objectiveCamera = blockade6Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 21) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            objectiveCamera = weapon8Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 22) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            //GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = false;
            objectiveCamera = blockade7Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 23) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            objectiveCamera = weapon9Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 24) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            //GameObject.FindWithTag("MainCamera").GetComponent<CameraAI>().isOn = false;
            objectiveCamera = blockade8Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 25) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
            objectiveCamera = weapon10Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else
        {
            objectiveCamera = null;
            objectiveMenu = null;
            Debug.Log("well damn");
        }

        //Debug.Log("well damn");

        fromMenuCam = GameObject.FindWithTag("MainCamera");
        if (objective == 4) {
            if (level29TutStopCamOverride == true)
            {
                level29TutStopCamOverride = false;
                toMenuCam = objectiveCamera;
            }
            else
            {
                toMenuCam = gameCamera;
            }
        }
        else {
            toMenuCam = objectiveCamera;
        }
        //Debug.Log(toMenuCam);
        //Debug.Log(objective);
        fromMenuCamPos = fromMenuCam.transform.position;
        fromMenuCamRot = fromMenuCam.transform.rotation;
        fromMenuCamFOV = fromMenuCam.gameObject.GetComponent<Camera>().fieldOfView;
        switchTime = Time.time;

        //mainMenu.gameObject.SetActive(false);
        GameObject actmen = GameObject.FindWithTag("Active Menu");

        if (objective != 9 && objective != 11 && objective != 12 && objective != 14 && objective != 16 && objective != 18 && objective != 19 && objective != 21 && objective != 23 && objective != 25) {
            if (lookMomImIngame != true) {
                actmen.gameObject.SetActive(false);
                actmen.gameObject.tag = "Untagged";
            }
        }

        if (objective != 8 && objective != 9 && objective != 10 && objective != 11 && objective != 12 && objective != 13 && objective != 14 && objective != 15 && objective != 16 && objective != 17 && objective != 18 && objective != 19 && objective != 20 && objective != 21 && objective != 22 && objective != 23 && objective != 24 && objective != 25) {
            if (switchingToGame == false) {
                objectiveMenu.gameObject.SetActive(true);
                objectiveMenu.gameObject.tag = "Active Menu";
            }
        }

        //MASSIVE SUCCESS - NOW TRANSITION TO WEAPON CAM

        /*if (switchingToGame == false)
        {
            objectiveMenu.gameObject.SetActive(true);
            objectiveMenu.gameObject.tag = "Active Menu";
        }*/

        isSwitching = true;
        
    }

    void ExitAfterAd (/*ShowResult x*/) {
        Time.timeScale = 1;
        if (saveHighScore == true) {
            if (highscorepalce == 0) {
                PlayerPrefs.SetInt("High Score 8", PlayerPrefs.GetInt("High Score 7"));
                PlayerPrefs.SetString("High Score Name 8", PlayerPrefs.GetString("High Score Name 7"));
                PlayerPrefs.SetInt("High Score 7", PlayerPrefs.GetInt("High Score 6"));
                PlayerPrefs.SetString("High Score Name 7", PlayerPrefs.GetString("High Score Name 6"));
                PlayerPrefs.SetInt("High Score 6", PlayerPrefs.GetInt("High Score 5"));
                PlayerPrefs.SetString("High Score Name 6", PlayerPrefs.GetString("High Score Name 5"));
                PlayerPrefs.SetInt("High Score 5", PlayerPrefs.GetInt("High Score 4"));
                PlayerPrefs.SetString("High Score Name 5", PlayerPrefs.GetString("High Score Name 4"));
                PlayerPrefs.SetInt("High Score 4", PlayerPrefs.GetInt("High Score 3"));
                PlayerPrefs.SetString("High Score Name 4", PlayerPrefs.GetString("High Score Name 3"));
                PlayerPrefs.SetInt("High Score 3", PlayerPrefs.GetInt("High Score 2"));
                PlayerPrefs.SetString("High Score Name 3", PlayerPrefs.GetString("High Score Name 2"));
                PlayerPrefs.SetInt("High Score 2", PlayerPrefs.GetInt("High Score 1"));
                PlayerPrefs.SetString("High Score Name 2", PlayerPrefs.GetString("High Score Name 1"));
                PlayerPrefs.SetInt("High Score 1", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().upgradePoints);
                PlayerPrefs.SetString("High Score Name 1", newname.GetComponent<Text>().text);
            }
            else if (highscorepalce == 1) {
                PlayerPrefs.SetInt("High Score 8", PlayerPrefs.GetInt("High Score 7"));
                PlayerPrefs.SetString("High Score Name 8", PlayerPrefs.GetString("High Score Name 7"));
                PlayerPrefs.SetInt("High Score 7", PlayerPrefs.GetInt("High Score 6"));
                PlayerPrefs.SetString("High Score Name 7", PlayerPrefs.GetString("High Score Name 6"));
                PlayerPrefs.SetInt("High Score 6", PlayerPrefs.GetInt("High Score 5"));
                PlayerPrefs.SetString("High Score Name 6", PlayerPrefs.GetString("High Score Name 5"));
                PlayerPrefs.SetInt("High Score 5", PlayerPrefs.GetInt("High Score 4"));
                PlayerPrefs.SetString("High Score Name 5", PlayerPrefs.GetString("High Score Name 4"));
                PlayerPrefs.SetInt("High Score 4", PlayerPrefs.GetInt("High Score 3"));
                PlayerPrefs.SetString("High Score Name 4", PlayerPrefs.GetString("High Score Name 3"));
                PlayerPrefs.SetInt("High Score 3", PlayerPrefs.GetInt("High Score 2"));
                PlayerPrefs.SetString("High Score Name 3", PlayerPrefs.GetString("High Score Name 2"));
                PlayerPrefs.SetInt("High Score 2", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().upgradePoints);
                PlayerPrefs.SetString("High Score Name 2", newname.GetComponent<Text>().text);
            }
            else if (highscorepalce == 2) {
                PlayerPrefs.SetInt("High Score 8", PlayerPrefs.GetInt("High Score 7"));
                PlayerPrefs.SetString("High Score Name 8", PlayerPrefs.GetString("High Score Name 7"));
                PlayerPrefs.SetInt("High Score 7", PlayerPrefs.GetInt("High Score 6"));
                PlayerPrefs.SetString("High Score Name 7", PlayerPrefs.GetString("High Score Name 6"));
                PlayerPrefs.SetInt("High Score 6", PlayerPrefs.GetInt("High Score 5"));
                PlayerPrefs.SetString("High Score Name 6", PlayerPrefs.GetString("High Score Name 5"));
                PlayerPrefs.SetInt("High Score 5", PlayerPrefs.GetInt("High Score 4"));
                PlayerPrefs.SetString("High Score Name 5", PlayerPrefs.GetString("High Score Name 4"));
                PlayerPrefs.SetInt("High Score 4", PlayerPrefs.GetInt("High Score 3"));
                PlayerPrefs.SetString("High Score Name 4", PlayerPrefs.GetString("High Score Name 3"));
                PlayerPrefs.SetInt("High Score 3", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().upgradePoints);
                PlayerPrefs.SetString("High Score Name 3", newname.GetComponent<Text>().text);
            }
            else if (highscorepalce == 3) {
                PlayerPrefs.SetInt("High Score 8", PlayerPrefs.GetInt("High Score 7"));
                PlayerPrefs.SetString("High Score Name 8", PlayerPrefs.GetString("High Score Name 7"));
                PlayerPrefs.SetInt("High Score 7", PlayerPrefs.GetInt("High Score 6"));
                PlayerPrefs.SetString("High Score Name 7", PlayerPrefs.GetString("High Score Name 6"));
                PlayerPrefs.SetInt("High Score 6", PlayerPrefs.GetInt("High Score 5"));
                PlayerPrefs.SetString("High Score Name 6", PlayerPrefs.GetString("High Score Name 5"));
                PlayerPrefs.SetInt("High Score 5", PlayerPrefs.GetInt("High Score 4"));
                PlayerPrefs.SetString("High Score Name 5", PlayerPrefs.GetString("High Score Name 4"));
                PlayerPrefs.SetInt("High Score 4", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().upgradePoints);
                PlayerPrefs.SetString("High Score Name 4", newname.GetComponent<Text>().text);
            }
            else if (highscorepalce == 4) {
                PlayerPrefs.SetInt("High Score 8", PlayerPrefs.GetInt("High Score 7"));
                PlayerPrefs.SetString("High Score Name 8", PlayerPrefs.GetString("High Score Name 7"));
                PlayerPrefs.SetInt("High Score 7", PlayerPrefs.GetInt("High Score 6"));
                PlayerPrefs.SetString("High Score Name 7", PlayerPrefs.GetString("High Score Name 6"));
                PlayerPrefs.SetInt("High Score 6", PlayerPrefs.GetInt("High Score 5"));
                PlayerPrefs.SetString("High Score Name 6", PlayerPrefs.GetString("High Score Name 5"));
                PlayerPrefs.SetInt("High Score 5", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().upgradePoints);
                PlayerPrefs.SetString("High Score Name 5", newname.GetComponent<Text>().text);
            }
            else if (highscorepalce == 5) {
                PlayerPrefs.SetInt("High Score 8", PlayerPrefs.GetInt("High Score 7"));
                PlayerPrefs.SetString("High Score Name 8", PlayerPrefs.GetString("High Score Name 7"));
                PlayerPrefs.SetInt("High Score 7", PlayerPrefs.GetInt("High Score 6"));
                PlayerPrefs.SetString("High Score Name 7", PlayerPrefs.GetString("High Score Name 6"));
                PlayerPrefs.SetInt("High Score 6", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().upgradePoints);
                PlayerPrefs.SetString("High Score Name 6", newname.GetComponent<Text>().text);
            }
            else if (highscorepalce == 6) {
                PlayerPrefs.SetInt("High Score 8", PlayerPrefs.GetInt("High Score 7"));
                PlayerPrefs.SetString("High Score Name 8", PlayerPrefs.GetString("High Score Name 7"));
                PlayerPrefs.SetInt("High Score 7", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().upgradePoints);
                PlayerPrefs.SetString("High Score Name 7", newname.GetComponent<Text>().text);
            }
            else if (highscorepalce == 7) {
                PlayerPrefs.SetInt("High Score 8", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().upgradePoints);
                PlayerPrefs.SetString("High Score Name 8", newname.GetComponent<Text>().text);
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
