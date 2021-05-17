using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// Heavily intertwined class. Only refactored lightly. Apologies for the mess.
public class PlayerController : MonoBehaviour {
    [Header("Data")]
    public float attackAnimLength;
    public Vector3 screenRotationCorrection;
    public int moveSpeed;
    public float attackRange;
    public float attackCooldown;
    public float shakeAmount;
    public float attackLength;
    public float playerLookAtRotateSpeed;
    public float mouseSpecDistanceThreshold;
    public float spec2_5HoldThreshold;
    public float specialAttackTouchTimeDownThreshold;
    public float specialAttackTouchTimeUpThreshold;
    public float specialAttack2_5Threshold;
    public LayerMask myLayerMask;
    public SpecialAttackData specialAttack1Data;
    public SpecialAttackData specialAttack2Data;
    public WeaponStats[] weaponStats;

    [Header("References")]
    public GameObject pinklight;
    public GameObject cylinderTargetArea;
    public GameObject boxTargetArea;
    public Transform shockwavespawn;
    public Transform shockwavespawn2;
    public GameObject shockwave;
    public GameObject particle1prefab;
    public GameObject particle2prefab;
    public GameObject trailparticle;
    public GameObject gameCamera;
    public Material playerpinkTrim;
    public Material weapon8pinktrim;
    public GameObject poofWeap;
    public GameObject poofPLay;
    public GameObject pinkCaner;
    public GameObject modeltoRepl;
    public Material pinkGuyMaterial;
    public GameObject raycastSource;
    public CharacterController collider1;
    public SphereCollider collider2;
    public BoxCollider collider3;
    public GameObject shockwavePlayer2;

    [Header("UI")]
    public GameObject leburhighsc;
    public GameObject newname;
    public GameObject leburhighscnum;
    public GameObject pointNumbers;
    public GameObject healthBloodOnScreen;
    public GameObject hudCanvas;
    public GameObject backgroundNotEnough;
    public GameObject notEnoughPrompt;
    public GameObject newHighScore;
    public GameObject blueJoystickAim;
    public GameObject redAim;
    public GameObject redCircle;
    public GameObject blackBackgroundJuan;
    public GameObject blackBackgroundDos;
    public GameObject redTarget;
    public GameObject redCancelIcon;
    public GameObject greenCircle;
    public GameObject orangeCircle;
    public GameObject powerUpIcon;
    public GameObject backimage1;
    public GameObject price;
    public GameObject buybutton;
    public GameObject ascencionScreen;
    public GameObject healthPerkIcon;
    public GameObject healthPerkBack;
    public GameObject speedPerkIcon;
    public GameObject speedPerkBack;
    public GameObject jumpPerkIcon;
    public GameObject jumpPerkBack;

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
    [HideInInspector] public int playerPowerLevel = 0;

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
    Vector3 refoutvar = Vector3.zero;
    Vector3 CurrrentJoystickDirection = Vector3.zero;
    const float LowerLimit = 0.1f;
    Vector3 lastMousePosition = Vector3.zero;
    float orangeActivateTime;
    float primaryCooldown;
    float secondaryCooldown;
    bool isSpecAttack2 = true;
    bool isSpecAttack1 = true;
    float specialAttack1LastTrig;
    float specialAttack2LastTrig;
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
    const float shakeInterval = .0625f;

    [System.Serializable]
    public struct SpecialAttackData {
        public int mode;
        public float cooldown;
        public GameObject indicatorCircle;
        public GameObject indicatorCircleGlow;
        public GameObject indicatorIcon;
        public GameObject indicatorIconGlow;
    }

    [System.Serializable]
    public struct WeaponStats {
        public float attackLength;
        public float attackRange;
        public int damageAmount;
        public float attackCooldown;
        public GameObject weaponModel;
        public WithinWeaponPickUp weaponPickup;
    }

