using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDisplayScript : MonoBehaviour
{
    public float a = 1.8f;
    public float yOffSet;

    public int objectCount;
    public int objectIndex;
    public GameObject moneyGainText;
    public GameObject gameObject1;
    public Color[] colors = new Color[3];
    public GameObject[] gameObjects = new GameObject[3];

    Transform playerTransform;

    void Start() {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update() {
        transform.position = playerTransform.position + new Vector3(0, yOffSet, 0);

        // if (Input.GetKeyDown("q")) {
        //     increment(0);
        // }
        
        // if (Input.GetKeyDown("w")) {
        //     increment(1);
        // }

        // if (Input.GetKeyDown("e")) {
        //     increment(2);
        // }

        objectCount = 0;
        for (int i = 0; i < 3; i++) {
            if (gameObjects[i] != null) {
                objectCount++;
            }
        }

        float xOffset = -(objectCount - 1) / 2.0f * a;

        objectIndex = 0;
        for (int i = 0; i < 3; i++) {
            if (gameObjects[i] != null) {
                gameObjects[i].GetComponent<MoneyGainTextScript>().xPos = (xOffset + a * objectIndex) * transform.localScale.x;
                objectIndex++;
            }
        }
    }

    public void increment(int k, int ammount) {
        if (gameObjects[k] == null) {
            instantiateObject(k, ammount);
        } else {
            if (gameObjects[k].GetComponent<MoneyGainTextScript>().flag1) {
                instantiateObject(k, ammount);
            } else {
                gameObjects[k].GetComponent<MoneyGainTextScript>().ammount += ammount;
            }
        }

        objectIndex++;
    }

    void instantiateObject(int k, int ammount) {
        gameObjects[k] = Instantiate(moneyGainText, gameObject.transform);
        gameObjects[k].GetComponent<TextMesh>().color = colors[k];
        gameObjects[k].GetComponent<MoneyGainTextScript>().ammount += ammount;
    }
}
