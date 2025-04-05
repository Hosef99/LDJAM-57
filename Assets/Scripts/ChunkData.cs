using System;
using UnityEngine;

[Serializable]
public class ChunkData
{
    public int chunkX;
    public int chunkY;
    public Color[,] tilesColor;
    public int[,] tilesType;
    // 0: hole
    // 1: stone1
    // 2: stone2
    // 3: stone3
    // 4: stone4
    // 5: stone5
    // 6: fossil1
    // 7: fossil2
    // 8: fossil3
    // 9: gold1
    //10: gold2
    //11: gold3 

    public const int CHUNK_SIZE = 9;

    public ChunkData(int cx, int cy)
    {
        chunkX = cx;
        chunkY = cy;
        tilesColor = new Color[CHUNK_SIZE, CHUNK_SIZE];
        tilesType = new int[CHUNK_SIZE, CHUNK_SIZE];

    }
}