using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WithinWeaponPickUp : MonoBehaviour {
    public int weaponType;
    public int weaponPrice;
    public string priceSubtitle;
    public Font fontToUse;

    [Header("UI")]
    public GameObject backimage1;
    public GameObject price;
    public GameObject buybutton;
    public GameObject backimage2;
    public GameObject notenoughvespene;

    [Header("Pink Guy")]
    public GameObject pinkguyback;
    public GameObject pinkguytext;
    public GameObject pinkfizz;    

    UnityAction buyFunction;

    void Start() {
        buyFunction = new UnityAction(delegate () {
            PinkIsTheNewEvil.PlayerController.BuyWeapon(weaponType);
        });
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player Identifier")
            return;

        if (weaponType == 420) {
            pinkguyback.SetActive(true);
            pinkguytext.SetActive(true);
            pinkfizz.SetActive(true);

            Destroy(pinkguyback, 6);
            Destroy(pinkguytext, 6);
            Destroy(pinkfizz, 1);

            buyFunction();
            return;
        }

        backimage1.SetActive(true);
        price.SetActive(true);
        buybutton.SetActive(true);
        
        price.GetComponent<Text>().text = priceSubtitle + weaponPrice;
        price.GetComponent<Text>().font = fontToUse;
        buybutton.GetComponent<Button>().onClick.AddListener(buyFunction);

        if (weaponType > 8 && PinkIsTheNewEvil.EnemySpawner.level < 29)
            price.GetComponent<Text>().text = "Ascencion: 100000";
    }

    void OnTriggerExit(Collider other) {
        if (other.tag != "Player Identifier")
            return;

        backimage1.SetActive(false);
        price.SetActive(false);
        buybutton.SetActive(false);

        buybutton.GetComponent<Button>().onClick.RemoveListener(buyFunction);
    }
}