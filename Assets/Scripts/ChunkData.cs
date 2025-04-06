using System;
using UnityEngine;

[Serializable]
public class ChunkData
{
    public int chunkX;
    public int chunkY;
    public int[,] tilesType;

    public const int HOLE = 0;
    public const int STONE1 = 1;
    public const int STONE2 = 2;
    public const int STONE3 = 3;
    public const int STONE4 = 4;
    public const int STONE5 = 5;
    public const int FOSSIL1 = 6;
    public const int FOSSIL2 = 7;
    public const int FOSSIL3 = 8;
    public const int FOSSIL4 = 9;
    public const int FOSSIL5 = 10;
    public const int FOSSIL6 = 11;
    public const int GOLD1 = 12;
    public const int GOLD2 = 13;
    public const int GOLD3 = 14;
    public const int DIRT = 15;


    public const int CHUNK_SIZE = 9;

    public ChunkData(int cx, int cy)
    {
        chunkX = cx;
        chunkY = cy;
        tilesType = new int[CHUNK_SIZE, CHUNK_SIZE];

    }
}