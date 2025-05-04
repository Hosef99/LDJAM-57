using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiationTest : MonoBehaviour
{
    public GameObject moneyGainText;
    public Transform trasnfrom;
    public GameObject gameObject1;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("q")) {
            createObject();
        }
    }

    void createObject() {
        if (gameObject1 == null) {
            gameObject1 = Instantiate(moneyGainText);
        } else {
            if (gameObject1.GetComponent<MoneyGainTextScript>().flag1) {
                gameObject1 = Instantiate(moneyGainText);
            } else {
                gameObject1.GetComponent<MoneyGainTextScript>().ammount++;
            }
        }
    }
}
