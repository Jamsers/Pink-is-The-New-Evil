using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using System.Linq;
using UnityStandardAssets.Water;
using UnityEngine.Rendering;

public class MainSystems : MonoBehaviour {
    public float switchToGameLength;
    public float switchLength;
    public GameObject pinkSuit;

    [Header("Debug Options")]
    public bool debugMenusEnabled;
    public bool debugRawRender;
    public bool debugDisableSpawning;
    public bool debugInvulnerable;

    [Header("Scalability Data")]
    public ScalabilitySettings rawScalabilitySettings;
    public ScalabilitySettings lowScalabilitySettings;
    public ScalabilitySettings highScalabilitySettings;

    [Header("Main Menu Default Buttons")]
    public Button MainMenuDefaultButton;
    public Button SettingsDefaultButton;
    public Button CreditsDefaultButton;
    public Button HighScoreDefaultButton;
    public Button ResetConfirmDefaultButton;
    public Button HighScoreResetConfirmDefaultButton;
    public Button HighScalabilitySelectSwitchToButton;
    public Button LowScalabilitySelectSwitchToButton;
    public Button PauseScreenDefaultButton;

    [Header("UI")]
    public GameObject blockAllInput;
    public GameObject highScoreMessage;
    public GameObject highScoreNewName;
    public GameObject highScoreNewValue;
    public Button highSettingButton;
    public Button lowSettingButton;
    public GameObject resetProgress;
    public GameObject progressReset;
    public GameObject resetScoreConfirm;
    public GameObject loadingScreen;
    public GameObject toggleLightingButton;
    public GameObject gamePause;
    public GameObject hud;
    public GameObject gameOver;
    public GameObject logo;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject highScoremenu;
    public GameObject[] tutorialScreens;
    public GameObject[] highScoreName;
    public GameObject[] highScore;

    [Header("Scalability References")]
    public WaterBase waterForScalability;
    public Light[] lightsforscalability;
    public Light[] lightsForToggleScalability;
    public PostProcessingBehaviour[] camerasForScalability;

    [Header("Cameras")]
    public GameObject mainMenuCamera;
    public GameObject settingsMenuCamera;
    public GameObject creditsMenuCamera;
    public GameObject highscorecamera;
    public GameObject gameCamera;
    public GameObject[] weaponCameras;
    public GameObject[] blockadeCameras;

    [Header("Debug References")]
    public GameObject debugHUDMenuItems;
    public GameObject debugMainMenuItems;
    public Text debuglevel;
    public Text debugweapon;
    public Text debugpoints;
    public Toggle debugPlayerInvulnerabilityToggle;
    public Toggle debugDisableSpawningToggle;
    public Toggle debugRawRenderToggle;

    [HideInInspector] public const int HighScoreListLength = 8;
    [HideInInspector] public HighScorePair[] currentHighScoreList = new HighScorePair[HighScoreListLength + 1];
    [HideInInspector] public bool saveHighScore = false;
    [HideInInspector] public bool isInGame = false;
    [HideInInspector] public Vector3 cameraStartPosition;
    [HideInInspector] public Quaternion cameraStartRotation;
    [HideInInspector] public float cameraStartFOV;

    GameObject fromMenuCam;
    GameObject toMenuCam;
    Vector3 fromMenuCamPos;
    Quaternion fromMenuCamRot;
    float fromMenuCamFOV;
    float switchTime;
    bool isSwitching = false;
    bool switchBackMain = false;
    bool firstObjective4 = true;
    bool level29TutStop = true;
    bool level29TutStopCamOverride = false;
    CameraSwitchMode cameraSwitchMode = CameraSwitchMode.None;

    enum CameraSwitchMode {
        None,
        ToMenu,
        ToGame,
        ToWeapon,
        ToBarrier
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
        public LightShadowResolution lightShadowResolution;
        public PostProcessingProfile postProcessingProfile;
    }

    public struct HighScorePair {
        public string highScoreName;
        public int highScoreValue;

        public HighScorePair(string highScoreName, int highScoreValue) {
            this.highScoreName = highScoreName;
            this.highScoreValue = highScoreValue;
        }
    }

