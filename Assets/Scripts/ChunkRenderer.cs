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
    public Tile goldTile3;

    
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


                switch (chunk.tilesType[x, y])
                {
                    case ChunkData.DIRT:
                        tilemap.SetTile(cellPos, dirtTile);
                        break;
                    
                    case ChunkData.STONE1:
                        tilemap.SetTile(cellPos, stoneTile);
                        break;
                    
                    case ChunkData.GOLD1:
                        tilemap.SetTile(cellPos, goldTile1);
                        break;
                    
                    case ChunkData.GOLD2:
                        tilemap.SetTile(cellPos, goldTile2);
                        break;
                    
                    case ChunkData.GOLD3:
                        tilemap.SetTile(cellPos, goldTile3);
                        break;
                    
                    case ChunkData.FOSSIL1:
                        tilemap.SetTile(cellPos, fossilTile1);
                        break;
                    
                    case ChunkData.FOSSIL2:
                        tilemap.SetTile(cellPos, fossilTile2);
                        break;
                    
                    case ChunkData.FOSSIL3:
                        tilemap.SetTile(cellPos, fossilTile3);
                        break;
                    
                    case ChunkData.FOSSIL4:
                        tilemap.SetTile(cellPos, fossilTile4);
                        break;
                    
                    case ChunkData.FOSSIL5:
                        tilemap.SetTile(cellPos, fossilTile5);
                        break;
                    
                    case ChunkData.FOSSIL6:
                        tilemap.SetTile(cellPos, fossilTile6);
                        break;
                    
                    default:
                        tilemap.SetTile(cellPos, null);
                        break;
                }
               
            }
        }
    }


    public void RefreshChunk(ChunkData chunk)
    {
        RenderChunk(chunk);
    }
}