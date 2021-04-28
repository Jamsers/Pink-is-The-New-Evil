using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public EnemySpawner systemsProcSpawn;

    public LayerMask myLayerMask;

    public GameObject[] highScoreName;

    public GameObject[] highScore;

    public GameObject leburhighsc;
    public GameObject newname;
    public GameObject leburhighscnum;
    public GameObject pinklight;

    public Transform transformToShake;

    // Health()
    public int currenthealth = 100;

    //bool triggerTriggered;

    public GameObject cylinderTargetArea;
    public GameObject boxTargetArea;
    public Transform shockwavespawn;
    public Transform shockwavespawn2;
    public GameObject shockwave;

    // Attack()
    int amAttacking = 0;

    public GameObject pointNumbers;

    public float attackAnimLength;

    public Transform bloodParticleTransform;
    public GameObject particle1prefab;
    public GameObject particle2prefab;

    // VirtualJoystick()
    bool initialPressStored = false;
    Vector3 initialPressPosition = new Vector3(0, 0, 0);
    public Vector3 screenRotationCorrection;
    public Vector3 storedMoveDirection;

    // MovePlayer()
    CharacterController playerCollider;
    public int moveSpeed;
    public float attackRange;
    public float attackCooldown;
    int attackOnCooldown = 0;
    //float prevTime;
    int isAttacking = 0;
    int hasDamaged;

    public float shakeAmount;

    public float attackLength;
    //float attackLengthDamage;

    bool attackDamageTicked = false;
    bool attackIsDone = false;

    public int upgradePoints;

    public bool amAttackingRightNow = false;
    //bool prevAmAttackingRightNow = false;

    public int attackMode;

    public GameObject healthBloodOnScreen;
    public GameObject hudCanvas;

    // UpdateTarget()
    List<GameObject> listOfTargets = new List<GameObject>();
    GameObject enemyTarget = null;

    // AnimSwitchTo()
    //string lastTrigger = "null";
    Animator animator;

    public void ResumeSkyfall () {
        animator.SetFloat("fallFromSkySpeed", 1);
    }

    bool isDead = false;

    public bool isControlOff = true;

    int damageAmount;

    Vector3 moveUpperCubeorigpos = new Vector3(999, 999, 999);

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

    void CheckIfInHighscores () {
        int[] currentHighscores = new int[8] {PlayerPrefs.GetInt("High Score 1"), PlayerPrefs.GetInt("High Score 2"), PlayerPrefs.GetInt("High Score 3"), PlayerPrefs.GetInt("High Score 4"), PlayerPrefs.GetInt("High Score 5"), PlayerPrefs.GetInt("High Score 6"), PlayerPrefs.GetInt("High Score 7"), PlayerPrefs.GetInt("High Score 8"),};
        string[] currentHighscoresNames = new string[8] {PlayerPrefs.GetString("High Score Name 1"), PlayerPrefs.GetString("High Score Name 2"), PlayerPrefs.GetString("High Score Name 3"), PlayerPrefs.GetString("High Score Name 4"), PlayerPrefs.GetString("High Score Name 5"), PlayerPrefs.GetString("High Score Name 6"), PlayerPrefs.GetString("High Score Name 7"), PlayerPrefs.GetString("High Score Name 8"), };

        for (int i = 0; i < 8; i++) {
            if (upgradePoints > currentHighscores[i]) {
                leburhighsc.gameObject.SetActive(true);
                newname.gameObject.SetActive(true);
                leburhighscnum.gameObject.SetActive(true);
                newHighScore.SetActive(true);
                leburhighscnum.GetComponent<Text>().text = upgradePoints.ToString();
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().saveHighScore = true;
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().highscorepalce = i;
                break;
            }
        }
    }

    void fillInHighScores () {
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
            if (isControlOn == true) {
                currenthealth = currenthealth + value;
                Vector3 bloodpos = new Vector3(transform.position.x, transform.position.y + .6f, transform.position.z);
                GameObject particle = (GameObject)Instantiate(particle2prefab, bloodpos, particle2prefab.transform.rotation);
                Destroy(particle, 1.0f);
                GetComponent<SoundManager>().PlaySound(7);
            }
            if (currenthealth <= 0) {
                isDead = true;
                //AnimSwitchTo("isDead");
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

    void HealthRegeneration () {
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

    float mouseSpecDistanceThresholdreplace;
    void Start() {
        
        fillInHighScores();
        if (PlayerPrefs.HasKey("Upgrade Points"))
            upgradePoints = PlayerPrefs.GetInt("Upgrade Points");
            //upgradePoints = 100000;
        else
            upgradePoints = 0;
        HealthRegeneration();
        playerCollider = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //InvokeRepeating("UpdateTarget", .25f, .25f);
        //prevTime = Time.time;

        if (PlayerPrefs.HasKey("Weapon"))
            setAttackMode(PlayerPrefs.GetInt("Weapon"));
            //setAttackMode(8);
        else
            setAttackMode(1);

        origOrangeCircle = orangeCircle;

        //Debug.Log(systemsProcSpawn.level);
        /*if (systemsProcSpawn.level == 29) {
            Debug.Log("hello");
            isFallingFromSky = true;
        }*/

        trailParticle = (GameObject)Instantiate(trailparticle, shockwavespawn.position, shockwave.transform.rotation);

        var main = trailParticle.GetComponent<ParticleSystem>().main;
        var emission = trailParticle.GetComponent<ParticleSystem>().emission;

        main.loop = true;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        //main.startSpeed = 6.5f;
        main.startSpeed = 3f;
        main.startSize = 0.65f;

        emission.rateOverTime = 200f;

        trailParticle.GetComponent<ParticleSystem>().Stop();

        if (PlayerPrefs.GetInt("Level") == 29)
        {
            modeltoRepl.GetComponent<Renderer>().sharedMaterial = playerpinkTrim;
        }
    }

    public Material playerpinkTrim;

    int targetHealth = 100;

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
            //attackLengthDamage = attackLength * 0.50f;
            //animator.SetFloat("attackSpeed", 0.542f / attackLength);
            attackMode = 1;
            attackRange = 1.2f;
            damageAmount = -30;
            attackCooldown = .5f;
            disableOtherWeapons();
        }
        else if (mode == 2) {
            attackLength = .5f;
            //attackLengthDamage = attackLength * 0.73f;
            //animator.SetFloat("attackSpeed", 0.667f / attackLength);
            attackMode = 2;
            attackRange = 1.6f;
            damageAmount = -50;
            attackCooldown = 1f;
            disableOtherWeapons();
            weaponModel2.SetActive(true);
        }
        else if (mode == 3) {
            attackLength = 0.5f;
            //attackLengthDamage = attackLength * 0.73f;
            //animator.SetFloat("attackSpeed", 0.667f / attackLength);
            attackMode = 3;
            attackRange = 1.7f;
            damageAmount = -90;
            attackCooldown = 1f;
            disableOtherWeapons();
            weaponModel3.SetActive(true);
        }
        else if (mode == 4) {
            attackLength = 0.5f;
            //attackLengthDamage = attackLength * 0.73f;
            //animator.SetFloat("attackSpeed", 0.667f / attackLength);
            attackMode = 4;
            attackRange = 1.6f;
            damageAmount = -150;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel4.SetActive(true);
        }
        else if (mode == 5) {
            attackLength = 0.5f;
            //attackLengthDamage = attackLength * 0.73f;
            //animator.SetFloat("attackSpeed", 0.667f / attackLength);
            attackMode = 5;
            attackRange = 2f;
            damageAmount = -300;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel5.SetActive(true);
        }
        else if (mode == 6) {
            attackLength = 1f;
            //attackLengthDamage = attackLength * 0.73f;
            //animator.SetFloat("attackSpeed", 0.875f / attackLength);
            attackMode = 6;
            attackRange = 2.9f;
            damageAmount = -600;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel6.SetActive(true);
        }
        else if (mode == 7) {
            attackLength = 1f;
            //attackLengthDamage = attackLength * 0.73f;
            //animator.SetFloat("attackSpeed", 0.875f / attackLength);
            attackMode = 7;
            attackRange = 3.9f;
            damageAmount = -800;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel7.SetActive(true);
        }
        else if (mode == 8) {
            attackLength = 1f;
            //attackLengthDamage = attackLength * 0.73f;
            //animator.SetFloat("attackSpeed", 0.875f / attackLength);
            attackMode = 8;
            attackRange = 3.9f;
            damageAmount = -1200;
            attackCooldown = .5f;
            disableOtherWeapons();
            weaponModel8.SetActive(true);
            if (PlayerPrefs.GetInt("Level") == 29)
            {
                weaponModel8.GetComponent<MeshRenderer>().material = weapon8pinktrim;
            }
        }
    }

    public Material weapon8pinktrim;

    public GameObject gayPoofWeap;
    public GameObject gayPoofPLay;

    public GameObject backimage1;
    public GameObject price;
    public GameObject buybutton;

    public void BuyWeapon(int type) {
        //Debug.Log(type);
        //BRUH
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

                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon1Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel2.transform.position, gayPoofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                systemsProcSpawn.weapon1.SetActive(false);
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

                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon2Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel3.transform.position, gayPoofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                systemsProcSpawn.weapon1.SetActive(false);
                systemsProcSpawn.weapon2.SetActive(false);
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

                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon3Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel4.transform.position, gayPoofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                systemsProcSpawn.weapon1.SetActive(false);
                systemsProcSpawn.weapon2.SetActive(false);
                systemsProcSpawn.weapon3.SetActive(false);
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

                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon4Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel5.transform.position, gayPoofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                systemsProcSpawn.weapon1.SetActive(false);
                systemsProcSpawn.weapon2.SetActive(false);
                systemsProcSpawn.weapon3.SetActive(false);
                systemsProcSpawn.weapon4.SetActive(false);
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

                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon5Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel6.transform.position, gayPoofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                systemsProcSpawn.weapon1.SetActive(false);
                systemsProcSpawn.weapon2.SetActive(false);
                systemsProcSpawn.weapon3.SetActive(false);
                systemsProcSpawn.weapon4.SetActive(false);
                systemsProcSpawn.weapon5.SetActive(false);
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

                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon6Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel7.transform.position, gayPoofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                systemsProcSpawn.weapon1.SetActive(false);
                systemsProcSpawn.weapon2.SetActive(false);
                systemsProcSpawn.weapon3.SetActive(false);
                systemsProcSpawn.weapon4.SetActive(false);
                systemsProcSpawn.weapon5.SetActive(false);
                systemsProcSpawn.weapon6.SetActive(false);
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

                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon7Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel8.transform.position, gayPoofPLay.transform.rotation);
                poof1.transform.parent = null;
                Destroy(poof1, 4);
                Destroy(poof2, 4);
                systemsProcSpawn.weapon1.SetActive(false);
                systemsProcSpawn.weapon2.SetActive(false);
                systemsProcSpawn.weapon3.SetActive(false);
                systemsProcSpawn.weapon4.SetActive(false);
                systemsProcSpawn.weapon5.SetActive(false);
                systemsProcSpawn.weapon6.SetActive(false);
                systemsProcSpawn.weapon7.SetActive(false);
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
            if (systemsProcSpawn.level == 29 && upgradePoints >= 70000) {
                upgradePoints = upgradePoints - 70000;
                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon8Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel8.transform.position, gayPoofPLay.transform.rotation);
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
                systemsProcSpawn.weapon8.SetActive(false);
                animator.SetFloat("specAttack1speed", 0.5f);
                speedAttackCooldown = speedAttackCooldown + 2;
                //longer speed
            }
            else if (upgradePoints >= 100000) {
                upgradePoints = upgradePoints - 100000;
                PlayerPrefs.SetInt("Level", 29);
                PlayerPrefs.SetInt("Upgrade Points", 0);
                PlayerPrefs.SetInt("Weapon", 8);
                //PlayerPrefs.SetInt("Weapon", 8);
                PlayerPrefs.Save();
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().hud.SetActive(false);
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().gamePause.SetActive(false);
                //Health(-100);
                ascencionScreen.SetActive(true);
                systemsProcSpawn.constantlyDenyInput = true;
                //death logic
                //make player invul
                //ASCENCION, PLAY ANIMATION
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
            if (systemsProcSpawn.level == 29 && upgradePoints >= 90000) {
                upgradePoints = upgradePoints - 90000;
                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon7Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel8.transform.position, gayPoofPLay.transform.rotation);
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
                systemsProcSpawn.weapon9.SetActive(false);
                jumpAttackCooldown = jumpAttackCooldown / 3;
                //faster jump
            }
            else if (upgradePoints >= 100000) {
                upgradePoints = upgradePoints - 100000;
                PlayerPrefs.SetInt("Level", 29);
                PlayerPrefs.SetInt("Upgrade Points", 0);
                PlayerPrefs.SetInt("Weapon", 8);
                //PlayerPrefs.SetInt("Weapon", 8);
                PlayerPrefs.Save();
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().hud.SetActive(false);
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().gamePause.SetActive(false);
                //Health(-100);
                ascencionScreen.SetActive(true);
                systemsProcSpawn.constantlyDenyInput = true;
                //death logic
                //make player invul
                //ASCENCION, PLAY ANIMATION
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
            if (systemsProcSpawn.level == 29 && upgradePoints >= 30000) {
                upgradePoints = upgradePoints - 30000;
                GameObject poof1 = Instantiate(gayPoofWeap, systemsProcSpawn.weapon7Model.transform.position, gayPoofWeap.transform.rotation);
                GameObject poof2 = Instantiate(gayPoofPLay, weaponModel8.transform.position, gayPoofPLay.transform.rotation);
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
                systemsProcSpawn.weapon10.SetActive(false);
                targetHealth = targetHealth * 2;
                //double health
            }
            else if (upgradePoints >= 100000) {
                upgradePoints = upgradePoints - 100000;
                PlayerPrefs.SetInt("Level", 29);
                PlayerPrefs.SetInt("Upgrade Points", 0);
                PlayerPrefs.SetInt("Weapon", 8);
                //PlayerPrefs.SetInt("Weapon", 8);
                PlayerPrefs.Save();
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().hud.SetActive(false);
                GameObject.FindGameObjectWithTag("Systems Process").GetComponent<MainSystems>().gamePause.SetActive(false);
                //Health(-100);
                ascencionScreen.SetActive(true);
                systemsProcSpawn.constantlyDenyInput = true;
                //death logic
                //make player invul
                //ASCENCION, PLAY ANIMATION
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
            modeltoRepl.GetComponent<Renderer>().sharedMaterial = pinkCancer;
            pinklight.SetActive(true);
        }
    }

    public GameObject pinkCaner;
    public GameObject modeltoRepl;
    public Material pinkCancer;

    bool isAscending = false;

    public GameObject ascencionScreen;

    public GameObject healthPerkIcon;
    public GameObject healthPerkBack;
    public GameObject speedPerkIcon;
    public GameObject speedPerkBack;
    public GameObject jumpPerkIcon;
    public GameObject jumpPerkBack;
    
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

    public bool isControlOn = true;

    bool isFallingFromSky = false;

    bool hasDoneIt = false;

    public void SpawnSmokeForFall () {
        GameObject shocking = Instantiate(shockwavePlayer2);
        shocking.transform.position = new Vector3(transform.position.x, transform.position.y + .4f, transform.position.z);
        Destroy(shocking, 4);
    }

    public void StopSkyfall () {
        isFallingFromSky = false;
    }

    void SpecialAttackManual(int mode)
    {
        if (orangeCircle.GetComponent<Transform>().position == startLocationOfMouseDown && orangeCircle.activeSelf == false)
        {
            specialAttackAim = Input.mousePosition;
            CancelInvoke("VanishAndResetOrangeCircle");
            orangeCircle.SetActive(true);
        }
        else
        {
            specialAttackAim = orangeCircle.GetComponent<Transform>().position;
        }

        orangeCircle.GetComponent<Transform>().position = specialAttackAim;

        isSpecialAttackUnderWay = true;
        SpecialPhase1 = false;
        SpecialPhase1_5 = false;
        SpecialPhase2 = false;
        SpecialPhase2_5 = false;
        if (mode == 1)
        {
            specialAttackMode = 1;
            powerUpIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            if (isSpecAttack2 == true)
            {
                speedAttackLastTrig = Time.time;
            }
        }
        else
        {
            specialAttackMode = 2;
            powerUpIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
            if (isSpecAttack1 == true)
            {
                jumpAttackLastTrig = Time.time;
            }
        }
        isFadingOrange = true;
        fadingOrangeTimeStart = Time.time;

        powerUpIcon.GetComponent<RectTransform>().position = new Vector3(orangeCircle.GetComponent<RectTransform>().position.x, orangeCircle.GetComponent<RectTransform>().position.y, 0);
        powerUpIcon.SetActive(true);

        if (isSpecAttack2 == false && specialAttackMode == 1)
        {
            isSpecialAttackUnderWay = false;
            powerUpIcon.SetActive(false);
            orangeCircle.SetActive(false);
            CancelInvoke("removeOrangeCircle");
        }
        else if (isSpecAttack1 == false && specialAttackMode == 2)
        {
            isSpecialAttackUnderWay = false;
            powerUpIcon.SetActive(false);
            orangeCircle.SetActive(false);
            CancelInvoke("removeOrangeCircle");
        }
    }

    void updateJoystickAim()
    {
        int screenWidth = gameCamera.GetComponent<Camera>().pixelWidth;
        int screenHeight = gameCamera.GetComponent<Camera>().pixelHeight;

        Vector3 screenCenter = new Vector3(screenWidth * 0.5f, screenHeight * 0.65f, 0);

        float aimHorizontal = Input.GetAxis("HorizontalAim");
        float aimVertical = Input.GetAxis("VerticalAim");

        Vector3 virtualJoystickDirection = new Vector3(aimHorizontal, aimVertical, 0);

        virtualJoystickDirection = Quaternion.Euler(screenRotationCorrection) * virtualJoystickDirection;
        virtualJoystickDirection = Vector3.ClampMagnitude(virtualJoystickDirection, 1f);

        float joystickAimMoveSpeed = 1500f;
        float resetTime = 0.75f;

        if (aimHorizontal == 0 && aimVertical == 0)
        {
            Invoke("VanishAndResetOrangeCircle", resetTime);
        }
        else
        {
            CancelInvoke("VanishAndResetOrangeCircle");

            Vector3 finalPostion = orangeCircle.GetComponent<Transform>().position + ((virtualJoystickDirection * joystickAimMoveSpeed) * Time.deltaTime);

            if (finalPostion.x > screenWidth-10)
            {
                finalPostion.x = screenWidth-10;
            }
            else if (finalPostion.x < 10)
            {
                finalPostion.x = 10;
            }

            if (finalPostion.y > screenHeight-10)
            {
                finalPostion.y = screenHeight-10;
            }
            else if (finalPostion.y < 10)
            {
                finalPostion.y = 10;
            }

            finalPostion.z = 0;

            orangeCircle.SetActive(true);
            orangeCircle.GetComponent<Transform>().position = finalPostion;
        }

    }

    void VanishAndResetOrangeCircle()
    {
        int screenWidth = gameCamera.GetComponent<Camera>().pixelWidth;
        int screenHeight = gameCamera.GetComponent<Camera>().pixelHeight;

        Vector3 screenCenter = new Vector3(screenWidth * 0.5f, screenHeight * 0.65f, 0);

        orangeCircle.GetComponent<Transform>().position = screenCenter;
        orangeCircle.SetActive(false);
    }

    bool isMouseOverButton = false;

    public void setisMouseOverButton(bool setBool)
    {
        isMouseOverButton = setBool;
    }

    void Update() {

        startLocationOfMouseDown = new Vector3(gameCamera.GetComponent<Camera>().pixelWidth * 0.5f, gameCamera.GetComponent<Camera>().pixelHeight * 0.65f, 0);
        mouseSpecDistanceThresholdreplace = gameCamera.GetComponent<Camera>().pixelHeight * 0.0707290533f;

        if ((Input.GetAxis("Fire2Joystick") > 0.5f) && isSpecAttack1 == true && isControlOn == true && isControlOff == false && Time.timeScale != 0)
        {
            if (orangeCircle.transform.position != startLocationOfMouseDown)
            {
                SpecialAttackManual(2);
            }
        }
        else if ((Input.GetAxis("Fire1Joystick") > 0.5f) && isSpecAttack2 == true && isControlOn == true && isControlOff == false && Time.timeScale != 0)
        {
            if (orangeCircle.transform.position != startLocationOfMouseDown)
            {
                SpecialAttackManual(1);
            }
        }
        else if ((Input.GetButtonDown("Fire2") == true) && isSpecAttack1 == true && isControlOn == true && isControlOff == false && Time.timeScale != 0)
        {
            if (isMouseOverButton != true)
            {
                SpecialAttackManual(2);
            } 
        }
        else if ((Input.GetButtonDown("Fire1") == true) && isSpecAttack2 == true && isControlOn == true && isControlOff == false && Time.timeScale != 0)
        {
            if (isMouseOverButton != true)
            {
                SpecialAttackManual(1);
            }
        }

        if (isControlOn == true && isControlOff == false && (isSpecAttack1 == true || isSpecAttack2 == true) && Time.timeScale != 0)
        {
            updateJoystickAim();
        }
        

        if (Time.timeScale == 0) {
            AudioListener.volume = 0;

        }
        else {
            AudioListener.volume = 1;
        }

        //triggerTriggered = false;
        if (moveUpperCubeorigpos == new Vector3(999, 999, 999)) {
            moveUpperCubeorigpos = transformToShake.localPosition;
        }

        if (hasDoneIt == false) {
            if (systemsProcSpawn.level == 29) {
                //Debug.Log("hello");
                isFallingFromSky = true;
            }
            animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            hasDoneIt = true;
        }

        //Debug.Log(isFallingFromSky);

        HealthSplatterUpdate();
        pointNumbers.GetComponent<Text>().text = upgradePoints.ToString();
        UpdateTarget();
        //AttackCooldownManager();
        if (isSpecialAttackUnderWay == false) {
            if (isDead == true) {
                if (systemsProcSpawn.level == 29) {
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
                    //if (Time.timeScale != 0)
                        //PlayerSpecialAttack();
                }
                else if (isControlOn == false) {
                    AnimSwitchTo("goToIdle3");
                    greenCircle.SetActive(false);
                    blueJoystickAim.SetActive(false);
                    backimage1.SetActive(false);
                    price.SetActive(false);
                    buybutton.SetActive(false);
                    buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                    //Debug.Log("hello");
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
            //systemsProcSpawn.constantlyDenyInput = true;
        }

        //Debug.Log(isControlOn);
        //Debug.Log(isSpecialAttackUnderWay);

        fadeAwayOrangeGroup();
        SpecialAttackIndicatorsFadedXDDDD();
        //Debug.Log("SpecialPhase1 = " + SpecialPhase1 + ", SpecialPhase1_5 = " + SpecialPhase1_5 + ", SpecialPhase2 = " + SpecialPhase2 + ", SpecialPhase2_5 = " + SpecialPhase2_5);
        //Debug.Log("mouseup = " + startDurationOfMouseUp + ", mouse down = " + startDurationOfMouseDown);

    }

    public int specialAttackMode;

    bool lastIsMouseDown = false;

    float startDurationOfMouseDown = 0;
    Vector3 startLocationOfMouseDown = new Vector3(0,0,0);

    float startDurationOfMouseUp = 0;
    Vector3 startLocationOfMouseUp = new Vector3(0, 0, 0);

    public float mouseSpecDistanceThreshold;

    bool SpecialPhase1 = false;
    bool SpecialPhase1_5 = false;
    bool SpecialPhase2 = false;
    bool SpecialPhase2_5 = false;

    float spec2_5StartTime;

    public float spec2_5HoldThreshold;


    public float specialAttackTouchTimeDownThreshold;
    public float specialAttackTouchTimeUpThreshold;
    public float specialAttack2_5Threshold;

    int specialAttackPhase = 1;

    public bool isSpecialAttackUnderWay = false;

    List<GameObject> targetsInRange = new List<GameObject>();

    public GameObject raycastSource;

    public GameObject specialAttackAimSphere;

    GameObject specialAttackEnemyTarget;

    float specialAttackFlightStartTime;
    Vector3 specialAttackFlightStartPos;
    float specialAttackFlightTargetTime;
    Vector3 specialAttackFlightTargetPos;

    public CharacterController collider1;
    public SphereCollider collider2;
    public BoxCollider collider3;

    Vector3 scanPoint;

    public void specialAttackPhaseSetPlay(int phase) {
        specialAttackPhase = phase;
    }

    void PlayerSpecialAttackLogic() {
        //Debug.Log(specialAttackPhase);
        if (specialAttackPhase == 1) {
            //Debug.Log("hello1");
            AnimSwitchTo("SpecAttack3");
            isControlOff = true;
            specialAttackPhase = 2;
            Ray ray = GameObject.Find("Game Camera").GetComponent<Camera>().ScreenPointToRay(specialAttackAim);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask);
            scanPoint = hit.point;
            //specialAttackAimSphere.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            //Debug.Log(specialAttackAimSphere.gameObject.GetComponent<SpecialAttackAimLogic>().RetrieveTarget());
        }
        else if (specialAttackPhase == 2) {
            //Debug.Log("hello2");
            //specialAttackEnemyTarget = specialAttackAimSphere.GetComponent<SpecialAttackAimLogic>().RetrieveTarget();

            //MEMELORD EXTREME

            for (int i = 0; i < systemsProcSpawn.listOfAllEnemies.Count; i++) {
                if (specialAttackEnemyTarget == null) {
                    specialAttackEnemyTarget = systemsProcSpawn.listOfAllEnemies[i];
                }
                else if (Vector3.Distance(scanPoint, systemsProcSpawn.listOfAllEnemies[i].transform.position) < Vector3.Distance(scanPoint, specialAttackEnemyTarget.transform.position)) {
                    specialAttackEnemyTarget = systemsProcSpawn.listOfAllEnemies[i];
                }
            }

            if (specialAttackEnemyTarget != null) {
                //Debug.DrawLine(scanPoint, specialAttackEnemyTarget.transform.position, Color.red, 20, false);
                //Debug.Log((Vector3.Distance(scanPoint, specialAttackEnemyTarget.transform.position) < 6));
                if (Vector3.Distance(scanPoint, specialAttackEnemyTarget.transform.position) > 6) {
                    specialAttackEnemyTarget = null;
                }
            }
            //Debug.Log(specialAttackEnemyTarget == null);
            if (specialAttackEnemyTarget != null) {
                backimage1.SetActive(false);
                price.SetActive(false);
                buybutton.SetActive(false);
                buybutton.GetComponent<Button>().onClick.RemoveAllListeners();
                specialAttackPhase = 3;
                transform.LookAt(specialAttackEnemyTarget.transform.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

                //Debug.Log("trigger");
            }
            else {
                //AnimSwitchTo("goToIdle");
                specialAttackPhase = 8;
                isThereNoTargetForJump = true;
                jumpAttackLastTrigRESET = true;
                //Debug.Log("trigger2");
            }
        }
        else if (specialAttackPhase == 3) {
            //Debug.Log("hello3");
            transform.LookAt(specialAttackEnemyTarget.transform.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        else if (specialAttackPhase == 4) {
            //Debug.Log("hello4");
            if (specialAttackEnemyTarget == null) {
                //Debug.Log("FAIL");
                //AnimSwitchTo("goToIdle");
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
                //0.696
                specialAttackFlightTargetPos = specialAttackEnemyTarget.transform.position;

                collider1.enabled = false;
                collider2.enabled = false;
                collider3.enabled = false;

                specialAttackPhase = 5;
            }
        }
        else if (specialAttackPhase == 5) {
           // Debug.Log("hello5");
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
            //Debug.Log("hello6");
            collider1.enabled = true;
            collider2.enabled = true;
            collider3.enabled = true;

            specAttack3IsGo = true;

            GameObject shocking = Instantiate(shockwavePlayer2);
            shocking.transform.position = new Vector3 (transform.position.x, transform.position.y-.4f, transform.position.z);
            Destroy(shocking, 4);

            specialAttackPhase = 7;
        }
        else if (specialAttackPhase == 7) {
           // Debug.Log("hello7");
        }
        else if (specialAttackPhase == 8) {
           // Debug.Log("hello8");
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

    bool jumpAttackLastTrigRESET = false;

    public GameObject shockwavePlayer2;

    bool specAttack3IsGo = false;

    void FixedUpdate () {
        if (specAttack3IsGo == true) {
            transform.position = specialAttackFlightTargetPos;

            GameObject[] listofEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < listofEnemies.Length; i++) {
                float range = 5f;
                if (Vector3.Distance(raycastSource.transform.position, listofEnemies[i].transform.position) < range) {
                    listofEnemies[i].GetComponent<EnemyAI>().Health(damageAmount * 4);
                }
            }

            /*for (int i = 0; i < systemsProcSpawn.listOfAllEnemies.Count; i++) {
                float range = 5f;
                if (Vector3.Distance(raycastSource.transform.position, systemsProcSpawn.listOfAllEnemies[i].transform.position) < range) {
                    systemsProcSpawn.listOfAllEnemies[i].GetComponent<EnemyAI>().Health(damageAmount * 4);
                }
            }*/

            specAttack3IsGo = false;
        }
    }

    GameObject trailParticle;

    void PlayerSpecialAttackLogic1 () {
        if (specialAttackPhase == 1) {
            //gameObject.GetComponent<NavMeshAgent>().enabled = true;
            // Vector3 virtualJoystickDirection = startLocationOfMouseDown - Input.mousePosition;
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

            //get reference to box identifieer gameobject
            //specattack2enemy
            RaycastHit hit;
            //var layerMask = 1 << 9;
            Physics.Raycast(/*raycastSource.transform.position*/ new Vector3(raycastSource.transform.position.x, raycastSource.transform.position.y+1, raycastSource.transform.position.z), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, myLayerMask);
            Vector3 pointHit = hit.point;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(pointHit);
            specialAttackPhase = 4;

            if (attackMode == 7 || attackMode == 8)
            {
                trailParticle.transform.position = shockwavespawn2.position;
            }
            else
            {
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
        //Debug.Log("ATTACKSPEC");
    }

    Vector3 specialAttackAim;

    public GameObject greenCircle;
    public GameObject orangeCircle;
    public GameObject powerUpIcon;

    void removeOrangeCircle() {
        orangeCircle.SetActive(false);
        CancelInvoke("removeOrangeCircle");
    }

    bool isFadingOrange = false;
    float fadingOrangeTimeStart = 0;
    float timeToFade = .25f;
    float orangeGoalScale = 2f;

    public GameObject redTarget;

    public GameObject redCancelIcon;

    bool isThereNoTargetForJump = false;

    GameObject origOrangeCircle;

    void fadeAwayOrangeGroup () {
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
        //CancelInvoke("removeOrangeCircle");
        //powerUpIcon;
        //orangeCircle;
    }

    float orangeActivateTime;
    bool isOrangeDeact = false;

    public GameObject redAim;
    public GameObject redCircle;

    float primaryCooldown;
    float secondaryCooldown;

    bool isSpecAttack1 = true; //hop attack
    bool isSpecAttack2 = true; //speedy attack

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

    float speedAttackCooldown = 5;
    float jumpAttackCooldown = 20;
    //float jumpAttackCooldown = 1;

    float speedAttackLastTrig = -5;
    float jumpAttackLastTrig = -20;
    //float jumpAttackLastTrig = -1;

    //MEMELORD EXTEREME 2

    int MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH = 0;

    void SpecialAttackIndicatorsFadedXDDDD () {
        if (systemsProcSpawn.level > 14) {
            MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH = 2;
        }
        else if (systemsProcSpawn.level > 5) {
            MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH = 1;
        }
        else {
            MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH = 0;
        }

        if (MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH == 2) {
            blackBackgroundJuan.SetActive(false);
            blackBackgroundDos.SetActive(true);
        }
        else if (MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH == 1) {
            blackBackgroundJuan.SetActive(true);
            blackBackgroundDos.SetActive(false);
        }
        else {
            blackBackgroundJuan.SetActive(false);
            blackBackgroundDos.SetActive(false);
        }


        if (MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH < 1) {
            isSpecAttack2 = false;
        }
        else if ((Time.time - speedAttackLastTrig) > speedAttackCooldown) {
            isSpecAttack2 = true;
        }
        else if ((Time.time - speedAttackLastTrig) < speedAttackCooldown) {
            isSpecAttack2 = false;
        }

        if (MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH < 2) {
            isSpecAttack1 = false;
        }
        else if ((Time.time - jumpAttackLastTrig) > jumpAttackCooldown) {
            isSpecAttack1 = true;
        }
        else if ((Time.time - jumpAttackLastTrig) < jumpAttackCooldown) {
            isSpecAttack1 = false;
        }

        float a = (Time.time - speedAttackLastTrig) / speedAttackCooldown;
        Color speedTrans = speedAttackIndicatorCircle.GetComponent<Image>().color;
        if (MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH < 1) {
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
        if (MUHPOWERLEVELLLLLLLOVAR9000000AAAHHHHH < 2) {
            jumpTrans.a = 0;
            jumpAttackIndicatorCircleGlow.SetActive(false);
            jumpAttackIndicatorIconGlow.SetActive(false);
        }
        else if (b > 1) {
            jumpTrans.a = 1;
            jumpAttackIndicatorCircleGlow.SetActive(true);
            jumpAttackIndicatorIconGlow.SetActive(true);
            //isSpecialAttackUnderWay = false;
        }
        else {
            jumpTrans.a = Mathf.Lerp(0, 1, b) - .2f;
            jumpAttackIndicatorCircleGlow.SetActive(false);
            jumpAttackIndicatorIconGlow.SetActive(false);
        }
        jumpAttackIndicatorCircle.GetComponent<Image>().color = jumpTrans;
        jumpAttackIndicatorIcon.GetComponent<Image>().color = jumpTrans;
    }

    void PlayerSpecialAttack () {
        

        if (isControlOff == false) {
            if (Input.GetMouseButton(0) == true) {
                if (lastIsMouseDown == false) {
                    if (SpecialPhase1 == true || SpecialPhase2 == true) {
                        float durationOfMouseUp = Time.time - startDurationOfMouseUp;
                        if (durationOfMouseUp < specialAttackTouchTimeUpThreshold) {
                            if (SpecialPhase2 == true) {
                                SpecialPhase2_5 = true;
                                spec2_5StartTime = Time.time;
                            }
                            else if (SpecialPhase1 == true) {
                                float mouseUpMouseDownDiff = Vector3.Distance(startLocationOfMouseDown, Input.mousePosition);

                                if (mouseUpMouseDownDiff < mouseSpecDistanceThresholdreplace) {
                                    SpecialPhase1_5 = true;
                                    if (isSpecAttack1 == false && isSpecAttack2 == false) {

                                    }
                                    else {
                                        orangeCircle.GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                                        orangeCircle.SetActive(true);
                                        orangeActivateTime = Time.time;
                                        isOrangeDeact = false;
                                    }
                                }
                            }
                        }
                        else {
                            SpecialPhase1 = false;
                            SpecialPhase1_5 = false;
                            SpecialPhase2 = false;
                            SpecialPhase2_5 = false;
                            orangeCircle.SetActive(false);
                            CancelInvoke("removeOrangeCircle");
                        }
                        startDurationOfMouseDown = 0;
                    }

                    startDurationOfMouseUp = 0;
                    startDurationOfMouseDown = Time.time;
                    startLocationOfMouseDown = Input.mousePosition;

                    if (SpecialPhase1 == false && SpecialPhase1_5 == false && SpecialPhase2 == false && SpecialPhase2_5 == false) {
                        orangeCircle.SetActive(false);
                        CancelInvoke("removeOrangeCircle");
                    }

                    greenCircle.GetComponent<RectTransform>().position = new Vector3(startLocationOfMouseDown.x, startLocationOfMouseDown.y, 0);
                    redCircle.GetComponent<RectTransform>().position = new Vector3(startLocationOfMouseDown.x, startLocationOfMouseDown.y, 0);

                    if (SpecialPhase2 == true) {
                        redTarget.GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                        redCircle.GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                        if (Vector3.Distance(startLocationOfMouseDown, Input.mousePosition) > mouseSpecDistanceThresholdreplace) {
                            if (isSpecAttack2 == true) {
                                redCircle.SetActive(true);
                                redTarget.SetActive(false);
                                redAim.SetActive(false);
                                redAim.GetComponent<RectTransform>().position = blueJoystickAim.GetComponent<RectTransform>().position;
                                redAim.GetComponent<RectTransform>().localRotation = blueJoystickAim.GetComponent<RectTransform>().localRotation;
                            }
                            else {
                                redTarget.SetActive(false);
                                redCircle.SetActive(false);
                                redAim.SetActive(false);
                            }
                            //PUT CODE HERE SOMEWHERE KILL ME
                        }
                        else if (isSpecAttack1 == true) {
                            if (isSpecAttack1 == true) {
                                redTarget.SetActive(true);
                                redCircle.SetActive(false);
                                redAim.SetActive(false);
                            }
                            else {
                                redTarget.SetActive(false);
                                redCircle.SetActive(false);
                                redAim.SetActive(false);
                            }
                        }
                        canShowNormJoystick = false;
                    }
                    else {
                        //greenCircle.SetActive(true);
                    }
                }
                else {
                    if (SpecialPhase2 == true) {
                        if (Vector3.Distance(startLocationOfMouseDown, Input.mousePosition) > mouseSpecDistanceThresholdreplace) {
                            if (isSpecAttack2 == true) {
                                redCircle.SetActive(true);
                                redTarget.SetActive(false);
                                redAim.SetActive(false);
                                redAim.GetComponent<RectTransform>().position = blueJoystickAim.GetComponent<RectTransform>().position;
                                redAim.GetComponent<RectTransform>().localRotation = blueJoystickAim.GetComponent<RectTransform>().localRotation;
                            }
                            else {
                                redTarget.SetActive(false);
                                redCircle.SetActive(false);
                                redAim.SetActive(false);
                            }
                        }
                        else if (isSpecAttack1 == true) {
                            if (isSpecAttack1 == true) {
                                redTarget.SetActive(true);
                                redCircle.SetActive(false);
                                redAim.SetActive(false);
                            }
                            else {
                                redTarget.SetActive(false);
                                redCircle.SetActive(false);
                                redAim.SetActive(false);
                            }
                        }
                    }

                    if ((SpecialPhase1_5 == true && SpecialPhase2 == false) && ((Time.time - orangeActivateTime) > specialAttackTouchTimeDownThreshold)) {
                        isOrangeDeact = true;
                        orangeCircle.SetActive(false);
                        CancelInvoke("removeOrangeCircle");
                    }

                    if (Time.time - spec2_5StartTime > spec2_5HoldThreshold && SpecialPhase2_5 == true) {
                        isSpecialAttackUnderWay = true;
                        specialAttackAim = Input.mousePosition;
                        SpecialPhase1 = false;
                        SpecialPhase1_5 = false;
                        SpecialPhase2 = false;
                        SpecialPhase2_5 = false;
                        if (Vector3.Distance(startLocationOfMouseDown, Input.mousePosition) > mouseSpecDistanceThresholdreplace) {
                            specialAttackMode = 1;
                            powerUpIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
                            if (isSpecAttack2 == true) {
                                speedAttackLastTrig = Time.time;
                            }
                        }
                        else {
                            specialAttackMode = 2;
                            powerUpIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
                            if (isSpecAttack1 == true) {
                                jumpAttackLastTrig = Time.time;
                            }
                        }
                        isFadingOrange = true;
                        fadingOrangeTimeStart = Time.time;

                        powerUpIcon.GetComponent<RectTransform>().position = new Vector3(orangeCircle.GetComponent<RectTransform>().position.x, orangeCircle.GetComponent<RectTransform>().position.y, 0);
                        powerUpIcon.SetActive(true);

                        if (isSpecAttack2 == false && specialAttackMode == 1) {
                            isSpecialAttackUnderWay = false;
                            powerUpIcon.SetActive(false);
                            orangeCircle.SetActive(false);
                            CancelInvoke("removeOrangeCircle");
                        }
                        else if (isSpecAttack1 == false && specialAttackMode == 2) {
                            isSpecialAttackUnderWay = false;
                            powerUpIcon.SetActive(false);
                            orangeCircle.SetActive(false);
                            CancelInvoke("removeOrangeCircle");
                        }
                    }
                }
                lastIsMouseDown = true;
            }
            else {
                if (lastIsMouseDown == true) {
                    if (SpecialPhase1 == false || SpecialPhase1_5 == true || SpecialPhase2_5 == true) {
                        float durationOfMouseDown = Time.time - startDurationOfMouseDown;
                        if (SpecialPhase2_5 == true) {
                            isSpecialAttackUnderWay = true;

                            isFadingOrange = true;
                            fadingOrangeTimeStart = Time.time;

                            canShowNormJoystick = true;

                            powerUpIcon.GetComponent<RectTransform>().position = new Vector3(orangeCircle.GetComponent<RectTransform>().position.x, orangeCircle.GetComponent<RectTransform>().position.y, 0);
                            powerUpIcon.SetActive(true);

                            if (Vector3.Distance(startLocationOfMouseDown, Input.mousePosition) > mouseSpecDistanceThresholdreplace) {
                                specialAttackMode = 1;
                                powerUpIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
                                if (isSpecAttack2 == true) {
                                    speedAttackLastTrig = Time.time;
                                }
                            }
                            else {
                                specialAttackMode = 2;
                                powerUpIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,-90);
                                if (isSpecAttack1 == true) {
                                    jumpAttackLastTrig = Time.time;
                                }
                            }

                            specialAttackAim = Input.mousePosition;
                            SpecialPhase1 = false;
                            SpecialPhase1_5 = false;
                            SpecialPhase2 = false;
                            SpecialPhase2_5 = false;

                            if (isSpecAttack2 == false && specialAttackMode == 1) {
                                isSpecialAttackUnderWay = false;
                                powerUpIcon.SetActive(false);
                                orangeCircle.SetActive(false);
                                CancelInvoke("removeOrangeCircle");
                            }
                            else if (isSpecAttack1 == false && specialAttackMode == 2) {
                                isSpecialAttackUnderWay = false;
                                powerUpIcon.SetActive(false);
                                orangeCircle.SetActive(false);
                                CancelInvoke("removeOrangeCircle");
                            }
                        }
                        else if (durationOfMouseDown < specialAttackTouchTimeDownThreshold) {
                            if (SpecialPhase1_5 == true) {
                                SpecialPhase2 = true;
                            }
                            else if (SpecialPhase1 == false) {
                                SpecialPhase1 = true;
                            }
                        }
                        else {
                            SpecialPhase1 = false;
                            SpecialPhase1_5 = false;
                            SpecialPhase2 = false;
                            SpecialPhase2_5 = false;
                            orangeCircle.SetActive(false);
                            CancelInvoke("removeOrangeCircle");
                        }
                    }

                    startDurationOfMouseDown = 0;
                    startDurationOfMouseUp = Time.time;
                    startLocationOfMouseUp = Input.mousePosition;

                    greenCircle.SetActive(false);
                }
                else {
                    float durationOfMouseUp = Time.time - startDurationOfMouseUp;
                    if (durationOfMouseUp > specialAttackTouchTimeUpThreshold) {
                        orangeCircle.SetActive(false);
                        CancelInvoke("removeOrangeCircle");
                    }
                }
                lastIsMouseDown = false;
            }
        }
    }

    public GameObject gameCamera;

    Vector3 refoutvar = Vector3.zero;
    Vector3 CurrrentJoystickDirection = Vector3.zero;

    float lowerlimit = 0.1f;

    Vector3 lastMousePosition = Vector3.zero;

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
            else if (verticalAxisJoystick < 0f && verticalAxisJoystick > -joystickAxisLowerLimit)
            {
                verticalAxisJoystick = -joystickAxisLowerLimit;
            }

            if (horizontalAxisJoystick != 0 || verticalAxisJoystick != 0)
            {
                Cursor.visible = false;
            }
            else if (Input.mousePosition != lastMousePosition)
            {
                Cursor.visible = true;
                lastMousePosition = Input.mousePosition;
            }
            else if (horizontalAxisKeyboard != 0 || verticalAxisKeyboard != 0) {
                Cursor.visible = true;
            }

            float horizontalAxis = 0f;
            float verticalAxis = 0f;

            if (horizontalAxisJoystick > 0 && horizontalAxisKeyboard > 0)
            {
                horizontalAxis = Mathf.Max(horizontalAxisJoystick, horizontalAxisKeyboard);
            }
            else if (horizontalAxisJoystick < 0 && horizontalAxisKeyboard < 0)
            {
                horizontalAxis = Mathf.Min(horizontalAxisJoystick, horizontalAxisKeyboard);
            }
            else
            {
                if (horizontalAxisJoystick != 0)
                {
                    horizontalAxis = horizontalAxisJoystick;
                }
                else
                {
                    horizontalAxis = horizontalAxisKeyboard;
                }
            }

            if (verticalAxisJoystick > 0 && verticalAxisKeyboard > 0)
            {
                verticalAxis = Mathf.Max(verticalAxisJoystick, verticalAxisKeyboard);
            }
            else if (verticalAxisJoystick < 0 && verticalAxisKeyboard < 0)
            {
                verticalAxis = Mathf.Min(verticalAxisJoystick, verticalAxisKeyboard);
            }
            else
            {
                if (verticalAxisJoystick != 0)
                {
                    verticalAxis = verticalAxisJoystick;
                }
                else
                {
                    verticalAxis = verticalAxisKeyboard;
                }
            }

            Vector3 virtualJoystickDirection = new Vector3(horizontalAxis, 0, verticalAxis);

            virtualJoystickDirection = Quaternion.Euler(screenRotationCorrection) * virtualJoystickDirection;
            virtualJoystickDirection = Vector3.ClampMagnitude(virtualJoystickDirection, 1f);
            

            CurrrentJoystickDirection = Vector3.SmoothDamp(CurrrentJoystickDirection, virtualJoystickDirection, ref refoutvar, 0.1f);

           

            Vector3 LowerClampedJoystickDirection = CurrrentJoystickDirection;

            if (LowerClampedJoystickDirection.x < lowerlimit && LowerClampedJoystickDirection.x > -lowerlimit)
            {
                LowerClampedJoystickDirection.x = 0f;
            }

            if (LowerClampedJoystickDirection.y < lowerlimit && LowerClampedJoystickDirection.y > -lowerlimit)
            {
                LowerClampedJoystickDirection.y = 0f;
            }

            if (LowerClampedJoystickDirection.z < lowerlimit && LowerClampedJoystickDirection.z > -lowerlimit)
            {
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

    public void setAttackIsDone () {
        attackOnCooldown = 1;
        Invoke("AttackCooldownManager", attackCooldown);
        attackIsDone = true;
        amAttackingRightNow = false;
    }

    void AttackCooldownManager () {
        attackOnCooldown = 0;
        /*if (prevAmAttackingRightNow == true && amAttackingRightNow == false && attackOnCooldown == 0) {
            prevTime = Time.time;
            attackOnCooldown = 1;
        }
        else if (Time.time - prevTime > attackCooldown && attackOnCooldown == 1)
            attackOnCooldown = 0;

        prevAmAttackingRightNow = amAttackingRightNow;*/
    }

    bool prevBlueArrow = false;

    public GameObject blueJoystickAim;

    bool canShowNormJoystick = true;

    public float playerLookAtRotateSpeed;

    void MovePlayer (Vector3 direction) {

        playerCollider.SimpleMove((direction * moveSpeed) * Time.fixedDeltaTime);

        if (direction != new Vector3(0,0,0)) {
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

        //TAKE LOCATION OF GREEN CIRCLE
        //TAKE DIRECTION
        //SHOOT LINE IN DIRECTION, LIMIT DISTANCE
        //PUT BLUE DIREC ON EDGE OF LINE

        //THEN

        //ROTATE TOWARDS GREEN CIRCLE THEN INVERT

        //MAGNUM DONG

        if (enemyTarget == null) {
            //Debug.Log("1");

            if (((Input.GetButton("Fire3") == true && isControlOn == true && isControlOff == false && Time.timeScale != 0) && attackOnCooldown == 0) || amAttackingRightNow == true)
            {
                amAttackingRightNow = true;
                attackOnCooldown = 1;

                if (attackMode == 1)
                    AnimSwitchTo("goToAttack");
                else if (attackMode == 6 || attackMode == 7 || attackMode == 8)
                    AnimSwitchTo("goToAttack3");
                else
                    AnimSwitchTo("goToAttack2");

                if (isAttacking == 0)
                {
                    //prevTime = Time.time;
                    isAttacking = 1;
                    hasDamaged = 0;
                }

                if (attackDamageTicked == true && hasDamaged == 0)
                {
                    //enemyTarget.GetComponent<EnemyAI>().Health(-20);
                    hasDamaged = 1;
                    attackDamageTicked = false;
                }

                if (attackIsDone == true)
                {
                    attackIsDone = false;
                    attackOnCooldown = 1;
                    isAttacking = 0;
                    //prevTime = Time.time;
                }
            }
            else if (direction == new Vector3(0, 0, 0)) {
                if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
                    AnimSwitchTo("goToIdle3");
                }
                else
                    AnimSwitchTo("goToIdle");
                transform.LookAt(transform.position + direction);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((transform.position + direction) - transform.position), playerLookAtRotateSpeed * Time.deltaTime);
            }
            else {
                if (attackMode == 6 || attackMode == 7 || attackMode == 8) {
                    AnimSwitchTo("goToWalkForward3");
                }
                else
                    AnimSwitchTo("goToWalkForward");
                //transform.LookAt(transform.position + direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((transform.position + direction) - transform.position), playerLookAtRotateSpeed * Time.deltaTime);
            }

            /*if (Time.time - prevTime > attackCooldown && attackOnCooldown == 1)
                attackOnCooldown = 0;*/

            isAttacking = 0;

            transformToShake.localPosition = new Vector3(0, 0, 0);

            //amAttackingRightNow = false;

        }
        else {
            //Debug.Log("2");
            //transform.LookAt(enemyTarget.transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(enemyTarget.transform.position - transform.position), playerLookAtRotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            Vector3 enemyAim = enemyTarget.transform.position - transform.position;
            enemyAim = new Vector3(enemyAim.x, 0f, enemyAim.z).normalized;

            Vector3 walkAim = direction;

            float angle = Vector3.Angle(enemyAim, walkAim);

            Vector3 enemyWalkPerpendicular = Vector3.Cross(enemyAim, walkAim);
            Vector3 up = new Vector3(0f, 1f, 0f);
            float leftRightIdentifier = Vector3.Dot(enemyWalkPerpendicular, up);

            if (((Input.GetButton("Fire3") == true && isControlOn == true && isControlOff == false && Time.timeScale != 0) && attackOnCooldown == 0) || amAttackingRightNow == true)
            {
                amAttackingRightNow = true;
                attackOnCooldown = 1;

                if (attackMode == 1)
                    AnimSwitchTo("goToAttack");
                else if (attackMode == 6 || attackMode == 7 || attackMode == 8)
                    AnimSwitchTo("goToAttack3");
                else
                    AnimSwitchTo("goToAttack2");
                
                if (isAttacking == 0) {
                    //prevTime = Time.time;
                    isAttacking = 1;
                    hasDamaged = 0;
                }

                if (attackDamageTicked == true && hasDamaged == 0) {
                    //enemyTarget.GetComponent<EnemyAI>().Health(-20);
                    hasDamaged = 1;
                    attackDamageTicked = false;
                }

                if (attackIsDone == true) {
                    attackIsDone = false;
                    attackOnCooldown = 1;
                    isAttacking = 0;
                    //prevTime = Time.time;
                }
            }
            else {
                //Debug.Log("2");
                amAttackingRightNow = false;

                /*if (Time.time - prevTime > attackCooldown && attackOnCooldown == 1)
                    attackOnCooldown = 0;*/

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
                    /*else {
                        AnimSwitchTo("goToIdle");
                    }*/
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
                /*else
                    Debug.Log("WARNING: Targeting error.");*/
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
                    // logic for when enemy is dead
                    listOfTargets.RemoveAt(i);
                }
                else if (Vector3.Distance(transform.position, listOfTargets[i].transform.position) < Vector3.Distance(transform.position, enemyTarget.transform.position)) {
                    enemyTarget = listOfTargets[i];
                }
                else {
                    // do nothing, keep current target 
                }
                //Debug.Log(listOfTargets[i]);
            }
        }
    }

    /*void AnimSwitchTo(string trigger)
    {
        if (triggerTriggered == false) {
            triggerTriggered = true;
            lastTrigger
            if (trigger != lastTrigger) {
                animator.SetTrigger(trigger);
                lastTrigger = ReturnCurrentTrigger();
            }
        }
    }*/

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

    void setAnimatorBoolCluster (string boolFocus) {
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

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Enemy" && listOfTargets.Contains(other.gameObject) == false)
            listOfTargets.Add(other.gameObject);
    }

    void OnTriggerExit (Collider other) {
        if (other.tag == "Enemy" && listOfTargets.Contains(other.gameObject) == true)
            listOfTargets.Remove(other.gameObject);
    }
}
