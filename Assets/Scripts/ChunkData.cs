using System;
using UnityEngine;

[Serializable]
public class ChunkData
{
    public int chunkX;
    public int chunkY;
    public Color[,] tiles;  // 保存每个方块的颜色等信息

    public const int CHUNK_SIZE = 9;

    public ChunkData(int cx, int cy)
    {
        chunkX = cx;
        chunkY = cy;
        tiles = new Color[CHUNK_SIZE, CHUNK_SIZE];
    }
}