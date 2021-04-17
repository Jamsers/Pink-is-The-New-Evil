using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class WithinWeaponPickUp : MonoBehaviour {

    public GameObject backimage1;
    public GameObject price;
    public GameObject buybutton;

    public GameObject backimage2;
    public GameObject notenoughvespene;

    public GameObject pinkguyback;
    public GameObject pinkguytext;
    public GameObject pinkfizz;

    public int weaponType;
    public int weaponPrice;

    UnityAction func;

	// Use this for initialization
	void Start () {
        func = new UnityAction(delegate () { GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().BuyWeapon(weaponType); });
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player Identifier") {
            if (weaponType == 420) {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().BuyWeapon(weaponType);
                pinkguyback.SetActive(true);
                pinkguytext.SetActive(true);
                pinkfizz.SetActive(true);
                Destroy(pinkfizz, 1);
                Destroy(pinkguyback, 6);
                Destroy(pinkguytext, 6);
            }
            else {
                backimage1.SetActive(true);
                price.SetActive(true);
                buybutton.SetActive(true);
                if (weaponType == 9 || weaponType == 10 || weaponType == 11) {
                    price.GetComponent<Text>().text = "Ascencion: " + weaponPrice;
                    price.GetComponent<Text>().font = GameObject.Find("Systems Process").GetComponent<EnemySpawner>().poiret;

                    if (weaponType == 9 && GameObject.Find("Systems Process").GetComponent<EnemySpawner>().level == 29) {
                        price.GetComponent<Text>().text = "Extended Rush: 70000";
                        price.GetComponent<Text>().font = GameObject.Find("Systems Process").GetComponent<EnemySpawner>().poiret;
                    }
                    else if (weaponType == 10 && GameObject.Find("Systems Process").GetComponent<EnemySpawner>().level == 29) {
                        price.GetComponent<Text>().text = "Fast-Charge Jump: 90000";
                        price.GetComponent<Text>().font = GameObject.Find("Systems Process").GetComponent<EnemySpawner>().poiret;
                    }
                    else if (weaponType == 11 && GameObject.Find("Systems Process").GetComponent<EnemySpawner>().level == 29) {
                        price.GetComponent<Text>().text = "Double Health: 30000";
                        price.GetComponent<Text>().font = GameObject.Find("Systems Process").GetComponent<EnemySpawner>().poiret;
                    }
                }
                else {
                    price.GetComponent<Text>().text = "Weapon Price: " + weaponPrice;
                    price.GetComponent<Text>().font = GameObject.Find("Systems Process").GetComponent<EnemySpawner>().forq;
                }
                buybutton.GetComponent<Button>().onClick.AddListener(func);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player Identifier") {
            backimage1.SetActive(false);
            price.SetActive(false);
            buybutton.SetActive(false);
            buybutton.GetComponent<Button>().onClick.RemoveListener(func);
        }
            //remove the prompts
    }
}