    void Start() {
        Cursor.visible = true;
        logo.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
        mainMenu.gameObject.tag = "Active Menu";
        mainMenuCamera.gameObject.SetActive(true);
        mainMenuCamera.gameObject.tag = "MainCamera";
        PinkIsTheNewEvil.PlayerSoundManager.MusicManager(SoundManager.MusicMood.MainMenu);

        SetScalability();
        Invoke("SetScalability", 1f);

        for (int i = 0; i < HighScoreListLength; i++) {
            string highScoreNameRetrieved = PlayerPrefs.GetString("High Score Name " + (i + 1));
            int highScoreValueRetrieved = PlayerPrefs.GetInt("High Score " + (i + 1));
            highScoreName[i].GetComponent<Text>().text = (i + 1) + ": " + highScoreNameRetrieved;
            highScore[i].GetComponent<Text>().text = highScoreValueRetrieved.ToString();
            currentHighScoreList[i] = new HighScorePair(highScoreNameRetrieved, highScoreValueRetrieved);
        }

        if (debugMenusEnabled == true) {
            debugHUDMenuItems.SetActive(true);
            debugMainMenuItems.SetActive(true);
            debugPlayerInvulnerabilityToggle.isOn = debugInvulnerable;
            debugDisableSpawningToggle.isOn = debugDisableSpawning;
            debugRawRenderToggle.isOn = debugRawRender;
        }
    }

    void Update() {
        if (isSwitching == true)
            SwitchToMenu();
    }

    public void OpenPrompt(int mode) {
        PinkIsTheNewEvil.PlayerSoundManager.PlaySound(16);

        switch (mode) {
            case 1:
                resetProgress.gameObject.SetActive(true);
                ResetConfirmDefaultButton.Select();
                break;
            case 2:
                loadingScreen.SetActive(true);
                PlayerPrefs.DeleteAll();
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case 3:
                progressReset.gameObject.SetActive(false);
                break;
            case 4:
                resetProgress.gameObject.SetActive(false);
                SettingsDefaultButton.Select();
                break;
            case 6:
                PinkIsTheNewEvil.EnemySpawner.ToggleLighting();
                break;
            case 7:
                if (Time.timeScale == 0)
                    break;
                PinkIsTheNewEvil.PlayerSoundManager.AllowMusicToPlayWhilePaused(false);
                hud.SetActive(false);
                gamePause.SetActive(true);
                Time.timeScale = 0;
                PauseScreenDefaultButton.Select();
                Cursor.visible = true;
                break;
            case 8:
                if (Time.timeScale != 0)
                    break;
                PinkIsTheNewEvil.PlayerSoundManager.AllowMusicToPlayWhilePaused(true);
                hud.SetActive(true);
                gamePause.SetActive(false);
                Time.timeScale = 1;
                break;
            case 9:
                hud.SetActive(false);
                gamePause.SetActive(false);
                gameOver.SetActive(true);
                Cursor.visible = true;
                break;
            case 10:
                resetScoreConfirm.SetActive(false);
                HighScoreDefaultButton.Select();
                break;
            case 11:
                loadingScreen.SetActive(true);
                for (int i = 0; i < HighScoreListLength; i++) {
                    PlayerPrefs.DeleteKey("High Score Name " + (i + 1));
                    PlayerPrefs.DeleteKey("High Score " + (i + 1));
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case 13:
                tutorialScreens[1 - 1].SetActive(false);
                Time.timeScale = 1;
                break;
            case 14:
                tutorialScreens[2 - 1].SetActive(false);
                Time.timeScale = 1;
                break;
            case 15:
                tutorialScreens[3 - 1].SetActive(false);
                Time.timeScale = 1;
                break;
            case 16:
                tutorialScreens[4 - 1].SetActive(false);
                Time.timeScale = 1;
                break;
            case 17:
                tutorialScreens[5 - 1].SetActive(false);
                Time.timeScale = 1;
                break;
            case 18:
                tutorialScreens[6 - 1].SetActive(false);
                CameraTransition(4);
                break;
            case 19:
                ToggleScalability();
                if (PlayerPrefs.GetInt("LowQuality") == 1)
                    LowScalabilitySelectSwitchToButton.Select();
                else
                    HighScalabilitySelectSwitchToButton.Select();
                break;
            case 20:
                int levelToSwitchTo;
                bool didLevelParseSucceed = int.TryParse(debuglevel.text, out levelToSwitchTo);

                if (levelToSwitchTo > 29)
                    levelToSwitchTo = 29;
                else if (levelToSwitchTo < 1)
                    levelToSwitchTo = 1;

                if (didLevelParseSucceed == true)
                    PlayerPrefs.SetInt("Level", levelToSwitchTo);
                break;
            case 21:
                int weaponToSwitchTo;
                bool didWeaponParseSucceed = int.TryParse(debugweapon.text, out weaponToSwitchTo);

                if (weaponToSwitchTo > 8)
                    weaponToSwitchTo = 8;
                else if (weaponToSwitchTo < 1)
                    weaponToSwitchTo = 1;

                if (didWeaponParseSucceed == true)
                    PlayerPrefs.SetInt("Weapon", weaponToSwitchTo);
                break;
            case 22:
                int pointsToSwitchTo;
                bool didPointsParseSucceed = int.TryParse(debugpoints.text, out pointsToSwitchTo);

                if (pointsToSwitchTo > 1000000)
                    pointsToSwitchTo = 1000000;
                else if (pointsToSwitchTo < 0)
                    pointsToSwitchTo = 0;

                if (didPointsParseSucceed == true)
                    PlayerPrefs.SetInt("Upgrade Points", pointsToSwitchTo);
                break;
            case 23:
                Application.Quit();
                break;
            case 24:
                if (Screen.fullScreen == true)
                    Screen.SetResolution(1024, 768, false);
                else
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                SetScalability();
                break;
            case 25:
                GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in allEnemies) {
                    enemy.GetComponent<EnemyAI>().Health(-10000);
                }
                break;
            case 26:
                debugInvulnerable = debugPlayerInvulnerabilityToggle.isOn;
                break;
            case 27:
                debugDisableSpawning = debugDisableSpawningToggle.isOn;
                break;
            case 28:
                debugRawRender = debugRawRenderToggle.isOn;
                SetScalability();
                break;
        }
    }

