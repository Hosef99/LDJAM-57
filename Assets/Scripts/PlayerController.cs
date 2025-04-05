using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 面向的四个方向
    private enum FacingDirection { Up, Down, Left, Right }
    private FacingDirection facing = FacingDirection.Right; // 初始面向右

    public float moveSpeed = 5f;         // 控制左右移动插值速度
    private bool isMoving = false;      // 是否正在移动到下一个格子
    private Vector3 targetPos;          // 离散移动的目标位置

    private WorldGenerator worldGenerator;

    void Start()
    {
        // 设置初始位置(0,0)，并记录为目标位置
        transform.position = Vector3.zero;
        targetPos = transform.position;

        // 找到场景中的 WorldGenerator
        worldGenerator = FindObjectOfType<WorldGenerator>();

        // 确保脚下区块已生成
        EnsureChunkLoadedAtPosition(transform.position);
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleFalling();
    }

    /// <summary>
    /// 处理玩家输入：W/S 改变朝向(不移动)，A/D 左右移动，Space 破坏面前方块
    /// </summary>
    void HandleInput()
    {
        // 若正在移动，不接受新的移动输入
        if (isMoving) return;

        // W / S 只改变面向方向，不移动
        if (Input.GetKey(KeyCode.W))
        {
            facing = FacingDirection.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            facing = FacingDirection.Down;
        }
        // A / D 左右移动
        else if (Input.GetKey(KeyCode.A))
        {
            facing = FacingDirection.Left;
            MoveHorizontal(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            facing = FacingDirection.Right;
            MoveHorizontal(1);
        }

        // Space 破坏面前方块
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyBlockInFront();
        }
    }

    /// <summary>
    /// 执行左右移动一格
    /// </summary>
    void MoveHorizontal(int dir)
    {
        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        targetPos = currentPos + new Vector3Int(dir, 0, 0);
        if (IsBlockAt(Vector3Int.RoundToInt(targetPos)))
        {
            return;
        }
        isMoving = true;

        // 确保目标位置所在 Chunk 已生成
        EnsureChunkLoadedAtPosition(targetPos);
    }

    /// <summary>
    /// 每帧插值移动到 targetPos，模拟格子移动
    /// </summary>
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

    /// <summary>
    /// 检测脚下是否为空，如果为空就往下掉一格
    /// </summary>
    void HandleFalling()
    {
        // 如果正在手动左右移动，则先不处理下落
        if (isMoving) return;

        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        Vector3Int belowPos = currentPos + new Vector3Int(0, -1, 0);

        // 判断 belowPos 是否有方块
        if (!IsBlockAt(belowPos))
        {
            // 如果没有方块则往下掉
            targetPos = belowPos;
            isMoving = true;

            // 确保下面的 Chunk 已生成
            EnsureChunkLoadedAtPosition(belowPos);
        }
    }

    /// <summary>
    /// 按朝向破坏前方的方块
    /// </summary>
    void DestroyBlockInFront()
    {
        Vector3Int currentTile = Vector3Int.RoundToInt(transform.position);
        Vector3Int frontTile = currentTile;

        switch (facing)
        {
            case FacingDirection.Up:
                frontTile += new Vector3Int(0, 1, 0);
                break;
            case FacingDirection.Down:
                frontTile += new Vector3Int(0, -1, 0);
                break;
            case FacingDirection.Left:
                frontTile += new Vector3Int(-1, 0, 0);
                break;
            case FacingDirection.Right:
                frontTile += new Vector3Int(1, 0, 0);
                break;
        }



        if (worldGenerator != null)
        {
            worldGenerator.DestroyBlockAt(frontTile);
        }
    }

    /// <summary>
    /// 检查指定 Tile 上是否有方块（alpha>0即视为有方块）
    /// </summary>
    bool IsBlockAt(Vector3Int tilePos)
    {
        if (worldGenerator == null) return false;

        Vector2Int chunkXY = worldGenerator.GetChunkXY(tilePos);
        int cx = chunkXY.x;
        int cy = chunkXY.y;
        ChunkData chunk = worldGenerator.GetOrGenerateChunk(cx, cy);

        Vector2Int localXY = worldGenerator.GetLocalXY(tilePos);

        int localX = localXY.x;
        int localY = localXY.y;

        // 越界就认为没方块
        if (localX < 0 || localX >= ChunkData.CHUNK_SIZE ||
            localY < 0 || localY >= ChunkData.CHUNK_SIZE)
        {
            return false;
        }

        Color c = chunk.tiles[localX, localY];
        return c.a > 0.0f;
    }

    /// <summary>
    /// 确保指定世界位置所在的 Chunk 已经生成
    /// </summary>
    void EnsureChunkLoadedAtPosition(Vector3 pos)
    {
        if (worldGenerator == null) return;

        int cx = Mathf.FloorToInt(pos.x / (float)ChunkData.CHUNK_SIZE);
        int cy = Mathf.FloorToInt(pos.y / (float)ChunkData.CHUNK_SIZE);
        worldGenerator.GetOrGenerateChunk(cx, cy);
    }
}
