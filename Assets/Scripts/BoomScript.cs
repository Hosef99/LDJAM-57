

using UnityEngine;

public class BoomScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    public float lifetime = 3f;
    public int range = 2;
    public bool collectOre = false;
    public PlayerController player;
    private float timer = 0f;
    private WorldGenerator worldGenerator;
    private bool isMoving = false;
    private Vector3 targetPos;


    void Start()
    {
        worldGenerator = FindObjectOfType<WorldGenerator>();
    }

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();

    }


    void Boom()
    {
        Vector3Int currentTile = Vector3Int.RoundToInt(transform.position);
        for (int i = 0; i < range * 2 + 1; i++)
        {
            for (int j = 0; j < range * 2 + 1; j++)
            {
                Vector3Int targetTile = currentTile + new Vector3Int(i - range, j - range, 0);
                float distance = Vector3.Distance(targetTile, currentTile);
                if (distance <= range - 0.5 )
                {
                    if (collectOre)
                    {
                        player.CollectBlockAt(currentTile + new Vector3Int(i - range, j - range, 0));
                    }
                    player.DestroyBlockAt(currentTile + new Vector3Int(i - range, j - range, 0));

                }
            }
        }
        Destroy(gameObject);
    }
    
    void Update()
    {
        HandleMovement();
        HandleFalling();
        timer += Time.deltaTime; 

        if (timer >= lifetime && !isMoving)
        {
            Boom();
        }
    }
    
    void HandleMovement()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                transform.position = targetPos;
                isMoving = false;
            }
        }

    }

    void HandleFalling()
    {


        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        Vector3Int belowPos = currentPos + new Vector3Int(0, -1, 0);

        if (!IsBlockAt(belowPos))
        {
            targetPos = belowPos;
            isMoving = true;
        }
    }
    
    public bool IsBlockAt(Vector3Int tilePos)
    {
        
        return GetTileTypeAt(tilePos) != ChunkData.HOLE;
    }

    int GetTileTypeAt(Vector3Int tilePos)
    {
        if (worldGenerator == null) return 0;

        Vector2Int chunkXY = worldGenerator.GetChunkXY(tilePos);
        int cx = chunkXY.x;
        int cy = chunkXY.y;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                worldGenerator.GetOrGenerateChunk(cx - 3 + i, cy - 2 + j);

            }
        }
        ChunkData chunk = worldGenerator.GetOrGenerateChunk(cx, cy);

        Vector2Int localXY = worldGenerator.GetChunkLocalXY(tilePos);

        int localX = localXY.x;
        int localY = localXY.y;

        if (localX < 0 || localX >= ChunkData.CHUNK_SIZE ||
            localY < 0 || localY >= ChunkData.CHUNK_SIZE)
        {
            return 0;
        }

        return chunk.tilesType[localX, localY];
    }
}
