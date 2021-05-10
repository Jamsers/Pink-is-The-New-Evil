using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public LayerMask myLayerMask;
    public GameObject[] highScoreName;
    public GameObject[] highScore;
    public GameObject leburhighsc;
    public GameObject newname;
    public GameObject leburhighscnum;
    public GameObject pinklight;
    public GameObject cylinderTargetArea;
    public GameObject boxTargetArea;
    public Transform shockwavespawn;
    public Transform shockwavespawn2;
    public GameObject shockwave;
    public GameObject pointNumbers;
    public float attackAnimLength;
    public GameObject particle1prefab;
    public GameObject particle2prefab;
    public Vector3 screenRotationCorrection;
    public int moveSpeed;
    public float attackRange;
    public float attackCooldown;
    public float shakeAmount;
    public float attackLength;
    public GameObject healthBloodOnScreen;
    public GameObject hudCanvas;
    public GameObject backgroundNotEnough;
    public GameObject notEnoughPrompt;
    public GameObject weaponModel2;
    public GameObject weaponModel3;
    public GameObject weaponModel4;
    public GameObject weaponModel5;
    public GameObject weaponModel6;
    public GameObject weaponModel7;
    public GameObject weaponModel8;
    public GameObject trailparticle;
    public GameObject newHighScore;
    public GameObject blueJoystickAim;
    public float playerLookAtRotateSpeed;
    public GameObject gameCamera;
    public GameObject redAim;
    public GameObject redCircle;
    public GameObject speedAttackIndicatorCircle;
    public GameObject speedAttackIndicatorIcon;
    public GameObject speedAttackIndicatorCircleGlow;
    public GameObject speedAttackIndicatorIconGlow;
    public GameObject jumpAttackIndicatorCircle;
    public GameObject jumpAttackIndicatorIcon;
    public GameObject jumpAttackIndicatorCircleGlow;
    public GameObject jumpAttackIndicatorIconGlow;
    public GameObject blackBackgroundJuan;
    public GameObject blackBackgroundDos;
    public GameObject redTarget;
    public GameObject redCancelIcon;
    public GameObject greenCircle;
    public GameObject orangeCircle;
    public GameObject powerUpIcon;
    public Material playerpinkTrim;
    public Material weapon8pinktrim;
    public GameObject poofWeap;
    public GameObject poofPLay;
    public GameObject backimage1;
    public GameObject price;
    public GameObject buybutton;
    public GameObject pinkCaner;
    public GameObject modeltoRepl;
    public Material pinkGuyMaterial;
    public GameObject ascencionScreen;
    public GameObject healthPerkIcon;
    public GameObject healthPerkBack;
    public GameObject speedPerkIcon;
    public GameObject speedPerkBack;
    public GameObject jumpPerkIcon;
    public GameObject jumpPerkBack;
    public float mouseSpecDistanceThreshold;
    public float spec2_5HoldThreshold;
    public float specialAttackTouchTimeDownThreshold;
    public float specialAttackTouchTimeUpThreshold;
    public float specialAttack2_5Threshold;
    public GameObject raycastSource;
    public CharacterController collider1;
    public SphereCollider collider2;
    public BoxCollider collider3;
    public GameObject shockwavePlayer2;

    [HideInInspector] public Transform transformToShake;
    [HideInInspector] public Transform bloodParticleTransform;
    [HideInInspector] public Vector3 storedMoveDirection;
    [HideInInspector] public int upgradePoints;
    [HideInInspector] public bool amAttackingRightNow = false;
    [HideInInspector] public int attackMode;
    [HideInInspector] public bool isControlOff = true;
    [HideInInspector] public bool isControlOn = true;
    [HideInInspector] public int specialAttackMode;
    [HideInInspector] public bool isSpecialAttackUnderWay = false;

    int currenthealth = 100;
    int amAttacking = 0;
    CharacterController playerCollider;
    int attackOnCooldown = 0;
    int isAttacking = 0;
    int hasDamaged;
    bool attackDamageTicked = false;
    bool attackIsDone = false;
    List<GameObject> listOfTargets = new List<GameObject>();
    GameObject enemyTarget = null;
    Animator animator;
    bool isDead = false;
    int damageAmount;
    Vector3 moveUpperCubeorigpos = new Vector3(999, 999, 999);
    bool prevBlueArrow = false;
    Vector3 refoutvar = Vector3.zero;
    Vector3 CurrrentJoystickDirection = Vector3.zero;
    float lowerlimit = 0.1f;
    Vector3 lastMousePosition = Vector3.zero;
    float orangeActivateTime;
    float primaryCooldown;
    float secondaryCooldown;
    bool isSpecAttack2 = true;
    bool isSpecAttack1 = true;
    float speedAttackCooldown = 5;
    float jumpAttackCooldown = 20;
    float speedAttackLastTrig = -5;
    float jumpAttackLastTrig = -20;
    int playerPowerLevel = 0;
    bool isFadingOrange = false;
    float fadingOrangeTimeStart = 0;
    float timeToFade = .25f;
    float orangeGoalScale = 2f;
    bool isThereNoTargetForJump = false;
    GameObject origOrangeCircle;
    Vector3 specialAttackAim;
    float mouseSpecDistanceThresholdreplace;
    int targetHealth = 100;
    bool isAscending = false;
    bool isFallingFromSky = false;
    bool hasDoneIt = false;
    bool isMouseOverButton = false;
    Vector3 startLocationOfMouseDown = new Vector3(0, 0, 0);
    float startDurationOfMouseUp = 0;
    bool SpecialPhase1 = false;
    bool SpecialPhase1_5 = false;
    bool SpecialPhase2 = false;
    bool SpecialPhase2_5 = false;
    float spec2_5StartTime;
    int specialAttackPhase = 1;
    List<GameObject> targetsInRange = new List<GameObject>();
    GameObject specialAttackEnemyTarget;
    float specialAttackFlightStartTime;
    Vector3 specialAttackFlightStartPos;
    float specialAttackFlightTargetTime;
    Vector3 specialAttackFlightTargetPos;
    Vector3 scanPoint;
    bool jumpAttackLastTrigRESET = false;
    bool specAttack3IsGo = false;
    GameObject trailParticle;

    void Start() {
        playerCollider = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        origOrangeCircle = orangeCircle;

        FillInHighScoreMenu();
        HealthRegeneration();
        upgradePoints = PlayerPrefs.GetInt("Upgrade Points", 0);
        setAttackMode(PlayerPrefs.GetInt("Weapon", 1));
        
        if (PlayerPrefs.GetInt("Level") == 29)
            modeltoRepl.GetComponent<Renderer>().sharedMaterial = playerpinkTrim;

        trailParticle = Instantiate(trailparticle, shockwavespawn.position, shockwave.transform.rotation);
        var main = trailParticle.GetComponent<ParticleSystem>().main;
        var emission = trailParticle.GetComponent<ParticleSystem>().emission;

        main.loop = true;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.startSpeed = 3f;
        main.startSize = 0.65f;
        emission.rateOverTime = 200f;

        trailParticle.GetComponent<ParticleSystem>().Stop();
    }

    void Update() {
        startLocationOfMouseDown = new Vector3(gameCamera.GetComponent<Camera>().pixelWidth * 0.5f, gameCamera.GetComponent<Camera>().pixelHeight * 0.65f, 0);
        mouseSpecDistanceThresholdreplace = gameCamera.GetComponent<Camera>().pixelHeight * 0.0707290533f;

        if (isControlOn == true && isControlOff == false && Time.timeScale != 0) {
            if (isSpecAttack1 == true) {
                if (Input.GetAxis("Fire1Joystick") > 0.5f) {
                    SpecialAttackManual(1);
                }
                else if (Input.GetButtonDown("Fire1") == true) {
                    if (isMouseOverButton == false) {
                        SpecialAttackManual(1);
                    }
                }
            }

            if (isSpecAttack2 == true) {
                if (Input.GetAxis("Fire2Joystick") > 0.5f) {
                    SpecialAttackManual(2);
                }
                else if (Input.GetButtonDown("Fire2") == true) {
                    if (isMouseOverButton == false) {
                        SpecialAttackManual(2);
                    }
                }
            }
        }

        if (isControlOn == true && isControlOff == false && (isSpecAttack2 == true || isSpecAttack1 == true) && Time.timeScale != 0)
            updateJoystickAim();

        AudioListener.volume = Time.timeScale;

        if (moveUpperCubeorigpos == new Vector3(999, 999, 999))
            moveUpperCubeorigpos = transformToShake.localPosition;

        if (hasDoneIt == false) {
            if (PinkIsTheNewEvil.EnemySpawner.level == 29) {
                isFallingFromSky = true;
            }
            animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            hasDoneIt = true;
        }

        HealthSplatterUpdate();
        pointNumbers.GetComponent<Text>().text = upgradePoints.ToString();
        UpdateTarget();
        if (isSpecialAttackUnderWay == false) {
            if (isDead == true) {
                if (PinkIsTheNewEvil.EnemySpawner.level == 29) {
                    AnimSwitchTo("Ascending");
                    isControlOn = false;
                }
                else {
                    AnimSwitchTo("isDead");
                    isControlOn = false;
                }
            }
            else if (isFallingFromSky == true) {
                AnimSwitchTo("FallingFromSky");
            }
            else if (isAscending == true) {
                AnimSwitchTo("Ascending");
            }
            else {
                if (isControlOn == true) {
                    if (Time.timeScale != 0)
                        storedMoveDirection = VirtualJoystick();
                    MovePlayer(storedMoveDirection);
                }
                else if (isControlOn == false) {
                    AnimSwitchTo("goToIdle3");
                    greenCircle.SetActive(false);
                    blueJoystickAim.SetActive(false);
                    backimage1.SetActive(false);
                    price.SetActive(false);
                    buybutton.SetActive(false);
                    buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                }
            }
        }
        else if (isSpecialAttackUnderWay == true) {
            if (specialAttackMode == 1) {
                PlayerSpecialAttackLogic1();
            }
            else if (specialAttackMode == 2) {
                PlayerSpecialAttackLogic();
            }
        }

        fadeAwayOrangeGroup();
        FadingSpecialAttackIndicators();
    }

    void FixedUpdate() {
        if (specAttack3IsGo == true) {
            transform.position = specialAttackFlightTargetPos;

            GameObject[] listofEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < listofEnemies.Length; i++) {
                float range = 5f;
                if (Vector3.Distance(raycastSource.transform.position, listofEnemies[i].transform.position) < range) {
                    listofEnemies[i].GetComponent<EnemyAI>().Health(damageAmount * 4);
                }
            }

            specAttack3IsGo = false;
        }
    }

    void FillInHighScoreMenu() {
        highScoreName[0].GetComponent<Text>().text = "1: " + PlayerPrefs.GetString("High Score Name 1");
        highScore[0].GetComponent<Text>().text = PlayerPrefs.GetInt("High Score 1").ToString();
        highScoreName[1].GetComponent<Text>().text = "2: " + PlayerPrefs.GetString("High Score Name 2");
        highScore[1].GetComponent<Text>().text = PlayerPrefs.GetInt("High Score 2").ToString();
        highScoreName[2].GetComponent<Text>().text = "3: " + PlayerPrefs.GetString("High Score Name 3");
        highScore[2].GetComponent<Text>().text = PlayerPrefs.GetInt("High Score 3").ToString();
        highScoreName[3].GetComponent<Text>().text = "4: " + PlayerPrefs.GetString("High Score Name 4");
        highScore[3].GetComponent<Text>().text = PlayerPrefs.GetInt("High Score 4").ToString();
        highScoreName[4].GetComponent<Text>().text = "5: " + PlayerPrefs.GetString("High Score Name 5");
        highScore[4].GetComponent<Text>().text = PlayerPrefs.GetInt("High Score 5").ToString();
        highScoreName[5].GetComponent<Text>().text = "6: " + PlayerPrefs.GetString("High Score Name 6");
        highScore[5].GetComponent<Text>().text = PlayerPrefs.GetInt("High Score 6").ToString();
        highScoreName[6].GetComponent<Text>().text = "7: " + PlayerPrefs.GetString("High Score Name 7");
        highScore[6].GetComponent<Text>().text = PlayerPrefs.GetInt("High Score 7").ToString();
        highScoreName[7].GetComponent<Text>().text = "8: " + PlayerPrefs.GetString("High Score Name 8");
        highScore[7].GetComponent<Text>().text = PlayerPrefs.GetInt("High Score 8").ToString();
    }

    public void ResumeSkyfall() {
        animator.SetFloat("fallFromSkySpeed", 1);
    }

    void CheckIfInHighscores() {
        for (int i = 0; i < MainSystems.HighScoreListLength; i++) {
            if (upgradePoints > PinkIsTheNewEvil.MainSystems.currentHighScoreList[i].highScoreValue) {
                leburhighsc.gameObject.SetActive(true);
                newname.gameObject.SetActive(true);
                leburhighscnum.gameObject.SetActive(true);
                newHighScore.SetActive(true);
                leburhighscnum.GetComponent<Text>().text = upgradePoints.ToString();
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().saveHighScore = true;
                break;
            }
        }
    }

    void animateAttack() {
        if (amAttacking == 0) {
            amAttacking = 1;
            AnimSwitchTo("goToAttack");
            Invoke("amAttackingSwitchBack", attackAnimLength);
        }
    }

    void amAttackingSwitchBack() {
        amAttacking = 0;
    }

    public void Health(int value) {
        if (isSpecialAttackUnderWay == false) {
            if (isControlOn == true && PinkIsTheNewEvil.MainSystems.debugInvulnerable == false) {
                currenthealth = currenthealth + value;
                Vector3 bloodpos = new Vector3(transform.position.x, transform.position.y + .6f, transform.position.z);
                GameObject particle = (GameObject)Instantiate(particle2prefab, bloodpos, particle2prefab.transform.rotation);
                Destroy(particle, 1.0f);
                GetComponent<SoundManager>().PlaySound(7);
            }

            if (currenthealth <= 0) {
                isDead = true;
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().OpenPrompt(9);
                if (PlayerPrefs.GetInt("Level") == 29)
                    CheckIfInHighscores();
            }
            else {
                if (isControlOn == true) {
                    MoveUpper();
                    Invoke("MoveLower", .0625f);
                    Invoke("MoveUpper2", .125f);
                    Invoke("MoveLower", .1875f);
                    Invoke("MoveFinish", .25f);
                }
            }
        }
    }

    void HealthRegeneration() {
        int healthRegenAmount = 1;
        float healthRegenTick = .10f;
        if (currenthealth + healthRegenAmount > targetHealth) {
            currenthealth = targetHealth;
        }
        else if (currenthealth <= 0) {
            return;
        }
        else {
            currenthealth = currenthealth + healthRegenAmount;
        }
        Invoke("HealthRegeneration", healthRegenTick);
    }

    void HealthSplatterUpdate() {
        float x = hudCanvas.GetComponent<RectTransform>().sizeDelta.x;
        float y = hudCanvas.GetComponent<RectTransform>().sizeDelta.y;
        healthBloodOnScreen.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
        float scale;
        if (currenthealth < targetHealth) {
            float targetHealthconv = targetHealth;
            scale = Mathf.Lerp(1, 2, currenthealth / targetHealthconv);
        }
        else {
            scale = 2.9f;
        }
        healthBloodOnScreen.transform.localScale = new Vector3(scale, scale, 1);
    }

    public void setAttackMode(int mode) {
        if (mode == 1) {
            attackLength = .7f;
            attackMode = 1;
            attackRange = 1.2f;
            damageAmount = -30;
            attackCooldown = .5f;
            disableOtherWeapons();
        }
        else if (mode == 2) {
            attackLength = .5f;
            attackMode = 2;
            attackRange = 1.6f;
            damageAmount = -50;
            attackCooldown = 1f;
            disableOtherWeapons();
            weaponModel2.SetActive(true);
        }
        else if (mode == 3) {
            attackLength = 0.5f;
            attackMode = 3;
            attackRange = 1.7f;
            damageAmount = -90;
            attackCooldown = 1f;
            disableOtherWeapons();
            weaponModel3.SetActive(true);
        }
        else if (mode == 4) {
            attackLength = 0.5f;
            attackMode = 4;
            attackRange = 1.6f;
            damageAmount = -150;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel4.SetActive(true);
        }
        else if (mode == 5) {
            attackLength = 0.5f;
            attackMode = 5;
            attackRange = 2f;
            damageAmount = -300;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel5.SetActive(true);
        }
        else if (mode == 6) {
            attackLength = 1f;
            attackMode = 6;
            attackRange = 2.9f;
            damageAmount = -600;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel6.SetActive(true);
        }
        else if (mode == 7) {
            attackLength = 1f;
            attackMode = 7;
            attackRange = 3.9f;
            damageAmount = -800;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel7.SetActive(true);
        }
        else if (mode == 8) {
            attackLength = 1f;
            attackMode = 8;
            attackRange = 3.9f;
            damageAmount = -1200;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel8.SetActive(true);
            if (PlayerPrefs.GetInt("Level") == 29) {
                weaponModel8.GetComponent<MeshRenderer>().material = weapon8pinktrim;
            }
        }
    }

    public void BuyWeapon(int type) {
        if (type == 420) {
            GetComponent<SoundManager>().PlaySound(14);
        }
        else {
            GetComponent<SoundManager>().PlaySound(12);
        }

        if (type == 2) {
            if (upgradePoints >= 100) {
                upgradePoints = upgradePoints - 100;
                setAttackMode(2);

                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[1 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel2.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                PinkIsTheNewEvil.EnemySpawner.weapons[1 - 1].SetActive(false);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 3) {
            if (upgradePoints >= 1000) {
                upgradePoints = upgradePoints - 1000;
                setAttackMode(3);

                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[2 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel3.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                PinkIsTheNewEvil.EnemySpawner.weapons[1 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[2 - 1].SetActive(false);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 4) {
            if (upgradePoints >= 2300) {
                upgradePoints = upgradePoints - 2300;
                setAttackMode(4);

                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[3 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel4.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                PinkIsTheNewEvil.EnemySpawner.weapons[1 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[2 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[3 - 1].SetActive(false);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 5) {
            if (upgradePoints >= 4000) {
                upgradePoints = upgradePoints - 4000;
                setAttackMode(5);

                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[4 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel5.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                PinkIsTheNewEvil.EnemySpawner.weapons[1 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[2 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[3 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[4 - 1].SetActive(false);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 6) {
            if (upgradePoints >= 11000) {
                upgradePoints = upgradePoints - 11000;
                setAttackMode(6);

                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[5 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel6.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                PinkIsTheNewEvil.EnemySpawner.weapons[1 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[2 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[3 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[4 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[5 - 1].SetActive(false);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 7) {
            if (upgradePoints >= 20000) {
                upgradePoints = upgradePoints - 20000;
                setAttackMode(7);

                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[6 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel7.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                PinkIsTheNewEvil.EnemySpawner.weapons[1 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[2 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[3 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[4 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[5 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[6 - 1].SetActive(false);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 8) {
            if (upgradePoints >= 43000) {
                upgradePoints = upgradePoints - 43000;
                setAttackMode(8);

                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[7 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel8.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                PinkIsTheNewEvil.EnemySpawner.weapons[1 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[2 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[3 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[4 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[5 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[6 - 1].SetActive(false);
                PinkIsTheNewEvil.EnemySpawner.weapons[7 - 1].SetActive(false);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 9) {
            if (PinkIsTheNewEvil.EnemySpawner.level == 29 && upgradePoints >= 70000) {
                upgradePoints = upgradePoints - 70000;
                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[8 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel8.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                poof1.transform.localScale = poof1.transform.localScale * 3;
                poof2.transform.localScale = poof2.transform.localScale * 3;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                speedPerkIcon.SetActive(true);
                speedPerkBack.SetActive(true);
                PinkIsTheNewEvil.EnemySpawner.weapons[8 - 1].SetActive(false);
                animator.SetFloat("specAttack1speed", 0.5f);
                speedAttackCooldown = speedAttackCooldown + 2;
            }
            else if (upgradePoints >= 100000) {
                upgradePoints = upgradePoints - 100000;
                PlayerPrefs.SetInt("Level", 29);
                PlayerPrefs.SetInt("Upgrade Points", 0);
                PlayerPrefs.SetInt("Weapon", 8);
                PlayerPrefs.Save();
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().hud.SetActive(false);
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().gamePause.SetActive(false);
                ascencionScreen.SetActive(true);
                PinkIsTheNewEvil.EnemySpawner.constantlyDenyInput = true;
                isAscending = true;
                GameObject.Find("Player").GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.Ascend);
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 10) {
            if (PinkIsTheNewEvil.EnemySpawner.level == 29 && upgradePoints >= 90000) {
                upgradePoints = upgradePoints - 90000;
                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[7 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel8.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                poof1.transform.localScale = poof1.transform.localScale * 3;
                poof2.transform.localScale = poof2.transform.localScale * 3;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                jumpPerkIcon.SetActive(true);
                jumpPerkBack.SetActive(true);
                PinkIsTheNewEvil.EnemySpawner.weapons[9 - 1].SetActive(false);
                jumpAttackCooldown = jumpAttackCooldown / 3;
            }
            else if (upgradePoints >= 100000) {
                upgradePoints = upgradePoints - 100000;
                PlayerPrefs.SetInt("Level", 29);
                PlayerPrefs.SetInt("Upgrade Points", 0);
                PlayerPrefs.SetInt("Weapon", 8);
                PlayerPrefs.Save();
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().hud.SetActive(false);
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().gamePause.SetActive(false);
                ascencionScreen.SetActive(true);
                PinkIsTheNewEvil.EnemySpawner.constantlyDenyInput = true;
                isAscending = true;
                GameObject.Find("Player").GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.Ascend);
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 11) {
            if (PinkIsTheNewEvil.EnemySpawner.level == 29 && upgradePoints >= 30000) {
                upgradePoints = upgradePoints - 30000;
                GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[7 - 1].transform.position, poofWeap.transform.rotation);
                GameObject poof2 = Instantiate(poofPLay, weaponModel8.transform.position, poofPLay.transform.rotation);
                poof1.transform.parent = null;
                poof1.transform.localScale = poof1.transform.localScale * 3;
                poof2.transform.localScale = poof2.transform.localScale * 3;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                healthPerkIcon.SetActive(true);
                healthPerkBack.SetActive(true);
                PinkIsTheNewEvil.EnemySpawner.weapons[10 - 1].SetActive(false);
                targetHealth = targetHealth * 2;
            }
            else if (upgradePoints >= 100000) {
                upgradePoints = upgradePoints - 100000;
                PlayerPrefs.SetInt("Level", 29);
                PlayerPrefs.SetInt("Upgrade Points", 0);
                PlayerPrefs.SetInt("Weapon", 8);
                PlayerPrefs.Save();
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().hud.SetActive(false);
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().gamePause.SetActive(false);
                ascencionScreen.SetActive(true);
                PinkIsTheNewEvil.EnemySpawner.constantlyDenyInput = true;
                isAscending = true;
                GameObject.Find("Player").GetComponent<SoundManager>().MusicManager(SoundManager.MusicMood.Ascend);
            }
            else {
                backgroundNotEnough.SetActive(true);
                notEnoughPrompt.SetActive(true);
                Invoke("GetRidOfNotEnoughPrompt", 3);
            }
        }
        else if (type == 420) {
            backimage1.SetActive(false);
            price.SetActive(false);
            buybutton.SetActive(false);
            buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
            pinkCaner.SetActive(false);
            modeltoRepl.GetComponent<Renderer>().sharedMaterial = pinkGuyMaterial;
            pinklight.SetActive(true);
        }
    }

    void GetRidOfNotEnoughPrompt() {
        backgroundNotEnough.SetActive(false);
        notEnoughPrompt.SetActive(false);
    }

    void disableOtherWeapons() {
        weaponModel2.SetActive(false);
        weaponModel3.SetActive(false);
        weaponModel4.SetActive(false);
        weaponModel5.SetActive(false);
        weaponModel6.SetActive(false);
        weaponModel7.SetActive(false);
        weaponModel8.SetActive(false);
    }

    public void SpawnSmokeForFall() {
        GameObject shocking = Instantiate(shockwavePlayer2);
        shocking.transform.position = new Vector3(transform.position.x, transform.position.y + .4f, transform.position.z);
        Destroy(shocking, 4);
    }

    public void StopSkyfall() {
        isFallingFromSky = false;
    }

    void SpecialAttackManual(int mode) {
        if (orangeCircle.GetComponent<Transform>().position == startLocationOfMouseDown && orangeCircle.activeSelf == false) {
            specialAttackAim = Input.mousePosition;
            CancelInvoke("VanishAndResetOrangeCircle");
            orangeCircle.SetActive(true);
        }
        else {
            specialAttackAim = orangeCircle.GetComponent<Transform>().position;
        }

        orangeCircle.GetComponent<Transform>().position = specialAttackAim;

        isSpecialAttackUnderWay = true;
        SpecialPhase1 = false;
        SpecialPhase1_5 = false;
        SpecialPhase2 = false;
        SpecialPhase2_5 = false;
        if (mode == 1) {
            specialAttackMode = 1;
            powerUpIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            if (isSpecAttack1 == true) {
                speedAttackLastTrig = Time.time;
            }
        }
        else {
            specialAttackMode = 2;
            powerUpIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
            if (isSpecAttack2 == true) {
                jumpAttackLastTrig = Time.time;
            }
        }
        isFadingOrange = true;
        fadingOrangeTimeStart = Time.time;

        powerUpIcon.GetComponent<RectTransform>().position = new Vector3(orangeCircle.GetComponent<RectTransform>().position.x, orangeCircle.GetComponent<RectTransform>().position.y, 0);
        powerUpIcon.SetActive(true);

        if (isSpecAttack1 == false && specialAttackMode == 1) {
            isSpecialAttackUnderWay = false;
            powerUpIcon.SetActive(false);
            orangeCircle.SetActive(false);
            CancelInvoke("removeOrangeCircle");
        }
        else if (isSpecAttack2 == false && specialAttackMode == 2) {
            isSpecialAttackUnderWay = false;
            powerUpIcon.SetActive(false);
            orangeCircle.SetActive(false);
            CancelInvoke("removeOrangeCircle");
        }
    }

    void updateJoystickAim() {
        int screenWidth = gameCamera.GetComponent<Camera>().pixelWidth;
        int screenHeight = gameCamera.GetComponent<Camera>().pixelHeight;

        float aimHorizontal = Input.GetAxis("HorizontalAim");
        float aimVertical = Input.GetAxis("VerticalAim");

        Vector3 virtualJoystickDirection = new Vector3(aimHorizontal, aimVertical, 0);

        virtualJoystickDirection = Quaternion.Euler(screenRotationCorrection) * virtualJoystickDirection;
        virtualJoystickDirection = Vector3.ClampMagnitude(virtualJoystickDirection, 1f);

        float joystickAimMoveSpeed = 1500f;
        float resetTime = 0.75f;

        if (aimHorizontal == 0 && aimVertical == 0) {
            Invoke("VanishAndResetOrangeCircle", resetTime);
        }
        else {
            CancelInvoke("VanishAndResetOrangeCircle");

            Vector3 finalPostion = orangeCircle.GetComponent<Transform>().position + ((virtualJoystickDirection * joystickAimMoveSpeed) * Time.deltaTime);

            if (finalPostion.x > screenWidth - 10) {
                finalPostion.x = screenWidth - 10;
            }
            else if (finalPostion.x < 10) {
                finalPostion.x = 10;
            }

            if (finalPostion.y > screenHeight - 10) {
                finalPostion.y = screenHeight - 10;
            }
            else if (finalPostion.y < 10) {
                finalPostion.y = 10;
            }

            finalPostion.z = 0;

            orangeCircle.SetActive(true);
            orangeCircle.GetComponent<Transform>().position = finalPostion;
        }

    }

    void VanishAndResetOrangeCircle() {
        int screenWidth = gameCamera.GetComponent<Camera>().pixelWidth;
        int screenHeight = gameCamera.GetComponent<Camera>().pixelHeight;

        Vector3 screenCenter = new Vector3(screenWidth * 0.5f, screenHeight * 0.65f, 0);

        orangeCircle.GetComponent<Transform>().position = screenCenter;
        orangeCircle.SetActive(false);
    }

    public void setisMouseOverButton(bool setBool) {
        isMouseOverButton = setBool;
    }

    public void specialAttackPhaseSetPlay(int phase) {
        specialAttackPhase = phase;
    }

    void PlayerSpecialAttackLogic() {
        if (specialAttackPhase == 1) {
            AnimSwitchTo("SpecAttack3");
            isControlOff = true;
            specialAttackPhase = 2;
            Ray ray = GameObject.Find("Game Camera").GetComponent<Camera>().ScreenPointToRay(specialAttackAim);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask);
            scanPoint = hit.point;
        }
        else if (specialAttackPhase == 2) {
            for (int i = 0; i < PinkIsTheNewEvil.EnemySpawner.listOfAllEnemies.Count; i++) {
                if (specialAttackEnemyTarget == null) {
                    specialAttackEnemyTarget = PinkIsTheNewEvil.EnemySpawner.listOfAllEnemies[i];
                }
                else if (Vector3.Distance(scanPoint, PinkIsTheNewEvil.EnemySpawner.listOfAllEnemies[i].transform.position) < Vector3.Distance(scanPoint, specialAttackEnemyTarget.transform.position)) {
                    specialAttackEnemyTarget = PinkIsTheNewEvil.EnemySpawner.listOfAllEnemies[i];
                }
            }

            if (specialAttackEnemyTarget != null) {
                if (Vector3.Distance(scanPoint, specialAttackEnemyTarget.transform.position) > 6) {
                    specialAttackEnemyTarget = null;
                }
            }
            if (specialAttackEnemyTarget != null) {
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                specialAttackPhase = 3;
                transform.LookAt(specialAttackEnemyTarget.transform.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
            else {
                specialAttackPhase = 8;
                isThereNoTargetForJump = true;
                jumpAttackLastTrigRESET = true;
            }
        }
        else if (specialAttackPhase == 3) {
            transform.LookAt(specialAttackEnemyTarget.transform.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        else if (specialAttackPhase == 4) {
            if (specialAttackEnemyTarget == null) {
                specialAttackPhase = 8;
                isThereNoTargetForJump = true;
                jumpAttackLastTrigRESET = true;
            }
            else {
                AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

                float playbackTime = currentState.normalizedTime % 1;

                specialAttackFlightStartTime = playbackTime;
                specialAttackFlightStartPos = transform.position;
                specialAttackFlightTargetTime = 0.696f;
                specialAttackFlightTargetPos = specialAttackEnemyTarget.transform.position;

                collider1.enabled = false;
                collider2.enabled = false;
                collider3.enabled = false;

                specialAttackPhase = 5;
            }
        }
        else if (specialAttackPhase == 5) {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            float switchTimePercent = ((currentState.normalizedTime % 1) - specialAttackFlightStartTime) / (specialAttackFlightTargetTime - specialAttackFlightStartTime);

            if (switchTimePercent > 1) {
                specialAttackPhase = 6;
            }
            else {
                transform.position = Vector3.Lerp(specialAttackFlightStartPos, specialAttackFlightTargetPos, switchTimePercent);
            }
        }
        else if (specialAttackPhase == 6) {
            collider1.enabled = true;
            collider2.enabled = true;
            collider3.enabled = true;

            specAttack3IsGo = true;

            GameObject shocking = Instantiate(shockwavePlayer2);
            shocking.transform.position = new Vector3(transform.position.x, transform.position.y - .4f, transform.position.z);
            Destroy(shocking, 4);

            specialAttackPhase = 7;
        }
        else if (specialAttackPhase == 7) {
        }
        else if (specialAttackPhase == 8) {
            isControlOff = false;
            targetsInRange = new List<GameObject>();
            isSpecialAttackUnderWay = false;
            specialAttackPhase = 1;
            specialAttackEnemyTarget = null;
            if (jumpAttackLastTrigRESET == true) {
                jumpAttackLastTrig = Time.time - (jumpAttackCooldown - 1f);
                jumpAttackLastTrigRESET = false;
            }
        }
    }

    void PlayerSpecialAttackLogic1() {
        if (specialAttackPhase == 1) {
            Vector3 virtualJoystickDirection = startLocationOfMouseDown - orangeCircle.GetComponent<Transform>().position;
            virtualJoystickDirection = new Vector3(-virtualJoystickDirection.x, 0, -virtualJoystickDirection.y);
            virtualJoystickDirection = Quaternion.Euler(screenRotationCorrection) * virtualJoystickDirection;
            transform.LookAt(transform.position + virtualJoystickDirection.normalized);

            AnimSwitchTo("SpecAttack1");
            isControlOff = true;
            specialAttackPhase = 2;


        }
        else if (specialAttackPhase == 2) {
        }
        else if (specialAttackPhase == 3) {
            RaycastHit hit;
            Physics.Raycast(new Vector3(raycastSource.transform.position.x, raycastSource.transform.position.y + 1, raycastSource.transform.position.z), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, myLayerMask);
            Vector3 pointHit = hit.point;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(pointHit);
            specialAttackPhase = 4;

            if (attackMode == 7 || attackMode == 8) {
                trailParticle.transform.position = shockwavespawn2.position;
            }
            else {
                trailParticle.transform.position = shockwavespawn.position;
            }

            trailParticle.transform.SetParent(this.transform);

            Vector3 newtrailPos = trailParticle.transform.localPosition;
            newtrailPos.y = newtrailPos.y - 0.3f;
            newtrailPos.z = newtrailPos.z - 1.25f;

            trailParticle.transform.localPosition = newtrailPos;
            trailParticle.transform.localScale = new Vector3(1.2f, 1.2f, .65f);
            trailParticle.transform.localRotation = Quaternion.Euler(0, 0, 0);

            trailParticle.GetComponent<ParticleSystem>().Play();
        }
        else if (specialAttackPhase == 4) {
            EnemiesInAttackArea targetarea = boxTargetArea.GetComponent<EnemiesInAttackArea>();
            List<GameObject> targetsInRangeDup = targetsInRange;

            for (int i = 0; i < targetsInRangeDup.Count; i++) {
                if (targetarea.enemiesWithinArea.Contains(targetsInRangeDup[i]) == false || Vector3.Distance(transform.position, targetsInRangeDup[i].transform.position) > attackRange) {
                    targetsInRange.Remove(targetsInRangeDup[i]);
                }
            }

            for (int i = 0; i < listOfTargets.Count; i++) {
                if (targetarea.enemiesWithinArea.Contains(listOfTargets[i]) == true && Vector3.Distance(transform.position, listOfTargets[i].transform.position) <= attackRange) {
                    if (targetsInRange.Contains(listOfTargets[i]) == false) {
                        listOfTargets[i].GetComponent<EnemyAI>().Health(damageAmount * 2);
                        targetsInRange.Add(listOfTargets[i]);
                    }
                }
            }
        }
        else if (specialAttackPhase == 5) {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            specialAttackPhase = 6;
        }
        else if (specialAttackPhase == 6) {
        }
        else if (specialAttackPhase == 7) {
            isControlOff = false;
            targetsInRange = new List<GameObject>();
            isSpecialAttackUnderWay = false;
            specialAttackPhase = 1;

            trailParticle.GetComponent<ParticleSystem>().Stop();
        }
    }

    void removeOrangeCircle() {
        orangeCircle.SetActive(false);
        CancelInvoke("removeOrangeCircle");
    }

    void fadeAwayOrangeGroup() {
        if (isFadingOrange == true) {

            if (isThereNoTargetForJump == true) {
                orangeCircle.SetActive(false);
                redCancelIcon.SetActive(true);
                redCancelIcon.transform.position = orangeCircle.transform.position;
            }

            Color orangeColor = orangeCircle.GetComponent<Image>().color;
            float lerpPoint = (((fadingOrangeTimeStart + timeToFade) - Time.time) / timeToFade);
            orangeColor.a = Mathf.Lerp(0, 1, lerpPoint);
            orangeCircle.GetComponent<Image>().color = orangeColor;

            CancelInvoke("removeOrangeCircle");

            Color powerColor = powerUpIcon.GetComponent<Image>().color;
            powerColor.a = orangeColor.a + .50f;
            powerUpIcon.GetComponent<Image>().color = powerColor;

            Color redColor = redTarget.GetComponent<Image>().color;
            redColor.a = orangeColor.a;
            redTarget.GetComponent<Image>().color = redColor;
            redCircle.GetComponent<Image>().color = redColor;
            redAim.GetComponent<Image>().color = redColor;

            float groupScale = Mathf.Lerp(orangeGoalScale, 1, lerpPoint);
            orangeCircle.GetComponent<RectTransform>().localScale = new Vector3(groupScale, groupScale, 0);
            powerUpIcon.GetComponent<RectTransform>().localScale = new Vector3(groupScale, groupScale, 0);

            if (lerpPoint <= 0) {
                isThereNoTargetForJump = false;
                orangeCircle.SetActive(false);
                VanishAndResetOrangeCircle();
                redCancelIcon.SetActive(false);

                isFadingOrange = false;
                orangeColor.a = 1;
                powerColor.a = 1;
                redColor.a = 1;
                orangeCircle.GetComponent<Image>().color = orangeColor;
                powerUpIcon.GetComponent<Image>().color = powerColor;
                redTarget.GetComponent<Image>().color = redColor;
                redCircle.GetComponent<Image>().color = redColor;
                redAim.GetComponent<Image>().color = redColor;
                orangeCircle.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
                powerUpIcon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
                orangeCircle.SetActive(false);
                powerUpIcon.SetActive(false);
                redTarget.SetActive(false);
                redAim.SetActive(false);
                redCircle.SetActive(false);
            }
        }
    }

    void FadingSpecialAttackIndicators() {
        if (PinkIsTheNewEvil.EnemySpawner.level > 14) {
            playerPowerLevel = 2;
        }
        else if (PinkIsTheNewEvil.EnemySpawner.level > 5) {
            playerPowerLevel = 1;
        }
        else {
            playerPowerLevel = 0;
        }

        if (playerPowerLevel == 2) {
            blackBackgroundJuan.SetActive(false);
            blackBackgroundDos.SetActive(true);
        }
        else if (playerPowerLevel == 1) {
            blackBackgroundJuan.SetActive(true);
            blackBackgroundDos.SetActive(false);
        }
        else {
            blackBackgroundJuan.SetActive(false);
            blackBackgroundDos.SetActive(false);
        }


        if (playerPowerLevel < 1) {
            isSpecAttack1 = false;
        }
        else if ((Time.time - speedAttackLastTrig) > speedAttackCooldown) {
            isSpecAttack1 = true;
        }
        else if ((Time.time - speedAttackLastTrig) < speedAttackCooldown) {
            isSpecAttack1 = false;
        }

        if (playerPowerLevel < 2) {
            isSpecAttack2 = false;
        }
        else if ((Time.time - jumpAttackLastTrig) > jumpAttackCooldown) {
            isSpecAttack2 = true;
        }
        else if ((Time.time - jumpAttackLastTrig) < jumpAttackCooldown) {
            isSpecAttack2 = false;
        }

        float a = (Time.time - speedAttackLastTrig) / speedAttackCooldown;
        Color speedTrans = speedAttackIndicatorCircle.GetComponent<Image>().color;
        if (playerPowerLevel < 1) {
            speedTrans.a = 0;
            speedAttackIndicatorCircleGlow.SetActive(false);
            speedAttackIndicatorIconGlow.SetActive(false);
        }
        else if (a > 1) {
            speedTrans.a = 1;
            speedAttackIndicatorCircleGlow.SetActive(true);
            speedAttackIndicatorIconGlow.SetActive(true);
        }
        else {
            speedTrans.a = Mathf.Lerp(0, 1, a) - .2f;
            speedAttackIndicatorCircleGlow.SetActive(false);
            speedAttackIndicatorIconGlow.SetActive(false);
        }
        speedAttackIndicatorCircle.GetComponent<Image>().color = speedTrans;
        speedAttackIndicatorIcon.GetComponent<Image>().color = speedTrans;

        float b = (Time.time - jumpAttackLastTrig) / jumpAttackCooldown;
        Color jumpTrans = jumpAttackIndicatorCircle.GetComponent<Image>().color;
        if (playerPowerLevel < 2) {
            jumpTrans.a = 0;
            jumpAttackIndicatorCircleGlow.SetActive(false);
            jumpAttackIndicatorIconGlow.SetActive(false);
        }
        else if (b > 1) {
            jumpTrans.a = 1;
            jumpAttackIndicatorCircleGlow.SetActive(true);
            jumpAttackIndicatorIconGlow.SetActive(true);
        }
        else {
            jumpTrans.a = Mathf.Lerp(0, 1, b) - .2f;
            jumpAttackIndicatorCircleGlow.SetActive(false);
            jumpAttackIndicatorIconGlow.SetActive(false);
        }
        jumpAttackIndicatorCircle.GetComponent<Image>().color = jumpTrans;
        jumpAttackIndicatorIcon.GetComponent<Image>().color = jumpTrans;
    }

    Vector3 VirtualJoystick() {
        if (isControlOff == false) {
            float horizontalAxisKeyboard = Input.GetAxis("HorizontalKeyboard");
            float verticalAxisKeyboard = Input.GetAxis("VerticalKeyboard");
            float horizontalAxisJoystick = Input.GetAxis("HorizontalJoystick");
            float verticalAxisJoystick = Input.GetAxis("VerticalJoystick");

            float joystickAxisLowerLimit = 0.5f;

            if (horizontalAxisJoystick > 0f && horizontalAxisJoystick < joystickAxisLowerLimit) {
                horizontalAxisJoystick = joystickAxisLowerLimit;
            }
            else if (horizontalAxisJoystick < 0f && horizontalAxisJoystick > -joystickAxisLowerLimit) {
                horizontalAxisJoystick = -joystickAxisLowerLimit;
            }

            if (verticalAxisJoystick > 0f && verticalAxisJoystick < joystickAxisLowerLimit) {
                verticalAxisJoystick = joystickAxisLowerLimit;
            }
            else if (verticalAxisJoystick < 0f && verticalAxisJoystick > -joystickAxisLowerLimit) {
                verticalAxisJoystick = -joystickAxisLowerLimit;
            }

            if (horizontalAxisJoystick != 0 || verticalAxisJoystick != 0) {
                Cursor.visible = false;
            }
            else if (Input.mousePosition != lastMousePosition) {
                Cursor.visible = true;
                lastMousePosition = Input.mousePosition;
            }
            else if (horizontalAxisKeyboard != 0 || verticalAxisKeyboard != 0) {
                Cursor.visible = true;
            }

            float horizontalAxis = 0f;
            float verticalAxis = 0f;

            if (horizontalAxisJoystick > 0 && horizontalAxisKeyboard > 0) {
                horizontalAxis = Mathf.Max(horizontalAxisJoystick, horizontalAxisKeyboard);
            }
            else if (horizontalAxisJoystick < 0 && horizontalAxisKeyboard < 0) {
                horizontalAxis = Mathf.Min(horizontalAxisJoystick, horizontalAxisKeyboard);
            }
            else {
                if (horizontalAxisJoystick != 0) {
                    horizontalAxis = horizontalAxisJoystick;
                }
                else {
                    horizontalAxis = horizontalAxisKeyboard;
                }
            }

            if (verticalAxisJoystick > 0 && verticalAxisKeyboard > 0) {
                verticalAxis = Mathf.Max(verticalAxisJoystick, verticalAxisKeyboard);
            }
            else if (verticalAxisJoystick < 0 && verticalAxisKeyboard < 0) {
                verticalAxis = Mathf.Min(verticalAxisJoystick, verticalAxisKeyboard);
            }
            else {
                if (verticalAxisJoystick != 0) {
                    verticalAxis = verticalAxisJoystick;
                }
                else {
                    verticalAxis = verticalAxisKeyboard;
                }
            }

            Vector3 virtualJoystickDirection = new Vector3(horizontalAxis, 0, verticalAxis);

            virtualJoystickDirection = Quaternion.Euler(screenRotationCorrection) * virtualJoystickDirection;
            virtualJoystickDirection = Vector3.ClampMagnitude(virtualJoystickDirection, 1f);


            CurrrentJoystickDirection = Vector3.SmoothDamp(CurrrentJoystickDirection, virtualJoystickDirection, ref refoutvar, 0.1f);



            Vector3 LowerClampedJoystickDirection = CurrrentJoystickDirection;

            if (LowerClampedJoystickDirection.x < lowerlimit && LowerClampedJoystickDirection.x > -lowerlimit) {
                LowerClampedJoystickDirection.x = 0f;
            }

            if (LowerClampedJoystickDirection.y < lowerlimit && LowerClampedJoystickDirection.y > -lowerlimit) {
                LowerClampedJoystickDirection.y = 0f;
            }

            if (LowerClampedJoystickDirection.z < lowerlimit && LowerClampedJoystickDirection.z > -lowerlimit) {
                LowerClampedJoystickDirection.z = 0f;
            }

            return LowerClampedJoystickDirection;

        }
        else {
            return new Vector3(0, 0, 0);
        }
    }

    public void attackDamageTickedSet() {
        attackDamageTicked = true;

        if (attackMode == 2 || attackMode == 3 || attackMode == 4 || attackMode == 5) {
            EnemiesInAttackArea targetarea = cylinderTargetArea.GetComponent<EnemiesInAttackArea>();
            for (int i = 0; i < listOfTargets.Count; i++) {
                if (targetarea.enemiesWithinArea.Contains(listOfTargets[i]) == true && Vector3.Distance(transform.position, listOfTargets[i].transform.position) <= attackRange) {
                    listOfTargets[i].GetComponent<EnemyAI>().Health(damageAmount);
                }
            }
        }
        else if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
            EnemiesInAttackArea targetarea = boxTargetArea.GetComponent<EnemiesInAttackArea>();
            if (attackMode == 7 || attackMode == 8)
                shockwavespawn = shockwavespawn2;
            GameObject shackwavebrah = (GameObject)Instantiate(shockwave, shockwavespawn.position, shockwave.transform.rotation);
            shackwavebrah.transform.localScale = new Vector3(.4f, .4f, .2f);
            Destroy(shackwavebrah, 1);
            for (int i = 0; i < listOfTargets.Count; i++) {
                if (targetarea.enemiesWithinArea.Contains(listOfTargets[i]) == true && Vector3.Distance(transform.position, listOfTargets[i].transform.position) <= attackRange) {
                    listOfTargets[i].GetComponent<EnemyAI>().Health(damageAmount);
                }
            }
        }
        else {
            if (Vector3.Distance(transform.position, enemyTarget.transform.position) <= attackRange) {
                enemyTarget.GetComponent<EnemyAI>().Health(damageAmount);
            }
        }
    }

    public void setAttackIsDone() {
        attackOnCooldown = 1;
        Invoke("AttackCooldownManager", attackCooldown);
        attackIsDone = true;
        amAttackingRightNow = false;
    }

    void AttackCooldownManager() {
        attackOnCooldown = 0;
    }

    void MovePlayer(Vector3 direction) {

        playerCollider.SimpleMove((direction * moveSpeed) * Time.fixedDeltaTime);

        if (direction != new Vector3(0, 0, 0)) {
            if (prevBlueArrow == false) {
                if (SpecialPhase2 != true) {
                }
                else {

                }
            }
            prevBlueArrow = true;
        }
        else {
            if (prevBlueArrow == true) {
            }
            prevBlueArrow = false;
        }

        if (enemyTarget == null) {
            if (((Input.GetButton("Fire3") == true && isControlOn == true && isControlOff == false && Time.timeScale != 0) && attackOnCooldown == 0) || amAttackingRightNow == true) {
                amAttackingRightNow = true;
                attackOnCooldown = 1;

                if (attackMode == 1)
                    AnimSwitchTo("goToAttack");
                else if (attackMode == 6 || attackMode == 7 || attackMode == 8)
                    AnimSwitchTo("goToAttack3");
                else
                    AnimSwitchTo("goToAttack2");

                if (isAttacking == 0) {
                    isAttacking = 1;
                    hasDamaged = 0;
                }

                if (attackDamageTicked == true && hasDamaged == 0) {
                    hasDamaged = 1;
                    attackDamageTicked = false;
                }

                if (attackIsDone == true) {
                    attackIsDone = false;
                    attackOnCooldown = 1;
                    isAttacking = 0;
                }
            }
            else if (direction == new Vector3(0, 0, 0)) {
                if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
                    AnimSwitchTo("goToIdle3");
                }
                else
                    AnimSwitchTo("goToIdle");
                transform.LookAt(transform.position + direction);
            }
            else {
                if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
                    AnimSwitchTo("goToWalkForward3");
                }
                else
                    AnimSwitchTo("goToWalkForward");
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((transform.position + direction) - transform.position), playerLookAtRotateSpeed * Time.deltaTime);
            }

            isAttacking = 0;

            transformToShake.localPosition = new Vector3(0, 0, 0);
        }
        else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(enemyTarget.transform.position - transform.position), playerLookAtRotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            Vector3 enemyAim = enemyTarget.transform.position - transform.position;
            enemyAim = new Vector3(enemyAim.x, 0f, enemyAim.z).normalized;

            Vector3 walkAim = direction;

            float angle = Vector3.Angle(enemyAim, walkAim);

            Vector3 enemyWalkPerpendicular = Vector3.Cross(enemyAim, walkAim);
            Vector3 up = new Vector3(0f, 1f, 0f);
            float leftRightIdentifier = Vector3.Dot(enemyWalkPerpendicular, up);

            if (((Input.GetButton("Fire3") == true && isControlOn == true && isControlOff == false && Time.timeScale != 0) && attackOnCooldown == 0) || amAttackingRightNow == true) {
                amAttackingRightNow = true;
                attackOnCooldown = 1;

                if (attackMode == 1)
                    AnimSwitchTo("goToAttack");
                else if (attackMode == 6 || attackMode == 7 || attackMode == 8)
                    AnimSwitchTo("goToAttack3");
                else
                    AnimSwitchTo("goToAttack2");

                if (isAttacking == 0) {
                    isAttacking = 1;
                    hasDamaged = 0;
                }

                if (attackDamageTicked == true && hasDamaged == 0) {
                    hasDamaged = 1;
                    attackDamageTicked = false;
                }

                if (attackIsDone == true) {
                    attackIsDone = false;
                    attackOnCooldown = 1;
                    isAttacking = 0;
                }
            }
            else {
                amAttackingRightNow = false;

                if (direction == new Vector3(0, 0, 0)) {
                    if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
                        AnimSwitchTo("goToIdle3");
                    }
                    else
                        AnimSwitchTo("goToIdle");
                }
                else if (angle < 135f && angle > 45f) {

                    if (leftRightIdentifier < 0) {
                        if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
                            AnimSwitchTo("goToWalkLeft3");
                        }
                        else
                            AnimSwitchTo("goToWalkLeft");
                    }
                    else if (leftRightIdentifier > 0) {
                        if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
                            AnimSwitchTo("goToWalkRight3");
                        }
                        else
                            AnimSwitchTo("goToWalkRight");
                    }
                }
                else if (angle < 45f) {
                    if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
                        AnimSwitchTo("goToWalkForward3");
                    }
                    else
                        AnimSwitchTo("goToWalkForward");
                }
                else if (angle > 135f) {
                    if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
                        AnimSwitchTo("goToWalkBack3");
                    }
                    else
                        AnimSwitchTo("goToWalkBack");
                }
            }
        }
    }

    void UpdateTarget() {
        if (listOfTargets.Count == 0) {
            enemyTarget = null;
        }
        else {
            for (int i = 0; i < listOfTargets.Count; i++) {
                if (enemyTarget == null) {
                    enemyTarget = listOfTargets[i];
                }
                else if (listOfTargets[i] == null || listOfTargets[i].gameObject.tag != "Enemy") {
                    listOfTargets.RemoveAt(i);
                }
                else if (Vector3.Distance(transform.position, listOfTargets[i].transform.position) < Vector3.Distance(transform.position, enemyTarget.transform.position)) {
                    enemyTarget = listOfTargets[i];
                }
                else {
                }
            }
        }
    }

    void AnimSwitchTo(string trigger) {
        if (trigger == "goToAttack") {
            setAnimatorBoolCluster("Attack1");
        }
        else if (trigger == "goToAttack2") {
            setAnimatorBoolCluster("Attack2");
        }
        else if (trigger == "goToAttack3") {
            setAnimatorBoolCluster("Attack3");
        }
        else if (trigger == "isDead") {
            setAnimatorBoolCluster("Death");
        }
        else if (trigger == "goToWalkRight") {
            setAnimatorBoolCluster("RunningL");
        }
        else if (trigger == "goToWalkBack") {
            setAnimatorBoolCluster("RunningB");
        }
        else if (trigger == "goToWalkForward") {
            setAnimatorBoolCluster("RunningF");
        }
        else if (trigger == "goToWalkLeft") {
            setAnimatorBoolCluster("RunningR");
        }
        else if (trigger == "goToIdle3") {
            setAnimatorBoolCluster("Idle3");
        }
        else if (trigger == "goToWalkForward3") {
            setAnimatorBoolCluster("RunningF3");
        }
        else if (trigger == "goToWalkRight3") {
            setAnimatorBoolCluster("RunningL3");
        }
        else if (trigger == "goToWalkLeft3") {
            setAnimatorBoolCluster("RunningR3");
        }
        else if (trigger == "goToWalkBack3") {
            setAnimatorBoolCluster("RunningB3");
        }
        else if (trigger == "goToIdle") {
            setAnimatorBoolCluster("Idle");
        }
        else if (trigger == "SpecAttack1") {
            setAnimatorBoolCluster("SpecAttack1");
        }
        else if (trigger == "SpecAttack3") {
            setAnimatorBoolCluster("SpecAttack3");
        }
        else if (trigger == "Ascending") {
            setAnimatorBoolCluster("Ascending");
        }
        else if (trigger == "FallingFromSky") {
            setAnimatorBoolCluster("FallingFromSky");
        }
        else {
            Debug.Log("ERROR: Animation not found!");
        }
    }

    void setAnimatorBoolCluster(string boolFocus) {
        if (animator.GetBool("Idle") != false)
            animator.SetBool("Idle", false);
        if (animator.GetBool("Death") != false)
            animator.SetBool("Death", false);
        if (animator.GetBool("Attack1") != false)
            animator.SetBool("Attack1", false);
        if (animator.GetBool("Attack2") != false)
            animator.SetBool("Attack2", false);
        if (animator.GetBool("Attack3") != false)
            animator.SetBool("Attack3", false);
        if (animator.GetBool("RunningF") != false)
            animator.SetBool("RunningF", false);
        if (animator.GetBool("RunningB") != false)
            animator.SetBool("RunningB", false);
        if (animator.GetBool("RunningL") != false)
            animator.SetBool("RunningL", false);
        if (animator.GetBool("RunningR") != false)
            animator.SetBool("RunningR", false);
        if (animator.GetBool("Idle3") != false)
            animator.SetBool("Idle3", false);
        if (animator.GetBool("RunningF3") != false)
            animator.SetBool("RunningF3", false);
        if (animator.GetBool("RunningB3") != false)
            animator.SetBool("RunningB3", false);
        if (animator.GetBool("RunningL3") != false)
            animator.SetBool("RunningL3", false);
        if (animator.GetBool("RunningR3") != false)
            animator.SetBool("RunningR3", false);
        if (animator.GetBool("SpecAttack1") != false)
            animator.SetBool("SpecAttack1", false);
        if (animator.GetBool("SpecAttack3") != false)
            animator.SetBool("SpecAttack3", false);
        if (animator.GetBool("Ascending") != false)
            animator.SetBool("Ascending", false);
        if (animator.GetBool("FallingFromSky") != false)
            animator.SetBool("FallingFromSky", false);
        if (animator.GetBool(boolFocus) != true)
            animator.SetBool(boolFocus, true);
    }

    string ReturnCurrentTrigger() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackPunch")) {
            return "goToAttack";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attackBat_001")) {
            return "goToAttack2";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_3")) {
            return "goToAttack3";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("die")) {
            return "isDead";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningL")) {
            return "goToWalkRight";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningB")) {
            return "goToWalkBack";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningF")) {
            return "goToWalkForward";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningR")) {
            return "goToWalkLeft";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Standing_Idle")) {
            return "goToIdle3";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running_Forward")) {
            return "goToWalkForward3";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running_Left")) {
            return "goToWalkRight3";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running_Right")) {
            return "goToWalkLeft3";
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running_Backwards")) {
            return "goToWalkBack3";
        }
        else {
            return "goToIdle";
        }
    }

    void MoveUpper() {
        transformToShake.position = new Vector3(transformToShake.position.x + shakeAmount / 2, transformToShake.position.y + shakeAmount / 2, transformToShake.position.z + shakeAmount / 2);
    }
    void MoveLower() {
        transformToShake.position = new Vector3(transformToShake.position.x - shakeAmount, transformToShake.position.y - shakeAmount, transformToShake.position.z - shakeAmount);
    }
    void MoveUpper2() {
        transformToShake.position = new Vector3(transformToShake.position.x + shakeAmount, transformToShake.position.y + shakeAmount, transformToShake.position.z + shakeAmount);
    }
    void MoveFinish() {
        transformToShake.localPosition = moveUpperCubeorigpos;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" && listOfTargets.Contains(other.gameObject) == false)
            listOfTargets.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy" && listOfTargets.Contains(other.gameObject) == true)
            listOfTargets.Remove(other.gameObject);
    }
}