using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int seed = 12345;

    private Dictionary<Vector2Int, ChunkData> loadedChunks 
        = new Dictionary<Vector2Int, ChunkData>();

    public ChunkRenderer chunkRenderer;

    private System.Random random;

    void Awake()
    {
        random = new System.Random(seed);
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
        ChunkData chunk = new ChunkData(cx, cy);

        for (int x = 0; x < ChunkData.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < ChunkData.CHUNK_SIZE; y++)
            {

                float r = (float)random.NextDouble();
                float g = (float)random.NextDouble();
                float b = (float)random.NextDouble();
                chunk.tiles[x, y] = new Color(r, g, b, 1f);
            }
        }
        return chunk;
    }

    public Vector2Int GetChunkXY(Vector3Int tilePos)
    {
        int cx = Mathf.FloorToInt((tilePos.x + ChunkData.CHUNK_SIZE /(float) 2)  / (float)ChunkData.CHUNK_SIZE);
        int cy = Mathf.FloorToInt((tilePos.y + ChunkData.CHUNK_SIZE /(float) 2) / (float)ChunkData.CHUNK_SIZE);
        return new Vector2Int(cx, cy);
    } 
    
    public Vector2Int GetLocalXY(Vector3Int tilePos)
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
        Vector2Int localXY = GetLocalXY(tilePos);

        int localX = localXY.x;
        int localY = localXY.y;

        
        if (localX < 0 || localX >= ChunkData.CHUNK_SIZE ||
            localY < 0 || localY >= ChunkData.CHUNK_SIZE)
            return;

        chunk.tiles[localX, localY] = new Color(0, 0, 0, 0);

        if (chunkRenderer != null)
        {
            chunkRenderer.RefreshChunk(chunk);
        }
    }
}
