using UnityEngine;

public class BoomScript : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float moveSpeed = 5f;
    public float lifetime = 3f;
    public int range = 3;

    private float timer = 0f;
    private bool isMoving = false;
    private Vector3 targetPos;

    private WorldGenerator worldGenerator;
    private PlayerController player;
    private PlayerData data;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        worldGenerator = FindObjectOfType<WorldGenerator>();
        data = PlayerData.Instance;
    }

    void Update()
    {
        timer += Time.deltaTime;

        HandleMovement();
        HandleFalling();

        if (timer >= lifetime && !isMoving)
        {
            Boom();
        }
    }

    private void Boom()
    {
        SoundManager.Instance.PlaySFX("explosion");

        Vector3Int center = Vector3Int.RoundToInt(transform.position);

        for (int dx = -range; dx <= range; dx++)
        {
            for (int dy = -range; dy <= range; dy++)
            {
                Vector3Int offset = new Vector3Int(dx, dy, 0);
                Vector3Int targetTile = center + offset;

                if (offset.sqrMagnitude <= (range - 0.5f) * (range - 0.5f))
                {
                    if (data.GetStat(Stat.BombCollectOre) != 0)
                        player.CollectBlockAt(targetTile);

                    if (data.GetStat(Stat.BombImmune) == 0 && Vector3.Distance(player.transform.position, targetTile) < 0.5f)
                    {
                        Debug.Log("Player DEATH");
                    }

                    player.DestroyBlockAt(targetTile);
                }
            }
        }

        Destroy(gameObject);
    }

    private void HandleMovement()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.001f)
        {
            transform.position = targetPos;
            isMoving = false;
        }
    }

    private void HandleFalling()
    {
        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        Vector3Int belowPos = currentPos + Vector3Int.down;

        if (!IsBlockAt(belowPos))
        {
            targetPos = belowPos;
            isMoving = true;
        }
    }

    private bool IsBlockAt(Vector3Int tilePos)
    {
        return GetTileTypeAt(tilePos) != ChunkData.HOLE;
    }

    private int GetTileTypeAt(Vector3Int tilePos)
    {
        if (worldGenerator == null) return 0;

        Vector2Int chunkXY = worldGenerator.GetChunkXY(tilePos);
        Vector2Int localXY = worldGenerator.GetChunkLocalXY(tilePos);

        // Preload surrounding chunks (for safety/visuals maybe?)
        for (int dx = -3; dx <= 3; dx++)
        {
            for (int dy = -2; dy <= 2; dy++)
            {
                worldGenerator.GetOrGenerateChunk(chunkXY.x + dx, chunkXY.y + dy);
            }
        }

        ChunkData chunk = worldGenerator.GetOrGenerateChunk(chunkXY.x, chunkXY.y);

        if (localXY.x < 0 || localXY.x >= ChunkData.CHUNK_SIZE ||
            localXY.y < 0 || localXY.y >= ChunkData.CHUNK_SIZE)
        {
            return 0;
        }

        return chunk.tilesType[localXY.x, localXY.y];
    }
}
