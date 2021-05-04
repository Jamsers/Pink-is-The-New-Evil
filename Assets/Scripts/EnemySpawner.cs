using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
    [Header("Data")]
    public LevelData[] levelData;
    public LevelText[] levelTexts;

    [Header("References")]
    public GameObject[] tutorialScreens;
    public GameObject[] enemies;
    public GameObject[] blockades;
    public GameObject[] weapons;
    public GameObject[] weaponModels;
    public Transform[] spawnPoints;
    public GameObject spawnDust;

    [Header("UI")]
    public GameObject levelBannerBackground;
    public GameObject levelTitle;
    public GameObject poiBannerBackground;
    public GameObject poiMessage;
    public GameObject levelSubtitle;
    public RectTransform hudCanvas;
    public GameObject resetHighScoresButton;
    public Text newGameButtonText;

    [Header("Lighting")]
    public GameObject noonLight;
    public GameObject afternoonLight;
    public GameObject currentLight;
    public GameObject nightmareLight;
    public GameObject nightmareUnderlight;
    public Material nightmareSkybox;
    public Material daySkybox;

    [HideInInspector] public int level = 1;
    [HideInInspector] public List<GameObject> listOfAllEnemies = new List<GameObject>();
    [HideInInspector] public int transitionMode = 0;
    [HideInInspector] public int TransitionLevelObjective;
    [HideInInspector] public bool isTransitionDone = false;
    [HideInInspector] public bool isCheckingForTransition = false;
    [HideInInspector] public bool constantlyDenyInput = false;
    [HideInInspector] public bool isLightingTransitioning = false;
    [HideInInspector] public float lightTransitionStart = 0;

    bool inputRestored = true;
    const float TransitionTimeLength = .5f;
    const float TransitionLingerTime = 6;
    const int InfiniteEnemyCap = 39;
    const float InfiniteSpawnerTick = 0.25f;

    const float LightTransitionLength = 5f;
    bool isLightTransitionInitialized = false;
    LightTransitionType lightTransitionToType;
    Color currentLightBeginningColor;
    Color currentLightEndColor;
    Quaternion currentLightBeginningRotation;
    Quaternion currentLightEndRotation;
    Color spookyUnderlightBeginningColor;
    Color spookyUnderlightEndColor;

    const float BorderSpace = 500f;
    bool isTitleTransitioning = false;
    bool isTitleAnimating = false;
    bool isTitleTransitionInitialized = false;
    float transitionBeginningTime;
    float levelBannerOriginalAlpha;

    Vector3 levelBannerOrigPosition;
    Vector3 levelTitleOrigPosition;
    Vector3 levelSubtitleOrigPosition;
    Vector3 levelBannerFadeInPosition;
    Vector3 levelTitleFadeInPosition;
    Vector3 levelSubtitleFadeInPosition;
    Vector3 levelBannerFadeOutPosition;
    Vector3 levelTitleFadeOutPosition;
    Vector3 levelSubtitleFadeOutPosition;

    const float TimeToFadeWeapon = 2f;
    int weaponToFade;
    bool weaponIsFading = false;
    float fadingWeaponStartTime;
    Vector3 objectOriginalScale;
    GameObject objectToScale;

    enum TransitionDoneType {
        NewWeapon,
        Blockade,
        Filler
    }

    enum LightTransitionType {
        ToNightmare,
        ToNoon,
        ToLevel
    }

    [System.Serializable]
    public struct SpawnData {
        public int enemyType;
        public int spawnPoint;

        public SpawnData(int enemyType, int spawnPoint) {
            this.enemyType = enemyType;
            this.spawnPoint = spawnPoint;
        }
    }

    [System.Serializable]
    public struct LevelData {
        public SpawnData[] spawnDataArray;

        public LevelData(SpawnData[] spawnDataArray) {
            this.spawnDataArray = spawnDataArray;
        }
    }

    [System.Serializable]
    public struct LevelText {
        public string title;
        public string[] subtitles;
        public Font subtitleFont;
        public Color subtitleColor;

        public LevelText(string title, string[] subtitles, Font subtitleFont, Color subtitleColor) {
            this.title = title;
            this.subtitles = subtitles;
            this.subtitleFont = subtitleFont;
            this.subtitleColor = subtitleColor;
        }
    }

    void Start() {
        level = PlayerPrefs.GetInt("Level", 1);

        if (level == 1) {
            newGameButtonText.text = "New Game";
        }
        else if (level == 29) {
            newGameButtonText.text = "Survival";
        }
        else {
            newGameButtonText.text = "Resume";
        }

        if (level != 29) {
            resetHighScoresButton.GetComponent<Button>().interactable = false;
        }

        SetMainMenuLighting();

        if (level == 29) {
            PinkIsTheNewEvil.MainSystems.toggleLightingButton.SetActive(true);
            weapons[10 - 1].SetActive(true);
        }
    }

    void Update() {
        if (isCheckingForTransition == true) {
            CheckForTransitionDoneReport();
        }

        LightingTransition();
        FadeInWeaponModel();
        LevelTitleTransition();

        if (constantlyDenyInput == true) {
            PinkIsTheNewEvil.MainSystems.isAllInputEnabled(false);
            inputRestored = false;
        }
        else if (constantlyDenyInput == false && inputRestored == false) {
            PinkIsTheNewEvil.MainSystems.isAllInputEnabled(true);
            inputRestored = true;
        }
    }

    public void ToggleLighting() {
        int isSurvivalLightingFlipped = PlayerPrefs.GetInt("Is Survival Lighting Flipped");

        if (isSurvivalLightingFlipped == 1) {
            PlayerPrefs.SetInt("Is Survival Lighting Flipped", 0); ;
        }
        else if (isSurvivalLightingFlipped == 0) {
            PlayerPrefs.SetInt("Is Survival Lighting Flipped", 1); ;
        }

        SetMainMenuLighting();
    }

    void SetMainMenuLighting() {
        int isSurvivalLightingFlipped = PlayerPrefs.GetInt("Is Survival Lighting Flipped");

        if (level == 29 && isSurvivalLightingFlipped == 0) {
            currentLight.GetComponent<Light>().color = nightmareLight.GetComponent<Light>().color;
            currentLight.GetComponent<Transform>().rotation = nightmareLight.GetComponent<Transform>().rotation;
            RenderSettings.skybox = nightmareSkybox;
            nightmareUnderlight.SetActive(true);
        }
        else {
            currentLight.GetComponent<Light>().color = noonLight.GetComponent<Light>().color;
            currentLight.GetComponent<Transform>().rotation = noonLight.GetComponent<Transform>().rotation;
            RenderSettings.skybox = daySkybox;
            nightmareUnderlight.SetActive(false);
        }

        RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
        PinkIsTheNewEvil.ReflectionProbe.RenderProbe();
    }

    void InfiniteSpawner() {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > InfiniteEnemyCap) {
            Invoke("InfiniteSpawner", InfiniteSpawnerTick);
            return;
        }

        int enemyType = Random.Range(1, enemies.Length);
        int spawnPoint = Random.Range(1, spawnPoints.Length);
        StartCoroutine(SpawnEnemy(enemyType, spawnPoint, 0));
        Invoke("InfiniteSpawner", InfiniteSpawnerTick);
    }

    void LightingTransition() {
        if (isLightingTransitioning == false)
            return;

        if (isLightTransitionInitialized == false) {
            InitializeLightTransition();
            return;
        }

        float lightTransitionPercentage = (Time.time - lightTransitionStart) / LightTransitionLength;

        if (lightTransitionPercentage > 1) {
            if (lightTransitionToType == LightTransitionType.ToNightmare) {
                nightmareUnderlight.SetActive(true);
                RenderSettings.skybox = nightmareSkybox;
            }
            else {
                nightmareUnderlight.SetActive(false);
                RenderSettings.skybox = daySkybox;
            }

            PinkIsTheNewEvil.ReflectionProbe.RenderProbe();

            isLightTransitionInitialized = false;
            isLightingTransitioning = false;
        }
        else {
            float smoothedPercentage = Mathf.SmoothStep(0f, 1f, lightTransitionPercentage);

            currentLight.GetComponent<Light>().color = Color.Lerp(currentLightBeginningColor, currentLightEndColor, smoothedPercentage);
            currentLight.GetComponent<Transform>().rotation = Quaternion.Lerp(currentLightBeginningRotation, currentLightEndRotation, smoothedPercentage);
            if (lightTransitionToType == LightTransitionType.ToNightmare) {
                nightmareUnderlight.GetComponent<Light>().color = Color.Lerp(spookyUnderlightBeginningColor, spookyUnderlightEndColor, smoothedPercentage);
            }

            RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
        }
    }

    void InitializeLightTransition() {
        currentLightBeginningColor = currentLight.GetComponent<Light>().color;
        currentLightBeginningRotation = currentLight.GetComponent<Transform>().rotation;

        if (TransitionLevelObjective == 29) {
            if (PlayerPrefs.GetInt("Is Survival Lighting Flipped") == 1) {
                lightTransitionToType = LightTransitionType.ToNightmare;
            }
            else {
                lightTransitionToType = LightTransitionType.ToNoon;
            }
        }
        else if (TransitionLevelObjective == 28) {
            lightTransitionToType = LightTransitionType.ToNightmare;
        }
        else {
            lightTransitionToType = LightTransitionType.ToLevel;
        }

        InitializeLightEndVariables(lightTransitionToType);
        isLightTransitionInitialized = true;
    }

    void InitializeLightEndVariables(LightTransitionType transitionType) {
        if (transitionType == LightTransitionType.ToLevel) {
            currentLightEndColor = Color.Lerp(noonLight.GetComponent<Light>().color, afternoonLight.GetComponent<Light>().color, TransitionLevelObjective / 27f);
            currentLightEndRotation = Quaternion.Lerp(noonLight.GetComponent<Transform>().rotation, afternoonLight.GetComponent<Transform>().rotation, TransitionLevelObjective / 27f);
        }
        else if (transitionType == LightTransitionType.ToNightmare) {
            currentLightEndColor = nightmareLight.GetComponent<Light>().color;
            currentLightEndRotation = nightmareLight.GetComponent<Transform>().rotation;

            spookyUnderlightBeginningColor = Color.black;
            spookyUnderlightEndColor = nightmareUnderlight.GetComponent<Light>().color;

            nightmareUnderlight.GetComponent<Light>().color = spookyUnderlightBeginningColor;
            nightmareUnderlight.SetActive(true);
        }
        else if (transitionType == LightTransitionType.ToNoon) {
            currentLightEndColor = noonLight.GetComponent<Light>().color;
            currentLightEndRotation = noonLight.GetComponent<Transform>().rotation;

            spookyUnderlightBeginningColor = nightmareUnderlight.GetComponent<Light>().color;
            spookyUnderlightEndColor = Color.black;
        }
    }

    public void LevelSetup() {
        PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().PlaySound(15);

        for (int i = 0; i < weapons.Length; i++) {
            if (i < PinkIsTheNewEvil.PlayerController.attackMode - 1) {
                weapons[i].SetActive(false);
            }
            else {
                weapons[i].SetActive(true);
            }
        }

        if (level < 28) {
            weapons[8 - 1].SetActive(false);
            weapons[9 - 1].SetActive(false);
            weapons[10 - 1].SetActive(false);
        }
        if (level < 20) {
            weapons[7 - 1].SetActive(false);
            weapons[6 - 1].SetActive(false);
        }
        if (level < 16) {
            weapons[5 - 1].SetActive(false);
        }
        if (level < 12) {
            weapons[4 - 1].SetActive(false);
        }
        if (level < 7) {
            weapons[3 - 1].SetActive(false);
            weapons[2 - 1].SetActive(false);
        }
        if (level < 3) {
            weapons[1 - 1].SetActive(false);
        }

        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Upgrade Points", PinkIsTheNewEvil.PlayerController.upgradePoints);
        PlayerPrefs.SetInt("Weapon", PinkIsTheNewEvil.PlayerController.attackMode);
        PlayerPrefs.Save();

        isTitleTransitioning = true;
        transitionBeginningTime = Time.time;

        if (level < 7) {
            PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.BridgeSection);
        }
        else if (level < 20) {
            PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.WorldOpenUp);
        }
        else if (level < 28) {
            PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.NorthernPart);
        }
        else if (level == 28) {
            PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.Nightmare);
        }
        else if (level == 29) {
            if (PlayerPrefs.GetInt("Is Survival Lighting Flipped") == 1) {
                PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.Nightmare);
            }
            else {
                PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.WorldOpenUp);
            }

        }

        if (level == 1 || level == 3 || level == 6 || level == 15 || level == 28) {
            Invoke("ShowTutorial", 4);
        }


        if (level >= 3) {
            blockades[1 - 1].SetActive(false);
        }
        if (level >= 7) {
            blockades[2 - 1].SetActive(false);
        }
        if (level >= 12) {
            blockades[3 - 1].SetActive(false);
        }
        if (level >= 16) {
            blockades[4 - 1].SetActive(false);
        }
        if (level >= 20) {
            blockades[5 - 1].SetActive(false);
        }
        if (level >= 28) {
            blockades[6 - 1].SetActive(false);
            blockades[7 - 1].SetActive(false);
            blockades[8 - 1].SetActive(false);
        }

        Invoke("RemoveLevelPrompts", 4);
        Invoke("SpawnEnemies", 6);
    }

    void LevelTitleTransition() {
        if (isTitleTransitioning == true) {
            if (isTitleTransitionInitialized == false) {
                InitializeTitleTransition();
            }

            float currentTransitionToLingerPercentage = (Time.time - transitionBeginningTime) / TransitionTimeLength;
            float currentLingerTime = Time.time - (transitionBeginningTime + TransitionTimeLength);
            float currentTransitionToFadeOutPercentage = (Time.time - (transitionBeginningTime + TransitionTimeLength + TransitionLingerTime)) / TransitionTimeLength;

            if (currentTransitionToLingerPercentage > 1) {
                isTitleAnimating = true;
                levelBannerBackground.transform.position = levelBannerOrigPosition;
                levelTitle.transform.position = levelTitleOrigPosition;
                levelSubtitle.transform.position = levelSubtitleOrigPosition;
            }

            if (currentTransitionToFadeOutPercentage > 1) {
                levelBannerBackground.SetActive(false);
                levelTitle.SetActive(false);
                levelSubtitle.SetActive(false);

                Color backgroundColor = levelBannerBackground.GetComponent<Image>().color;

                levelBannerBackground.transform.position = levelBannerOrigPosition;
                levelTitle.transform.position = levelTitleOrigPosition;
                levelSubtitle.transform.position = levelSubtitleOrigPosition;
                backgroundColor.a = levelBannerOriginalAlpha;

                levelBannerBackground.GetComponent<Image>().color = backgroundColor;

                isTitleTransitioning = false;
                isTitleAnimating = false;
                isTitleTransitionInitialized = false;
            }
            else {
                if (isTitleAnimating == false) {
                    FadeTitle(true, currentTransitionToLingerPercentage);
                }
                else if (isTitleAnimating == true && (currentLingerTime > TransitionLingerTime)) {
                    FadeTitle(false, currentTransitionToFadeOutPercentage);
                }
            }
        }
    }

    void FadeTitle(bool isFadeIn, float percentage) {
        Color backgroundColor = levelBannerBackground.GetComponent<Image>().color;
        Color titleColor = levelTitle.GetComponent<Text>().color;
        Color subtitleColor = levelSubtitle.GetComponent<Text>().color;
        float smoothedPercentage = Mathf.SmoothStep(0, 1, percentage);

        if (isFadeIn) {
            levelBannerBackground.transform.position = Vector3.Lerp(levelBannerFadeInPosition, levelBannerOrigPosition, smoothedPercentage);
            levelTitle.transform.position = Vector3.Lerp(levelTitleFadeInPosition, levelTitleOrigPosition, smoothedPercentage);
            levelSubtitle.transform.position = Vector3.Lerp(levelSubtitleFadeInPosition, levelSubtitleOrigPosition, smoothedPercentage);
            backgroundColor.a = Mathf.Lerp(0, levelBannerOriginalAlpha, smoothedPercentage);
            titleColor.a = Mathf.Lerp(0, 1, smoothedPercentage);
            subtitleColor.a = Mathf.Lerp(0, 1, smoothedPercentage);
        }
        else {
            levelBannerBackground.transform.position = Vector3.Lerp(levelBannerOrigPosition, levelBannerFadeOutPosition, smoothedPercentage);
            levelTitle.transform.position = Vector3.Lerp(levelTitleOrigPosition, levelTitleFadeOutPosition, smoothedPercentage);
            levelSubtitle.transform.position = Vector3.Lerp(levelSubtitleOrigPosition, levelSubtitleFadeOutPosition, smoothedPercentage);
            backgroundColor.a = Mathf.Lerp(levelBannerOriginalAlpha, 0, smoothedPercentage);
            titleColor.a = Mathf.Lerp(1, 0, smoothedPercentage);
            subtitleColor.a = Mathf.Lerp(1, 0, smoothedPercentage);
        }

        levelBannerBackground.GetComponent<Image>().color = backgroundColor;
        levelTitle.GetComponent<Text>().color = titleColor;
        levelSubtitle.GetComponent<Text>().color = subtitleColor;
    }

    void InitializeTitleTransition() {
        levelBannerFadeInPosition = new Vector3((levelBannerBackground.transform.position.x + (hudCanvas.sizeDelta.x / 2) + BorderSpace), levelBannerBackground.transform.position.y, 0);
        levelTitleFadeInPosition = new Vector3((levelTitle.transform.position.x + (hudCanvas.sizeDelta.x / 2) + BorderSpace), levelTitle.transform.position.y, 0);
        levelSubtitleFadeInPosition = new Vector3((levelSubtitle.transform.position.x + (hudCanvas.sizeDelta.x / 2) + BorderSpace), levelSubtitle.transform.position.y, 0);

        levelBannerOrigPosition = levelBannerBackground.transform.position;
        levelTitleOrigPosition = levelTitle.transform.position;
        levelSubtitleOrigPosition = levelSubtitle.transform.position;

        levelBannerFadeOutPosition = new Vector3((levelBannerBackground.transform.position.x - (hudCanvas.sizeDelta.x / 2) - BorderSpace), levelBannerBackground.transform.position.y, 0);
        levelTitleFadeOutPosition = new Vector3((levelTitle.transform.position.x - (hudCanvas.sizeDelta.x / 2) - BorderSpace), levelTitle.transform.position.y, 0);
        levelSubtitleFadeOutPosition = new Vector3((levelSubtitle.transform.position.x - (hudCanvas.sizeDelta.x / 2) - BorderSpace), levelSubtitle.transform.position.y, 0);

        levelBannerOriginalAlpha = levelBannerBackground.GetComponent<Image>().color.a;

        int minInt = 0;
        int subtitlesAmount = levelTexts[level - 1].subtitles.Length - 1;
        int subtitleToUse = Random.Range(minInt, subtitlesAmount);

        levelTitle.GetComponent<Text>().text = levelTexts[level - 1].title;
        levelSubtitle.GetComponent<Text>().text = levelTexts[level - 1].subtitles[subtitleToUse];
        levelSubtitle.GetComponent<Text>().font = levelTexts[level - 1].subtitleFont;
        levelSubtitle.GetComponent<Text>().color = levelTexts[level - 1].subtitleColor;

        levelBannerBackground.SetActive(true);
        levelTitle.SetActive(true);
        levelSubtitle.SetActive(true);

        isTitleTransitionInitialized = true;
    }

    public void ShowTutorial() {
        switch (level) {
            case 1:
                tutorialScreens[1 - 1].SetActive(true);
                break;
            case 3:
                tutorialScreens[2 - 1].SetActive(true);
                break;
            case 6:
                tutorialScreens[3 - 1].SetActive(true);
                break;
            case 15:
                tutorialScreens[4 - 1].SetActive(true);
                break;
            case 28:
                tutorialScreens[5 - 1].SetActive(true);
                break;
            case 29:
                tutorialScreens[6 - 1].SetActive(true);
                break;
        }

        if (level != 29) {
            Time.timeScale = 0;
        }

        Cursor.visible = true;
    }

    void RemoveLevelPrompts() {
        poiBannerBackground.SetActive(false);
        poiMessage.SetActive(false);
    }

    void SpawnEnemies() {
        if (PinkIsTheNewEvil.MainSystems.debugDisableSpawning == true)
            return;

        PinkIsTheNewEvil.PlayerController.isControlOff = false;
        if (level < 28) {
            float delay = 0f;
            foreach (SpawnData spawnData in levelData[level - 1].spawnDataArray) {
                StartCoroutine(SpawnEnemy(spawnData.enemyType, spawnData.spawnPoint, delay));
                if (level < 22) {
                    delay += 1;
                }
                else {
                    delay += 0.5f;
                }
            }
            InvokeRepeating("CheckIfAllEnemiesAreDead", delay + 0.25f, 0.25f);
        }
        else if (level == 28) {
            InfiniteSpawner();
        }
        else {
            InfiniteSpawner();
        }
    }

    void CheckIfAllEnemiesAreDead() {
        GameObject areTheAlive = GameObject.FindWithTag("Enemy");
        if (areTheAlive == null) {
            isLightingTransitioning = true;
            lightTransitionStart = Time.time;
            TransitionLevelObjective = level + 1;

            switch (level) {
                case 2:
                    AllEnemiesDeadTransition(0, 8);
                    break;
                case 6:
                    AllEnemiesDeadTransition(4, 10);
                    break;
                case 11:
                    AllEnemiesDeadTransition(11, 13);
                    break;
                case 15:
                    AllEnemiesDeadTransition(16, 15);
                    break;
                case 19:
                    AllEnemiesDeadTransition(21, 17);
                    break;
                case 27:
                    AllEnemiesDeadTransition(28, 20);
                    break;
                default:
                    level++;
                    LevelSetup();
                    break;
            }

            CancelInvoke("CheckIfAllEnemiesAreDead");
        }
    }

    void AllEnemiesDeadTransition (int transitionTo, int settingsType) {
        PinkIsTheNewEvil.MainSystems.GoToSettings(settingsType);
        if (level != 2) {
            transitionMode = transitionTo;
        }
        isCheckingForTransition = true;
        isTransitionDone = false;
        constantlyDenyInput = true;
    }

    void FadeInWeaponModel() {
        if (weaponIsFading == true) {
            if (weaponToFade > 7) {
                weapons[weaponToFade - 1].SetActive(true);
            }
            else {
                weapons[weaponToFade - 1].SetActive(true);
                objectOriginalScale = weaponModels[weaponToFade - 1].transform.localScale;
                objectToScale = weaponModels[weaponToFade - 1];
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                PinkIsTheNewEvil.PlayerController.GetComponent<SoundManager>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }

            bool fadeErUp = true;

            if (weaponToFade == 8 || weaponToFade == 9 || weaponToFade == 10) {
                fadeErUp = false;
            }

            weaponToFade = 0;

            float lerpTime = ((Time.time - fadingWeaponStartTime) / TimeToFadeWeapon);

            if (fadeErUp == false) {
                weaponIsFading = false;
            }
            else {
                if (lerpTime > 1) {
                    weaponIsFading = false;
                }
                else {
                    objectToScale.transform.localScale = objectOriginalScale * Mathf.SmoothStep(0, 1, lerpTime);
                }
            }
        }

    }

    void CheckForTransitionDoneReport() {
        if (isTransitionDone == true) {

            switch (transitionMode) {
                case 1:
                    TransitionAction(TransitionDoneType.NewWeapon, 1);
                    break;
                case 3:
                    TransitionAction(TransitionDoneType.Filler, 0);
                    break;
                case 4:
                    TransitionAction(TransitionDoneType.Blockade, 2);
                    break;
                case 6:
                    TransitionAction(TransitionDoneType.NewWeapon, 2);
                    break;
                case 8:
                    TransitionAction(TransitionDoneType.NewWeapon, 3);
                    break;
                case 10:
                    TransitionAction(TransitionDoneType.Filler, 0);
                    break;
                case 11:
                    TransitionAction(TransitionDoneType.Blockade, 3);
                    break;
                case 13:
                    TransitionAction(TransitionDoneType.NewWeapon, 4);
                    break;
                case 15:
                    TransitionAction(TransitionDoneType.Filler, 0);
                    break;
                case 16:
                    TransitionAction(TransitionDoneType.Blockade, 4);
                    break;
                case 18:
                    TransitionAction(TransitionDoneType.NewWeapon, 5);
                    break;
                case 20:
                    TransitionAction(TransitionDoneType.Filler, 0);
                    break;
                case 21:
                    TransitionAction(TransitionDoneType.Blockade, 5);
                    break;
                case 23:
                    TransitionAction(TransitionDoneType.NewWeapon, 6);
                    break;
                case 25:
                    TransitionAction(TransitionDoneType.NewWeapon, 7);
                    break;
                case 27:
                    TransitionAction(TransitionDoneType.Filler, 0);
                    break;
                case 28:
                    TransitionAction(TransitionDoneType.Blockade, 6);
                    break;
                case 30:
                    TransitionAction(TransitionDoneType.NewWeapon, 8);
                    break;
                case 32:
                    TransitionAction(TransitionDoneType.Blockade, 7);
                    break;
                case 34:
                    TransitionAction(TransitionDoneType.NewWeapon, 9);
                    break;
                case 36:
                    TransitionAction(TransitionDoneType.Blockade, 8);
                    break;
                case 38:
                    TransitionAction(TransitionDoneType.NewWeapon, 10);
                    break;
                case 40:
                    TransitionAction(TransitionDoneType.Filler, 0);
                    break;
                default:
                    poiBannerBackground.SetActive(true);
                    poiMessage.SetActive(true);
                    blockades[1 - 1].GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                    Invoke("EnableIsCheckingForTransition", 2.5f);
                    break;
            }

            isCheckingForTransition = false;
            isTransitionDone = false;
        }
        else {
            PinkIsTheNewEvil.PlayerController.isControlOff = true;
        }
    }

    void TransitionAction(TransitionDoneType transitionDoneType, int objectToFade) {
        if (transitionDoneType == TransitionDoneType.Filler) {
            isCheckingForTransition = false;
            isTransitionDone = false;
            constantlyDenyInput = false;
        }
        else {
            poiBannerBackground.SetActive(true);
            poiMessage.SetActive(true);
            string transitionText = "";

            if (transitionDoneType == TransitionDoneType.NewWeapon) {
                if (objectToFade > 7) {
                    transitionText = "The Flames Of Ascencion burn!";
                }
                else {
                    transitionText = "New weapon available!";
                }

                weaponIsFading = true;
                weaponToFade = objectToFade;
                fadingWeaponStartTime = Time.time;
            }
            else if (transitionDoneType == TransitionDoneType.Blockade) {
                transitionText = "The blockade has been lifted!";
                blockades[objectToFade - 1].GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
            }

            poiMessage.GetComponent<Text>().text = transitionText;
            transitionMode += 1;
            Invoke("EnableIsCheckingForTransition", 2.5f);
        }
    }

    void EnableIsCheckingForTransition() {
        RemoveLevelPrompts();

        switch (transitionMode) {
            case 2:
                PinkIsTheNewEvil.MainSystems.GoToSettings(4);
                break;
            case 0:
                PinkIsTheNewEvil.MainSystems.GoToSettings(9);
                break;
            case 5:
                PinkIsTheNewEvil.MainSystems.GoToSettings(11);
                break;
            case 7:
                PinkIsTheNewEvil.MainSystems.GoToSettings(12);
                break;
            case 9:
                PinkIsTheNewEvil.MainSystems.GoToSettings(4);
                break;
            case 12:
                PinkIsTheNewEvil.MainSystems.GoToSettings(14);
                break;
            case 14:
                PinkIsTheNewEvil.MainSystems.GoToSettings(4);
                break;
            case 17:
                PinkIsTheNewEvil.MainSystems.GoToSettings(16);
                break;
            case 19:
                PinkIsTheNewEvil.MainSystems.GoToSettings(4);
                break;
            case 22:
                PinkIsTheNewEvil.MainSystems.GoToSettings(18);
                break;
            case 24:
                PinkIsTheNewEvil.MainSystems.GoToSettings(19);
                break;
            case 26:
                PinkIsTheNewEvil.MainSystems.GoToSettings(4);
                break;
            case 29:
                PinkIsTheNewEvil.MainSystems.GoToSettings(21);
                break;
            case 31:
                PinkIsTheNewEvil.MainSystems.GoToSettings(22);
                break;
            case 33:
                PinkIsTheNewEvil.MainSystems.GoToSettings(23);
                break;
            case 35:
                PinkIsTheNewEvil.MainSystems.GoToSettings(24);
                break;
            case 37:
                PinkIsTheNewEvil.MainSystems.GoToSettings(25);
                break;
            case 39:
                PinkIsTheNewEvil.MainSystems.GoToSettings(4);
                break;
        }

        transitionMode += 1;
        isCheckingForTransition = true;
        isTransitionDone = false;
    }

    IEnumerator SpawnEnemy(int enemyType, int spawnPoint, float spawnDelay) {
        yield return new WaitForSeconds(spawnDelay);
        GameObject spawnedEnemy = Instantiate(enemies[enemyType - 1], spawnPoints[spawnPoint - 1].position, spawnPoints[spawnPoint - 1].rotation);
        listOfAllEnemies.Add(spawnedEnemy);
    }
}