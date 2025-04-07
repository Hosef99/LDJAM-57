using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private int seedX;
    private int seedY;
    public float basicScale = 0.1f;
    public float goldScale = 0.3f;
    public float fossilScale = 0.3f;
    public float obsidianScale = 0.3f;
    public float diamondScale = 0.3f;
    public float redStoneScale = 0.3f;

    
    public float holeThreshold = 0.8f;
    public float stoneThreshold = 0.2f;
    
    public float gold1Threshold = 0.8f;
    public float gold2Threshold = 0.87f;
    public float gold3Threshold = 10000f;

    public float fossilThreshold = 0.9f;
    public float obsidianThreshold = 0.9f;
    public float diamondThreshold = 0.9f;
    public float redStoneThreshold = 0.9f;

    public float obsidianXYRatio = 1;

    
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
    
    int GetRandomType(List<int> typeList)
    {
        if (typeList == null || typeList.Count == 0) return 0;

        int index = Random.Range(0, typeList.Count);
        return typeList[index];
    }

    private void GenerateOre(ref ChunkData chunk, int x, int y, float scale, float XOverYRatio, float threshold, List<int> type, int seedX, int seedY)
    {
        Vector3Int tilePos = getTilePos(chunk, new Vector2Int(x, y)); 

        float perlinValue = Mathf.PerlinNoise((tilePos.x * scale * XOverYRatio) + seedX, (tilePos.y * fossilScale) + seedY);

        if (perlinValue > threshold)
        {
            chunk.tilesType[x, y] = GetRandomType(type);
        }
        

    }
    

    private ChunkData GenerateChunk(int cx, int cy)
    {
        int type; 
        ChunkData chunk = new ChunkData(cx, cy);
        if (cy > 0) return chunk;

        for (int x = 0; x < ChunkData.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < ChunkData.CHUNK_SIZE; y++)
            {
                if (cy > -1)
                {
                    Vector3Int tilePos = getTilePos(chunk, new Vector2Int(x, y)); 
                    float perlinValue = Mathf.PerlinNoise((tilePos.x * basicScale) + seedX, (tilePos.y * basicScale) + seedY);

                
                    // base
                
                    if (perlinValue > holeThreshold)
                    {
                        type = ChunkData.HOLE;
                    }
                    else if (perlinValue > stoneThreshold)
                    {
                        type = ChunkData.STONE1;
                    }
                    else
                    {
                        type = ChunkData.DIRT;
                    }
                    chunk.tilesType[x, y] = type;

                
                    if (type == ChunkData.STONE1)
                    {
                        GenerateOre(ref chunk, x, y, goldScale,  1,gold1Threshold, new List<int> {ChunkData.GOLD1}, seedX + 1, seedY + 1);
                        GenerateOre(ref chunk, x, y, goldScale,  1,gold2Threshold,  new List<int> {ChunkData.GOLD2}, seedX + 1, seedY + 1);
                        GenerateOre(ref chunk, x, y, redStoneScale,  1,redStoneThreshold,  new List<int> {ChunkData.REDSTONE}, seedX + 5, seedY + 5);
                        
                        
                    }

                    if (type == ChunkData.DIRT)
                    {
                        GenerateOre(ref chunk, x, y, fossilScale, 1, fossilThreshold, new List<int> {ChunkData.FOSSIL1, ChunkData.FOSSIL2, ChunkData.FOSSIL3 , ChunkData.FOSSIL4 ,ChunkData.FOSSIL5 ,ChunkData.FOSSIL6}, seedX + 2, seedY + 2);

                    }
                } else if (cy <= -8)
                {
                    Vector3Int tilePos = getTilePos(chunk, new Vector2Int(x, y)); 
                    float perlinValue = Mathf.PerlinNoise((tilePos.x * basicScale) + seedX, (tilePos.y * basicScale) + seedY);

                
                    // base
                
                    if (perlinValue > holeThreshold)
                    {
                        type = ChunkData.HOLE;
                    }
                    else if (perlinValue > stoneThreshold)
                    {
                        type = ChunkData.STONE1;
                    }
                    else
                    {
                        type = ChunkData.DIRT;
                    }
                    chunk.tilesType[x, y] = type;

                
                    if (chunk.tilesType[x, y] == ChunkData.STONE1)
                    {
                        GenerateOre(ref chunk, x, y, goldScale,  1,gold1Threshold, new List<int> {ChunkData.GOLD1}, seedX + 1, seedY + 1);
                        GenerateOre(ref chunk, x, y, goldScale,  1,gold2Threshold,  new List<int> {ChunkData.GOLD2}, seedX + 1, seedY + 1);
                        GenerateOre(ref chunk, x, y, redStoneScale,  1,redStoneThreshold,  new List<int> {ChunkData.REDSTONE}, seedX + 5, seedY + 5);
                        GenerateOre(ref chunk, x, y, obsidianScale,  obsidianXYRatio,obsidianThreshold,  new List<int> {ChunkData.OBSIDIAN1}, seedX + 3, seedY + 3);
                        
                    }
                    
                    if (chunk.tilesType[x, y] == ChunkData.OBSIDIAN1) GenerateOre(ref chunk, x, y, diamondScale,  1,diamondThreshold,  new List<int> {ChunkData.DIAMOND}, seedX + 4, seedY + 4);

                    if (chunk.tilesType[x, y] == ChunkData.DIRT)
                    {
                        GenerateOre(ref chunk, x, y, fossilScale, 1, fossilThreshold,
                            new List<int>
                            {
                                ChunkData.FOSSIL1, ChunkData.FOSSIL2, ChunkData.FOSSIL3, ChunkData.FOSSIL4,
                                ChunkData.FOSSIL5, ChunkData.FOSSIL6
                            }, seedX + 2, seedY + 2);

                    }
                } else if (cy > -8)
                {
                    Vector3Int tilePos = getTilePos(chunk, new Vector2Int(x, y)); 
                    float perlinValue = Mathf.PerlinNoise((tilePos.x * basicScale) + seedX, (tilePos.y * basicScale) + seedY);

                
                    // base
                
                    if (perlinValue > holeThreshold + (cy * ChunkData.CHUNK_SIZE - y)*0.005 )
                    {
                        type = ChunkData.HOLE;
                    }
                    else if (perlinValue > stoneThreshold)
                    {
                        type = ChunkData.STONE1;
                    }
                    else
                    {
                        type = ChunkData.DIRT;
                    }
                    chunk.tilesType[x, y] = type;

                
                    if (type == ChunkData.STONE1)
                    {
                        GenerateOre(ref chunk, x, y, goldScale,  1,gold1Threshold, new List<int> {ChunkData.GOLD1}, seedX + 1, seedY + 1);
                        GenerateOre(ref chunk, x, y, goldScale,  1,gold2Threshold,  new List<int> {ChunkData.GOLD2}, seedX + 1, seedY + 1);
                        GenerateOre(ref chunk, x, y, redStoneScale,  1,redStoneThreshold,  new List<int> {ChunkData.REDSTONE}, seedX + 5, seedY + 5);
                        
                        
                    }

                    if (type == ChunkData.DIRT)
                    {
                        GenerateOre(ref chunk, x, y, fossilScale, 1, fossilThreshold, new List<int> {ChunkData.FOSSIL1, ChunkData.FOSSIL2, ChunkData.FOSSIL3 , ChunkData.FOSSIL4 ,ChunkData.FOSSIL5 ,ChunkData.FOSSIL6}, seedX + 2, seedY + 2);

                    }
                }
                
                
                
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


}