    void ToggleScalability() {
        if (PlayerPrefs.GetInt("LowQuality") == 1)
            PlayerPrefs.SetInt("LowQuality", 0);
        else if (PlayerPrefs.GetInt("LowQuality") == 0)
            PlayerPrefs.SetInt("LowQuality", 1);

        SetScalability();
    }

    public void SetScalability() {
        ScalabilitySettings scalabilitySettings;

        if (debugRawRender) {
            scalabilitySettings = rawScalabilitySettings;
            highSettingButton.interactable = false;
            lowSettingButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("LowQuality") == 1) {
            scalabilitySettings = lowScalabilitySettings;
            highSettingButton.interactable = true;
            lowSettingButton.interactable = false;
        }
        else {
            scalabilitySettings = highScalabilitySettings;
            highSettingButton.interactable = false;
            lowSettingButton.interactable = true;
        }

        QualitySettings.vSyncCount = scalabilitySettings.vSyncCount;
        PinkIsTheNewEvil.ReflectionProbe.resolution = scalabilitySettings.reflectionResolution;
        PinkIsTheNewEvil.ReflectionProbe.hdr = scalabilitySettings.reflectionHdr;
        PinkIsTheNewEvil.ReflectionProbe.cullingMask = scalabilitySettings.reflectionMask;
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

        PinkIsTheNewEvil.ReflectionProbe.RenderProbe();
    }

