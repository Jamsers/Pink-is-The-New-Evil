using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public bool DebugDisableSpawning;

    
    public int level = 1;
    public PlayerController playerController;
    public bool isTransitionDone = false;
    public bool isCheckingForTransition = false;
    public bool constantlyDenyInput = false;
    public bool isLightingTransitioning = false;
    public float TrannyStart = 0;
    public int TransitionLevelObjective;
    public int transitionMode = 0;

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
    public GameObject blackBackground;
    public GameObject levelTitle;
    public GameObject blackBackground2;
    public GameObject blockadeText;
    public GameObject levelSubtitle;
    public GameObject hudCanvas;
    public GameObject resetHighScoresButton;
    public GameObject newGameButtonText;

    [Header("Lighting")]
    public GameObject noonLight;
    public GameObject afternoonLight;
    public GameObject currentLight;
    public GameObject nightmareLight;
    public GameObject nightmareUnderlight;
    public Material altsky;
    public Material normsky;

    [Header("List of All Enemies")]
    public List<GameObject> listOfAllEnemies = new List<GameObject>();

    bool inputRestored = true;

    float transitionTimeLight = 5;

    bool isLightTransitionInitialized = false;

    Color begColor;
    Color endColor;
    Quaternion begRotation;
    Quaternion endRotation;

    Color nightBegColor;
    Color nightEndColor;

    

    bool isTitleTransitioning = false;
    bool isTitleTransitionInitialized = false;
    float beginAyyTime;

    

    Vector3 backSourcePos;
    Vector3 textSourcePos;
    Vector3 text2SourcePos;

    Vector3 backOrigPos;
    Vector3 textOrigPos;
    Vector3 text2OrigPos;

    Vector3 backDestPos;
    Vector3 textDestPos;
    Vector3 text2DestPos;

    float initAlphaVal;

    

    bool isTitleAnimating = false;

    float ayyTransitionTime = .5f;
    float ayyStayTime = 6;

    float borderSpace = 500;

    

    int weaponToFade;
    bool weaponIsFading = false;
    float timeToFadeWeapon = 2f;
    float fadingWeaponStartTime;
    Vector3 originalScale;
    GameObject objectToScale;

    

    enum TransitionDoneType {
        NewWeapon,
        Blockade,
        Filler
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

        public LevelText(string title, string[] subtitle, Font subtitleFont, Color subtitleColor) {
            this.title = title;
            this.subtitles = subtitle;
            this.subtitleFont = subtitleFont;
            this.subtitleColor = subtitleColor;
        }
    }

    void Start() {
        level = PlayerPrefs.GetInt("Level", 1);

        if (level == 1) {
            newGameButtonText.GetComponent<Text>().text = "New Game";
        }
        else if (level == 29) {
            newGameButtonText.GetComponent<Text>().text = "Survival";
        }
        else {
            newGameButtonText.GetComponent<Text>().text = "Resume";
        }

        if (level != 29) {
            resetHighScoresButton.GetComponent<Button>().interactable = false;
        }

        RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
        if (level == 29) {
            GetComponent<MainSystems>().toggleLightingButton.SetActive(true);
            if (PlayerPrefs.GetInt("Is Shadows On") == 1) {
                currentLight.GetComponent<Light>().color = noonLight.GetComponent<Light>().color;
                RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
                currentLight.GetComponent<Transform>().rotation = noonLight.GetComponent<Transform>().rotation;
            }
            else {
                currentLight.GetComponent<Light>().color = nightmareLight.GetComponent<Light>().color;
                RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
                currentLight.GetComponent<Transform>().rotation = nightmareLight.GetComponent<Transform>().rotation;
                nightmareUnderlight.SetActive(true);
                RenderSettings.skybox = altsky;
            }
            weapons[10 - 1].SetActive(true);
        }
    }

    public void ToggleLighting() {
        if (PlayerPrefs.GetInt("Is Shadows On") == 1) {

            PlayerPrefs.SetInt("Is Shadows On", 0);
            currentLight.GetComponent<Light>().color = nightmareLight.GetComponent<Light>().color;
            RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
            currentLight.GetComponent<Transform>().rotation = nightmareLight.GetComponent<Transform>().rotation;
            nightmareUnderlight.SetActive(true);
            RenderSettings.skybox = altsky;
            GameObject.Find("Reflection Probe").GetComponent<ReflectionProbe>().RenderProbe();
        }
        else {
            PlayerPrefs.SetInt("Is Shadows On", 1);
            currentLight.GetComponent<Light>().color = noonLight.GetComponent<Light>().color;
            RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
            currentLight.GetComponent<Transform>().rotation = noonLight.GetComponent<Transform>().rotation;
            nightmareUnderlight.SetActive(false);
            RenderSettings.skybox = normsky;
            GameObject.Find("Reflection Probe").GetComponent<ReflectionProbe>().RenderProbe();
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
            GameObject.Find("Main Systems").GetComponent<MainSystems>().isAllInputEnabled(false);
            inputRestored = false;
        }
        else if (constantlyDenyInput == false && inputRestored == false) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().isAllInputEnabled(true);
            inputRestored = true;
        }
    }

    void InfiniteSpawner() {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 39) {
            Invoke("InfiniteSpawner", .25f);
            return;
        }
        int low = 1;
        int high = 8;
        int spawnType = Random.Range(low, high);

        int slow = 1;
        int shigh = 46;
        int sspawnType = Random.Range(slow, shigh);

        StartCoroutine(SpawnEnemy(spawnType, sspawnType, 0));
        Invoke("InfiniteSpawner", .25f);
    }

    void LightingTransition() {
        if (isLightingTransitioning == true) {
            if (isLightTransitionInitialized == false) {
                begColor = currentLight.GetComponent<Light>().color;
                begRotation = currentLight.GetComponent<Transform>().rotation;

                if (TransitionLevelObjective == 28 || (TransitionLevelObjective == 29 && PlayerPrefs.GetInt("Is Shadows On") == 1)) {
                    endColor = nightmareLight.GetComponent<Light>().color;
                    endRotation = nightmareLight.GetComponent<Transform>().rotation;
                    nightmareUnderlight.SetActive(true);
                    Color nightlight = nightmareUnderlight.GetComponent<Light>().color;
                    nightEndColor = nightlight;
                    nightlight = Color.black;
                    nightmareUnderlight.GetComponent<Light>().color = nightlight;
                    nightBegColor = nightlight;
                }
                else if (TransitionLevelObjective == 29) {
                    endColor = noonLight.GetComponent<Light>().color;
                    endRotation = noonLight.GetComponent<Transform>().rotation;
                    Color nightlight = nightmareUnderlight.GetComponent<Light>().color;
                    nightBegColor = nightlight;
                    nightlight = Color.black;
                    nightEndColor = nightlight;
                }
                else {
                    endColor = Color.Lerp(noonLight.GetComponent<Light>().color, afternoonLight.GetComponent<Light>().color, TransitionLevelObjective / 27f);
                    endRotation = Quaternion.Lerp(noonLight.GetComponent<Transform>().rotation, afternoonLight.GetComponent<Transform>().rotation, TransitionLevelObjective / 27f);
                }

                isLightTransitionInitialized = true;
            }
            else {
                float lerpNum = (Time.time - TrannyStart) / transitionTimeLight;
                if (lerpNum > 1) {
                    isLightTransitionInitialized = false;
                    isLightingTransitioning = false;
                    if (TransitionLevelObjective == 29) {
                        if (PlayerPrefs.GetInt("Is Shadows On") == 1) {
                            nightmareUnderlight.SetActive(true);
                            RenderSettings.skybox = altsky;
                        }
                        else {
                            nightmareUnderlight.SetActive(false);
                            RenderSettings.skybox = normsky;
                        }
                    }
                    else if (TransitionLevelObjective == 28) {
                        nightmareUnderlight.SetActive(true);
                        RenderSettings.skybox = altsky;
                    }
                    GameObject.Find("Reflection Probe").GetComponent<ReflectionProbe>().RenderProbe();
                }
                else {
                    currentLight.GetComponent<Light>().color = Color.Lerp(begColor, endColor, Mathf.SmoothStep(0f, 1f, lerpNum));
                    RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
                    currentLight.GetComponent<Transform>().rotation = Quaternion.Lerp(begRotation, endRotation, Mathf.SmoothStep(0f, 1f, lerpNum));
                    if (TransitionLevelObjective == 28 || TransitionLevelObjective == 29) {
                        nightmareUnderlight.GetComponent<Light>().color = Color.Lerp(nightBegColor, nightEndColor, Mathf.SmoothStep(0f, 1f, lerpNum));
                    }
                }
            }
        }
    }

    public void LevelSetup() {
        playerController.GetComponent<SoundManager>().PlaySound(15);

        for (int i = 0; i < weapons.Length; i++) {
            if (i < playerController.attackMode - 1) {
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
        PlayerPrefs.SetInt("Upgrade Points", playerController.upgradePoints);
        PlayerPrefs.SetInt("Weapon", playerController.attackMode);
        PlayerPrefs.Save();

        isTitleTransitioning = true;
        beginAyyTime = Time.time;

        if (level < 7) {
            playerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.BridgeSection);
        }
        else if (level < 20) {
            playerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.WorldOpenUp);
        }
        else if (level < 28) {
            playerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.NorthernPart);
        }
        else if (level == 28) {
            playerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.Nightmare);
        }
        else if (level == 29) {
            if (PlayerPrefs.GetInt("Is Shadows On") == 1) {
                playerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.Nightmare);
            }
            else {
                playerController.GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.WorldOpenUp);
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
                backSourcePos = new Vector3((blackBackground.transform.position.x + (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) + borderSpace), blackBackground.transform.position.y, 0);
                textSourcePos = new Vector3((levelTitle.transform.position.x + (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) + borderSpace), levelTitle.transform.position.y, 0);
                text2SourcePos = new Vector3((levelSubtitle.transform.position.x + (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) + borderSpace), levelSubtitle.transform.position.y, 0);
                backOrigPos = blackBackground.transform.position;
                textOrigPos = levelTitle.transform.position;
                text2OrigPos = levelSubtitle.transform.position;
                backDestPos = new Vector3((blackBackground.transform.position.x - (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) - borderSpace), blackBackground.transform.position.y, 0);
                textDestPos = new Vector3((levelTitle.transform.position.x - (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) - borderSpace), levelTitle.transform.position.y, 0);
                text2DestPos = new Vector3((levelSubtitle.transform.position.x - (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) - borderSpace), levelSubtitle.transform.position.y, 0);
                blackBackground.SetActive(true);
                levelTitle.SetActive(true);
                levelSubtitle.SetActive(true);

                int minInt = 0;
                int maxInt = levelTexts[level - 1].subtitles.Length - 1;
                int randInt = Random.Range(minInt, maxInt);

                levelTitle.GetComponent<Text>().text = levelTexts[level - 1].title;
                levelSubtitle.GetComponent<Text>().text = levelTexts[level - 1].subtitles[randInt];
                levelSubtitle.GetComponent<Text>().font = levelTexts[level - 1].subtitleFont;
                levelSubtitle.GetComponent<Text>().color = levelTexts[level - 1].subtitleColor;

                initAlphaVal = blackBackground.GetComponent<Image>().color.a;

                isTitleTransitionInitialized = true;
            }

            float lerpBeg = (Time.time - beginAyyTime) / ayyTransitionTime;
            float stay = Time.time - (beginAyyTime + ayyTransitionTime);
            float lerpEnd = (Time.time - (beginAyyTime + ayyTransitionTime + ayyStayTime)) / ayyTransitionTime;

            if (lerpBeg > 1) {
                isTitleAnimating = true;
                blackBackground.transform.position = backOrigPos;
                levelTitle.transform.position = textOrigPos;
                levelSubtitle.transform.position = text2OrigPos;
            }

            if (lerpEnd > 1) {
                isTitleTransitioning = false;
                blackBackground.SetActive(false);
                levelTitle.SetActive(false);
                levelSubtitle.SetActive(false);
                blackBackground.transform.position = backOrigPos;
                levelTitle.transform.position = textOrigPos;
                levelSubtitle.transform.position = text2OrigPos;
                isTitleAnimating = false;
                isTitleTransitionInitialized = false;
                Color backColor = blackBackground.GetComponent<Image>().color;
                backColor.a = initAlphaVal;
                blackBackground.GetComponent<Image>().color = backColor;
            }
            else {
                if (isTitleAnimating == false) {
                    blackBackground.transform.position = Vector3.Lerp(backSourcePos, backOrigPos, Mathf.SmoothStep(0, 1, lerpBeg));
                    levelTitle.transform.position = Vector3.Lerp(textSourcePos, textOrigPos, Mathf.SmoothStep(0, 1, lerpBeg));
                    levelSubtitle.transform.position = Vector3.Lerp(text2SourcePos, text2OrigPos, Mathf.SmoothStep(0, 1, lerpBeg));
                    Color backColor = blackBackground.GetComponent<Image>().color;
                    backColor.a = Mathf.Lerp(0, initAlphaVal, Mathf.SmoothStep(0, 1, lerpBeg));
                    blackBackground.GetComponent<Image>().color = backColor;
                    Color textColor = levelTitle.GetComponent<Text>().color;
                    textColor.a = Mathf.Lerp(0, 1, Mathf.SmoothStep(0, 1, lerpBeg));
                    levelTitle.GetComponent<Text>().color = textColor;
                    Color textColor2 = levelSubtitle.GetComponent<Text>().color;
                    textColor2.a = Mathf.Lerp(0, 1, Mathf.SmoothStep(0, 1, lerpBeg));
                    levelSubtitle.GetComponent<Text>().color = textColor2;
                }
                else if (isTitleAnimating == true && (stay > ayyStayTime)) {
                    blackBackground.transform.position = Vector3.Lerp(backOrigPos, backDestPos, Mathf.SmoothStep(0, 1, lerpEnd));
                    levelTitle.transform.position = Vector3.Lerp(textOrigPos, textDestPos, Mathf.SmoothStep(0, 1, lerpEnd));
                    levelSubtitle.transform.position = Vector3.Lerp(text2OrigPos, text2DestPos, Mathf.SmoothStep(0, 1, lerpEnd));
                    Color backColor = blackBackground.GetComponent<Image>().color;
                    backColor.a = Mathf.Lerp(initAlphaVal, 0, Mathf.SmoothStep(0, 1, lerpEnd));
                    blackBackground.GetComponent<Image>().color = backColor;
                    Color textColor = levelTitle.GetComponent<Text>().color;
                    textColor.a = Mathf.Lerp(1, 0, Mathf.SmoothStep(0, 1, lerpEnd));
                    levelTitle.GetComponent<Text>().color = textColor;
                    Color textColor2 = levelSubtitle.GetComponent<Text>().color;
                    textColor2.a = Mathf.Lerp(1, 0, Mathf.SmoothStep(0, 1, lerpEnd));
                    levelSubtitle.GetComponent<Text>().color = textColor2;

                }
            }
        }
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
        blackBackground2.SetActive(false);
        blockadeText.SetActive(false);
    }

    void SpawnEnemies() {
        if (DebugDisableSpawning != true) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isControlOff = false;
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
    }

    void CheckIfAllEnemiesAreDead() {
        GameObject areTheAlive = GameObject.FindWithTag("Enemy");
        if (areTheAlive == null) {
            isLightingTransitioning = true;
            TrannyStart = Time.time;
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
        GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(settingsType);
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
                originalScale = weaponModels[weaponToFade - 1].transform.localScale;
                objectToScale = weaponModels[weaponToFade - 1];
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                playerController.GetComponent<SoundManager>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }

            bool fadeErUp = true;

            if (weaponToFade == 8 || weaponToFade == 9 || weaponToFade == 10) {
                fadeErUp = false;
            }

            weaponToFade = 0;

            float lerpTime = ((Time.time - fadingWeaponStartTime) / timeToFadeWeapon);

            if (fadeErUp == false) {
                weaponIsFading = false;
            }
            else {
                if (lerpTime > 1) {
                    weaponIsFading = false;
                }
                else {
                    objectToScale.transform.localScale = originalScale * Mathf.SmoothStep(0, 1, lerpTime);
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
                    blackBackground2.SetActive(true);
                    blockadeText.SetActive(true);
                    blockades[1 - 1].GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                    Invoke("EnableIsCheckingForTransition", 2.5f);
                    break;
            }

            isCheckingForTransition = false;
            isTransitionDone = false;
        }
        else {
            playerController.isControlOff = true;
        }
    }

    void TransitionAction(TransitionDoneType transitionDoneType, int objectToFade) {
        if (transitionDoneType == TransitionDoneType.Filler) {
            isCheckingForTransition = false;
            isTransitionDone = false;
            constantlyDenyInput = false;
        }
        else {
            blackBackground2.SetActive(true);
            blockadeText.SetActive(true);
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

            blockadeText.GetComponent<Text>().text = transitionText;
            transitionMode += 1;
            Invoke("EnableIsCheckingForTransition", 2.5f);
        }
    }

    void EnableIsCheckingForTransition() {
        RemoveLevelPrompts();
        MainSystems mainSystems = GameObject.Find("Main Systems").GetComponent<MainSystems>();

        switch (transitionMode) {
            case 2:
                mainSystems.GoToSettings(4);
                break;
            case 0:
                mainSystems.GoToSettings(9);
                break;
            case 5:
                mainSystems.GoToSettings(11);
                break;
            case 7:
                mainSystems.GoToSettings(12);
                break;
            case 9:
                mainSystems.GoToSettings(4);
                break;
            case 12:
                mainSystems.GoToSettings(14);
                break;
            case 14:
                mainSystems.GoToSettings(4);
                break;
            case 17:
                mainSystems.GoToSettings(16);
                break;
            case 19:
                mainSystems.GoToSettings(4);
                break;
            case 22:
                mainSystems.GoToSettings(18);
                break;
            case 24:
                mainSystems.GoToSettings(19);
                break;
            case 26:
                mainSystems.GoToSettings(4);
                break;
            case 29:
                mainSystems.GoToSettings(21);
                break;
            case 31:
                mainSystems.GoToSettings(22);
                break;
            case 33:
                mainSystems.GoToSettings(23);
                break;
            case 35:
                mainSystems.GoToSettings(24);
                break;
            case 37:
                mainSystems.GoToSettings(25);
                break;
            case 39:
                mainSystems.GoToSettings(4);
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