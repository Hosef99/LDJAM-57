using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkRenderer : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase baseTile; 

    public void RenderChunk(ChunkData chunk)
    {
        int cx = chunk.chunkX;
        int cy = chunk.chunkY;

        for (int x = 0; x < ChunkData.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < ChunkData.CHUNK_SIZE; y++)
            {
                Vector3Int cellPos = new Vector3Int(
                    cx * ChunkData.CHUNK_SIZE + x - ChunkData.CHUNK_SIZE / 2 - 2,
                    cy * ChunkData.CHUNK_SIZE + y - ChunkData.CHUNK_SIZE / 2 - 2,
                    0);
                Color c = chunk.tilesColor[x, y];
                if (c.a > 0.0f)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.color = c;
                    tile.sprite = (baseTile as Tile).sprite; 
                    tilemap.SetTile(cellPos, tile);
                }else
                {
                    tilemap.SetTile(cellPos, null);
                }
            }
        }
    }

    public void RefreshChunk(ChunkData chunk)
    {
        RenderChunk(chunk);
    }
}