    void SwitchToMenu() {
        float switchTimePercent;

        if (isInGame == true)
            switchTimePercent = (Time.time - switchTime) / switchLength;
        else if (cameraSwitchMode == CameraSwitchMode.ToGame)
            switchTimePercent = (Time.time - switchTime) / switchToGameLength;
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

            if (cameraSwitchMode == CameraSwitchMode.ToGame) {
                PinkIsTheNewEvil.PlayerController.isControlOff = false;
                logo.SetActive(false);
                hud.SetActive(true);
                hud.tag = "Active Menu";
                PinkIsTheNewEvil.CameraLogic.isTracking = true;

                switch (PinkIsTheNewEvil.EnemySpawner.transitionMode) {
                    case 3:
                        PinkIsTheNewEvil.EnemySpawner.level = 3;
                        break;
                    case 10:
                        PinkIsTheNewEvil.EnemySpawner.level = 7;
                        break;
                    case 15:
                        PinkIsTheNewEvil.EnemySpawner.level = 12;
                        break;
                    case 20:
                        PinkIsTheNewEvil.EnemySpawner.level = 16;
                        break;
                    case 27:
                        PinkIsTheNewEvil.EnemySpawner.level = 20;
                        break;
                    case 40:
                        PinkIsTheNewEvil.EnemySpawner.level = 28;
                        break;
                }

                PinkIsTheNewEvil.EnemySpawner.LevelSetup();
                isInGame = true;
                pinkSuit.SetActive(true);
            }

            PinkIsTheNewEvil.EnemySpawner.isTransitionDone = true;
            switchBackMain = false;
            IsAllInputEnabled(true);
        }
        else {
            float smoothedLerp = Mathf.SmoothStep(0f, 1f, switchTimePercent);
            fromMenuCam.transform.position = Vector3.Lerp(fromMenuCamPos, toMenuCam.transform.position, smoothedLerp);
            fromMenuCam.transform.rotation = Quaternion.Lerp(fromMenuCamRot, toMenuCam.transform.rotation, smoothedLerp);
            fromMenuCam.gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(fromMenuCamFOV, toMenuCam.gameObject.GetComponent<Camera>().fieldOfView, smoothedLerp);
            IsAllInputEnabled(false);
        }
    }

    public void IsAllInputEnabled(bool isAllInputEnabled) {
        if (isAllInputEnabled == true) {
            blockAllInput.SetActive(false);
            PinkIsTheNewEvil.PlayerController.isControlOn = true;
        }
        else if (isAllInputEnabled == false) {
            blockAllInput.SetActive(true);
            PinkIsTheNewEvil.PlayerController.isControlOn = false;
        }
    }

    public void CameraTransition(int objective) {
        PinkIsTheNewEvil.PlayerSoundManager.PlaySound(16);
        GameObject objectiveCamera = null;
        GameObject objectiveMenu = null;

        switch (objective) {
            case 1:
                cameraSwitchMode = CameraSwitchMode.ToMenu;
                objectiveCamera = mainMenuCamera;
                objectiveMenu = mainMenu;
                MainMenuDefaultButton.Select();
                break;
            case 2:
                cameraSwitchMode = CameraSwitchMode.ToMenu;
                objectiveCamera = settingsMenuCamera;
                objectiveMenu = settingsMenu;
                SettingsDefaultButton.Select();
                break;
            case 3:
                cameraSwitchMode = CameraSwitchMode.ToMenu;
                objectiveCamera = creditsMenuCamera;
                objectiveMenu = creditsMenu;
                CreditsDefaultButton.Select();
                break;
            case 4:
                if (PinkIsTheNewEvil.EnemySpawner.level == 29 && level29TutStop == true) {
                    cameraSwitchMode = CameraSwitchMode.ToMenu;
                    level29TutStop = false;
                    level29TutStopCamOverride = true;
                    objectiveCamera = mainMenuCamera;
                    objectiveMenu = mainMenu;
                    mainMenu.transform.position = mainMenu.transform.position + new Vector3(0, -9999, 0);
                    PinkIsTheNewEvil.EnemySpawner.ShowTutorial();
                    break;
                }

                if (firstObjective4 == true) {
                    PinkIsTheNewEvil.EnemySpawner.isLightingTransitioning = true;
                    PinkIsTheNewEvil.EnemySpawner.lightTransitionStart = Time.time;
                    PinkIsTheNewEvil.EnemySpawner.TransitionLevelObjective = PinkIsTheNewEvil.EnemySpawner.level;
                    PinkIsTheNewEvil.PlayerController.ResumeSkyfall();
                }

                objectiveCamera = gameCamera;
                cameraSwitchMode = CameraSwitchMode.ToGame;
                firstObjective4 = false;
                break;
            case 5:
                cameraSwitchMode = CameraSwitchMode.ToMenu;
                loadingScreen.SetActive(true);
                SaveHighScores();
                objectiveCamera = mainMenuCamera;
                objectiveMenu = mainMenu;
                break;
            case 6:
                cameraSwitchMode = CameraSwitchMode.ToMenu;
                objectiveCamera = highscorecamera;
                objectiveMenu = highScoremenu;
                HighScoreDefaultButton.Select();
                break;
            case 7:
                cameraSwitchMode = CameraSwitchMode.ToMenu;
                objectiveCamera = highscorecamera;
                objectiveMenu = highScoremenu;
                resetScoreConfirm.SetActive(true);
                HighScoreResetConfirmDefaultButton.Select();
                break;
            case 8:
                cameraSwitchMode = CameraSwitchMode.ToBarrier;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                PinkIsTheNewEvil.CameraLogic.isTracking = false;
                objectiveCamera = blockadeCameras[1 - 1];
                break;
            case 9:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[1 - 1];
                break;
            case 10:
                cameraSwitchMode = CameraSwitchMode.ToBarrier;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                PinkIsTheNewEvil.CameraLogic.isTracking = false;
                objectiveCamera = blockadeCameras[2 - 1];
                break;
            case 11:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[2 - 1];
                break;
            case 12:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[3 - 1];
                break;
            case 13:
                cameraSwitchMode = CameraSwitchMode.ToBarrier;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                PinkIsTheNewEvil.CameraLogic.isTracking = false;
                objectiveCamera = blockadeCameras[3 - 1];
                break;
            case 14:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[4 - 1];
                break;
            case 15:
                cameraSwitchMode = CameraSwitchMode.ToBarrier;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                PinkIsTheNewEvil.CameraLogic.isTracking = false;
                objectiveCamera = blockadeCameras[4 - 1];
                break;
            case 16:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[5 - 1];
                break;
            case 17:
                cameraSwitchMode = CameraSwitchMode.ToBarrier;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                PinkIsTheNewEvil.CameraLogic.isTracking = false;
                objectiveCamera = blockadeCameras[5 - 1];
                break;
            case 18:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[6 - 1];
                break;
            case 19:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[7 - 1];
                break;
            case 20:
                cameraSwitchMode = CameraSwitchMode.ToBarrier;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                PinkIsTheNewEvil.CameraLogic.isTracking = false;
                objectiveCamera = blockadeCameras[6 - 1];
                break;
            case 21:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[8 - 1];
                break;
            case 22:
                cameraSwitchMode = CameraSwitchMode.ToBarrier;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = blockadeCameras[7 - 1];
                break;
            case 23:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[9 - 1];
                break;
            case 24:
                cameraSwitchMode = CameraSwitchMode.ToBarrier;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = blockadeCameras[8 - 1];
                break;
            case 25:
                cameraSwitchMode = CameraSwitchMode.ToWeapon;
                PinkIsTheNewEvil.PlayerController.isControlOff = true;
                objectiveCamera = weaponCameras[10 - 1];
                break;
        }

        fromMenuCam = GameObject.FindWithTag("MainCamera");
        toMenuCam = objectiveCamera;

        if (cameraSwitchMode == CameraSwitchMode.ToGame) {
            if (level29TutStopCamOverride == true)
                level29TutStopCamOverride = false;
            else
                toMenuCam = gameCamera;
        }

        fromMenuCamPos = fromMenuCam.transform.position;
        fromMenuCamRot = fromMenuCam.transform.rotation;
        fromMenuCamFOV = fromMenuCam.gameObject.GetComponent<Camera>().fieldOfView;
        switchTime = Time.time;
        GameObject activeMenu = GameObject.FindWithTag("Active Menu");

        if (cameraSwitchMode != CameraSwitchMode.ToWeapon && isInGame == false) {
            activeMenu.gameObject.SetActive(false);
            activeMenu.gameObject.tag = "Untagged";
        }

        if (cameraSwitchMode == CameraSwitchMode.ToMenu) {
            objectiveMenu.gameObject.SetActive(true);
            objectiveMenu.gameObject.tag = "Active Menu";
        }

        isSwitching = true;
    }

    void SaveHighScores() {
        Time.timeScale = 1;

        if (saveHighScore == true) {
            currentHighScoreList[HighScoreListLength] = new HighScorePair(highScoreNewName.GetComponent<Text>().text, PinkIsTheNewEvil.PlayerController.upgradePoints);
            currentHighScoreList = currentHighScoreList.OrderByDescending(highScorePair => highScorePair.highScoreValue).ToArray();
            for (int i = 0; i < HighScoreListLength; i++) {
                PlayerPrefs.SetString("High Score Name " + (i + 1), currentHighScoreList[i].highScoreName);
                PlayerPrefs.SetInt("High Score " + (i + 1), currentHighScoreList[i].highScoreValue);
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}