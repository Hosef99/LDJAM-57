using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private int seedX;
    private int seedY;
    public float scale = 0.1f;
    public float goldScale = 0.1f;
    public float fossilScale = 0.1f;

    
    private Dictionary<Vector2Int, ChunkData> loadedChunks 
        = new Dictionary<Vector2Int, ChunkData>();

    public ChunkRenderer chunkRenderer;


    void Awake()
    {
        seedX = Random.Range(0,1000);
        seedY = Random.Range(0,1000);
    }


    public ChunkData GetOrGenerateChunk(int cx, int cy)
    {
        Vector2Int key = new Vector2Int(cx, cy);
        if (!loadedChunks.ContainsKey(key))
        {
            ChunkData newChunk = GenerateChunk(cx, cy);
            loadedChunks.Add(key, newChunk);

            if (chunkRenderer != null)
            {
                chunkRenderer.RenderChunk(newChunk);
            }
        }
        return loadedChunks[key];
    }


    private ChunkData GenerateChunk(int cx, int cy)
    {
        int type; // 0: hole
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
                  //12: dirt
        ChunkData chunk = new ChunkData(cx, cy);
        if (cy > 0) return chunk;

        for (int x = 0; x < ChunkData.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < ChunkData.CHUNK_SIZE; y++)
            {
                Vector3Int tilePos = getTilePos(chunk, new Vector2Int(x, y)); 
                float perlinValue = Mathf.PerlinNoise((tilePos.x * scale) + seedX, (tilePos.y * scale) + seedY);
                float goldPerlinValue = Mathf.PerlinNoise((tilePos.x * goldScale) + seedX + 1, (tilePos.y * goldScale) + seedY+ 1);
                float fossilPerlinValue = Mathf.PerlinNoise((tilePos.x * goldScale) + seedX + 2, (tilePos.y * goldScale) + seedY+ 2);

                if (perlinValue > 0.8f)
                {
                    // Cave: leave as transparent or black
                    type = 0;
                }
                else if (perlinValue > 0.2f)
                {
                    type = 1;
                }
                else
                {
                    type = 12;
                }

                if (type != 0)
                {
                    if (goldPerlinValue > 0.87f)
                    {
                        // Cave: leave as transparent or black
                        type = 9;
                    }
                    else if (goldPerlinValue > 0.8f)
                    {
                        type = 10;
                    }
                }

                chunk.tilesType[x, y] = type;
            }
        }
        return chunk;
    }

    public Vector3Int getTilePos(ChunkData chunk, Vector2Int localPos)
    {
        int globalX = chunk.chunkX * ChunkData.CHUNK_SIZE + localPos.x - ChunkData.CHUNK_SIZE / 2;
        int globalY = chunk.chunkY * ChunkData.CHUNK_SIZE + localPos.y - ChunkData.CHUNK_SIZE / 2;
        return new Vector3Int(globalX, globalY, 0);
    }

    public Vector2Int GetChunkXY(Vector3Int tilePos)
    {
        int cx = Mathf.FloorToInt((tilePos.x + ChunkData.CHUNK_SIZE /(float) 2)  / (float)ChunkData.CHUNK_SIZE);
        int cy = Mathf.FloorToInt((tilePos.y + ChunkData.CHUNK_SIZE /(float) 2) / (float)ChunkData.CHUNK_SIZE);
        return new Vector2Int(cx, cy);
    } 
    
    public Vector2Int GetChunkLocalXY(Vector3Int tilePos)
    {
        Vector2Int chunkXY = GetChunkXY(tilePos);
        int cx = chunkXY.x;
        int cy = chunkXY.y;
        int localX = tilePos.x - cx * ChunkData.CHUNK_SIZE + ChunkData.CHUNK_SIZE / 2;
        int localY = tilePos.y - cy * ChunkData.CHUNK_SIZE + ChunkData.CHUNK_SIZE / 2;
        return new Vector2Int(localX, localY);
    } 

    public void DestroyBlockAt(Vector3Int tilePos)
    {
        Vector2Int chunkXY = GetChunkXY(tilePos);
        int cx = chunkXY.x;
        int cy = chunkXY.y;

        ChunkData chunk = GetOrGenerateChunk(cx, cy); 
        Vector2Int localXY = GetChunkLocalXY(tilePos);

        int localX = localXY.x;
        int localY = localXY.y;

        
        if (localX < 0 || localX >= ChunkData.CHUNK_SIZE ||
            localY < 0 || localY >= ChunkData.CHUNK_SIZE)
            return;

        chunk.tilesType[localX, localY] = 0;

        if (chunkRenderer != null)
        {
            chunkRenderer.RefreshChunk(chunk);
        }
    }
}


