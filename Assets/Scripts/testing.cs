using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tilePrefab;  

    void Start()
    {
        GenerateTileAt(0, 0);  
        GenerateTileAt(2, 1); 
        GenerateTileAt(-3, -2); 
    }

    // Update is called once per frame
    void GenerateTileAt(int x, int y)
    {
        Vector2 position = new Vector2(x, y);
        Instantiate(tilePrefab, position, Quaternion.identity);
    }
}
