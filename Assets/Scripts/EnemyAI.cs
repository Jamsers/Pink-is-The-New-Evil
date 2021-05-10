using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    public int enemyType;
    public int currenthealth;
    public int normalDamage;
    public int specialDamage;
    public int pointsRewardedToPlayer;
    public float attackRange;
    public float attackCooldown;
    public float attackLength;
    public float specialTriggerRange;
    public float specialTriggerCooldown;
    public float specialProcChance;
    public float specialMovementSpeed;
    public float decomposeLength;

    [Header("Adjustments")]
    public Vector3 spawnDustAdjustment;
    public Vector3 spawnDustScale;
    public Vector3 bloodAdjustment;
    public Vector3 bloodScale;
    public float shakeAmount;
    
    [Header("References")]
    public GameObject spawnDust;
    public GameObject particle1prefab;
    public GameObject enemy4Shockwave;
    public GameObject enemy7Spikey;
    public Animator enemy5animator;
    public GameObject enemy6fizz;
    public GameObject enemy6Squirtysquirt;

    [HideInInspector] public Transform enemyModel;
    [HideInInspector] public bool isPlayerInDanger = false;
    [HideInInspector] public Transform enemyCube;

    Animator animator;
    NavMeshAgent navMeshAgent;
    float navSelfSpeedStorage;
    Vector3 enemyModelOrigLocalPosition;
    GameObject spawnDustReference;
    bool isSpawning = true;
    bool hasDustSpawned = false;
    bool enemy5spawndeaddust = false;
    float prevTime;
    int isAttacking = 0;
    bool isDead = false;
    bool pointsGiven = false;
    float decomposeStart;
    bool decomposing = false;
    const float shakeInterval = .0625f;

    bool isSpecialAttackStarting = false;
    Vector3 enemy6fizzposition;
    bool enemy6HasShotFizz = false;
    GameObject enemy7SpikeyReference;
    float specialAttackFlightStartTime;
    Vector3 specialAttackFlightStartPos;
    float specialAttackFlightTargetTime;
    Vector3 specialAttackFlightTargetPos;
    bool specialProcTick = true;
    bool specialUnderway = false;
    bool hasDamagedPlayer = false;
    int specialAttackPhase = 1;

    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (enemyType == 5) {
            animator = enemy5animator;
        }
        else {
            animator = GetComponent<Animator>();
        }

        animator.SetFloat("attackSpeed", 0.708f / attackLength);
        transform.LookAt(PinkIsTheNewEvil.PlayerController.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void Update() {
        if (isSpawning == true) {
            AnimSwitchTo("spawning");
            if (spawnDustReference == null) {
                Vector3 spawnDustPosition = transform.position + spawnDustAdjustment;
                spawnDustReference = Instantiate(spawnDust, spawnDustPosition, spawnDust.transform.rotation);
                spawnDustReference.transform.localScale = spawnDustScale;
                enemyModelOrigLocalPosition = enemyModel.localPosition;
            }
        }
        else {
            AttackLogic();
        }

        if (decomposing == true) {
            Decomposition();
        }

        if (enemyType != 1 && enemyType != 5 && enemyType != 6) {
            if (Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) <= specialTriggerRange) {
                if (specialProcTick == true) {
                    specialProcTick = false;
                    Invoke("GenerateSpecialProcTick", specialTriggerCooldown);
                }
            }
        }
    }

    public void SetIsSpawning() {
        isSpawning = false;
        Destroy(spawnDustReference, 1.5f);
    }

    public void Health(int value) {
        currenthealth = currenthealth + value;
        Vector3 bloodPosition = transform.position + bloodAdjustment;
        GameObject particle = Instantiate(particle1prefab, bloodPosition, particle1prefab.transform.rotation);
        particle.transform.localScale = bloodScale;
        Destroy(particle, 3.0f);

        Invoke("MoveUpper", shakeInterval * 0);
        Invoke("MoveLower", shakeInterval * 1);
        Invoke("MoveUpper2", shakeInterval * 2);
        Invoke("MoveLower", shakeInterval * 3);
        Invoke("MoveFinish", shakeInterval * 4);

        if (currenthealth <= 0) {
            if (enemyType == 7 && (enemy7SpikeyReference != null)) {
                enemy7SpikeyReference.GetComponent<SpawnBlades>().LetsSpawnOut();
            }

            isDead = true;

            if (pointsGiven == false) {
                PinkIsTheNewEvil.PlayerController.upgradePoints += pointsRewardedToPlayer;
                pointsGiven = true;
            }

            AnimSwitchTo("isDead");
            PinkIsTheNewEvil.EnemySpawner.listOfAllEnemies.Remove(gameObject);
            gameObject.tag = "Dead Enemy";
            navMeshAgent.enabled = false;
        }

        switch (PinkIsTheNewEvil.PlayerController.attackMode) {
            case 1:
                GetComponent<SoundManager>().PlaySoundHit(0);
                break;
            case 2:
            case 3:
                GetComponent<SoundManager>().PlaySoundHit(1);
                break;
            case 4:
            case 5:
                GetComponent<SoundManager>().PlaySoundHit(2);
                break;
            case 6:
            case 7:
                GetComponent<SoundManager>().PlaySoundHit(3);
                break;
            case 8:
                GetComponent<SoundManager>().PlaySoundHit(2);
                GetComponent<SoundManager>().PlaySoundHit(3);
                break;
        }
    }

    public void StartDecomposing() {
        GetComponent<CapsuleCollider>().isTrigger = true;
        decomposeStart = Time.time;
        decomposing = true;
        Destroy(gameObject, decomposeLength);
    }

    void Decomposition() {
        float decomposeTimePercent = (Time.time - decomposeStart) / decomposeLength;
        enemyModel.localPosition = Vector3.Lerp(enemyModelOrigLocalPosition, new Vector3(0, -2, 0), decomposeTimePercent);
    }

    void GenerateSpecialProcTick() {
        float proc = Random.Range(0, 100);
        if (proc <= specialProcChance) {
            specialAttackPhase = 1;
            isSpecialAttackStarting = true;
        }
        else {
            specialProcTick = true;
        }
    }

    public void attackDamageTick() {
        if (enemyType == 6) {
            enemy6HasShotFizz = true;
            enemy6fizzposition.y += .1f;
            Instantiate(enemy6fizz, enemy6fizzposition, enemy6fizz.transform.rotation);
            Vector3 squirtpos = new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z);
            Instantiate(enemy6Squirtysquirt, squirtpos, transform.rotation);
        }
        else if (Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) <= attackRange && isPlayerInDanger == true) {
            PinkIsTheNewEvil.PlayerController.Health(-normalDamage);
        }
    }

    public void attackDoneTick() {
        isAttacking = 0;
        if (enemyType == 6) {
            enemy6HasShotFizz = false;
        }
    }

    public void specialAttackPhaseSet(int phase) {
        specialAttackPhase = phase;
    }

    void AttackLogic() {
        if (isDead == true) {
            AnimSwitchTo("isDead");

            if (enemy5spawndeaddust == false && enemyType == 5) {
                Vector3 spawnDustPosition = transform.position + spawnDustAdjustment;
                spawnDustReference = Instantiate(spawnDust, spawnDustPosition, spawnDust.transform.rotation);
                spawnDustReference.transform.localScale = spawnDustScale;
                Destroy(spawnDustReference, 2f);
                enemy5spawndeaddust = true;
            }
        }
        else if ((Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) <= (attackRange * .75f) || isAttacking == 1) && specialUnderway == false && enemyType != 5) {
            AnimSwitchTo("attack");

            if ((Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) <= attackRange) && enemy6HasShotFizz == false && enemyType == 6) {
                transform.LookAt(PinkIsTheNewEvil.PlayerController.transform.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                enemy6fizzposition = PinkIsTheNewEvil.PlayerController.transform.position;
            }

            if (isAttacking == 0) {
                isAttacking = 1;
                navMeshAgent.Stop();
                transform.LookAt(PinkIsTheNewEvil.PlayerController.transform.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }
        else if (isSpecialAttackStarting == true) {
            switch (enemyType) {
                case 2:
                    SpecialAttack2();
                    break;
                case 3:
                    SpecialAttack3();
                    break;
                case 4:
                    SpecialAttack4();
                    break;
                case 7:
                    SpecialAttack7();
                    break;
            }
        }
        else {
            if (enemyType == 5) {
                if (Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) <= attackRange && hasDamagedPlayer == false) {
                    PinkIsTheNewEvil.PlayerController.Health(-normalDamage);
                    hasDamagedPlayer = true;
                    Invoke("ResetHasDamagedPlayer", .5f);
                }
                else if (Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) > attackRange) {
                    CancelInvoke("ResetHasDamagedPlayer");
                    hasDamagedPlayer = false;
                }
            }

            AnimSwitchTo("isWalking");
            navMeshAgent.SetDestination(PinkIsTheNewEvil.PlayerController.transform.position);
            navMeshAgent.Resume();
        }
    }

    void ResetHasDamagedPlayer() {
        hasDamagedPlayer = false;
    }

    void SpecialAttack2() {
        if (specialAttackPhase == 1) {
            specialUnderway = true;
            AnimSwitchTo("specialAttack");
            transform.LookAt(PinkIsTheNewEvil.PlayerController.transform.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            navMeshAgent.Stop();
        }
        else if (specialAttackPhase == 2) {
            AnimSwitchTo("specialAttack");
            RaycastHit hit;
            var layerMask = 1 << 9;
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask);
            navMeshAgent.Resume();
            navMeshAgent.SetDestination(hit.point);
            navSelfSpeedStorage = navMeshAgent.speed;
            navMeshAgent.speed = specialMovementSpeed;
            specialAttackPhase = 3;
        }
        else if (specialAttackPhase == 3) {
            if (Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) <= attackRange && isPlayerInDanger == true && hasDamagedPlayer == false) {
                PinkIsTheNewEvil.PlayerController.Health(-specialDamage);
                hasDamagedPlayer = true;
            }
            else if (isPlayerInDanger == false) {
                hasDamagedPlayer = false;
            }
        }
        else if (specialAttackPhase == 4) {
            AnimSwitchTo("specialAttack");
            navMeshAgent.Stop();
            navMeshAgent.speed = navSelfSpeedStorage;
        }
        else if (specialAttackPhase == 5) {
            isSpecialAttackStarting = false;
            specialProcTick = true;
            specialUnderway = false;
        }
    }

    void SpecialAttack3() {
        if (specialAttackPhase == 1) {
            specialUnderway = true;
            AnimSwitchTo("specialAttack");
            navMeshAgent.enabled = false;
            Vector3 digDustPosition = transform.position + spawnDustAdjustment;
            GameObject digdust = Instantiate(spawnDust, digDustPosition, spawnDust.transform.rotation);
            digdust.transform.localScale = spawnDustScale / 2f;
            Destroy(digdust, 3);
            specialAttackPhase = 9;
        }
        else if (specialAttackPhase == 2) {
            gameObject.tag = "Dead Enemy";
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Invoke("SpecialAttackPhase4", 1);
            specialAttackPhase = 3;
        }
        else if (specialAttackPhase == 4) {
            AnimSwitchTo("specialAttack2");
            transform.position = PinkIsTheNewEvil.PlayerController.transform.position;
            specialAttackPhase = 9;
        }
        else if (specialAttackPhase == 5) {
            gameObject.tag = "Enemy";
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            navMeshAgent.enabled = true;

            if (hasDustSpawned == false) {
                Vector3 digDustPosition = transform.position + spawnDustAdjustment;
                GameObject digdust = Instantiate(spawnDust, digDustPosition, spawnDust.transform.rotation);
                digdust.transform.localScale = spawnDustScale;
                Destroy(digdust, 3);
                hasDustSpawned = true;
            }

            if (Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) <= attackRange && hasDamagedPlayer == false) {
                PinkIsTheNewEvil.PlayerController.Health(-specialDamage);
                hasDamagedPlayer = true;
            }
            else if (Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) > attackRange) {
                hasDamagedPlayer = false;
            }
        }
        else if (specialAttackPhase == 7) {
            hasDustSpawned = false;
            isSpecialAttackStarting = false;
            specialProcTick = true;
            specialUnderway = false;
        }
    }

    void SpecialAttack4() {
        if (specialAttackPhase == 1) {
            specialUnderway = true;
            AnimSwitchTo("specialAttack");
            transform.LookAt(PinkIsTheNewEvil.PlayerController.transform.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            navMeshAgent.enabled = false;
        }
        else if (specialAttackPhase == 3) {
            transform.LookAt(PinkIsTheNewEvil.PlayerController.transform.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            float playbackTime = currentState.normalizedTime % 1;
            specialAttackFlightStartTime = playbackTime;
            specialAttackFlightStartPos = transform.position;
            specialAttackFlightTargetTime = 0.608f;
            specialAttackFlightTargetPos = PinkIsTheNewEvil.PlayerController.transform.position;
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
            navMeshAgent.enabled = true;

            if (Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position) <= 3) {
                PinkIsTheNewEvil.PlayerController.Health(-specialDamage);
            }

            Vector3 specialAttackShockwavePosition = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            GameObject specialAttackShockwave = Instantiate(enemy4Shockwave, specialAttackShockwavePosition, enemy4Shockwave.transform.rotation);
            specialAttackShockwave.transform.localScale = new Vector3(.7f, .7f, .7f);
            Destroy(specialAttackShockwave, 1);
            specialAttackPhase = 6;
        }
        if (specialAttackPhase == 8) {
            isSpecialAttackStarting = false;
            specialProcTick = true;
            specialUnderway = false;
        }
    }

    void SpecialAttack7() {
        if (specialAttackPhase == 1) {
            specialUnderway = true;
            AnimSwitchTo("specialAttack");
            navMeshAgent.Stop();
        }
        else if (specialAttackPhase == 2) {
            enemy7SpikeyReference = Instantiate(enemy7Spikey, enemy7Spikey.transform.position, enemy7Spikey.transform.rotation);
            enemy7SpikeyReference.transform.position = PinkIsTheNewEvil.PlayerController.transform.position;
            enemy7SpikeyReference.transform.position = new Vector3(enemy7SpikeyReference.transform.position.x, enemy7SpikeyReference.transform.position.y + 1.3f, enemy7SpikeyReference.transform.position.z);
            Vector3 digDustPosition = transform.position + spawnDustAdjustment;
            GameObject digdust = Instantiate(spawnDust, digDustPosition, spawnDust.transform.rotation);
            digdust.transform.localScale = new Vector3(.5f, .5f, .3f); ;
            Destroy(digdust, 3);
            specialAttackPhase = 3;
        }
        else if (specialAttackPhase == 4) {
            enemy7SpikeyReference.GetComponent<SpawnBlades>().LetsSpawnOut();
            specialAttackPhase = 5;
        }
        else if (specialAttackPhase == 6) {
            isSpecialAttackStarting = false;
            specialProcTick = true;
            specialUnderway = false;
        }
    }

    void SpecialAttackPhase4() {
        specialAttackPhase = 4;
    }

    void AnimSwitchTo(string trigger) {
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

    void MoveUpper() {
        enemyModel.position = new Vector3(enemyModel.position.x + shakeAmount / 2, enemyModel.position.y + shakeAmount / 2, enemyModel.position.z + shakeAmount / 2);
    }

    void MoveLower() {
        enemyModel.position = new Vector3(enemyModel.position.x - shakeAmount, enemyModel.position.y - shakeAmount, enemyModel.position.z - shakeAmount);
    }

    void MoveUpper2() {
        enemyModel.position = new Vector3(enemyModel.position.x + shakeAmount, enemyModel.position.y + shakeAmount, enemyModel.position.z + shakeAmount);
    }

    void MoveFinish() {
        enemyModel.localPosition = enemyModelOrigLocalPosition;
    }
}