    void Start() {
        playerCollider = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        origOrangeCircle = orangeCircle;
        specialAttack1LastTrig = -specialAttack1Data.cooldown;
        specialAttack2LastTrig = -specialAttack2Data.cooldown;

        HealthRegeneration();
        InitializeControlImages();
        upgradePoints = PlayerPrefs.GetInt("Upgrade Points", 0);
        setAttackMode(PlayerPrefs.GetInt("Weapon", 1));

        if (PlayerPrefs.GetInt("Level") == 29) {
            modeltoRepl.GetComponent<Renderer>().sharedMaterial = playerpinkTrim;
            weaponStats[8 - 1].weaponModel.GetComponent<MeshRenderer>().material = weapon8pinktrim;
        }

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
                    if (isMouseOverButton == false)
                        SpecialAttackManual(1);
                }
            }
            if (isSpecAttack2 == true) {
                if (Input.GetAxis("Fire2Joystick") > 0.5f) {
                    SpecialAttackManual(2);
                }
                else if (Input.GetButtonDown("Fire2") == true) {
                    if (isMouseOverButton == false)
                        SpecialAttackManual(2);
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

        pointNumbers.GetComponent<Text>().text = upgradePoints.ToString();
        HealthSplatterUpdate();
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
                PlayerSpecialAttackLogic2();
            }
        }

        fadeAwayOrangeGroup();
        SpecialAttackTimerLogic();
    }

    void FixedUpdate() {
        if (specAttack3IsGo == false)
            return;

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

    void InitializeControlImages() {
        int screenWidth = gameCamera.GetComponent<Camera>().pixelWidth;
        int screenHeight = gameCamera.GetComponent<Camera>().pixelHeight;
        Vector3 screenCenter = new Vector3(screenWidth * 0.5f, screenHeight * 0.65f, 0);

        greenCircle.GetComponent<Transform>().position = screenCenter;
        orangeCircle.GetComponent<Transform>().position = screenCenter;
        powerUpIcon.GetComponent<Transform>().position = screenCenter;
        redCircle.GetComponent<Transform>().position = screenCenter;
        redTarget.GetComponent<Transform>().position = screenCenter;
        redCancelIcon.GetComponent<Transform>().position = screenCenter;
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
                PinkIsTheNewEvil.MainSystems.saveHighScore = true;
                break;
            }
        }
    }

    void animateAttack() {
        if (amAttacking == 1)
            return;

        amAttacking = 1;
        AnimSwitchTo("goToAttack");
        Invoke("amAttackingSwitchBack", attackAnimLength);

    }

    void amAttackingSwitchBack() {
        amAttacking = 0;
    }

    public void Health(int value) {
        if (isSpecialAttackUnderWay == true)
            return;

        if (isControlOn == true && PinkIsTheNewEvil.MainSystems.debugInvulnerable == false) {
            currenthealth += value;
            Vector3 bloodpos = new Vector3(transform.position.x, transform.position.y + .6f, transform.position.z);
            GameObject particle = Instantiate(particle2prefab, bloodpos, particle2prefab.transform.rotation);
            Destroy(particle, 1.0f);
            GetComponent<SoundManager>().PlaySound(7);
        }

        if (currenthealth <= 0) {
            isDead = true;
            PinkIsTheNewEvil.MainSystems.OpenPrompt(9);
            if (PlayerPrefs.GetInt("Level") == 29)
                CheckIfInHighscores();
        }
        else {
            if (isControlOn == true) {
                Invoke("MoveUpper", shakeInterval * 0);
                Invoke("MoveLower", shakeInterval * 1);
                Invoke("MoveUpper2", shakeInterval * 2);
                Invoke("MoveLower", shakeInterval * 3);
                Invoke("MoveFinish", shakeInterval * 4);
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
        healthBloodOnScreen.GetComponent<RectTransform>().sizeDelta = hudCanvas.GetComponent<RectTransform>().sizeDelta;
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
        attackMode = mode;
        attackLength = weaponStats[mode - 1].attackLength;
        attackRange = weaponStats[mode - 1].attackRange;
        damageAmount = weaponStats[mode - 1].damageAmount;
        attackCooldown = weaponStats[mode - 1].attackCooldown;

        for (int i = 0; i < 8; i++) {
            weaponStats[i].weaponModel.SetActive(false);
        }

        weaponStats[mode - 1].weaponModel.SetActive(true);
    }

    public void BuyWeapon(int type) {
        if (type == 420) {
            BuyWeaponEffects(type);
            ClearWeaponBuyPopUp();
        }
        else if ((PinkIsTheNewEvil.EnemySpawner.level == 28 && type > 8) && upgradePoints >= 100000) {
            BuyWeaponEffects(type);
            ClearWeaponBuyPopUp();
        }
        else if (upgradePoints >= weaponStats[type-1].weaponPickup.weaponPrice) {
            CreatePoofBuyWeapon(type);
            BuyWeaponEffects(type);
            ClearWeaponBuyPopUp();
        }
        else {
            GetComponent<SoundManager>().PlaySound(12);
            backgroundNotEnough.SetActive(true);
            notEnoughPrompt.SetActive(true);
            Invoke("GetRidOfNotEnoughPrompt", 3);
        }
    }

    void BuyWeaponEffects(int type) {
        if (type == 420) {
            GetComponent<SoundManager>().PlaySound(14);
            pinkCaner.SetActive(false);
            pinklight.SetActive(true);
            modeltoRepl.GetComponent<Renderer>().sharedMaterial = pinkGuyMaterial;
        }
        else if (PinkIsTheNewEvil.EnemySpawner.level == 28 && type > 8) {
            upgradePoints -= 100000;

            PinkIsTheNewEvil.MainSystems.hud.SetActive(false);
            PinkIsTheNewEvil.MainSystems.gamePause.SetActive(false);
            PinkIsTheNewEvil.EnemySpawner.constantlyDenyInput = true;
            PinkIsTheNewEvil.PlayerSoundManager.MusicManager(SoundManager.MusicMood.Ascend);
            ascencionScreen.SetActive(true);
            isAscending = true;

            PlayerPrefs.SetInt("Level", 29);
            PlayerPrefs.SetInt("Upgrade Points", 0);
            PlayerPrefs.SetInt("Weapon", 8);
            PlayerPrefs.Save();
        }
        else if (type > 8) {
            upgradePoints = upgradePoints - weaponStats[type - 1].weaponPickup.weaponPrice;
            GetComponent<SoundManager>().PlaySound(12);
            PinkIsTheNewEvil.EnemySpawner.weapons[(type - 1) - 1].SetActive(false);

            if (type == 9) {
                speedPerkIcon.SetActive(true);
                speedPerkBack.SetActive(true);
                animator.SetFloat("specAttack1speed", 0.5f);
                specialAttack1Data.cooldown += 2;
            }
            else if (type == 10) {
                jumpPerkIcon.SetActive(true);
                jumpPerkBack.SetActive(true);
                specialAttack2Data.cooldown /= 3;
            }
            else if (type == 11) {
                healthPerkIcon.SetActive(true);
                healthPerkBack.SetActive(true);
                targetHealth = targetHealth * 2;
            }
        }
        else {
            upgradePoints = upgradePoints - weaponStats[type - 1].weaponPickup.weaponPrice;
            GetComponent<SoundManager>().PlaySound(12);
            for (int i = 0; i < type - 1; i++) {
                PinkIsTheNewEvil.EnemySpawner.weapons[i].SetActive(false);
            }
            setAttackMode(type);
        }
    }

    void CreatePoofBuyWeapon(int type) {
        int poof2Target = type - 1;

        if (type > 8)
            poof2Target = 8 - 1;

        GameObject poof1 = Instantiate(poofWeap, PinkIsTheNewEvil.EnemySpawner.weaponModels[(type - 1) - 1].transform.position, poofWeap.transform.rotation);
        GameObject poof2 = Instantiate(poofPLay, weaponStats[poof2Target].weaponModel.transform.position, poofPLay.transform.rotation);
        poof1.transform.parent = null;
        Destroy(poof1, 4);
        Destroy(poof2, 4);

        if (type > 8) {
            poof1.transform.localScale *= 3;
            poof2.transform.localScale *= 3;
        }
    }

    void ClearWeaponBuyPopUp() {
        backimage1.SetActive(false);
        price.SetActive(false);
        buybutton.SetActive(false);
        buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    void GetRidOfNotEnoughPrompt() {
        backgroundNotEnough.SetActive(false);
        notEnoughPrompt.SetActive(false);
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
                specialAttack1LastTrig = Time.time;
            }
        }
        else if (mode == 2) {
            specialAttackMode = 2;
            powerUpIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
            if (isSpecAttack2 == true) {
                specialAttack2LastTrig = Time.time;
            }
        }

        isFadingOrange = true;
        fadingOrangeTimeStart = Time.time;
        powerUpIcon.GetComponent<RectTransform>().position = new Vector3(orangeCircle.GetComponent<RectTransform>().position.x, orangeCircle.GetComponent<RectTransform>().position.y, 0);
        powerUpIcon.SetActive(true);

        if ((isSpecAttack1 == false && specialAttackMode == 1) || (isSpecAttack2 == false && specialAttackMode == 2)) {
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
            else if (finalPostion.x < 0 + 10) {
                finalPostion.x = 10;
            }

            if (finalPostion.y > screenHeight - 10) {
                finalPostion.y = screenHeight - 10;
            }
            else if (finalPostion.y < 0 + 10) {
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

    void PlayerSpecialAttackLogic2() {
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
                else {
                    float distanceToCurrentEnemy = Vector3.Distance(scanPoint, PinkIsTheNewEvil.EnemySpawner.listOfAllEnemies[i].transform.position);
                    float distanceToStoredEnemy = Vector3.Distance(scanPoint, specialAttackEnemyTarget.transform.position);

                    if (distanceToCurrentEnemy < distanceToStoredEnemy)
                        specialAttackEnemyTarget = PinkIsTheNewEvil.EnemySpawner.listOfAllEnemies[i];
                }
            }

            if (specialAttackEnemyTarget != null) {
                if (Vector3.Distance(scanPoint, specialAttackEnemyTarget.transform.position) > 6) {
                    specialAttackEnemyTarget = null;
                }
            }

            if (specialAttackEnemyTarget != null) {
                ClearWeaponBuyPopUp();
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
                specialAttack2LastTrig = Time.time - (specialAttack2Data.cooldown - 1f);
                jumpAttackLastTrigRESET = false;
            }
        }
    }

    void PlayerSpecialAttackLogic1() {
        if (specialAttackPhase == 1) {
            Vector3 playerCurrentLocationInScreenSpace = gameCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position);
            Vector3 virtualJoystickDirection = playerCurrentLocationInScreenSpace - orangeCircle.GetComponent<Transform>().position;
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
            gameObject.layer = LayerMask.NameToLayer("Player Special 1");

            if (attackMode == 7 || attackMode == 8)
                trailParticle.transform.position = shockwavespawn2.position;
            else
                trailParticle.transform.position = shockwavespawn.position;

            trailParticle.transform.SetParent(this.transform);
            Vector3 newtrailPos = trailParticle.transform.localPosition;
            newtrailPos.y = newtrailPos.y - 0.3f;
            newtrailPos.z = newtrailPos.z - 1.25f;

            trailParticle.transform.localPosition = newtrailPos;
            trailParticle.transform.localScale = new Vector3(1.2f, 1.2f, .65f);
            trailParticle.transform.localRotation = Quaternion.Euler(0, 0, 0);
            trailParticle.GetComponent<ParticleSystem>().Play();

            specialAttackPhase = 4;
        }
        else if (specialAttackPhase == 4) {
            playerCollider.SimpleMove((transform.forward * 500) * Time.fixedDeltaTime);

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
            gameObject.layer = LayerMask.NameToLayer("Player");
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

    // Can't keep track anymore of all that this function does, left untouched 😳
    void fadeAwayOrangeGroup() {
        if (isFadingOrange == false)
            return;

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

    void SpecialAttackTimerLogic() {
        if (playerPowerLevel < 1) {
            isSpecAttack1 = false;
        }
        else if ((Time.time - specialAttack1LastTrig) > specialAttack1Data.cooldown) {
            isSpecAttack1 = true;
        }
        else if ((Time.time - specialAttack1LastTrig) < specialAttack1Data.cooldown) {
            isSpecAttack1 = false;
        }

        if (playerPowerLevel < 2) {
            isSpecAttack2 = false;
        }
        else if ((Time.time - specialAttack2LastTrig) > specialAttack2Data.cooldown) {
            isSpecAttack2 = true;
        }
        else if ((Time.time - specialAttack2LastTrig) < specialAttack2Data.cooldown) {
            isSpecAttack2 = false;
        }

        FadeSpecialAttackIndicators(specialAttack1Data);
        FadeSpecialAttackIndicators(specialAttack2Data);
    }

    void FadeSpecialAttackIndicators(SpecialAttackData specialAttackData) {
        float lastTrig;

        if (specialAttackData.mode == 1) {
            lastTrig = specialAttack1LastTrig;
        }
        else {
            lastTrig = specialAttack2LastTrig;
        }

        float fadeLerp = (Time.time - lastTrig) / specialAttackData.cooldown;
        Color circleColor = specialAttackData.indicatorCircle.GetComponent<Image>().color;

        if (playerPowerLevel < specialAttackData.mode) {
            circleColor.a = 0;
            specialAttackData.indicatorCircleGlow.SetActive(false);
            specialAttackData.indicatorIconGlow.SetActive(false);
        }
        else if (fadeLerp > 1) {
            circleColor.a = 1;
            specialAttackData.indicatorCircleGlow.SetActive(true);
            specialAttackData.indicatorIconGlow.SetActive(true);
        }
        else {
            circleColor.a = Mathf.Lerp(0, 1, fadeLerp) - .2f;
            specialAttackData.indicatorCircleGlow.SetActive(false);
            specialAttackData.indicatorIconGlow.SetActive(false);
        }

        specialAttackData.indicatorCircle.GetComponent<Image>().color = circleColor;
        specialAttackData.indicatorIcon.GetComponent<Image>().color = circleColor;
    }

    Vector3 VirtualJoystick() {
        if (isControlOff == true)
            return Vector3.zero;

        float rawHorizontalAxisKeyboard = Input.GetAxis("HorizontalKeyboard");
        float rawVerticalAxisKeyboard = Input.GetAxis("VerticalKeyboard");
        float rawHorizontalAxisJoystick = Input.GetAxis("HorizontalJoystick");
        float rawVerticalAxisJoystick = Input.GetAxis("VerticalJoystick");

        float rawJoystickAxisLowerLimit = 0.5f;

        if (rawHorizontalAxisJoystick > 0f && rawHorizontalAxisJoystick < rawJoystickAxisLowerLimit) {
            rawHorizontalAxisJoystick = rawJoystickAxisLowerLimit;
        }
        else if (rawHorizontalAxisJoystick < 0f && rawHorizontalAxisJoystick > -rawJoystickAxisLowerLimit) {
            rawHorizontalAxisJoystick = -rawJoystickAxisLowerLimit;
        }

        if (rawVerticalAxisJoystick > 0f && rawVerticalAxisJoystick < rawJoystickAxisLowerLimit) {
            rawVerticalAxisJoystick = rawJoystickAxisLowerLimit;
        }
        else if (rawVerticalAxisJoystick < 0f && rawVerticalAxisJoystick > -rawJoystickAxisLowerLimit) {
            rawVerticalAxisJoystick = -rawJoystickAxisLowerLimit;
        }

        if (rawHorizontalAxisJoystick != 0 || rawVerticalAxisJoystick != 0) {
            Cursor.visible = false;
        }
        else if (Input.mousePosition != lastMousePosition) {
            Cursor.visible = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (rawHorizontalAxisKeyboard != 0 || rawVerticalAxisKeyboard != 0) {
            Cursor.visible = true;
        }

        float horizontalAxis;
        float verticalAxis;

        if (rawHorizontalAxisJoystick > 0 && rawHorizontalAxisKeyboard > 0) {
            horizontalAxis = Mathf.Max(rawHorizontalAxisJoystick, rawHorizontalAxisKeyboard);
        }
        else if (rawHorizontalAxisJoystick < 0 && rawHorizontalAxisKeyboard < 0) {
            horizontalAxis = Mathf.Min(rawHorizontalAxisJoystick, rawHorizontalAxisKeyboard);
        }
        else {
            if (rawHorizontalAxisJoystick != 0) {
                horizontalAxis = rawHorizontalAxisJoystick;
            }
            else {
                horizontalAxis = rawHorizontalAxisKeyboard;
            }
        }

        if (rawVerticalAxisJoystick > 0 && rawVerticalAxisKeyboard > 0) {
            verticalAxis = Mathf.Max(rawVerticalAxisJoystick, rawVerticalAxisKeyboard);
        }
        else if (rawVerticalAxisJoystick < 0 && rawVerticalAxisKeyboard < 0) {
            verticalAxis = Mathf.Min(rawVerticalAxisJoystick, rawVerticalAxisKeyboard);
        }
        else {
            if (rawVerticalAxisJoystick != 0) {
                verticalAxis = rawVerticalAxisJoystick;
            }
            else {
                verticalAxis = rawVerticalAxisKeyboard;
            }
        }

        Vector3 virtualJoystickDirection = new Vector3(horizontalAxis, 0, verticalAxis);
        virtualJoystickDirection = Quaternion.Euler(screenRotationCorrection) * virtualJoystickDirection;
        virtualJoystickDirection = Vector3.ClampMagnitude(virtualJoystickDirection, 1f);

        CurrrentJoystickDirection = Vector3.SmoothDamp(CurrrentJoystickDirection, virtualJoystickDirection, ref refoutvar, 0.1f);

        Vector3 LowerClampedJoystickDirection = CurrrentJoystickDirection;

        if (LowerClampedJoystickDirection.x < LowerLimit && LowerClampedJoystickDirection.x > -LowerLimit) {
            LowerClampedJoystickDirection.x = 0f;
        }

        if (LowerClampedJoystickDirection.y < LowerLimit && LowerClampedJoystickDirection.y > -LowerLimit) {
            LowerClampedJoystickDirection.y = 0f;
        }

        if (LowerClampedJoystickDirection.z < LowerLimit && LowerClampedJoystickDirection.z > -LowerLimit) {
            LowerClampedJoystickDirection.z = 0f;
        }

        return LowerClampedJoystickDirection;
    }

    public void attackDamageTickedSet() {
        EnemiesInAttackArea cylindertargetarea = cylinderTargetArea.GetComponent<EnemiesInAttackArea>();
        EnemiesInAttackArea boxtargetarea = boxTargetArea.GetComponent<EnemiesInAttackArea>();
        attackDamageTicked = true;

        if (attackMode == 7 || attackMode == 8)
            shockwavespawn = shockwavespawn2;

        switch (attackMode) {
            case 1:
                if (Vector3.Distance(transform.position, enemyTarget.transform.position) <= attackRange)
                    enemyTarget.GetComponent<EnemyAI>().Health(damageAmount);
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                foreach (GameObject target in listOfTargets) {
                    if (cylindertargetarea.enemiesWithinArea.Contains(target) == true && Vector3.Distance(transform.position, target.transform.position) <= attackRange)
                        target.GetComponent<EnemyAI>().Health(damageAmount);
                }
                break;
            case 6:
            case 7:
            case 8:
                GameObject shockwaveInstance = Instantiate(shockwave, shockwavespawn.position, shockwave.transform.rotation);
                shockwaveInstance.transform.localScale = new Vector3(.4f, .4f, .2f);
                Destroy(shockwaveInstance, 1);
                foreach (GameObject target in listOfTargets) {
                    if (boxtargetarea.enemiesWithinArea.Contains(target) == true && Vector3.Distance(transform.position, target.transform.position) <= attackRange)
                        target.GetComponent<EnemyAI>().Health(damageAmount);
                }
                break;
        }
    }

    public void setAttackIsDone() {
        attackOnCooldown = 1;
        attackIsDone = true;
        amAttackingRightNow = false;
        Invoke("AttackCooldownManager", attackCooldown);
    }

    void AttackCooldownManager() {
        attackOnCooldown = 0;
    }

    void MovePlayer(Vector3 direction) {
        float leftRightIdentifier;
        float angle;

        playerCollider.SimpleMove((direction * moveSpeed) * Time.fixedDeltaTime);

        if (enemyTarget != null) {
            AimAtEnemyLogic(direction, out angle, out leftRightIdentifier);
        }
        else {
            isAttacking = 0;
            transformToShake.localPosition = Vector3.zero;
            angle = 0;
            leftRightIdentifier = 0;
        }

        if (((Input.GetButton("Fire3") == true && isControlOn == true && isControlOff == false && Time.timeScale != 0) && attackOnCooldown == 0) || amAttackingRightNow == true) {
            AttackingAnimLogic();
        }
        else {
            if (enemyTarget != null)
                amAttackingRightNow = false;

            if (direction == new Vector3(0, 0, 0)) {
                IdleAnimLogic();
            }
            else if (angle < 135f && angle > 45f) {
                if (leftRightIdentifier < 0)
                    WalkLeftAnimLogic();
                else if (leftRightIdentifier > 0)
                    WalkRightAnimLogic();
            }
            else if (angle < 45f) {
                WalkForwardAnimLogic(direction);
            }
            else if (angle > 135f) {
                WalkBackwardsAnimLogic();
            }
        }

    }

    void AimAtEnemyLogic(Vector3 direction, out float angle, out float leftRightIdentifier) {
        Quaternion rotateTowards = Quaternion.LookRotation(enemyTarget.transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTowards, playerLookAtRotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        Vector3 enemyAim = enemyTarget.transform.position - transform.position;
        enemyAim = new Vector3(enemyAim.x, 0f, enemyAim.z).normalized;
        Vector3 enemyWalkPerpendicular = Vector3.Cross(enemyAim, direction);

        angle = Vector3.Angle(enemyAim, direction);
        leftRightIdentifier = Vector3.Dot(enemyWalkPerpendicular, Vector3.up);
    }

    void IdleAnimLogic() {
        if (attackMode == 6 || attackMode == 7 || attackMode == 8)
            AnimSwitchTo("goToIdle3");
        else
            AnimSwitchTo("goToIdle");
    }

    void WalkLeftAnimLogic() {
        if (attackMode == 6 || attackMode == 7 || attackMode == 8)
            AnimSwitchTo("goToWalkLeft3");
        else
            AnimSwitchTo("goToWalkLeft");
    }

    void WalkRightAnimLogic() {
        if (attackMode == 6 || attackMode == 7 || attackMode == 8)
            AnimSwitchTo("goToWalkRight3");
        else
            AnimSwitchTo("goToWalkRight");
    }

    void WalkForwardAnimLogic(Vector3 direction) {
        Quaternion rotateTowards = Quaternion.LookRotation((transform.position + direction) - transform.position);

        if (enemyTarget == null)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTowards, playerLookAtRotateSpeed * Time.deltaTime);

        if (attackMode == 6 || attackMode == 7 || attackMode == 8)
            AnimSwitchTo("goToWalkForward3");
        else
            AnimSwitchTo("goToWalkForward");
    }

    void WalkBackwardsAnimLogic() {
        if (attackMode == 6 || attackMode == 7 || attackMode == 8)
            AnimSwitchTo("goToWalkBack3");
        else
            AnimSwitchTo("goToWalkBack");
    }

    void AttackingAnimLogic() {
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

    void UpdateTarget() {
        if (listOfTargets.Count == 0) {
            enemyTarget = null;
            return;
        }

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
        }
    }

    void AnimSwitchTo(string trigger) {
        if (trigger == "goToAttack")
            setAnimatorBoolCluster("Attack1");
        else if (trigger == "goToAttack2")
            setAnimatorBoolCluster("Attack2");
        else if (trigger == "goToAttack3")
            setAnimatorBoolCluster("Attack3");
        else if (trigger == "isDead")
            setAnimatorBoolCluster("Death");
        else if (trigger == "goToWalkRight")
            setAnimatorBoolCluster("RunningL");
        else if (trigger == "goToWalkBack")
            setAnimatorBoolCluster("RunningB");
        else if (trigger == "goToWalkForward")
            setAnimatorBoolCluster("RunningF");
        else if (trigger == "goToWalkLeft")
            setAnimatorBoolCluster("RunningR");
        else if (trigger == "goToIdle3")
            setAnimatorBoolCluster("Idle3");
        else if (trigger == "goToWalkForward3")
            setAnimatorBoolCluster("RunningF3");
        else if (trigger == "goToWalkRight3")
            setAnimatorBoolCluster("RunningL3");
        else if (trigger == "goToWalkLeft3")
            setAnimatorBoolCluster("RunningR3");
        else if (trigger == "goToWalkBack3")
            setAnimatorBoolCluster("RunningB3");
        else if (trigger == "goToIdle")
            setAnimatorBoolCluster("Idle");
        else if (trigger == "SpecAttack1")
            setAnimatorBoolCluster("SpecAttack1");
        else if (trigger == "SpecAttack3")
            setAnimatorBoolCluster("SpecAttack3");
        else if (trigger == "Ascending")
            setAnimatorBoolCluster("Ascending");
        else if (trigger == "FallingFromSky")
            setAnimatorBoolCluster("FallingFromSky");
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackPunch"))
            return "goToAttack";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attackBat_001"))
            return "goToAttack2";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_3"))
            return "goToAttack3";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("die"))
            return "isDead";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningL"))
            return "goToWalkRight";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningB"))
            return "goToWalkBack";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningF"))
            return "goToWalkForward";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningR"))
            return "goToWalkLeft";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Standing_Idle"))
            return "goToIdle3";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running_Forward"))
            return "goToWalkForward3";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running_Left"))
            return "goToWalkRight3";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running_Right"))
            return "goToWalkLeft3";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running_Backwards"))
            return "goToWalkBack3";
        else
            return "goToIdle";
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