﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public bool DebugDisableSpawning;

    public LevelData[] levelData;

    public List<GameObject> listOfAllEnemies = new List<GameObject>();

    public GameObject blackBackground;
    public GameObject nextLevelText;
    public GameObject blackBackground2;
    public GameObject blockadeText;
    public GameObject tutorialScreen1;
    public GameObject tutorialScreen2;
    public GameObject tutorialScreen3;
    public GameObject tutorialScreen4;
    public GameObject tutorialScreen5;
    public GameObject tutorialScreen6;

    // 7
    public GameObject[] enemies;

    public GameObject level1Blockade;
    public GameObject level2Blockade;
    public GameObject level3Blockade;
    public GameObject level4Blockade;
    public GameObject level5Blockade;
    public GameObject level6Blockade;
    public GameObject level7Blockade;
    public GameObject level8Blockade;

    // 46
    public Transform[] spawnPoints;


    public int level = 1;

    public PlayerController playerai;

    public bool isTransitionDone = false;
    public bool isCheckingForTransition = false;

    public Material altsky;
    public Material normsky;

    public GameObject resetHighScoresButton;

    public GameObject newGameButtonText;

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

    // Use this for initialization
    void Start () {
        
        //Debug.Log(PlayerPrefs.HasKey("Level"));
        if (PlayerPrefs.HasKey("Level")) {
            level = PlayerPrefs.GetInt("Level");
            //level = 29;
        }
        else {
            level = 1;
        }

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
            GetComponent<MainSystems>().removeAdsButton.SetActive(true);
            if (PlayerPrefs.GetInt("Is Shadows On") == 1)
            {
                currentLight.GetComponent<Light>().color = noonLight.GetComponent<Light>().color;
                RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
                currentLight.GetComponent<Transform>().rotation = noonLight.GetComponent<Transform>().rotation;
            }
            else
            {
                currentLight.GetComponent<Light>().color = nightmareLight.GetComponent<Light>().color;
                RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
                currentLight.GetComponent<Transform>().rotation = nightmareLight.GetComponent<Transform>().rotation;
                nightmareUnderlight.SetActive(true);
                RenderSettings.skybox = altsky;
            }
            weapon10.SetActive(true);
        }
    }

    public void ToggleLighting()
    {
        if (PlayerPrefs.GetInt("Is Shadows On") == 1)
        {
            
            PlayerPrefs.SetInt("Is Shadows On", 0);
            currentLight.GetComponent<Light>().color = nightmareLight.GetComponent<Light>().color;
            RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
            currentLight.GetComponent<Transform>().rotation = nightmareLight.GetComponent<Transform>().rotation;
            nightmareUnderlight.SetActive(true);
            RenderSettings.skybox = altsky;
            GameObject.Find("Reflection Probe").GetComponent<ReflectionProbe>().RenderProbe();
        }
        else
        {
            PlayerPrefs.SetInt("Is Shadows On", 1);
            currentLight.GetComponent<Light>().color = noonLight.GetComponent<Light>().color;
            RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
            currentLight.GetComponent<Transform>().rotation = noonLight.GetComponent<Transform>().rotation;
            nightmareUnderlight.SetActive(false);
            RenderSettings.skybox = normsky;
            GameObject.Find("Reflection Probe").GetComponent<ReflectionProbe>().RenderProbe();
        }
    }

    // Update is called once per frame

    public bool constantlyDenyInput = false;

    bool inputRestored = true;

	void Update () {
        if (isCheckingForTransition == true) {
            CheckForTransitionDoneReport();
        }

        highSpeedCarCrash();
        //InfiniteSpawner();
        //Debug.Log(listOfAllEnemies.Count);
        FadeInWeaponModel();
        ExtremeMemeSpicyMemeLmaoAyyyKillMePleaseEndMySufferingIWantToDie();
        if (constantlyDenyInput == true) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().isAllInputEnabled(false);
            //Debug.Log("please kill me");
            inputRestored = false;
        }
        else if (constantlyDenyInput == false && inputRestored == false) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().isAllInputEnabled(true);
            //Debug.Log("i wanna fucking die");
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

        StartCoroutine(spawnEnemy(spawnType, sspawnType, 0));
        Invoke("InfiniteSpawner", .25f);
    }

    public bool isTransitioningBigCockTranny = false;
    public float TrannyStart = 0;
    float transitionTimeLight = 5;

    public GameObject noonLight;
    public GameObject afternoonLight;
    public GameObject currentLight;
    public GameObject nightmareLight;
    public GameObject nightmareUnderlight;

    bool createdSourceAndDest = false;

    Color begColor;
    Color endColor;
    Quaternion begRotation;
    Quaternion endRotation;

    Color nightBegColor;
    Color nightEndColor;

    public int TransitionLevelObjective;

    void highSpeedCarCrash() {
        if (isTransitioningBigCockTranny == true) {
            if (createdSourceAndDest == false) {
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

                createdSourceAndDest = true;
            }
            else {
                float lerpNum = (Time.time - TrannyStart) / transitionTimeLight;
                if (lerpNum > 1) {
                    createdSourceAndDest = false;
                    isTransitioningBigCockTranny = false;
                    if (TransitionLevelObjective == 29) {
                        if (PlayerPrefs.GetInt("Is Shadows On") == 1)
                        {
                            nightmareUnderlight.SetActive(true);
                            RenderSettings.skybox = altsky;
                        }
                        else
                        {
                            nightmareUnderlight.SetActive(false);
                            RenderSettings.skybox = normsky;
                        }
                    }
                    else if (TransitionLevelObjective == 28)
                    {
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

    public void Level1() {
        //level = 27;
        GameObject.Find("Player").GetComponent<SoundManager>().PlaySound(15);
        //isTransitioningBigCockTranny = true;
        //TrannyStart = Time.time;

        if (playerai.attackMode == 1) {
            weapon1.SetActive(true);
            weapon2.SetActive(true);
            weapon3.SetActive(true);
            weapon4.SetActive(true);
            weapon5.SetActive(true);
            weapon6.SetActive(true);
            weapon7.SetActive(true);
            weapon8.SetActive(true);
            weapon9.SetActive(true);
            weapon10.SetActive(true);
        }
        else if (playerai.attackMode == 2) {
            weapon2.SetActive(true);
            weapon3.SetActive(true);
            weapon4.SetActive(true);
            weapon5.SetActive(true);
            weapon6.SetActive(true);
            weapon7.SetActive(true);
            weapon8.SetActive(true);
            weapon9.SetActive(true);
            weapon10.SetActive(true);
        }
        else if (playerai.attackMode == 3) {
            weapon3.SetActive(true);
            weapon4.SetActive(true);
            weapon5.SetActive(true);
            weapon6.SetActive(true);
            weapon7.SetActive(true);
            weapon8.SetActive(true);
            weapon9.SetActive(true);
            weapon10.SetActive(true);
        }
        else if (playerai.attackMode == 4) {
            weapon4.SetActive(true);
            weapon5.SetActive(true);
            weapon6.SetActive(true);
            weapon7.SetActive(true);
            weapon8.SetActive(true);
            weapon9.SetActive(true);
            weapon10.SetActive(true);
        }
        else if (playerai.attackMode == 5) {
            weapon5.SetActive(true);
            weapon6.SetActive(true);
            weapon7.SetActive(true);
            weapon8.SetActive(true);
            weapon9.SetActive(true);
            weapon10.SetActive(true);
        }
        else if (playerai.attackMode == 6) {
            weapon6.SetActive(true);
            weapon7.SetActive(true);
            weapon8.SetActive(true);
            weapon9.SetActive(true);
            weapon10.SetActive(true);
        }
        else if (playerai.attackMode == 7) {
            weapon7.SetActive(true);
            weapon8.SetActive(true);
            weapon9.SetActive(true);
            weapon10.SetActive(true);
        }
        else if (playerai.attackMode == 8) {
            weapon8.SetActive(true);
            weapon9.SetActive(true);
            weapon10.SetActive(true);
        }

        if (level < 28) {
            weapon8.SetActive(false);
            weapon9.SetActive(false);
            weapon10.SetActive(false);
        }
        if (level < 20) {
            weapon7.SetActive(false);
            weapon6.SetActive(false);
        }
        if (level < 16) {
            weapon5.SetActive(false);
        }
        if (level < 12) {
            weapon4.SetActive(false);
        }
        if (level < 7) {
            weapon3.SetActive(false);
            weapon2.SetActive(false);
        }
        if (level < 3) {
            weapon1.SetActive(false);
        }

        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Upgrade Points", playerai.upgradePoints);
        PlayerPrefs.SetInt("Weapon", playerai.attackMode);
        PlayerPrefs.Save();

        //Debug.Log("wdsdfss");

        isAyying = true;
        beginAyyTime = Time.time;

        if (level < 7)
        {
            GameObject.Find("Player").GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.BridgeSection);
        }
        else if (level < 20) {
            GameObject.Find("Player").GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.WorldOpenUp);
        }
        else if (level < 28)
        {
            GameObject.Find("Player").GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.NorthernPart);
        }
        else if (level == 28)
        {
            GameObject.Find("Player").GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.Nightmare);
        }
        else if (level == 29)
        {
            if (PlayerPrefs.GetInt("Is Shadows On") == 1)
            {
                GameObject.Find("Player").GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.Nightmare);
            }
            else
            {
                /*int shake = Random.Range(1, 3);
                switch (shake)
                {
                    case 1:
                        GameObject.Find("Player").GetComponent<PlaySoundEffect>().MusicManager(PlaySoundEffect.MusicMood.BridgeSection);
                        break;
                    case 2:
                        GameObject.Find("Player").GetComponent<PlaySoundEffect>().MusicManager(PlaySoundEffect.MusicMood.WorldOpenUp);
                        break;
                    case 3:
                        GameObject.Find("Player").GetComponent<PlaySoundEffect>().MusicManager(PlaySoundEffect.MusicMood.NorthernPart);
                        break;
                }*/

                GameObject.Find("Player").GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.WorldOpenUp);
            }
            
        }

        if (level == 1) {
            Invoke("ShowTutorial", 4);
        }
        else if (level == 3) {
            Invoke("ShowTutorial", 4);
            // use pink points to buy weapons to fight tougher enemies.
        }
        else if (level == 6) {
            Invoke("ShowTutorial", 4);
            // double tap, then swipe in a direction to do a special rush attack that passes through enemies.
        }
        else if (level == 15) {
            Invoke("ShowTutorial", 4);
            // triple tap on an enemy to do a powerful, area of effect jump attack that damages all enemies in the area.
        }
        else if (level == 28) {
            Invoke("ShowTutorial", 4);
            // buy a fire pillar to ascend into eternity and end the game!
        }
        else if (level == 29) {
            //Invoke("ShowTutorial", 0.5f);
            // survive as long as you can and get the highest score! Fire pillars have now become perks - buy them with pink points!
        }

        if (level > 3) {
            level1Blockade.SetActive(false);
        }
        if (level > 7) {
            level2Blockade.SetActive(false);
        }
        if (level > 12) {
            level3Blockade.SetActive(false);
        }
        if (level > 16) {
            level4Blockade.SetActive(false);
        }
        if (level > 20) {
            level5Blockade.SetActive(false);
        }
        if (level > 28) {
            level6Blockade.SetActive(false);
            level7Blockade.SetActive(false);
            level8Blockade.SetActive(false);
        }

        if (level == 3)
        {
            level1Blockade.SetActive(false);
        }
        else if (level == 7)
        {
            level2Blockade.SetActive(false);
        }
        else if (level == 12) {
            level3Blockade.SetActive(false);
        }
        else if (level == 16) {
            level4Blockade.SetActive(false);
        }
        else if (level == 20) {
            level5Blockade.SetActive(false);
        }
        else if (level == 28) {
            level6Blockade.SetActive(false);
            level7Blockade.SetActive(false);
            level8Blockade.SetActive(false);
        }

        Invoke("RemoveLevelPrompts", 4);
        Invoke("SpawnLevel1", 6);
    }

    bool isAyying = false;
    bool hasCreatedAyyVars = false;
    float beginAyyTime;

    public GameObject nextLevelText2;

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

    public GameObject hudCanvas;

    bool ayyIsIntrod = false;

    float ayyTransitionTime = .5f;
    float ayyStayTime = 6;

    float borderSpace = 500;

    public Font nick;
    public Font poiret;
    public Font courgette;
    public Font forq;

    public Text poircolor;
    public Text courcolor;
    public Text nickcolor;

    void ExtremeMemeSpicyMemeLmaoAyyyKillMePleaseEndMySufferingIWantToDie () {
        if (isAyying == true) {
            if (hasCreatedAyyVars == false) {
                backSourcePos = new Vector3((blackBackground.transform.position.x + (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) + borderSpace), blackBackground.transform.position.y, 0);
                textSourcePos = new Vector3((nextLevelText.transform.position.x + (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) + borderSpace), nextLevelText.transform.position.y, 0);
                text2SourcePos = new Vector3((nextLevelText2.transform.position.x + (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) + borderSpace), nextLevelText2.transform.position.y, 0);
                backOrigPos = blackBackground.transform.position;
                textOrigPos = nextLevelText.transform.position;
                text2OrigPos = nextLevelText2.transform.position;
                backDestPos = new Vector3((blackBackground.transform.position.x - (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) - borderSpace), blackBackground.transform.position.y, 0);
                textDestPos = new Vector3((nextLevelText.transform.position.x - (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) - borderSpace), nextLevelText.transform.position.y, 0);
                text2DestPos = new Vector3((nextLevelText2.transform.position.x - (hudCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) - borderSpace), nextLevelText2.transform.position.y, 0);
                blackBackground.SetActive(true);
                nextLevelText.SetActive(true);
                nextLevelText2.SetActive(true);

                if (level == 1) {
                    nextLevelText.GetComponent<Text>().text = "The First Circle";
                    nextLevelText2.GetComponent<Text>().text = "Deez Pink Punks";
                    nextLevelText2.GetComponent<Text>().font = nick;
                    nextLevelText2.GetComponent<Text>().color = nickcolor.color;
                }
                else if (level == 2) {
                    nextLevelText.GetComponent<Text>().text = "Circle 2";
                    nextLevelText2.GetComponent<Text>().text = "You Might Get Hurt";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 3) {
                    nextLevelText.GetComponent<Text>().text = "3rd Circle";
                    nextLevelText2.GetComponent<Text>().text = "Pink Metal";
                    nextLevelText2.GetComponent<Text>().font = courgette;
                    nextLevelText2.GetComponent<Text>().color = courcolor.color;
                }
                else if (level == 4) {
                    nextLevelText.GetComponent<Text>().text = "The Fourth Circle";
                    nextLevelText2.GetComponent<Text>().text = "Snek";
                    nextLevelText2.GetComponent<Text>().font = nick;
                    nextLevelText2.GetComponent<Text>().color = nickcolor.color;
                }
                else if (level == 5) {
                    nextLevelText.GetComponent<Text>().text = "Circle 5";
                    nextLevelText2.GetComponent<Text>().text = "This'll Be Tough";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 6) {
                    nextLevelText.GetComponent<Text>().text = "Circle 6";
                    nextLevelText2.GetComponent<Text>().text = "Me Too";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 7) {
                    nextLevelText.GetComponent<Text>().text = "The Seventh Circle";
                    nextLevelText2.GetComponent<Text>().text = "Negan";
                    nextLevelText2.GetComponent<Text>().font = nick;
                    nextLevelText2.GetComponent<Text>().color = nickcolor.color;
                }
                else if (level == 8) {
                    nextLevelText.GetComponent<Text>().text = "Circle 8";
                    nextLevelText2.GetComponent<Text>().text = "All Together Now";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 9) {
                    nextLevelText.GetComponent<Text>().text = "Circle 9";
                    nextLevelText2.GetComponent<Text>().text = "You Know";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 10) {
                    nextLevelText.GetComponent<Text>().text = "10th Circle";
                    nextLevelText2.GetComponent<Text>().text = "Jiggly Spikey";
                    nextLevelText2.GetComponent<Text>().font = courgette;
                    nextLevelText2.GetComponent<Text>().color = courcolor.color;
                }
                else if (level == 11) {
                    nextLevelText.GetComponent<Text>().text = "The Eleventh Circle";
                    nextLevelText2.GetComponent<Text>().text = "Do You Even Lift?";
                    nextLevelText2.GetComponent<Text>().font = nick;
                    nextLevelText2.GetComponent<Text>().color = nickcolor.color;
                }
                else if (level == 12) {
                    nextLevelText.GetComponent<Text>().text = "The Twelfth Circle";
                    nextLevelText2.GetComponent<Text>().text = "Here's Johnny!";
                    nextLevelText2.GetComponent<Text>().font = nick;
                    nextLevelText2.GetComponent<Text>().color = nickcolor.color;
                }
                else if (level == 13) {
                    nextLevelText.GetComponent<Text>().text = "Circle 13";
                    nextLevelText2.GetComponent<Text>().text = "On The Horizon";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 14) {
                    nextLevelText.GetComponent<Text>().text = "14th Circle";
                    nextLevelText2.GetComponent<Text>().text = "Pink Hulk Smash";
                    nextLevelText2.GetComponent<Text>().font = courgette;
                    nextLevelText2.GetComponent<Text>().color = courcolor.color;
                }
                else if (level == 15) {
                    nextLevelText.GetComponent<Text>().text = "The Fifteenth Circle";
                    nextLevelText2.GetComponent<Text>().text = "Samurai Jack";
                    nextLevelText2.GetComponent<Text>().font = nick;
                    nextLevelText2.GetComponent<Text>().color = nickcolor.color;
                }
                else if (level == 16) {
                    nextLevelText.GetComponent<Text>().text = "16th Circle";
                    nextLevelText2.GetComponent<Text>().text = "Too Much Drink?";
                    nextLevelText2.GetComponent<Text>().font = courgette;
                    nextLevelText2.GetComponent<Text>().color = courcolor.color;
                }
                else if (level == 17) {
                    nextLevelText.GetComponent<Text>().text = "17th Circle";
                    nextLevelText2.GetComponent<Text>().text = "Pink Monster, Green Puddle";
                    nextLevelText2.GetComponent<Text>().font = courgette;
                    nextLevelText2.GetComponent<Text>().color = courcolor.color;
                }
                else if (level == 18) {
                    nextLevelText.GetComponent<Text>().text = "18th Circle";
                    nextLevelText2.GetComponent<Text>().text = "Whack A Mole";
                    nextLevelText2.GetComponent<Text>().font = courgette;
                    nextLevelText2.GetComponent<Text>().color = courcolor.color;
                }
                else if (level == 19) {
                    nextLevelText.GetComponent<Text>().text = "The Nineteenth Circle";
                    nextLevelText2.GetComponent<Text>().text = "Fizzy Sody Pop";
                    nextLevelText2.GetComponent<Text>().font = nick;
                    nextLevelText2.GetComponent<Text>().color = nickcolor.color;
                }
                else if (level == 20) {
                    nextLevelText.GetComponent<Text>().text = "20th Circle";
                    nextLevelText2.GetComponent<Text>().text = "Lick N' Stab";
                    nextLevelText2.GetComponent<Text>().font = courgette;
                    nextLevelText2.GetComponent<Text>().color = courcolor.color;
                }
                else if (level == 21) {
                    nextLevelText.GetComponent<Text>().text = "Circle 21";
                    nextLevelText2.GetComponent<Text>().text = "Roller Pin";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 22) {
                    nextLevelText.GetComponent<Text>().text = "Circle 22";
                    nextLevelText2.GetComponent<Text>().text = "Afternoon Delight";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 23) {
                    nextLevelText.GetComponent<Text>().text = "Circle 23";
                    nextLevelText2.GetComponent<Text>().text = "Don't Touch It";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 24) {
                    nextLevelText.GetComponent<Text>().text = "Circle 24";
                    nextLevelText2.GetComponent<Text>().text = "Just Die Already";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 25) {
                    nextLevelText.GetComponent<Text>().text = "The Twenty-Fifth Circle";
                    nextLevelText2.GetComponent<Text>().text = "A Weapon To Surpass...";
                    nextLevelText2.GetComponent<Text>().font = nick;
                    nextLevelText2.GetComponent<Text>().color = nickcolor.color;
                }
                else if (level == 26) {
                    nextLevelText.GetComponent<Text>().text = "Circle 26";
                    nextLevelText2.GetComponent<Text>().text = "Sunset Boulevard";
                    nextLevelText2.GetComponent<Text>().font = poiret;
                    nextLevelText2.GetComponent<Text>().color = poircolor.color;
                }
                else if (level == 27) {
                    nextLevelText.GetComponent<Text>().text = "27th Circle";
                    nextLevelText2.GetComponent<Text>().text = "Aww, You All Came";
                    nextLevelText2.GetComponent<Text>().font = courgette;
                    nextLevelText2.GetComponent<Text>().color = courcolor.color;
                }
                else if (level == 28) {
                    nextLevelText.GetComponent<Text>().text = "THE 28TH CIRCLE";
                    nextLevelText2.GetComponent<Text>().text = "Infinite Midnight";
                    nextLevelText2.GetComponent<Text>().font = forq;
                    nextLevelText2.GetComponent<Text>().color = Color.white;
                }
                else if (level == 29) {
                    nextLevelText.GetComponent<Text>().text = "GET HIGHEST SCORE";

                    int minInt = 1;
                    int maxInt = 10;
                    int randInt = Random.Range(minInt, maxInt);

                    if (randInt == 1) {
                        nextLevelText2.GetComponent<Text>().text = "I've Run Out Of Memes";
                    }
                    else if (randInt == 2) {
                        nextLevelText2.GetComponent<Text>().text = "Bet You Can't Do It Like Me";
                    }
                    else if (randInt == 3) {
                        nextLevelText2.GetComponent<Text>().text = "Why Only 8?";
                    }
                    else if (randInt == 4) {
                        nextLevelText2.GetComponent<Text>().text = "Whack All The Pink";
                    }
                    else if (randInt == 5) {
                        nextLevelText2.GetComponent<Text>().text = "Have You Found The Suit Yet?";
                    }
                    else if (randInt == 6) {
                        nextLevelText2.GetComponent<Text>().text = "Pink Discrimination";
                    }
                    else if (randInt == 7) {
                        nextLevelText2.GetComponent<Text>().text = "Pink Season";
                    }
                    else if (randInt == 8) {
                        nextLevelText2.GetComponent<Text>().text = "Anti-Pink Guy";
                    }
                    else if (randInt == 9) {
                        nextLevelText2.GetComponent<Text>().text = "Two In The Pink Eyes";
                    }

                    nextLevelText2.GetComponent<Text>().font = nick;
                    nextLevelText2.GetComponent<Text>().color = nickcolor.color;
                }

                initAlphaVal = blackBackground.GetComponent<Image>().color.a;

                hasCreatedAyyVars = true;
            }

            float lerpBeg = (Time.time - beginAyyTime) / ayyTransitionTime;
            float stay = Time.time - (beginAyyTime + ayyTransitionTime);
            float lerpEnd = (Time.time - (beginAyyTime + ayyTransitionTime + ayyStayTime)) / ayyTransitionTime;

            if (lerpBeg > 1) {
                ayyIsIntrod = true;
                blackBackground.transform.position = backOrigPos;
                nextLevelText.transform.position = textOrigPos;
                nextLevelText2.transform.position = text2OrigPos;
            }

            if (lerpEnd > 1) {
                isAyying = false;
                blackBackground.SetActive(false);
                nextLevelText.SetActive(false);
                nextLevelText2.SetActive(false);
                blackBackground.transform.position = backOrigPos;
                nextLevelText.transform.position = textOrigPos;
                nextLevelText2.transform.position = text2OrigPos;
                ayyIsIntrod = false;
                hasCreatedAyyVars = false;
                Color backColor = blackBackground.GetComponent<Image>().color;
                backColor.a = initAlphaVal;
                blackBackground.GetComponent<Image>().color = backColor;
            }
            else {
                if (ayyIsIntrod == false) {
                    blackBackground.transform.position = Vector3.Lerp(backSourcePos, backOrigPos, Mathf.SmoothStep(0, 1, lerpBeg));
                    nextLevelText.transform.position = Vector3.Lerp(textSourcePos, textOrigPos, Mathf.SmoothStep(0, 1, lerpBeg));
                    nextLevelText2.transform.position = Vector3.Lerp(text2SourcePos, text2OrigPos, Mathf.SmoothStep(0, 1, lerpBeg));
                    Color backColor = blackBackground.GetComponent<Image>().color;
                    backColor.a = Mathf.Lerp(0, initAlphaVal, Mathf.SmoothStep(0, 1, lerpBeg));
                    blackBackground.GetComponent<Image>().color = backColor;
                    Color textColor = nextLevelText.GetComponent<Text>().color;
                    textColor.a = Mathf.Lerp(0, 1, Mathf.SmoothStep(0, 1, lerpBeg));
                    nextLevelText.GetComponent<Text>().color = textColor;
                    Color textColor2 = nextLevelText2.GetComponent<Text>().color;
                    textColor2.a = Mathf.Lerp(0, 1, Mathf.SmoothStep(0, 1, lerpBeg));
                    nextLevelText2.GetComponent<Text>().color = textColor2;
                }
                else if (ayyIsIntrod == true && (stay > ayyStayTime)) {
                    blackBackground.transform.position = Vector3.Lerp(backOrigPos, backDestPos, Mathf.SmoothStep(0, 1, lerpEnd));
                    nextLevelText.transform.position = Vector3.Lerp(textOrigPos, textDestPos, Mathf.SmoothStep(0, 1, lerpEnd));
                    nextLevelText2.transform.position = Vector3.Lerp(text2OrigPos, text2DestPos, Mathf.SmoothStep(0, 1, lerpEnd));
                    Color backColor = blackBackground.GetComponent<Image>().color;
                    backColor.a = Mathf.Lerp(initAlphaVal, 0, Mathf.SmoothStep(0, 1, lerpEnd));
                    blackBackground.GetComponent<Image>().color = backColor;
                    Color textColor = nextLevelText.GetComponent<Text>().color;
                    textColor.a = Mathf.Lerp(1, 0, Mathf.SmoothStep(0, 1, lerpEnd));
                    nextLevelText.GetComponent<Text>().color = textColor;
                    Color textColor2 = nextLevelText2.GetComponent<Text>().color;
                    textColor2.a = Mathf.Lerp(1, 0, Mathf.SmoothStep(0, 1, lerpEnd));
                    nextLevelText2.GetComponent<Text>().color = textColor2;
                    
                }
            }
        }
    }

    public void ShowTutorial() {
        if (level == 1) {
            tutorialScreen1.SetActive(true);
            Time.timeScale = 0;
        }
        else if (level == 3) {
            tutorialScreen2.SetActive(true);
            Time.timeScale = 0;
        }
        else if (level == 6) {
            tutorialScreen3.SetActive(true);
            Time.timeScale = 0;
        }
        else if (level == 15) {
            tutorialScreen4.SetActive(true);
            Time.timeScale = 0;
        }
        else if (level == 28) {
            tutorialScreen5.SetActive(true);
            Time.timeScale = 0;
        }
        else if (level == 29) {
            tutorialScreen6.SetActive(true);
            //Time.timeScale = 0;
        }
        Cursor.visible = true;
    }

    void RemoveLevelPrompts ()
    {
        blackBackground2.SetActive(false);
        blockadeText.SetActive(false);
    }

    


    void SpawnLevel1 ()
    {
        if (DebugDisableSpawning != true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isControlOff = false;
            if (level < 28) {
                float delay = 0f;
                foreach (SpawnData spawnData in levelData[level - 1].spawnDataArray) {
                    StartCoroutine(spawnEnemy(spawnData.enemyType, spawnData.spawnPoint, delay));
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

    public int transitionMode = 0;

    void CheckIfAllEnemiesAreDead ()
    {
        GameObject areTheAlive = GameObject.FindWithTag("Enemy");
        //areTheAlive.transform.localScale = new Vector3(5,5,5);
        if (areTheAlive == null)
        {
            isTransitioningBigCockTranny = true;
            TrannyStart = Time.time;
            TransitionLevelObjective = level + 1;

            if (level == 2) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(8);
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 6) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(10);
                transitionMode = 4;
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 11) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(13);
                transitionMode = 11;
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 15) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(15);
                transitionMode = 16;
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 19) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(17);
                transitionMode = 21;
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 27) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(20);
                transitionMode = 28;
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else {
                level++;
                Level1();
                CancelInvoke("CheckIfAllEnemiesAreDead");
            }
        }
    }

    public GameObject weapon1;
    public GameObject weapon1Model;
    public GameObject weapon2;
    public GameObject weapon2Model;
    public GameObject weapon3;
    public GameObject weapon3Model;
    public GameObject weapon4;
    public GameObject weapon4Model;
    public GameObject weapon5;
    public GameObject weapon5Model;
    public GameObject weapon6;
    public GameObject weapon6Model;
    public GameObject weapon7;
    public GameObject weapon7Model;
    public GameObject weapon8;
    public GameObject weapon8Model;
    public GameObject weapon9;
    public GameObject weapon9Model;
    public GameObject weapon10;
    public GameObject weapon10Model;

    int weaponToFade;
    bool weaponIsFading = false;
    float timeToFadeWeapon = 2f;
    float fadingWeaponStartTime;
    Vector3 originalScale;
    GameObject objectToScale;

    public GameObject spawnDust;

    void FadeInWeaponModel () {
        if (weaponIsFading == true) {
            if (weaponToFade == 1) {
                weapon1.SetActive(true);
                originalScale = weapon1Model.transform.localScale;
                objectToScale = weapon1Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<SoundManager>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 2) {
                weapon2.SetActive(true);
                originalScale = weapon2Model.transform.localScale;
                objectToScale = weapon2Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<SoundManager>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 3) {
                weapon3.SetActive(true);
                originalScale = weapon3Model.transform.localScale;
                objectToScale = weapon3Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<SoundManager>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 4) {
                weapon4.SetActive(true);
                originalScale = weapon4Model.transform.localScale;
                objectToScale = weapon4Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<SoundManager>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 5) {
                weapon5.SetActive(true);
                originalScale = weapon5Model.transform.localScale;
                objectToScale = weapon5Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<SoundManager>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 6) {
                weapon6.SetActive(true);
                originalScale = weapon6Model.transform.localScale;
                objectToScale = weapon6Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<SoundManager>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 7) {
                weapon7.SetActive(true);
                originalScale = weapon7Model.transform.localScale;
                objectToScale = weapon7Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<SoundManager>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 8) {
                weapon8.SetActive(true);
            }
            else if (weaponToFade == 9) {
                weapon9.SetActive(true);
            }
            else if (weaponToFade == 10) {
                weapon10.SetActive(true);
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

    void CheckForTransitionDoneReport () {
        if (isTransitionDone == true) {
            //Debug.Log(transitionMode);

            if (transitionMode == 1) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "New weapon available!";
                transitionMode = 2;
                //CHECKPOINT 1: MAKE WEAPON GO POOF, FADE WEAPON IN
                weaponIsFading = true;
                weaponToFade = 1;
                fadingWeaponStartTime = Time.time;
                //Debug.Log("meme");
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 3) {
                isCheckingForTransition = false;
                isTransitionDone = false;
                constantlyDenyInput = false;
                //Debug.Log("hello");
            }
            else if (transitionMode == 4) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The blockade has been lifted!";
                transitionMode = 5;
                level2Blockade.GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 6) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "New weapon available!";
                weaponIsFading = true;
                weaponToFade = 2;
                fadingWeaponStartTime = Time.time;
                transitionMode = 7;
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 8) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "New weapon available!";
                weaponIsFading = true;
                weaponToFade = 3;
                fadingWeaponStartTime = Time.time;
                transitionMode = 9;
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 10) {
                isCheckingForTransition = false;
                isTransitionDone = false;
                constantlyDenyInput = false;
            }
            else if (transitionMode == 11) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The blockade has been lifted!";
                transitionMode = 12;
                level3Blockade.GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 13) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "New weapon available!";
                weaponIsFading = true;
                weaponToFade = 4;
                fadingWeaponStartTime = Time.time;
                transitionMode = 14;
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 15) {
                isCheckingForTransition = false;
                isTransitionDone = false;
                constantlyDenyInput = false;
            }
            else if (transitionMode == 16) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The blockade has been lifted!";
                transitionMode = 17;
                level4Blockade.GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 18) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "New weapon available!";
                weaponIsFading = true;
                weaponToFade = 5;
                fadingWeaponStartTime = Time.time;
                transitionMode = 19;
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 20) {
                isCheckingForTransition = false;
                isTransitionDone = false;
                constantlyDenyInput = false;
            }
            else if (transitionMode == 21) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The blockade has been lifted!";
                transitionMode = 22;
                level5Blockade.GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 23) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "New weapon available!";
                weaponIsFading = true;
                weaponToFade = 6;
                fadingWeaponStartTime = Time.time;
                transitionMode = 24;
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 25) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "New weapon available!";
                weaponIsFading = true;
                weaponToFade = 7;
                fadingWeaponStartTime = Time.time;
                transitionMode = 26;
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 27) {
                isCheckingForTransition = false;
                isTransitionDone = false;
                constantlyDenyInput = false;
            }
            else if (transitionMode == 28) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The blockade has been lifted!";
                transitionMode = 29;
                level6Blockade.GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 30) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The Flames Of Ascencion burn!";
                weaponIsFading = true;
                weaponToFade = 8; //Debug.Log("hello1");
                fadingWeaponStartTime = Time.time;
                transitionMode = 31;
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 32) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The blockade has been lifted!";
                transitionMode = 33;
                level7Blockade.GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 34) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The Flames Of Ascencion burn!";
                weaponIsFading = true;
                weaponToFade = 9; //Debug.Log("hello2");
                fadingWeaponStartTime = Time.time;
                transitionMode = 35;
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 36) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The blockade has been lifted!";
                transitionMode = 37;
                level8Blockade.GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 38) {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                blockadeText.GetComponent<Text>().text = "The Flames Of Ascencion burn!";
                weaponIsFading = true;
                weaponToFade = 10; //Debug.Log("hello3");
                fadingWeaponStartTime = Time.time;
                transitionMode = 39;
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }
            else if (transitionMode == 40) {
                isCheckingForTransition = false;
                isTransitionDone = false;
                constantlyDenyInput = false;
            }
            else {
                blackBackground2.SetActive(true);
                blockadeText.SetActive(true);
                level1Blockade.GetComponent<SinkAndDespawnBlockade>().DespawnBlockade();
                Invoke("EnableIsCheckingForTransition", 2.5f);
            }

            isCheckingForTransition = false;
            isTransitionDone = false;
            //constantlyDenyInput = false;
        }
        else {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isControlOff = true;
        }
    }

    void EnableIsCheckingForTransition () {
        RemoveLevelPrompts();

        if (transitionMode == 2) {
            transitionMode = 3;
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(4);
            //Debug.Log("bop");
        }
        else if (transitionMode == 0) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(9);
            transitionMode = 1;
            //Debug.Log("bop2");
        }
        else if (transitionMode == 5) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(11);
            transitionMode = 6;
        }
        else if (transitionMode == 7) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(12);
            transitionMode = 8;
        }
        else if (transitionMode == 9) {
            transitionMode = 10;
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(4);
        }
        else if (transitionMode == 12) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(14);
            transitionMode = 13;
        }
        else if (transitionMode == 14) {
            transitionMode = 15;
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(4);
        }
        else if (transitionMode == 17) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(16);
            transitionMode = 18;
        }
        else if (transitionMode == 19) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(4);
            transitionMode = 20;
        }
        else if (transitionMode == 22) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(18);
            transitionMode = 23;
        }
        else if (transitionMode == 24) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(19);
            transitionMode = 25;
        }
        else if (transitionMode == 26) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(4);
            transitionMode = 27;
        }
        else if (transitionMode == 29) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(21);
            transitionMode = 30;
        }
        else if (transitionMode == 31) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(22);
            transitionMode = 32;
        }
        else if (transitionMode == 33) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(23);
            transitionMode = 34;
        }
        else if (transitionMode == 35) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(24);
            transitionMode = 36;
        }
        else if (transitionMode == 37) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(25);
            transitionMode = 38;
        }
        else if (transitionMode == 39) {
            GameObject.Find("Main Systems").GetComponent<MainSystems>().GoToSettings(4);
            transitionMode = 40;
        }
        else {
            Debug.Log("shit's broke");
        }
 
        isCheckingForTransition = true;
        isTransitionDone = false;
    }

    IEnumerator spawnEnemy(int enemyType, int spawnPoint, float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        GameObject spawnedEnemy = Instantiate(enemies[enemyType - 1], spawnPoints[spawnPoint-1].position, spawnPoints[spawnPoint - 1].rotation);
        listOfAllEnemies.Add(spawnedEnemy);
    }
}
