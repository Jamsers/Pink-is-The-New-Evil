using UnityEngine;

public class DamagePlayerInArea : MonoBehaviour {
    bool playerDamagedCooldown = false;
    const float DamageResetTime = 0.5f;
    const int DamageHealthAmount = -20;
	
	void Update () {
        float distanceToPlayer = Vector3.Distance(transform.position, PinkIsTheNewEvil.PlayerController.transform.position);

        // afraid to touch these magic numbers so they'll stay for now
        if (distanceToPlayer <= (transform.lossyScale.x / 2) && playerDamagedCooldown == false) {
            PinkIsTheNewEvil.PlayerController.Health(DamageHealthAmount);
            playerDamagedCooldown = true;
            Invoke("DamageCooldownReset", DamageResetTime);
        }
        else if (distanceToPlayer > 1.5) {
            CancelInvoke("DamageCooldownReset");
            playerDamagedCooldown = false;
        }
    }

    void DamageCooldownReset () {
        playerDamagedCooldown = false;
    }
}
