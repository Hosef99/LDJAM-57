using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public Material material;
    public float a = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("opacity", 2 * Mathf.Max(-((Time.time / a) % 1) + 0.5f, 0)); 
    }
}
