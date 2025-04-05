using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkRenderer : MonoBehaviour
{
    public Tilemap tilemap;


    public Tile stoneTile; // 在 Inspector 里拖进来的 Tile
    public Tile dirtTile;
    public Tile fossilTile1;
    public Tile fossilTile2;
    public Tile fossilTile3;
    public Tile fossilTile4;
    public Tile fossilTile5;
    public Tile fossilTile6;
    public Tile goldTile1;
    public Tile goldTile2;

    
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
                //12: dirt
                
                if (chunk.tilesType[x, y] == 0)
                {
                    tilemap.SetTile(cellPos, null);
                }
                else if (chunk.tilesType[x, y] == 1)
                {
                    tilemap.SetTile(cellPos, stoneTile);
                }else if (chunk.tilesType[x, y] == 12)
                {
                    tilemap.SetTile(cellPos, dirtTile);
                }else if (chunk.tilesType[x, y] == 9)
                {
                    tilemap.SetTile(cellPos, goldTile1);
                }else if (chunk.tilesType[x, y] == 10)
                {
                    tilemap.SetTile(cellPos, goldTile2);
                }
            }
        }
    }


    public void RefreshChunk(ChunkData chunk)
    {
        RenderChunk(chunk);
    }
}