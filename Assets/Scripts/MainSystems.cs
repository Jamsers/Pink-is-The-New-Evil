using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using System.Linq;
using UnityStandardAssets.Water;

public class MainSystems : MonoBehaviour {
    [Header("Debug Options")]
    public bool debugMenusEnabled;
    public bool debugRawRender;
    public bool debugDisableSpawning;
    public bool debugInvulnerable;

    [Header("")]
    public Button MainMenuDefaultButton;
    public Button SettingsDefaultButton;
    public Button CreditsDefaultButton;
    public Button HighScoreDefaultButton;
    public Button ResetConfirmDefaultButton;
    public Button HighScoreResetConfirmDefaultButton;
    public Button HighScalabilitySelectSwitchToButton;
    public Button LowScalabilitySelectSwitchToButton;
    public Button PauseScreenDefaultButton;
    public Toggle debugPlayerInvulnerabilityToggle;
    public Toggle debugDisableSpawningToggle;
    public Toggle debugRawRenderToggle;


    public GameObject DebugMenu;

    public GameObject blockAllInput;

    public GameObject[] highScoreName;

    public GameObject[] highScore;

    public bool saveHighScore = false;

    public GameObject leburhighsc;
    public GameObject newname;
    public GameObject leburhighscnum;

    public Button highsetting;
    public Button lowsetting;
    public Light[] lightsforscalability;
    public Light[] lightsForToggleScalability;
    public LayerMask reflectionScalabilityMask;
    public WaterBase waterForScalability;
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

    public GameObject toggleLightingButton;
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

    public Vector3 cameraStartPosition;
    public Quaternion cameraStartRotation;
    public float cameraStartFOV;

    public struct HighScorePair {
        public string highScoreName;
        public int highScoreValue;

        public HighScorePair(string highScoreName, int highScoreValue) {
            this.highScoreName = highScoreName;
            this.highScoreValue = highScoreValue;
        }
    }

    public const int HighScoreListLength = 8;

    public HighScorePair[] currentHighScoreList = new HighScorePair[HighScoreListLength + 1];

    void togglescalability() {
        if (PlayerPrefs.GetInt("LowQuality") == 1) {
            PlayerPrefs.SetInt("LowQuality", 0);
        }
        else {
            PlayerPrefs.SetInt("LowQuality", 1);
        }
        setscalability();
    }

    public void setscalability() {
        ScalabilitySettings scalabilitySettings;

        if (debugRawRender) {
            scalabilitySettings = rawScalabilitySettings;
            highsetting.interactable = false;
            lowsetting.interactable = false;
        }
        else if (PlayerPrefs.GetInt("LowQuality") == 1) {
            scalabilitySettings = lowScalabilitySettings;
            highsetting.interactable = true;
            lowsetting.interactable = false;
        }
        else {
            scalabilitySettings = highScalabilitySettings;
            highsetting.interactable = false;
            lowsetting.interactable = true;
        }

        QualitySettings.vSyncCount = scalabilitySettings.vSyncCount;
        reflectionprobe.resolution = scalabilitySettings.reflectionResolution;
        reflectionprobe.hdr = scalabilitySettings.reflectionHdr;
        reflectionprobe.cullingMask = scalabilitySettings.reflectionMask;
        waterForScalability.waterQuality = scalabilitySettings.waterQuality;
        waterForScalability.edgeBlend = scalabilitySettings.waterEdgeBlend;
        foreach (Light light in lightsforscalability) {
            light.shadows = scalabilitySettings.lightShadows;
            light.shadowResolution = scalabilitySettings.lightShadowResolution;
        }
        foreach (Light light in lightsForToggleScalability) {
            light.shadows = scalabilitySettings.lightShadows;
        }
        foreach (PostProcessingBehaviour cameraobject in camerasForScalability) {
            cameraobject.profile = scalabilitySettings.postProcessingProfile;
        }

        reflectionprobe.RenderProbe();
    }

    [System.Serializable]
    public struct ScalabilitySettings {
        public int vSyncCount;
        public int reflectionResolution;
        public bool reflectionHdr;
        public LayerMask reflectionMask;
        public WaterQuality waterQuality;
        public bool waterEdgeBlend;
        public LightShadows lightShadows;
        public UnityEngine.Rendering.LightShadowResolution lightShadowResolution;
        public PostProcessingProfile postProcessingProfile;
    }

    public ScalabilitySettings rawScalabilitySettings;
    public ScalabilitySettings lowScalabilitySettings;
    public ScalabilitySettings highScalabilitySettings;

