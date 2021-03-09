using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

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

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;

    public GameObject level1Blockade;
    public GameObject level2Blockade;
    public GameObject level3Blockade;
    public GameObject level4Blockade;
    public GameObject level5Blockade;
    public GameObject level6Blockade;
    public GameObject level7Blockade;
    public GameObject level8Blockade;

    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;
    public GameObject spawnPoint5;
    public GameObject spawnPoint6;
    public GameObject spawnPoint7;
    public GameObject spawnPoint8;
    public GameObject spawnPoint9;
    public GameObject spawnPoint10;
    public GameObject spawnPoint11;
    public GameObject spawnPoint12;
    public GameObject spawnPoint13;
    public GameObject spawnPoint14;
    public GameObject spawnPoint15;
    public GameObject spawnPoint16;
    public GameObject spawnPoint17;
    public GameObject spawnPoint18;
    public GameObject spawnPoint19;
    public GameObject spawnPoint20;
    public GameObject spawnPoint21;
    public GameObject spawnPoint22;
    public GameObject spawnPoint23;
    public GameObject spawnPoint24;
    public GameObject spawnPoint25;
    public GameObject spawnPoint26;
    public GameObject spawnPoint27;
    public GameObject spawnPoint28;
    public GameObject spawnPoint29;
    public GameObject spawnPoint30;
    public GameObject spawnPoint31;
    public GameObject spawnPoint32;
    public GameObject spawnPoint33;
    public GameObject spawnPoint34;
    public GameObject spawnPoint35;
    public GameObject spawnPoint36;
    public GameObject spawnPoint37;
    public GameObject spawnPoint38;
    public GameObject spawnPoint39;
    public GameObject spawnPoint40;
    public GameObject spawnPoint41;
    public GameObject spawnPoint42;
    public GameObject spawnPoint43;
    public GameObject spawnPoint44;
    public GameObject spawnPoint45;
    public GameObject spawnPoint46;

    public int level = 1;

    public PlayerAI playerai;

    public bool isTransitionDone = false;
    public bool isCheckingForTransition = false;

    public Material altsky;
    public Material normsky;

    public GameObject resetHighScoresButton;

    public GameObject newGameButtonText;

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
            GetComponent<SystemsProcess>().removeAdsButton.SetActive(true);
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
        }
        else
        {
            PlayerPrefs.SetInt("Is Shadows On", 1);
            currentLight.GetComponent<Light>().color = noonLight.GetComponent<Light>().color;
            RenderSettings.fogColor = currentLight.GetComponent<Light>().color;
            currentLight.GetComponent<Transform>().rotation = noonLight.GetComponent<Transform>().rotation;
            nightmareUnderlight.SetActive(false);
            RenderSettings.skybox = normsky;
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
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().isAllInputEnabled(false);
            //Debug.Log("please kill me");
            inputRestored = false;
        }
        else if (constantlyDenyInput == false && inputRestored == false) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().isAllInputEnabled(true);
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
        GameObject senemySpawned;
        switch (sspawnType) {
            case 1:
                senemySpawned = spawnPoint1;
                break;
            case 2:
                senemySpawned = spawnPoint2;
                break;
            case 3:
                senemySpawned = spawnPoint3;
                break;
            case 4:
                senemySpawned = spawnPoint4;
                break;
            case 5:
                senemySpawned = spawnPoint5;
                break;
            case 6:
                senemySpawned = spawnPoint6;
                break;
            case 7:
                senemySpawned = spawnPoint7;
                break;
            case 8:
                senemySpawned = spawnPoint8;
                break;
            case 9:
                senemySpawned = spawnPoint9;
                break;
            case 10:
                senemySpawned = spawnPoint10;
                break;
            case 11:
                senemySpawned = spawnPoint11;
                break;
            case 12:
                senemySpawned = spawnPoint12;
                break;
            case 13:
                senemySpawned = spawnPoint13;
                break;
            case 14:
                senemySpawned = spawnPoint14;
                break;
            case 15:
                senemySpawned = spawnPoint15;
                break;
            case 16:
                senemySpawned = spawnPoint16;
                break;
            case 17:
                senemySpawned = spawnPoint17;
                break;
            case 18:
                senemySpawned = spawnPoint18;
                break;
            case 19:
                senemySpawned = spawnPoint19;
                break;
            case 20:
                senemySpawned = spawnPoint20;
                break;
            case 21:
                senemySpawned = spawnPoint21;
                break;
            case 22:
                senemySpawned = spawnPoint22;
                break;
            case 23:
                senemySpawned = spawnPoint23;
                break;
            case 24:
                senemySpawned = spawnPoint24;
                break;
            case 25:
                senemySpawned = spawnPoint25;
                break;
            case 26:
                senemySpawned = spawnPoint26;
                break;
            case 27:
                senemySpawned = spawnPoint27;
                break;
            case 28:
                senemySpawned = spawnPoint28;
                break;
            case 29:
                senemySpawned = spawnPoint29;
                break;
            case 30:
                senemySpawned = spawnPoint30;
                break;
            case 31:
                senemySpawned = spawnPoint31;
                break;
            case 32:
                senemySpawned = spawnPoint32;
                break;
            case 33:
                senemySpawned = spawnPoint33;
                break;
            case 34:
                senemySpawned = spawnPoint34;
                break;
            case 35:
                senemySpawned = spawnPoint35;
                break;
            case 36:
                senemySpawned = spawnPoint36;
                break;
            case 37:
                senemySpawned = spawnPoint37;
                break;
            case 38:
                senemySpawned = spawnPoint38;
                break;
            case 39:
                senemySpawned = spawnPoint39;
                break;
            case 40:
                senemySpawned = spawnPoint40;
                break;
            case 41:
                senemySpawned = spawnPoint41;
                break;
            case 42:
                senemySpawned = spawnPoint42;
                break;
            case 43:
                senemySpawned = spawnPoint43;
                break;
            case 44:
                senemySpawned = spawnPoint44;
                break;
            case 45:
                senemySpawned = spawnPoint45;
                break;
            case 46:
                senemySpawned = spawnPoint46;
                break;
            default:
                senemySpawned = spawnPoint1;
                break;
        }

        StartCoroutine(spawnEnemy(spawnType, senemySpawned, 0));
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
                            nightmareUnderlight.SetActive(true);
                        else
                            nightmareUnderlight.SetActive(false);
                    }
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
        GameObject.Find("Player").GetComponent<PlaySoundEffect>().PlaySound(15);
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
    }

    void RemoveLevelPrompts ()
    {
        blackBackground2.SetActive(false);
        blockadeText.SetActive(false);
    }

    void SpawnLevel1 ()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = false;
        if (level == 1) {
            StartCoroutine(spawnEnemy(1, spawnPoint1, 0));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 1));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 1.25f, 0.25f);
        }
        else if (level == 2) {
            StartCoroutine(spawnEnemy(1, spawnPoint1, 0));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 1));
            StartCoroutine(spawnEnemy(1, spawnPoint1, 2));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 3));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 3.25f, 0.25f);
        }
        else if (level == 3) {
            StartCoroutine(spawnEnemy(1, spawnPoint3, 0));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 1));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 2));
            StartCoroutine(spawnEnemy(1, spawnPoint1, 3));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 4));
            StartCoroutine(spawnEnemy(1, spawnPoint1, 5));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 6));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 7));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 7.25f, 0.25f);
        }
        else if (level == 4) {
            StartCoroutine(spawnEnemy(2, spawnPoint3, 0));
            StartCoroutine(spawnEnemy(2, spawnPoint1, 1));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 2));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 3));
            StartCoroutine(spawnEnemy(1, spawnPoint1, 4));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 5));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 6));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 7));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 7.25f, 0.25f);
        }
        else if (level == 5) {
            StartCoroutine(spawnEnemy(1, spawnPoint3, 0));
            StartCoroutine(spawnEnemy(1, spawnPoint1, 1));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 2));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 3));
            StartCoroutine(spawnEnemy(1, spawnPoint1, 4));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 5));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 6));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 7));
            StartCoroutine(spawnEnemy(2, spawnPoint1, 8));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 9));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 10));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 11));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 11.25f, 0.25f);
        }
        else if (level == 6) {
            StartCoroutine(spawnEnemy(1, spawnPoint3, 0));
            StartCoroutine(spawnEnemy(1, spawnPoint1, 1));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 2));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 3));
            StartCoroutine(spawnEnemy(2, spawnPoint1, 4));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 5));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 6));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 7));
            StartCoroutine(spawnEnemy(1, spawnPoint1, 8));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 9));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 10));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 12));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 13));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 14));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 15));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 18));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 19));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 20));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 21));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 22));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 23));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 23.25f, 0.25f);
        }
        else if (level == 7) {
            StartCoroutine(spawnEnemy(1, spawnPoint5, 0));
            StartCoroutine(spawnEnemy(1, spawnPoint6, 1));
            StartCoroutine(spawnEnemy(1, spawnPoint7, 2));
            StartCoroutine(spawnEnemy(1, spawnPoint8, 3));
            StartCoroutine(spawnEnemy(1, spawnPoint9, 4));
            StartCoroutine(spawnEnemy(1, spawnPoint10, 5));
            StartCoroutine(spawnEnemy(1, spawnPoint11, 6));
            StartCoroutine(spawnEnemy(1, spawnPoint12, 7));
            StartCoroutine(spawnEnemy(3, spawnPoint4, 8));
            StartCoroutine(spawnEnemy(3, spawnPoint3, 9));
            StartCoroutine(spawnEnemy(3, spawnPoint1, 10));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 11));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 11.25f, 0.25f);
        }
        else if (level == 8) {
            StartCoroutine(spawnEnemy(3, spawnPoint5, 0));
            StartCoroutine(spawnEnemy(1, spawnPoint6, 1));
            StartCoroutine(spawnEnemy(3, spawnPoint7, 2));
            StartCoroutine(spawnEnemy(1, spawnPoint8, 3));
            StartCoroutine(spawnEnemy(3, spawnPoint9, 4));
            StartCoroutine(spawnEnemy(1, spawnPoint10, 5));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 6));
            StartCoroutine(spawnEnemy(1, spawnPoint12, 7));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 8));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 9));
            StartCoroutine(spawnEnemy(2, spawnPoint1, 10));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 11));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 11.25f, 0.25f);
        }
        else if (level == 9) {
            StartCoroutine(spawnEnemy(3, spawnPoint12, 0));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 1));
            StartCoroutine(spawnEnemy(3, spawnPoint4, 2));
            StartCoroutine(spawnEnemy(1, spawnPoint9, 3));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 4));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 5));
            StartCoroutine(spawnEnemy(1, spawnPoint11, 6));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 7));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 8));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 9));
            StartCoroutine(spawnEnemy(3, spawnPoint5, 10));
            StartCoroutine(spawnEnemy(1, spawnPoint6, 11));
            StartCoroutine(spawnEnemy(3, spawnPoint7, 12));
            StartCoroutine(spawnEnemy(1, spawnPoint8, 13));
            StartCoroutine(spawnEnemy(2, spawnPoint9, 14));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 15));
            StartCoroutine(spawnEnemy(1, spawnPoint11, 16));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 17));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 18));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 19));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 19.25f, 0.25f);
        }
        else if (level == 10) {
            StartCoroutine(spawnEnemy(2, spawnPoint12, 0));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 1));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 2));
            StartCoroutine(spawnEnemy(2, spawnPoint9, 3));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 4));
            StartCoroutine(spawnEnemy(2, spawnPoint10, 5));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 6));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 7));
            StartCoroutine(spawnEnemy(3, spawnPoint4, 8));
            StartCoroutine(spawnEnemy(3, spawnPoint3, 9));
            StartCoroutine(spawnEnemy(3, spawnPoint5, 10));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 11));
            StartCoroutine(spawnEnemy(3, spawnPoint7, 12));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 12.25f, 0.25f);
        }
        else if (level == 11) {
            StartCoroutine(spawnEnemy(4, spawnPoint12, 0));
            StartCoroutine(spawnEnemy(3, spawnPoint3, 1));
            StartCoroutine(spawnEnemy(3, spawnPoint4, 2));
            StartCoroutine(spawnEnemy(3, spawnPoint9, 3));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 4));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 5));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 6));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 7));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 8));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 9));
            StartCoroutine(spawnEnemy(3, spawnPoint5, 10));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 11));
            StartCoroutine(spawnEnemy(4, spawnPoint7, 12));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 13));
            StartCoroutine(spawnEnemy(3, spawnPoint9, 14));
            StartCoroutine(spawnEnemy(2, spawnPoint10, 15));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 16));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 17));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 18));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 19));
            StartCoroutine(spawnEnemy(4, spawnPoint8, 20));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 20.25f, 0.25f);
        }
        else if (level == 12) {
            StartCoroutine(spawnEnemy(4, spawnPoint12, 0));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 1));
            StartCoroutine(spawnEnemy(3, spawnPoint4, 2));
            StartCoroutine(spawnEnemy(3, spawnPoint9, 3));
            StartCoroutine(spawnEnemy(2, spawnPoint12, 4));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 13));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 6));
            StartCoroutine(spawnEnemy(3, spawnPoint13, 7));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 13));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 9));
            StartCoroutine(spawnEnemy(3, spawnPoint14, 10));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 11));
            StartCoroutine(spawnEnemy(4, spawnPoint7, 14));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 13));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 15));
            StartCoroutine(spawnEnemy(2, spawnPoint10, 15));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 16));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 17));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 18));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 16));
            StartCoroutine(spawnEnemy(4, spawnPoint17, 20));
            StartCoroutine(spawnEnemy(4, spawnPoint9, 21));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 21.25f, 0.25f);
        }
        else if (level == 13) {
            StartCoroutine(spawnEnemy(4, spawnPoint12, 0));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 1));
            StartCoroutine(spawnEnemy(3, spawnPoint4, 2));
            StartCoroutine(spawnEnemy(2, spawnPoint9, 3));
            StartCoroutine(spawnEnemy(2, spawnPoint12, 4));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 5));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 6));
            StartCoroutine(spawnEnemy(4, spawnPoint13, 7));
            StartCoroutine(spawnEnemy(3, spawnPoint14, 10));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 11));
            StartCoroutine(spawnEnemy(4, spawnPoint7, 12));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 13));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 14));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 15));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 16));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 17));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 18));
            StartCoroutine(spawnEnemy(4, spawnPoint3, 19));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 22));
            StartCoroutine(spawnEnemy(2, spawnPoint10, 23));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 24));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 25));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 26));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 26.25f, 0.25f);
        }
        else if (level == 14) {
            StartCoroutine(spawnEnemy(4, spawnPoint17, 0));
            StartCoroutine(spawnEnemy(4, spawnPoint16, 1));
            StartCoroutine(spawnEnemy(4, spawnPoint15, 2));
            StartCoroutine(spawnEnemy(4, spawnPoint14, 3));
            StartCoroutine(spawnEnemy(4, spawnPoint13, 4));
            StartCoroutine(spawnEnemy(4, spawnPoint12, 5));
            StartCoroutine(spawnEnemy(4, spawnPoint11, 6));
            StartCoroutine(spawnEnemy(4, spawnPoint10, 7));
            StartCoroutine(spawnEnemy(4, spawnPoint9, 8));
            StartCoroutine(spawnEnemy(4, spawnPoint8, 9));
            StartCoroutine(spawnEnemy(4, spawnPoint7, 10));
            StartCoroutine(spawnEnemy(4, spawnPoint6, 11));
            StartCoroutine(spawnEnemy(4, spawnPoint5, 12));
            StartCoroutine(spawnEnemy(4, spawnPoint4, 13));
            StartCoroutine(spawnEnemy(4, spawnPoint3, 14));
            StartCoroutine(spawnEnemy(4, spawnPoint2, 15));
            StartCoroutine(spawnEnemy(4, spawnPoint1, 16));
            StartCoroutine(spawnEnemy(4, spawnPoint2, 17));
            StartCoroutine(spawnEnemy(4, spawnPoint3, 18));
            StartCoroutine(spawnEnemy(4, spawnPoint4, 19));
            StartCoroutine(spawnEnemy(4, spawnPoint5, 20));
            StartCoroutine(spawnEnemy(4, spawnPoint6, 21));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 21.25f, 0.25f);
        }
        else if (level == 15) {
            StartCoroutine(spawnEnemy(4, spawnPoint17, 0));
            StartCoroutine(spawnEnemy(3, spawnPoint16, 1));
            StartCoroutine(spawnEnemy(3, spawnPoint15, 2));
            StartCoroutine(spawnEnemy(2, spawnPoint14, 3));
            StartCoroutine(spawnEnemy(1, spawnPoint13, 4));
            StartCoroutine(spawnEnemy(4, spawnPoint12, 5));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 6));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 7));
            StartCoroutine(spawnEnemy(2, spawnPoint9, 8));
            StartCoroutine(spawnEnemy(1, spawnPoint8, 9));
            StartCoroutine(spawnEnemy(4, spawnPoint7, 10));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 11));
            StartCoroutine(spawnEnemy(3, spawnPoint5, 12));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 13));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 14));
            StartCoroutine(spawnEnemy(4, spawnPoint2, 15));
            StartCoroutine(spawnEnemy(3, spawnPoint1, 16));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 17));
            StartCoroutine(spawnEnemy(2, spawnPoint3, 18));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 19));
            StartCoroutine(spawnEnemy(4, spawnPoint5, 20));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 21));
            StartCoroutine(spawnEnemy(3, spawnPoint7, 22));
            StartCoroutine(spawnEnemy(2, spawnPoint8, 23));
            StartCoroutine(spawnEnemy(1, spawnPoint9, 24));
            StartCoroutine(spawnEnemy(4, spawnPoint10, 25));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 26));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 27));
            StartCoroutine(spawnEnemy(2, spawnPoint13, 28));
            StartCoroutine(spawnEnemy(1, spawnPoint14, 29));
            StartCoroutine(spawnEnemy(4, spawnPoint15, 30));
            StartCoroutine(spawnEnemy(3, spawnPoint16, 31));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 32));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 33));
            StartCoroutine(spawnEnemy(1, spawnPoint15, 34));
            StartCoroutine(spawnEnemy(4, spawnPoint14, 35));
            StartCoroutine(spawnEnemy(3, spawnPoint13, 36));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 37));
            StartCoroutine(spawnEnemy(2, spawnPoint11, 38));
            StartCoroutine(spawnEnemy(1, spawnPoint10, 39));
            StartCoroutine(spawnEnemy(4, spawnPoint9, 40));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 41));
            StartCoroutine(spawnEnemy(3, spawnPoint7, 42));
            StartCoroutine(spawnEnemy(2, spawnPoint6, 43));
            StartCoroutine(spawnEnemy(1, spawnPoint5, 44));
            StartCoroutine(spawnEnemy(4, spawnPoint4, 45));
            StartCoroutine(spawnEnemy(3, spawnPoint3, 46));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 47));
            StartCoroutine(spawnEnemy(2, spawnPoint1, 48));
            StartCoroutine(spawnEnemy(1, spawnPoint2, 49));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 49.25f, 0.25f);
        }
        else if (level == 16) {
            StartCoroutine(spawnEnemy(4, spawnPoint1, 0));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 1));
            StartCoroutine(spawnEnemy(6, spawnPoint3, 2));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 3));
            StartCoroutine(spawnEnemy(1, spawnPoint5, 4));
            StartCoroutine(spawnEnemy(4, spawnPoint6, 5));
            StartCoroutine(spawnEnemy(6, spawnPoint7, 6));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 7));
            StartCoroutine(spawnEnemy(2, spawnPoint9, 8));
            StartCoroutine(spawnEnemy(1, spawnPoint10, 9));
            StartCoroutine(spawnEnemy(4, spawnPoint11, 10));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 11));
            StartCoroutine(spawnEnemy(6, spawnPoint13, 12));
            StartCoroutine(spawnEnemy(2, spawnPoint14, 13));
            StartCoroutine(spawnEnemy(1, spawnPoint15, 14));
            StartCoroutine(spawnEnemy(4, spawnPoint16, 15));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 16));
            StartCoroutine(spawnEnemy(6, spawnPoint18, 17));
            StartCoroutine(spawnEnemy(2, spawnPoint19, 18));
            StartCoroutine(spawnEnemy(1, spawnPoint20, 19));
            StartCoroutine(spawnEnemy(4, spawnPoint21, 20));
            StartCoroutine(spawnEnemy(3, spawnPoint22, 21));
            StartCoroutine(spawnEnemy(3, spawnPoint21, 22));
            StartCoroutine(spawnEnemy(2, spawnPoint20, 23));
            StartCoroutine(spawnEnemy(1, spawnPoint19, 24));
            StartCoroutine(spawnEnemy(4, spawnPoint18, 25));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 26));
            StartCoroutine(spawnEnemy(3, spawnPoint16, 27));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 28));
            StartCoroutine(spawnEnemy(1, spawnPoint14, 29));
            StartCoroutine(spawnEnemy(4, spawnPoint13, 30));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 31));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 32));
            StartCoroutine(spawnEnemy(2, spawnPoint10, 33));
            StartCoroutine(spawnEnemy(1, spawnPoint9, 34));
            StartCoroutine(spawnEnemy(4, spawnPoint8, 35));
            StartCoroutine(spawnEnemy(3, spawnPoint7, 36));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 37));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 38));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 39));
            StartCoroutine(spawnEnemy(4, spawnPoint3, 40));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 41));
            StartCoroutine(spawnEnemy(3, spawnPoint1, 42));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 43));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 44));
            StartCoroutine(spawnEnemy(4, spawnPoint4, 45));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 46));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 47));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 48));
            StartCoroutine(spawnEnemy(1, spawnPoint8, 49));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 49.25f, 0.25f);
        }
        else if (level == 17) {
            StartCoroutine(spawnEnemy(4, spawnPoint1, 0));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 1));
            StartCoroutine(spawnEnemy(6, spawnPoint3, 2));
            StartCoroutine(spawnEnemy(4, spawnPoint4, 3));
            StartCoroutine(spawnEnemy(3, spawnPoint5, 4));
            StartCoroutine(spawnEnemy(4, spawnPoint6, 5));
            StartCoroutine(spawnEnemy(6, spawnPoint7, 6));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 7));
            StartCoroutine(spawnEnemy(4, spawnPoint9, 8));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 9));
            StartCoroutine(spawnEnemy(4, spawnPoint11, 10));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 11));
            StartCoroutine(spawnEnemy(6, spawnPoint13, 12));
            StartCoroutine(spawnEnemy(4, spawnPoint14, 13));
            StartCoroutine(spawnEnemy(3, spawnPoint15, 14));
            StartCoroutine(spawnEnemy(4, spawnPoint16, 15));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 16));
            StartCoroutine(spawnEnemy(6, spawnPoint18, 17));
            StartCoroutine(spawnEnemy(4, spawnPoint19, 18));
            StartCoroutine(spawnEnemy(3, spawnPoint20, 19));
            StartCoroutine(spawnEnemy(4, spawnPoint21, 20));
            StartCoroutine(spawnEnemy(3, spawnPoint22, 21));
            StartCoroutine(spawnEnemy(3, spawnPoint21, 22));
            StartCoroutine(spawnEnemy(4, spawnPoint20, 23));
            StartCoroutine(spawnEnemy(3, spawnPoint19, 24));
            StartCoroutine(spawnEnemy(4, spawnPoint18, 25));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 26));
            StartCoroutine(spawnEnemy(3, spawnPoint16, 27));
            StartCoroutine(spawnEnemy(4, spawnPoint15, 28));
            StartCoroutine(spawnEnemy(3, spawnPoint14, 29));
            StartCoroutine(spawnEnemy(4, spawnPoint13, 30));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 31));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 32));
            StartCoroutine(spawnEnemy(2, spawnPoint10, 33));
            StartCoroutine(spawnEnemy(3, spawnPoint9, 34));
            StartCoroutine(spawnEnemy(4, spawnPoint8, 35));
            StartCoroutine(spawnEnemy(3, spawnPoint7, 36));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 37));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 38));
            StartCoroutine(spawnEnemy(3, spawnPoint4, 39));
            StartCoroutine(spawnEnemy(4, spawnPoint3, 40));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 41));
            StartCoroutine(spawnEnemy(3, spawnPoint1, 42));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 43));
            StartCoroutine(spawnEnemy(3, spawnPoint3, 44));
            StartCoroutine(spawnEnemy(4, spawnPoint4, 45));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 46));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 47));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 48));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 49));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 49.25f, 0.25f);
        }
        else if (level == 18) {
            StartCoroutine(spawnEnemy(4, spawnPoint1, 0));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 1));
            StartCoroutine(spawnEnemy(6, spawnPoint3, 2));
            StartCoroutine(spawnEnemy(4, spawnPoint4, 3));
            StartCoroutine(spawnEnemy(3, spawnPoint5, 4));
            StartCoroutine(spawnEnemy(4, spawnPoint6, 5));
            StartCoroutine(spawnEnemy(6, spawnPoint7, 6));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 7));
            StartCoroutine(spawnEnemy(4, spawnPoint9, 8));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 9));
            StartCoroutine(spawnEnemy(4, spawnPoint11, 10));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 11));
            StartCoroutine(spawnEnemy(6, spawnPoint13, 12));
            StartCoroutine(spawnEnemy(4, spawnPoint14, 13));
            StartCoroutine(spawnEnemy(3, spawnPoint15, 14));
            StartCoroutine(spawnEnemy(4, spawnPoint16, 15));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 16));
            StartCoroutine(spawnEnemy(6, spawnPoint18, 17));
            StartCoroutine(spawnEnemy(4, spawnPoint19, 18));
            StartCoroutine(spawnEnemy(3, spawnPoint20, 19));
            StartCoroutine(spawnEnemy(4, spawnPoint21, 20));
            StartCoroutine(spawnEnemy(3, spawnPoint22, 21));
            StartCoroutine(spawnEnemy(3, spawnPoint21, 22));
            StartCoroutine(spawnEnemy(4, spawnPoint20, 23));
            StartCoroutine(spawnEnemy(3, spawnPoint19, 24));
            StartCoroutine(spawnEnemy(4, spawnPoint18, 25));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 26));
            StartCoroutine(spawnEnemy(3, spawnPoint16, 27));
            StartCoroutine(spawnEnemy(4, spawnPoint15, 28));
            StartCoroutine(spawnEnemy(3, spawnPoint14, 29));
            StartCoroutine(spawnEnemy(4, spawnPoint13, 30));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 31));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 32));
            StartCoroutine(spawnEnemy(2, spawnPoint10, 33));
            StartCoroutine(spawnEnemy(3, spawnPoint9, 34));
            StartCoroutine(spawnEnemy(4, spawnPoint8, 35));
            StartCoroutine(spawnEnemy(3, spawnPoint7, 36));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 37));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 38));
            StartCoroutine(spawnEnemy(3, spawnPoint4, 39));
            StartCoroutine(spawnEnemy(4, spawnPoint3, 40));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 41));
            StartCoroutine(spawnEnemy(3, spawnPoint1, 42));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 43));
            StartCoroutine(spawnEnemy(3, spawnPoint3, 44));
            StartCoroutine(spawnEnemy(4, spawnPoint4, 45));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 46));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 47));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 48));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 49));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 49.25f, 0.25f);
        }
        else if (level == 19) {
            StartCoroutine(spawnEnemy(6, spawnPoint1, 0));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 1));
            StartCoroutine(spawnEnemy(6, spawnPoint3, 2));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 3));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 4));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 5));
            StartCoroutine(spawnEnemy(6, spawnPoint7, 6));
            StartCoroutine(spawnEnemy(2, spawnPoint8, 7));
            StartCoroutine(spawnEnemy(6, spawnPoint9, 8));
            StartCoroutine(spawnEnemy(2, spawnPoint10, 9));
            StartCoroutine(spawnEnemy(6, spawnPoint11, 10));
            StartCoroutine(spawnEnemy(2, spawnPoint12, 11));
            StartCoroutine(spawnEnemy(6, spawnPoint13, 12));
            StartCoroutine(spawnEnemy(2, spawnPoint14, 13));
            StartCoroutine(spawnEnemy(6, spawnPoint15, 14));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 15));
            StartCoroutine(spawnEnemy(2, spawnPoint17, 16));
            StartCoroutine(spawnEnemy(6, spawnPoint18, 17));
            StartCoroutine(spawnEnemy(2, spawnPoint19, 18));
            StartCoroutine(spawnEnemy(6, spawnPoint20, 19));
            StartCoroutine(spawnEnemy(2, spawnPoint21, 20));
            StartCoroutine(spawnEnemy(6, spawnPoint22, 21));
            StartCoroutine(spawnEnemy(2, spawnPoint21, 22));
            StartCoroutine(spawnEnemy(2, spawnPoint20, 23));
            StartCoroutine(spawnEnemy(6, spawnPoint19, 24));
            StartCoroutine(spawnEnemy(2, spawnPoint18, 25));
            StartCoroutine(spawnEnemy(6, spawnPoint17, 26));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 27));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 28));
            StartCoroutine(spawnEnemy(6, spawnPoint14, 29));
            StartCoroutine(spawnEnemy(2, spawnPoint13, 30));
            StartCoroutine(spawnEnemy(2, spawnPoint12, 31));
            StartCoroutine(spawnEnemy(2, spawnPoint11, 32));
            StartCoroutine(spawnEnemy(6, spawnPoint10, 33));
            StartCoroutine(spawnEnemy(2, spawnPoint9, 34));
            StartCoroutine(spawnEnemy(6, spawnPoint8, 35));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 36));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 37));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 38));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 39));
            StartCoroutine(spawnEnemy(6, spawnPoint3, 40));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 41));
            StartCoroutine(spawnEnemy(6, spawnPoint1, 42));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 43));
            StartCoroutine(spawnEnemy(6, spawnPoint3, 44));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 45));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 46));
            StartCoroutine(spawnEnemy(2, spawnPoint6, 47));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 48));
            StartCoroutine(spawnEnemy(6, spawnPoint8, 49));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 49.25f, 0.25f);
        }
        else if (level == 20) {
            StartCoroutine(spawnEnemy(7, spawnPoint22, 0));
            StartCoroutine(spawnEnemy(2, spawnPoint21, 1));
            StartCoroutine(spawnEnemy(4, spawnPoint20, 2));
            StartCoroutine(spawnEnemy(7, spawnPoint19, 3));
            StartCoroutine(spawnEnemy(4, spawnPoint18, 4));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 5));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 6));
            StartCoroutine(spawnEnemy(7, spawnPoint15, 7));
            StartCoroutine(spawnEnemy(6, spawnPoint14, 8));
            StartCoroutine(spawnEnemy(3, spawnPoint13, 9));
            StartCoroutine(spawnEnemy(6, spawnPoint12, 10));
            StartCoroutine(spawnEnemy(7, spawnPoint11, 11));
            StartCoroutine(spawnEnemy(6, spawnPoint10, 12));
            StartCoroutine(spawnEnemy(4, spawnPoint9, 13));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 14));
            StartCoroutine(spawnEnemy(7, spawnPoint7, 15));
            StartCoroutine(spawnEnemy(2, spawnPoint6, 16));
            StartCoroutine(spawnEnemy(3, spawnPoint5, 17));
            StartCoroutine(spawnEnemy(6, spawnPoint4, 18));
            StartCoroutine(spawnEnemy(7, spawnPoint3, 19));
            StartCoroutine(spawnEnemy(2, spawnPoint2, 20));
            StartCoroutine(spawnEnemy(4, spawnPoint1, 21));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 21.25f, 0.25f);
        }
        else if (level == 21) {
            StartCoroutine(spawnEnemy(7, spawnPoint22, 0));
            StartCoroutine(spawnEnemy(7, spawnPoint21, 1));
            StartCoroutine(spawnEnemy(4, spawnPoint20, 2));
            StartCoroutine(spawnEnemy(7, spawnPoint19, 3));
            StartCoroutine(spawnEnemy(4, spawnPoint18, 4));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 5));
            StartCoroutine(spawnEnemy(7, spawnPoint16, 6));
            StartCoroutine(spawnEnemy(7, spawnPoint15, 7));
            StartCoroutine(spawnEnemy(6, spawnPoint14, 8));
            StartCoroutine(spawnEnemy(3, spawnPoint13, 9));
            StartCoroutine(spawnEnemy(6, spawnPoint12, 10));
            StartCoroutine(spawnEnemy(7, spawnPoint11, 11));
            StartCoroutine(spawnEnemy(6, spawnPoint10, 12));
            StartCoroutine(spawnEnemy(4, spawnPoint9, 13));
            StartCoroutine(spawnEnemy(3, spawnPoint8, 14));
            StartCoroutine(spawnEnemy(7, spawnPoint7, 15));
            StartCoroutine(spawnEnemy(7, spawnPoint6, 16));
            StartCoroutine(spawnEnemy(3, spawnPoint5, 17));
            StartCoroutine(spawnEnemy(6, spawnPoint4, 18));
            StartCoroutine(spawnEnemy(7, spawnPoint3, 19));
            StartCoroutine(spawnEnemy(7, spawnPoint2, 20));
            StartCoroutine(spawnEnemy(4, spawnPoint1, 21));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 21.25f, 0.25f);
        }
        else if (level == 22) {
            StartCoroutine(spawnEnemy(1, spawnPoint20, 0f));
            StartCoroutine(spawnEnemy(1, spawnPoint21, .5f));
            StartCoroutine(spawnEnemy(1, spawnPoint22, 1f));
            StartCoroutine(spawnEnemy(7, spawnPoint20, 1.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint21, 2f));
            StartCoroutine(spawnEnemy(1, spawnPoint22, 2.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint20, 3f));
            StartCoroutine(spawnEnemy(1, spawnPoint21, 3.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint22, 4f));
            StartCoroutine(spawnEnemy(7, spawnPoint20, 4.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 7.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint6, 8f));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 8.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 9f));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 9.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 10f));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 10.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint6, 11f));
            StartCoroutine(spawnEnemy(6, spawnPoint7, 11.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 12f));
            StartCoroutine(spawnEnemy(1, spawnPoint26, 14f));
            StartCoroutine(spawnEnemy(1, spawnPoint27, 14.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint28, 15f));
            StartCoroutine(spawnEnemy(7, spawnPoint29, 15.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint30, 16f));
            StartCoroutine(spawnEnemy(1, spawnPoint31, 16.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint26, 17f));
            StartCoroutine(spawnEnemy(1, spawnPoint27, 17.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint28, 18f));
            StartCoroutine(spawnEnemy(7, spawnPoint29, 18.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 20.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 21f));
            StartCoroutine(spawnEnemy(2, spawnPoint17, 21.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint15, 22f));
            StartCoroutine(spawnEnemy(6, spawnPoint16, 22.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint17, 23f));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 23.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 24f));
            StartCoroutine(spawnEnemy(6, spawnPoint17, 24.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint15, 25f));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 27f));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 27.5f));
            StartCoroutine(spawnEnemy(4, spawnPoint12, 28f));
            StartCoroutine(spawnEnemy(4, spawnPoint10, 28.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 29f));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 29.5f));
            StartCoroutine(spawnEnemy(4, spawnPoint10, 30f));
            StartCoroutine(spawnEnemy(4, spawnPoint11, 30.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 31f));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 31.5f));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 31.75f, 0.25f);
        }
        else if (level == 23) {
            StartCoroutine(spawnEnemy(1, spawnPoint20, 0f));
            StartCoroutine(spawnEnemy(1, spawnPoint21, .5f));
            StartCoroutine(spawnEnemy(1, spawnPoint22, 1f));
            StartCoroutine(spawnEnemy(5, spawnPoint20, 1.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint21, 2f));
            StartCoroutine(spawnEnemy(1, spawnPoint22, 2.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint20, 3f));
            StartCoroutine(spawnEnemy(1, spawnPoint21, 3.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint22, 4f));
            StartCoroutine(spawnEnemy(7, spawnPoint20, 4.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 7.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint6, 8f));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 8.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 9f));
            StartCoroutine(spawnEnemy(5, spawnPoint6, 9.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 10f));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 10.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint6, 11f));
            StartCoroutine(spawnEnemy(6, spawnPoint7, 11.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 12f));
            StartCoroutine(spawnEnemy(1, spawnPoint26, 14f));
            StartCoroutine(spawnEnemy(1, spawnPoint27, 14.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint28, 15f));
            StartCoroutine(spawnEnemy(7, spawnPoint29, 15.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint30, 16f));
            StartCoroutine(spawnEnemy(1, spawnPoint31, 16.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint26, 17f));
            StartCoroutine(spawnEnemy(1, spawnPoint27, 17.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint28, 18f));
            StartCoroutine(spawnEnemy(7, spawnPoint29, 18.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 20.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 21f));
            StartCoroutine(spawnEnemy(2, spawnPoint17, 21.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint15, 22f));
            StartCoroutine(spawnEnemy(5, spawnPoint16, 22.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint17, 23f));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 23.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 24f));
            StartCoroutine(spawnEnemy(6, spawnPoint17, 24.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint15, 25f));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 27f));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 27.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint12, 28f));
            StartCoroutine(spawnEnemy(4, spawnPoint10, 28.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 29f));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 29.5f));
            StartCoroutine(spawnEnemy(4, spawnPoint10, 30f));
            StartCoroutine(spawnEnemy(4, spawnPoint11, 30.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 31f));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 31.5f));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 31.75f, 0.25f);
        }
        else if (level == 24) {
            StartCoroutine(spawnEnemy(3, spawnPoint20, 0f));
            StartCoroutine(spawnEnemy(3, spawnPoint21, .5f));
            StartCoroutine(spawnEnemy(3, spawnPoint22, 1f));
            StartCoroutine(spawnEnemy(5, spawnPoint20, 1.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint21, 2f));
            StartCoroutine(spawnEnemy(1, spawnPoint22, 2.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint20, 3f));
            StartCoroutine(spawnEnemy(1, spawnPoint21, 3.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint22, 4f));
            StartCoroutine(spawnEnemy(7, spawnPoint20, 4.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint5, 7.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint6, 8f));
            StartCoroutine(spawnEnemy(3, spawnPoint7, 8.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint5, 9f));
            StartCoroutine(spawnEnemy(5, spawnPoint6, 9.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 10f));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 10.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint6, 11f));
            StartCoroutine(spawnEnemy(5, spawnPoint7, 11.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 12f));
            StartCoroutine(spawnEnemy(3, spawnPoint26, 14f));
            StartCoroutine(spawnEnemy(3, spawnPoint27, 14.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint28, 15f));
            StartCoroutine(spawnEnemy(5, spawnPoint29, 15.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint30, 16f));
            StartCoroutine(spawnEnemy(1, spawnPoint31, 16.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint26, 17f));
            StartCoroutine(spawnEnemy(1, spawnPoint27, 17.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint28, 18f));
            StartCoroutine(spawnEnemy(7, spawnPoint29, 18.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint15, 20.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint16, 21f));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 21.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint15, 22f));
            StartCoroutine(spawnEnemy(5, spawnPoint16, 22.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint17, 23f));
            StartCoroutine(spawnEnemy(2, spawnPoint15, 23.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint16, 24f));
            StartCoroutine(spawnEnemy(5, spawnPoint17, 24.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint15, 25f));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 27f));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 27.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint12, 28f));
            StartCoroutine(spawnEnemy(5, spawnPoint10, 28.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint11, 29f));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 29.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint10, 30f));
            StartCoroutine(spawnEnemy(4, spawnPoint11, 30.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 31f));
            StartCoroutine(spawnEnemy(3, spawnPoint10, 31.5f));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 31.75f, 0.25f);
        }
        else if (level == 25) {
            StartCoroutine(spawnEnemy(7, spawnPoint6, 0f));
            StartCoroutine(spawnEnemy(7, spawnPoint19, .5f));
            StartCoroutine(spawnEnemy(7, spawnPoint7, 1f));
            StartCoroutine(spawnEnemy(7, spawnPoint20, 1.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint5, 2f));
            StartCoroutine(spawnEnemy(7, spawnPoint18, 2.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint8, 3f));
            StartCoroutine(spawnEnemy(7, spawnPoint21, 3.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint22, 4f));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 4.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint19, 5f));
            StartCoroutine(spawnEnemy(6, spawnPoint7, 5.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint20, 6f));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 6.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint18, 7f));
            StartCoroutine(spawnEnemy(6, spawnPoint8, 7.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint21, 8f));
            StartCoroutine(spawnEnemy(6, spawnPoint22, 8.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 9f));
            StartCoroutine(spawnEnemy(6, spawnPoint19, 9.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint7, 10f));
            StartCoroutine(spawnEnemy(6, spawnPoint20, 10.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 11f));
            StartCoroutine(spawnEnemy(6, spawnPoint18, 11.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint8, 12f));
            StartCoroutine(spawnEnemy(6, spawnPoint21, 12.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint22, 13f));
            StartCoroutine(spawnEnemy(5, spawnPoint13, 13.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint7, 14f));
            StartCoroutine(spawnEnemy(5, spawnPoint8, 14.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint19, 15f));
            StartCoroutine(spawnEnemy(5, spawnPoint22, 15.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint28, 16f));
            StartCoroutine(spawnEnemy(5, spawnPoint25, 16.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint14, 17f));
            StartCoroutine(spawnEnemy(5, spawnPoint11, 17.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint1, 18f));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 18.25f, 0.25f);
        }
        else if (level == 26) {
            StartCoroutine(spawnEnemy(7, spawnPoint1, 0f));
            StartCoroutine(spawnEnemy(6, spawnPoint2, .5f));
            StartCoroutine(spawnEnemy(6, spawnPoint3, 1f));
            StartCoroutine(spawnEnemy(5, spawnPoint4, 1.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint5, 2f));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 2.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint7, 3f));
            StartCoroutine(spawnEnemy(5, spawnPoint8, 3.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint9, 4f));
            StartCoroutine(spawnEnemy(6, spawnPoint10, 4.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint11, 5f));
            StartCoroutine(spawnEnemy(5, spawnPoint12, 5.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint13, 6f));
            StartCoroutine(spawnEnemy(6, spawnPoint14, 6.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint15, 7f));
            StartCoroutine(spawnEnemy(5, spawnPoint16, 7.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint17, 8f));
            StartCoroutine(spawnEnemy(6, spawnPoint18, 8.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint19, 9f));
            StartCoroutine(spawnEnemy(5, spawnPoint20, 9.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint21, 10f));
            StartCoroutine(spawnEnemy(6, spawnPoint22, 10.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint23, 11f));
            StartCoroutine(spawnEnemy(5, spawnPoint24, 11.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint25, 12f));
            StartCoroutine(spawnEnemy(6, spawnPoint26, 12.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint27, 13f));
            StartCoroutine(spawnEnemy(5, spawnPoint28, 13.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint29, 14f));
            StartCoroutine(spawnEnemy(6, spawnPoint30, 14.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint31, 15f));
            StartCoroutine(spawnEnemy(5, spawnPoint30, 15.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint29, 16f));
            StartCoroutine(spawnEnemy(6, spawnPoint28, 16.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint27, 17f));
            StartCoroutine(spawnEnemy(5, spawnPoint26, 17.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint25, 18f));
            StartCoroutine(spawnEnemy(6, spawnPoint24, 18.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint23, 19f));
            StartCoroutine(spawnEnemy(5, spawnPoint22, 19.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint21, 20f));
            StartCoroutine(spawnEnemy(6, spawnPoint20, 20.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint19, 21f));
            StartCoroutine(spawnEnemy(5, spawnPoint18, 21.5f));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 21.75f, 0.25f);
        }
        else if (level == 27) {
            StartCoroutine(spawnEnemy(7, spawnPoint1, 0f));
            StartCoroutine(spawnEnemy(1, spawnPoint2, .5f));
            StartCoroutine(spawnEnemy(1, spawnPoint3, 1f));
            StartCoroutine(spawnEnemy(1, spawnPoint4, 1.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint5, 2f));
            StartCoroutine(spawnEnemy(6, spawnPoint6, 2.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 3f));
            StartCoroutine(spawnEnemy(2, spawnPoint8, 3.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint9, 4f));
            StartCoroutine(spawnEnemy(2, spawnPoint10, 4.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint11, 5f));
            StartCoroutine(spawnEnemy(3, spawnPoint12, 5.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint13, 6f));
            StartCoroutine(spawnEnemy(4, spawnPoint14, 6.5f));
            StartCoroutine(spawnEnemy(4, spawnPoint15, 7f));
            StartCoroutine(spawnEnemy(4, spawnPoint16, 7.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint17, 8f));
            StartCoroutine(spawnEnemy(1, spawnPoint18, 8.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint19, 9f));
            StartCoroutine(spawnEnemy(1, spawnPoint20, 9.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint21, 10f));
            StartCoroutine(spawnEnemy(6, spawnPoint22, 10.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint23, 11f));
            StartCoroutine(spawnEnemy(2, spawnPoint24, 11.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint25, 12f));
            StartCoroutine(spawnEnemy(2, spawnPoint26, 12.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint27, 13f));
            StartCoroutine(spawnEnemy(3, spawnPoint28, 13.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint29, 14f));
            StartCoroutine(spawnEnemy(4, spawnPoint30, 14.5f));
            StartCoroutine(spawnEnemy(4, spawnPoint31, 15f));
            StartCoroutine(spawnEnemy(4, spawnPoint30, 15.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint29, 16f));
            StartCoroutine(spawnEnemy(1, spawnPoint28, 16.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint27, 17f));
            StartCoroutine(spawnEnemy(1, spawnPoint26, 17.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint25, 18f));
            StartCoroutine(spawnEnemy(6, spawnPoint24, 18.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint23, 19f));
            StartCoroutine(spawnEnemy(2, spawnPoint22, 19.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint21, 20f));
            StartCoroutine(spawnEnemy(2, spawnPoint20, 20.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint19, 21f));
            StartCoroutine(spawnEnemy(3, spawnPoint18, 21.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint17, 22f));
            StartCoroutine(spawnEnemy(4, spawnPoint16, 22.5f));
            StartCoroutine(spawnEnemy(4, spawnPoint15, 23f));
            StartCoroutine(spawnEnemy(4, spawnPoint14, 23.5f));
            StartCoroutine(spawnEnemy(7, spawnPoint13, 24f));
            StartCoroutine(spawnEnemy(1, spawnPoint12, 25.5f));
            StartCoroutine(spawnEnemy(1, spawnPoint11, 26f));
            StartCoroutine(spawnEnemy(1, spawnPoint10, 27.5f));
            StartCoroutine(spawnEnemy(6, spawnPoint9, 28f));
            StartCoroutine(spawnEnemy(6, spawnPoint8, 28.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint7, 29f));
            StartCoroutine(spawnEnemy(2, spawnPoint6, 29.5f));
            StartCoroutine(spawnEnemy(2, spawnPoint5, 30f));
            StartCoroutine(spawnEnemy(2, spawnPoint4, 30.5f));
            StartCoroutine(spawnEnemy(5, spawnPoint3, 31f));
            StartCoroutine(spawnEnemy(3, spawnPoint2, 31.5f));
            StartCoroutine(spawnEnemy(3, spawnPoint1, 32f));
            StartCoroutine(spawnEnemy(4, spawnPoint2, 32.5f));
            StartCoroutine(spawnEnemy(4, spawnPoint3, 33f));
            StartCoroutine(spawnEnemy(4, spawnPoint4, 33.5f));
            InvokeRepeating("CheckIfAllEnemiesAreDead", 33.75f, 0.25f);
        }
        else if (level == 28) {
            InfiniteSpawner();
        }
        else {
            InfiniteSpawner();
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
                GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(8);
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 6) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(10);
                transitionMode = 4;
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 11) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(13);
                transitionMode = 11;
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 15) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(15);
                transitionMode = 16;
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 19) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(17);
                transitionMode = 21;
                isCheckingForTransition = true;
                isTransitionDone = false;
                constantlyDenyInput = true;
            }
            else if (level == 27) {
                CancelInvoke("CheckIfAllEnemiesAreDead");
                GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(20);
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
                GameObject.FindWithTag("Player").GetComponent<PlaySoundEffect>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 2) {
                weapon2.SetActive(true);
                originalScale = weapon2Model.transform.localScale;
                objectToScale = weapon2Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<PlaySoundEffect>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 3) {
                weapon3.SetActive(true);
                originalScale = weapon3Model.transform.localScale;
                objectToScale = weapon3Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<PlaySoundEffect>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 4) {
                weapon4.SetActive(true);
                originalScale = weapon4Model.transform.localScale;
                objectToScale = weapon4Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<PlaySoundEffect>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 5) {
                weapon5.SetActive(true);
                originalScale = weapon5Model.transform.localScale;
                objectToScale = weapon5Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<PlaySoundEffect>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 6) {
                weapon6.SetActive(true);
                originalScale = weapon6Model.transform.localScale;
                objectToScale = weapon6Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<PlaySoundEffect>().PlaySound(13);
                dust.transform.parent = null;
                Destroy(dust, 4);
            }
            else if (weaponToFade == 7) {
                weapon7.SetActive(true);
                originalScale = weapon7Model.transform.localScale;
                objectToScale = weapon7Model;
                GameObject dust = Instantiate(spawnDust, objectToScale.transform.position, spawnDust.transform.rotation);
                GameObject.FindWithTag("Player").GetComponent<PlaySoundEffect>().PlaySound(13);
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().isControlOff = true;
        }
    }

    void EnableIsCheckingForTransition () {
        RemoveLevelPrompts();

        if (transitionMode == 2) {
            transitionMode = 3;
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(4);
            //Debug.Log("bop");
        }
        else if (transitionMode == 0) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(9);
            transitionMode = 1;
            //Debug.Log("bop2");
        }
        else if (transitionMode == 5) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(11);
            transitionMode = 6;
        }
        else if (transitionMode == 7) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(12);
            transitionMode = 8;
        }
        else if (transitionMode == 9) {
            transitionMode = 10;
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(4);
        }
        else if (transitionMode == 12) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(14);
            transitionMode = 13;
        }
        else if (transitionMode == 14) {
            transitionMode = 15;
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(4);
        }
        else if (transitionMode == 17) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(16);
            transitionMode = 18;
        }
        else if (transitionMode == 19) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(4);
            transitionMode = 20;
        }
        else if (transitionMode == 22) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(18);
            transitionMode = 23;
        }
        else if (transitionMode == 24) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(19);
            transitionMode = 25;
        }
        else if (transitionMode == 26) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(4);
            transitionMode = 27;
        }
        else if (transitionMode == 29) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(21);
            transitionMode = 30;
        }
        else if (transitionMode == 31) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(22);
            transitionMode = 32;
        }
        else if (transitionMode == 33) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(23);
            transitionMode = 34;
        }
        else if (transitionMode == 35) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(24);
            transitionMode = 36;
        }
        else if (transitionMode == 37) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(25);
            transitionMode = 38;
        }
        else if (transitionMode == 39) {
            GameObject.Find("Systems Process").GetComponent<SystemsProcess>().GoToSettings(4);
            transitionMode = 40;
        }
        else {
            Debug.Log("shit's broke");
        }
 
        isCheckingForTransition = true;
        isTransitionDone = false;
    }

    IEnumerator spawnEnemy(int type, GameObject spawnPoint, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (type == 1)
        {
            listOfAllEnemies.Add((GameObject)Instantiate(enemy1, spawnPoint.transform.position, spawnPoint.transform.rotation));
        }
        if (type == 2) {
            listOfAllEnemies.Add((GameObject)Instantiate(enemy2, spawnPoint.transform.position, spawnPoint.transform.rotation));
        }
        if (type == 3) {
            listOfAllEnemies.Add((GameObject)Instantiate(enemy3, spawnPoint.transform.position, spawnPoint.transform.rotation));
        }
        if (type == 4) {
            listOfAllEnemies.Add((GameObject)Instantiate(enemy4, spawnPoint.transform.position, spawnPoint.transform.rotation));
        }
        if (type == 5) {
            listOfAllEnemies.Add((GameObject)Instantiate(enemy5, spawnPoint.transform.position, spawnPoint.transform.rotation));
        }
        if (type == 6) {
            listOfAllEnemies.Add((GameObject)Instantiate(enemy6, spawnPoint.transform.position, spawnPoint.transform.rotation));
        }
        if (type == 7) {
            listOfAllEnemies.Add((GameObject)Instantiate(enemy7, spawnPoint.transform.position, spawnPoint.transform.rotation));
        }
    }
}
