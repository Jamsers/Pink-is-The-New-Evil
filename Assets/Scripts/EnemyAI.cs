using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public GameObject spawnDust;
    GameObject spawnDustReference;

    Vector3 moveUpperCubeorigpos;

    public int enemyType;

    bool hasDustSpawned = false;
    bool enemy5spawndeaddust = false;

    public GameObject enemy6fizz;
    Vector3 enemy6fizzposition;
    bool enemy6HasShotLoad = false;
    public GameObject enemy6Squirtysquirt;

    public int normalDamage;
    public int specialDamage;
    public int pointsRewardedToPlayer;
    public Vector3 spawnDustAdjustment;
    public Vector3 spawnDustScale;
    public Vector3 bloodAdjustment;
    public Vector3 bloodScale;
    public float shakeAmount;

    public GameObject enemy4Shockwave;

    GameObject enemy7SpikeyReference;
    public GameObject enemy7Spikey;

    float decomposeStart;
    bool decomposing = false;
    public float decomposeLength;
    bool EXECUTETHEFUCKINGSPECIALAHHHHHH = false;
    public float specialMovementSpeed;

    float specialAttackFlightStartTime;
    Vector3 specialAttackFlightStartPos;
    float specialAttackFlightTargetTime;
    Vector3 specialAttackFlightTargetPos;

    public Animator enemy5animator;

    bool specialProcTick = true;

    int specialAttackPhase = 1;

    float navSelfSpeedStorage;

    UnityEngine.AI.NavMeshAgent navSelf;
    Transform player;
    PlayerAI playerai;
    Animator animator;

    public float specialTriggerRange;
    public float specialTriggerCooldown;
    public float specialProcChance;

    public GameObject particle1prefab;

    bool specialUnderway = false;
    bool specialimsoDAMAGED = false;

    public float attackRange;
    public float attackCooldown;
    //int attackOnCooldown = 0;
    float prevTime;
    int isAttacking = 0;
    //int hasDamaged;
    public bool isPlayerInDanger = false;

    //store transform for shake
    public Transform cube;

    public float attackLength;
    //float attackLengthDamage;

    string lastTrigger = "null";

    public Transform enemyCube;

    bool isSpawning = true;

    bool pointsGiven = false;

    // Health()
    public int currenthealth;

    bool isDead = false;

    public void SetIsSpawning()
    {
        isSpawning = false;
        Destroy(spawnDustReference, 1.5f);
    }

    public void Health (int value)
    {
        currenthealth = currenthealth + value;
        Vector3 bloodPosition = new Vector3(transform.position.x + bloodAdjustment.x, transform.position.y + bloodAdjustment.y, transform.position.z + bloodAdjustment.z);
        GameObject particle = (GameObject)Instantiate(particle1prefab, bloodPosition, particle1prefab.transform.rotation);
        particle.transform.localScale = bloodScale;
        Destroy(particle, 3.0f);
        MoveUpper();
        Invoke("MoveLower", .0625f);
        Invoke("MoveUpper2", .125f);
        Invoke("MoveLower", .1875f);
        Invoke("MoveFinish", .25f);
        if (currenthealth <= 0)
        {
            if (enemyType == 7 && (enemy7SpikeyReference != null)) {
                enemy7SpikeyReference.GetComponent<spinnybaldes>().LetsSpawnOut();
            }
            isDead = true;
            if (pointsGiven == false) {
                playerai.upgradePoints = playerai.upgradePoints + pointsRewardedToPlayer;
                pointsGiven = true;
            }
            AnimSwitchTo("isDead");
            GameObject.Find("Systems Process").GetComponent<EnemySpawner>().listOfAllEnemies.Remove(gameObject);
            gameObject.tag = "Dead Enemy";
            navSelf.enabled = false;
            //GetComponent<CapsuleCollider>().isTrigger = true;
            //Destroy(gameObject, 4);


        }

        if (playerai.attackMode == 1) {
            GetComponent<PlaySoundEffect>().PlaySoundHit(0);
        }
        else if (playerai.attackMode == 2 || playerai.attackMode == 3) {
            GetComponent<PlaySoundEffect>().PlaySoundHit(1);
        }
        else if (playerai.attackMode == 4 || playerai.attackMode == 5) {
            GetComponent<PlaySoundEffect>().PlaySoundHit(2);
        }
        else if (playerai.attackMode == 6 || playerai.attackMode == 7) {
            GetComponent<PlaySoundEffect>().PlaySoundHit(3);
        }
        else if (playerai.attackMode == 8) {
            GetComponent<PlaySoundEffect>().PlaySoundHit(2);
            GetComponent<PlaySoundEffect>().PlaySoundHit(3);
        }

    }

    public void StartDecomposing () {
        GetComponent<CapsuleCollider>().isTrigger = true;
        decomposeStart = Time.time;
        decomposing = true;
        Destroy(gameObject, decomposeLength);
    }

    void Decomposition() {
        float decomposeTimePercent = (Time.time - decomposeStart) / decomposeLength;
        cube.localPosition = Vector3.Lerp(moveUpperCubeorigpos, new Vector3(0,-2,0), decomposeTimePercent);
    }

    void MoveUpper() {
        cube.position = new Vector3(cube.position.x + shakeAmount/2, cube.position.y + shakeAmount/2, cube.position.z + shakeAmount/2);
        //Debug.Log("moveup");
    }


    void MoveLower ()
    {
        cube.position = new Vector3(cube.position.x - shakeAmount, cube.position.y - shakeAmount, cube.position.z - shakeAmount);
        //Debug.Log("movelow");
    }

    void MoveUpper2()
    {
        cube.position = new Vector3(cube.position.x + shakeAmount, cube.position.y + shakeAmount, cube.position.z + shakeAmount);
        //Debug.Log("moveup");
    }

    void MoveFinish ()
    {
        //cube.position = new Vector3(cube.position.x + .05f, cube.position.y + .05f, cube.position.z + .05f);
        cube.localPosition = moveUpperCubeorigpos;
        //Debug.Log("moveup");
    }

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerai = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>();
        navSelf = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (enemyType == 5) {
            animator = enemy5animator;
        }
        else {
            animator = GetComponent<Animator>();
        }
        //AnimSwitchTo("isWalking");
        //attackLengthDamage = attackLength * 0.50f;
        animator.SetFloat("attackSpeed", 0.708f / attackLength);

        transform.LookAt(player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        

    }

    void Update () {
        if (isSpawning == true) {
            AnimSwitchTo("spawning");
            if (spawnDustReference == null) {
                spawnDustReference = (GameObject)Instantiate(spawnDust, transform.position, spawnDust.transform.rotation);
                spawnDustReference.transform.position = new Vector3(spawnDustReference.transform.position.x + spawnDustAdjustment.x, spawnDustReference.transform.position.y + spawnDustAdjustment.y, spawnDustReference.transform.position.z + spawnDustAdjustment.z);
                spawnDustReference.transform.localScale = spawnDustScale;
                moveUpperCubeorigpos = cube.localPosition;
            }
        }
        /*else if (playerai.currenthealth <= 0) {
            if (isAttacking == 1 || specialUnderway == true) {
                AttackLogic();
            }
            else {
                AnimSwitchTo("isIdle");
                transform.LookAt(player.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                navSelf.Stop();
            }
        }*/
        else
            AttackLogic();

        if (decomposing == true) {
            Decomposition();
        }

        if (enemyType != 1 && enemyType != 5 && enemyType != 6) {
            if (Vector3.Distance(transform.position, player.position) <= specialTriggerRange) {
                if (specialProcTick == true) {
                    specialProcTick = false;
                    Invoke("GenerateSpecialProcTick", specialTriggerCooldown);
                }
            }
        }
    }

    void GenerateSpecialProcTick () {
        float proc = Random.Range(0, 100);
        if (proc <= specialProcChance) {
            specialAttackPhase = 1;
            EXECUTETHEFUCKINGSPECIALAHHHHHH = true;
            //Debug.Log("OMG FUCKING TRIGGERED");
        }
        else {
            specialProcTick = true;
            //Debug.Log("not triggered lol try harder edgelord");
        }
    }

    public void attackDamageTick () {
        if (enemyType == 6) {
            enemy6HasShotLoad = true;
            enemy6fizzposition.y = enemy6fizzposition.y + .1f;
            Instantiate(enemy6fizz, enemy6fizzposition, enemy6fizz.transform.rotation);
            Vector3 squirtpos = new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z);
            Instantiate(enemy6Squirtysquirt, squirtpos, transform.rotation);
        }
        else if (Vector3.Distance(transform.position, player.position) <= attackRange && isPlayerInDanger == true) {
            playerai.Health(-normalDamage);
        }
    }

    public void attackDoneTick () {
        isAttacking = 0;
        if (enemyType == 6) {
            enemy6HasShotLoad = false;
        }
    }

    public void specialAttackPhaseSet (int phase) {
        specialAttackPhase = phase;
    }

    void AttackLogic()
    {
        if (isDead == true)
        {
            AnimSwitchTo("isDead");
            if (enemy5spawndeaddust == false && enemyType == 5) {
                spawnDustReference = (GameObject)Instantiate(spawnDust, transform.position, spawnDust.transform.rotation);
                spawnDustReference.transform.position = new Vector3(spawnDustReference.transform.position.x + spawnDustAdjustment.x, spawnDustReference.transform.position.y + spawnDustAdjustment.y, spawnDustReference.transform.position.z + spawnDustAdjustment.z);
                spawnDustReference.transform.localScale = spawnDustScale;
                Destroy(spawnDustReference, 2f);
                enemy5spawndeaddust = true;
            }
        }
        else if ((Vector3.Distance(transform.position, player.position) <= (attackRange * .75f)  || isAttacking == 1) && specialUnderway == false && enemyType != 5)
        {
            AnimSwitchTo("attack");

            if (((Vector3.Distance(transform.position, player.position) <= (attackRange)) && enemy6HasShotLoad == false) && enemyType == 6) {
                transform.LookAt(player.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                enemy6fizzposition = player.position;
            }

            if (isAttacking == 0)
            {
                //enemy6fizzposition = player.position;
                isAttacking = 1;
                //hasDamaged = 0;
                navSelf.Stop();
                transform.LookAt(player.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }
        else if (EXECUTETHEFUCKINGSPECIALAHHHHHH == true) {
            if (enemyType == 2) {
                SpecialAttack2();
            }
            else if (enemyType == 3) {
                SpecialAttack3();
            }
            else if (enemyType == 4) {
                SpecialAttack4();
            }
            else if (enemyType == 7) {
                SpecialAttack7();
            }
        }
        else {
            if (enemyType == 5) {
                if (Vector3.Distance(transform.position, player.position) <= attackRange && specialimsoDAMAGED == false) {
                    playerai.Health(-normalDamage);
                    specialimsoDAMAGED = true;
                    Invoke("HeiiiiImNotSoDamagedAfterAll", .5f);
                }
                else if (Vector3.Distance(transform.position, player.position) > attackRange) {
                    CancelInvoke("HeiiiiImNotSoDamagedAfterAll");
                    specialimsoDAMAGED = false;
                }
            }
            AnimSwitchTo("isWalking");
            navSelf.SetDestination(player.position);
            navSelf.Resume();
        }
    }

    void HeiiiiImNotSoDamagedAfterAll () {
        specialimsoDAMAGED = false;
    }

    void SpecialAttack2 () {
        if (specialAttackPhase == 1) {
            specialUnderway = true;
            AnimSwitchTo("specialAttack");
            transform.LookAt(player.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            navSelf.Stop();
        }
        else if (specialAttackPhase == 2) {
            AnimSwitchTo("specialAttack");
            RaycastHit hit;
            var layerMask = 1 << 9;
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask);
            Vector3 pointHit = hit.point;
            navSelf.Resume();
            navSelf.SetDestination(pointHit);
            navSelfSpeedStorage = navSelf.speed;
            navSelf.speed = specialMovementSpeed;
            //navSelf.angularSpeed = 0;
            specialAttackPhase = 3;
        }
        else if (specialAttackPhase == 3) {
            if (Vector3.Distance(transform.position, player.position) <= attackRange && isPlayerInDanger == true && specialimsoDAMAGED == false) {
                playerai.Health(-specialDamage);
                specialimsoDAMAGED = true;
            }
            else if (isPlayerInDanger == false) {
                specialimsoDAMAGED = false;
            }
        }
        else if (specialAttackPhase == 4) {
            AnimSwitchTo("specialAttack");
            navSelf.Stop();
            navSelf.speed = navSelfSpeedStorage;
            //navSelf.angularSpeed = 100000000000000;
        }
        else if (specialAttackPhase == 5) {
            EXECUTETHEFUCKINGSPECIALAHHHHHH = false;
            specialProcTick = true;
            specialUnderway = false;
        }
    }

    void SpecialAttack3() {
        if (specialAttackPhase == 1) {
            specialUnderway = true;
            AnimSwitchTo("specialAttack");
            navSelf.enabled = false;
            GameObject digdust = (GameObject)Instantiate(spawnDust, transform.position, spawnDust.transform.rotation);
            digdust.transform.position = new Vector3(digdust.transform.position.x + spawnDustAdjustment.x, digdust.transform.position.y + spawnDustAdjustment.y, digdust.transform.position.z + spawnDustAdjustment.z);
            digdust.transform.localScale = spawnDustScale / 2f;
            Destroy(digdust, 3);
            specialAttackPhase = 9;
        }
        else if (specialAttackPhase == 2) {
            gameObject.tag = "Dead Enemy";
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Invoke("IEmbraceTheSweetReleaseOfDeath", 1);
            specialAttackPhase = 3;
        }
        else if (specialAttackPhase == 4) {
            AnimSwitchTo("specialAttack2");
            transform.position = player.position;
            specialAttackPhase = 9;
        }
        else if (specialAttackPhase == 5) {
            gameObject.tag = "Enemy";
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            navSelf.enabled = true;
            if (hasDustSpawned == false) {
                GameObject digdust = (GameObject)Instantiate(spawnDust, transform.position, spawnDust.transform.rotation);
                digdust.transform.position = new Vector3(digdust.transform.position.x + spawnDustAdjustment.x, digdust.transform.position.y + spawnDustAdjustment.y + .5f, digdust.transform.position.z + spawnDustAdjustment.z);
                digdust.transform.localScale = spawnDustScale;
                Destroy(digdust, 3);
                hasDustSpawned = true;
            }
            if (Vector3.Distance(transform.position, player.position) <= attackRange && specialimsoDAMAGED == false) {
                playerai.Health(-specialDamage);
                specialimsoDAMAGED = true;
            }
            else if (Vector3.Distance(transform.position, player.position) > attackRange) {
                specialimsoDAMAGED = false;
            }
        }
        else if (specialAttackPhase == 7) {
            hasDustSpawned = false;
            EXECUTETHEFUCKINGSPECIALAHHHHHH = false;
            specialProcTick = true;
            specialUnderway = false;
        }
    }

    void SpecialAttack4() {
        if (specialAttackPhase == 1) {
            specialUnderway = true;
            AnimSwitchTo("specialAttack");
            transform.LookAt(player.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            navSelf.enabled = false;
        }
        else if (specialAttackPhase == 3) {
            transform.LookAt(player.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

            float playbackTime = currentState.normalizedTime % 1;

            specialAttackFlightStartTime = playbackTime;
            specialAttackFlightStartPos = transform.position;
            specialAttackFlightTargetTime = 0.608f;
            specialAttackFlightTargetPos = player.position;

            gameObject.tag = "Dead Enemy";
            gameObject.GetComponent<CapsuleCollider>().enabled = false;

            specialAttackPhase = 4;
        }
        else if (specialAttackPhase == 4) {

            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            float switchTimePercent = ((currentState.normalizedTime % 1) - specialAttackFlightStartTime) / (specialAttackFlightTargetTime - specialAttackFlightStartTime);

            if (switchTimePercent > 1) {
                specialAttackPhase = 5;
            }
            else {
                transform.position = Vector3.Lerp(specialAttackFlightStartPos, specialAttackFlightTargetPos, switchTimePercent);
            }
        }
        if (specialAttackPhase == 5) {
            gameObject.tag = "Enemy";
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            transform.position = specialAttackFlightTargetPos;
            navSelf.enabled = true;

            if (Vector3.Distance(transform.position, player.position) <= 3) {
                playerai.Health(-specialDamage);
            }
            Vector3 shackwavebrahpos = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            GameObject shackwavebrah = (GameObject)Instantiate(enemy4Shockwave, shackwavebrahpos, enemy4Shockwave.transform.rotation);
            shackwavebrah.transform.localScale = new Vector3(.7f,.7f,.7f);
            Destroy(shackwavebrah, 1);
            specialAttackPhase = 6;
        }
        if (specialAttackPhase == 8) {
            EXECUTETHEFUCKINGSPECIALAHHHHHH = false;
            specialProcTick = true;
            specialUnderway = false;
        }
    }

    void SpecialAttack7() {
        if (specialAttackPhase == 1) {
            specialUnderway = true;
            AnimSwitchTo("specialAttack");
            navSelf.Stop();
        }
        else if (specialAttackPhase == 2) {
            enemy7SpikeyReference = (GameObject)Instantiate(enemy7Spikey, enemy7Spikey.transform.position, enemy7Spikey.transform.rotation);
            enemy7SpikeyReference.transform.position = player.position;
            enemy7SpikeyReference.transform.position = new Vector3 (enemy7SpikeyReference.transform.position.x, enemy7SpikeyReference.transform.position.y + 1.3f, enemy7SpikeyReference.transform.position.z);
            GameObject digdust = (GameObject)Instantiate(spawnDust, transform.position, spawnDust.transform.rotation);
            digdust.transform.position = new Vector3(enemy7SpikeyReference.transform.position.x + spawnDustAdjustment.x, enemy7SpikeyReference.transform.position.y + -1.3f, enemy7SpikeyReference.transform.position.z + spawnDustAdjustment.z);
            digdust.transform.localScale = new Vector3(.5f, .5f, .3f); ;
            Destroy(digdust, 3);
            specialAttackPhase = 3;
        }
        else if (specialAttackPhase == 4) {
            enemy7SpikeyReference.GetComponent<spinnybaldes>().LetsSpawnOut();
            specialAttackPhase = 5;
        }
        else if (specialAttackPhase == 6) {
            EXECUTETHEFUCKINGSPECIALAHHHHHH = false;
            specialProcTick = true;
            specialUnderway = false;
        }
    }

    void IEmbraceTheSweetReleaseOfDeath () {
        specialAttackPhase = 4;
    }

    void AnimSwitchTo(string trigger)
    {
        if (animator.GetBool("attack") != false)
            animator.SetBool("attack", false);
        if (animator.GetBool("isWalking") != false)
            animator.SetBool("isWalking", false);
        if (animator.GetBool("isDead") != false)
            animator.SetBool("isDead", false);
        if (animator.GetBool("isIdle") != false)
            animator.SetBool("isIdle", false);
        if (animator.GetBool("spawning") != false)
            animator.SetBool("spawning", false);
        if (enemyType == 2) {
            if (animator.GetBool("specialAttack") != false)
                animator.SetBool("specialAttack", false);
        }
        if (enemyType == 3) {
            if (animator.GetBool("specialAttack") != false)
                animator.SetBool("specialAttack", false);
            if (animator.GetBool("specialAttack2") != false)
                animator.SetBool("specialAttack2", false);
        }
        if (enemyType == 4) {
            if (animator.GetBool("specialAttack") != false)
                animator.SetBool("specialAttack", false);
        }
        if (enemyType == 7) {
            if (animator.GetBool("specialAttack") != false)
                animator.SetBool("specialAttack", false);
        }
        if (animator.GetBool(trigger) != true)
            animator.SetBool(trigger, true);
    }

    void OnTriggerEnter(Collider other)
    {

    }

}