    void Start() {

        Cursor.visible = true;

        if (debugMenusEnabled == true) {
            DebugMenu.SetActive(true);
            debugDisableSpawningToggle.gameObject.SetActive(true);
            debugRawRenderToggle.gameObject.SetActive(true);
        }
        logo.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
        mainMenu.gameObject.tag = "Active Menu";
        mainMenuCamera.gameObject.SetActive(true);
        mainMenuCamera.gameObject.tag = "MainCamera";
        setscalability();
        Invoke("setscalability", 1f);
        PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.MainMenu);

        for (int i = 0; i < HighScoreListLength; i++) {
            string highScoreNameRetrieved = PlayerPrefs.GetString("High Score Name " + (i + 1));
            int highScoreValueRetrieved = PlayerPrefs.GetInt("High Score " + (i + 1));
            currentHighScoreList[i] = new HighScorePair(highScoreNameRetrieved, highScoreValueRetrieved);
        }

        debugPlayerInvulnerabilityToggle.isOn = debugInvulnerable;
        debugDisableSpawningToggle.isOn = debugDisableSpawning;
        debugRawRenderToggle.isOn = debugRawRender;
    }

    void Update() {
        if (isSwitching == true)
            SwitchToMenu();
    }

    public Text debuglevel;
    public Text debugweapon;
    public Text debugpoints;

    public void OpenPrompt(int mode) {
        PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().PlaySound(16);
        if (mode == 1) {
            resetProgress.gameObject.SetActive(true);
            ResetConfirmDefaultButton.Select();
        }
        else if (mode == 2) {
            loadingScreen.SetActive(true);
            PlayerPrefs.DeleteAll();
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (mode == 3) {
            progressReset.gameObject.SetActive(false);
        }
        else if (mode == 4) {
            resetProgress.gameObject.SetActive(false);
            SettingsDefaultButton.Select();
        }
        else if (mode == 5) {
            removeAds.gameObject.SetActive(false);
        }
        else if (mode == 6) {
            GetComponent<EnemySpawner>().ToggleLighting();
        }
        else if (mode == 7) {
            if (Time.timeScale != 0) {
                PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().AllowMusicToPlayWhilePaused(false);
                hud.SetActive(false);
                gamePause.SetActive(true);
                Time.timeScale = 0;
                PauseScreenDefaultButton.Select();
                Cursor.visible = true;
            }
        }
        else if (mode == 8) {
            if (Time.timeScale == 0) {
                PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().AllowMusicToPlayWhilePaused(true);
                hud.SetActive(true);
                gamePause.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else if (mode == 9) {
            hud.SetActive(false);
            gamePause.SetActive(false);
            gameOver.SetActive(true);
            Cursor.visible = true;
        }
        else if (mode == 10) {
            resetScoreConfirm.SetActive(false);
            HighScoreDefaultButton.Select();
        }
        else if (mode == 11) {
            loadingScreen.SetActive(true);
            for (int i = 0; i < HighScoreListLength; i++) {
                PlayerPrefs.DeleteKey("High Score Name " + (i + 1));
                PlayerPrefs.DeleteKey("High Score " + (i + 1));
            }
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
            GoToSettings(4);
        }
        else if (mode == 19) {
            togglescalability();
            if (PlayerPrefs.GetInt("LowQuality") == 1) {
                LowScalabilitySelectSwitchToButton.Select();
            }
            else {
                HighScalabilitySelectSwitchToButton.Select();
            }
        }
        else if (mode == 20) {
            int leveltoswitchto = 0;
            bool isParseSucc = int.TryParse(debuglevel.text, out leveltoswitchto);
            if (leveltoswitchto > 29) {
                leveltoswitchto = 29;
            }
            else if (leveltoswitchto < 1) {
                leveltoswitchto = 1;
            }

            if (isParseSucc == true) {
                PlayerPrefs.SetInt("Level", leveltoswitchto);
            }
        }
        else if (mode == 21) {
            int weapontoswitchto = 0;
            bool isParseSucc = int.TryParse(debugweapon.text, out weapontoswitchto);
            if (weapontoswitchto > 8) {
                weapontoswitchto = 8;
            }
            else if (weapontoswitchto < 1) {
                weapontoswitchto = 1;
            }

            if (isParseSucc == true) {
                PlayerPrefs.SetInt("Weapon", weapontoswitchto);
            }
        }
        else if (mode == 22) {
            int pointstoswitchto = 0;
            bool isParseSucc = int.TryParse(debugpoints.text, out pointstoswitchto);
            if (pointstoswitchto > 1000000) {
                pointstoswitchto = 1000000;
            }
            else if (pointstoswitchto < 0) {
                pointstoswitchto = 0;
            }

            if (isParseSucc == true) {
                PlayerPrefs.SetInt("Upgrade Points", pointstoswitchto);
            }
        }
        else if (mode == 23) {
            Application.Quit();
        }
        else if (mode == 24) {
            if (Screen.fullScreen == true) {
                Screen.SetResolution(1024, 768, false);
            }
            else {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            }
            setscalability();
        }
        else if (mode == 25) {
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in allEnemies) {
                enemy.GetComponent<EnemyAI>().Health(-10000);
            }
        }
        else if (mode == 26) {
            debugInvulnerable = debugPlayerInvulnerabilityToggle.isOn;
        }
        else if (mode == 27) {
            debugDisableSpawning = debugDisableSpawningToggle.isOn;
        }
        else if (mode == 28) {
            debugRawRender = debugRawRenderToggle.isOn;
            setscalability();
        }
        else {
            Debug.Log(mode);
        }
    }

    public bool lookMomImIngame = false;

    void SwitchToMenu() {
        float switchTimePercent;
        if (lookMomImIngame == true) {
            switchTimePercent = (Time.time - switchTime) / switchLength;
        }
        else if (switchingToGame == true) {
            switchTimePercent = (Time.time - switchTime) / switchToGameLength;
        }
        else
            switchTimePercent = (Time.time - switchTime) / switchLength;

        if (switchTimePercent > 1 || switchBackMain == true) {
            isSwitching = false;
            fromMenuCam.gameObject.tag = "Untagged";
            fromMenuCam.gameObject.SetActive(false);
            toMenuCam.gameObject.tag = "MainCamera";
            toMenuCam.gameObject.SetActive(true);

            fromMenuCam.transform.position = fromMenuCamPos;
            fromMenuCam.transform.rotation = fromMenuCamRot;
            fromMenuCam.gameObject.GetComponent<Camera>().fieldOfView = fromMenuCamFOV;

            if (switchingToGame == true) {
                PinkIsTheNewEvil.PlayerController.isControlOff = false;
                logo.SetActive(false);
                hud.SetActive(true);
                hud.tag = "Active Menu";
                PinkIsTheNewEvil.CameraLogic.isTracking = true;
                if (PinkIsTheNewEvil.EnemySpawner.transitionMode == 3) {
                    PinkIsTheNewEvil.EnemySpawner.level = 3;
                }
                else if (PinkIsTheNewEvil.EnemySpawner.transitionMode == 10) {
                    PinkIsTheNewEvil.EnemySpawner.level = 7;
                }
                else if (PinkIsTheNewEvil.EnemySpawner.transitionMode == 15) {
                    PinkIsTheNewEvil.EnemySpawner.level = 12;
                }
                else if (PinkIsTheNewEvil.EnemySpawner.transitionMode == 20) {
                    PinkIsTheNewEvil.EnemySpawner.level = 16;
                }
                else if (PinkIsTheNewEvil.EnemySpawner.transitionMode == 27) {
                    PinkIsTheNewEvil.EnemySpawner.level = 20;
                }
                else if (PinkIsTheNewEvil.EnemySpawner.transitionMode == 40) {
                    PinkIsTheNewEvil.EnemySpawner.level = 28;
                }
                PinkIsTheNewEvil.EnemySpawner.LevelSetup();
                lookMomImIngame = true;
                pinkSuit.SetActive(true);

            }

            PinkIsTheNewEvil.EnemySpawner.isTransitionDone = true;
            switchBackMain = false;
            isAllInputEnabled(true);
        }
        else {
            fromMenuCam.transform.position = Vector3.Lerp(fromMenuCamPos, toMenuCam.transform.position, Mathf.SmoothStep(0f, 1f, switchTimePercent));
            fromMenuCam.transform.rotation = Quaternion.Lerp(fromMenuCamRot, toMenuCam.transform.rotation, Mathf.SmoothStep(0f, 1f, switchTimePercent));
            fromMenuCam.gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(fromMenuCamFOV, toMenuCam.gameObject.GetComponent<Camera>().fieldOfView, Mathf.SmoothStep(0f, 1f, switchTimePercent));
            isAllInputEnabled(false);
        }

    }

    public GameObject pinkSuit;

    public void isAllInputEnabled(bool isIt) {
        if (isIt == true) {
            blockAllInput.SetActive(false);
            PinkIsTheNewEvil.PlayerController.isControlOn = true;
        }
        else if (isIt == false) {
            blockAllInput.SetActive(true);
            PinkIsTheNewEvil.PlayerController.isControlOn = false;
        }
    }

    bool firstObjective4 = true;

    bool level29TutStop = true;
    bool level29TutStopCamOverride = false;

    public void GoToSettings(int objective) {
        PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().PlaySound(16);
        GameObject objectiveCamera;
        GameObject objectiveMenu;

        if (objective == 1) {
            objectiveCamera = mainMenuCamera;
            objectiveMenu = mainMenu;
            MainMenuDefaultButton.Select();
        }
        else if (objective == 2) {
            objectiveCamera = settingsMenuCamera;
            objectiveMenu = settingsMenu;
            SettingsDefaultButton.Select();
        }
        else if (objective == 3) {
            objectiveCamera = creditsMenuCamera;
            objectiveMenu = creditsMenu;
            CreditsDefaultButton.Select();
        }
        else if (objective == 4) {
            if (PinkIsTheNewEvil.EnemySpawner.level == 29 && level29TutStop == true) {
                PinkIsTheNewEvil.EnemySpawner.ShowTutorial();
                level29TutStop = false;
                level29TutStopCamOverride = true;
                objectiveCamera = mainMenuCamera;
                objectiveMenu = mainMenu;
                mainMenu.transform.position = mainMenu.transform.position + new Vector3(0, -9999, 0);
                PinkIsTheNewEvil.EnemySpawner.ShowTutorial();
            }
            else {
                if (firstObjective4 == true) {
                    PinkIsTheNewEvil.EnemySpawner.isLightingTransitioning = true;
                    PinkIsTheNewEvil.EnemySpawner.lightTransitionStart = Time.time;
                    PinkIsTheNewEvil.EnemySpawner.TransitionLevelObjective = PinkIsTheNewEvil.EnemySpawner.level;
                    PinkIsTheNewEvil.PlayerController.ResumeSkyfall();
                }
                objectiveCamera = gameCamera;
                objectiveMenu = null;
                switchingToGame = true;
                firstObjective4 = false;
            }


        }
        else if (objective == 5) {
            loadingScreen.SetActive(true);
            ExitAfterAd();

            objectiveCamera = mainMenuCamera;
            objectiveMenu = mainMenu;
        }
        else if (objective == 6) {
            objectiveCamera = highscorecamera;
            objectiveMenu = highScoremenu;
            HighScoreDefaultButton.Select();
        }
        else if (objective == 7) {
            objectiveCamera = highscorecamera;
            objectiveMenu = highScoremenu;
            resetScoreConfirm.SetActive(true);
            HighScoreResetConfirmDefaultButton.Select();
        }
        else if (objective == 8) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            PinkIsTheNewEvil.CameraLogic.isTracking = false;
            objectiveCamera = blockade1Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 9) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon1Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 10) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            PinkIsTheNewEvil.CameraLogic.isTracking = false;
            objectiveCamera = blockade2Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 11) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon2Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 12) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon3Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 13) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            PinkIsTheNewEvil.CameraLogic.isTracking = false;
            objectiveCamera = blockade3Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 14) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon4Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 15) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            PinkIsTheNewEvil.CameraLogic.isTracking = false;
            objectiveCamera = blockade4Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 16) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon5Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 17) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            PinkIsTheNewEvil.CameraLogic.isTracking = false;
            objectiveCamera = blockade5Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 18) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon6Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 19) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon7Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 20) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            PinkIsTheNewEvil.CameraLogic.isTracking = false;
            objectiveCamera = blockade6Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 21) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon8Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 22) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = blockade7Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 23) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon9Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 24) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = blockade8Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else if (objective == 25) {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
            objectiveCamera = weapon10Cam;
            objectiveMenu = null;
            switchingToGame = false;
        }
        else {
            objectiveCamera = null;
            objectiveMenu = null;
            Debug.Log("well damn");
        }


        fromMenuCam = GameObject.FindWithTag("MainCamera");
        if (objective == 4) {
            if (level29TutStopCamOverride == true) {
                level29TutStopCamOverride = false;
                toMenuCam = objectiveCamera;
            }
            else {
                toMenuCam = gameCamera;
            }
        }
        else {
            toMenuCam = objectiveCamera;
        }
        fromMenuCamPos = fromMenuCam.transform.position;
        fromMenuCamRot = fromMenuCam.transform.rotation;
        fromMenuCamFOV = fromMenuCam.gameObject.GetComponent<Camera>().fieldOfView;
        switchTime = Time.time;

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

        isSwitching = true;

    }

    void ExitAfterAd() {
        Time.timeScale = 1;
        if (saveHighScore == true) {
            currentHighScoreList[HighScoreListLength] = new HighScorePair(newname.GetComponent<Text>().text, PinkIsTheNewEvil.PlayerController.upgradePoints);
            currentHighScoreList = currentHighScoreList.OrderByDescending(highScorePair => highScorePair.highScoreValue).ToArray();

            for (int i = 0; i < HighScoreListLength; i++) {
                PlayerPrefs.SetString("High Score Name " + (i + 1), currentHighScoreList[i].highScoreName);
                PlayerPrefs.SetInt("High Score " + (i + 1), currentHighScoreList[i].highScoreValue);
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}