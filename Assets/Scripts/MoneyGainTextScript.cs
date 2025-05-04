using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyGainTextScript : MonoBehaviour
{
    public float lifeTime = 0.46f;
    public float a = 0.45f;
    public int ammount;

    public float xPos;
    public bool flag1 = true;
    public float age = 0;
    float t = 0;
    int temp1;
    TextMesh textMesh;
    Vector3 initialPosition;
    GameObject parentObject;

    void Awake() {
        age = 0;
        flag1 = false;
        GetComponent<TextMesh>().text = "+" + ammount.ToString();
        temp1 = ammount;
    }

    void Start() {
        textMesh = GetComponent<TextMesh>();
        parentObject = GameObject.Find("MoneyDisplay");
    }

    void Update() {
        initialPosition = parentObject.GetComponent<Transform>().position;

        if (temp1 != ammount) {
            GetComponent<TextMesh>().text = "+" + ammount.ToString();
            age = 0;
        }

        age += Time.deltaTime;
        t = Mathf.InverseLerp(a, lifeTime, age);
        Color color = textMesh.color;
        color.a = 1 - t;
        textMesh.color = color;
        GetComponent<Transform>().position = new Vector3(initialPosition.x + xPos, Mathf.Lerp(initialPosition.y, initialPosition.y + 0.5f, t), -1);
        
        if (age >= a) {
            flag1 = true;
        }

        if (t >= 1) {
            Destroy(gameObject);
        }

        temp1 = ammount;
    }
}
