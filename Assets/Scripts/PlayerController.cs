using System;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    private enum FacingDirection { Up, Down, Left, Right }
    private FacingDirection facing = FacingDirection.Down;

    public float moveSpeed = 5f;
    public int maxAttempts = 200;
    public int digCount = 1;
    public float oreBrokeChance = 1f;
    private bool isMoving = false;
    private Vector3 targetPos;
    private Random rnd = new Random();

    private WorldGenerator worldGenerator;

    private int currentAttempts;
    public DigUI digUI;
    private SpriteRenderer sr;
    private Animator anim;
    
    public int goldCount = 0;
    public int fossil1Count = 0;
    public int fossil2Count = 0;
    public int fossil3Count = 0;
    public int fossil4Count = 0;
    public int fossil5Count = 0;
    public int fossil6Count = 0;
    public int sliverCount = 0;
    public int diamondCount = 0;
    public int redStoneCount = 0;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        transform.position = new Vector3Int(currentPos.x , ChunkData.CHUNK_SIZE/2 + 1, currentPos.z);
        targetPos = transform.position;

        worldGenerator = FindObjectOfType<WorldGenerator>();

        currentAttempts = maxAttempts;
        digUI.UpdateDigText(currentAttempts);
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleFalling();
    }

    void HandleInput()
    {
        if (isMoving) return;

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
            sr.flipX = true;
            MoveHorizontal(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            facing = FacingDirection.Right;
            sr.flipX = false;
            MoveHorizontal(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyBlocksInFront(digCount);
        }
    }

    void MoveHorizontal(int dir)
    {
        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        targetPos = currentPos + new Vector3Int(dir, 0, 0);
        if (IsBlockAt(Vector3Int.RoundToInt(targetPos)))
        {
            return;
        }
        isMoving = true;
    }

    void HandleMovement()
    {
        if (isMoving)
        {
            anim.SetBool("IsMove", true);
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
        else{
            anim.SetBool("IsMove", false);
        }
    }


    void HandleFalling()
    {

        if (isMoving) return;

        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        Vector3Int belowPos = currentPos + new Vector3Int(0, -1, 0);

        if (!IsBlockAt(belowPos))
        {
            targetPos = belowPos;
            isMoving = true;
        }
    }

    void DestroyBlocksInFront(int count)
    {
        bool gotBlock = false;

        Vector3Int currentTile = Vector3Int.RoundToInt(transform.position);
        Vector3Int frontTile = currentTile;
        

        
        for (int i = 0; i < count; i++)
        {
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
            if (IsBlockAt(frontTile))
            {

                CollectBlockAt(frontTile);
                if (GetTileTypeAt(frontTile) != ChunkData.DIRT &&
                    GetTileTypeAt(frontTile) != ChunkData.STONE1 &&
                    GetTileTypeAt(frontTile) != ChunkData.STONE2 &&
                    GetTileTypeAt(frontTile) != ChunkData.STONE3 )
                {
                    double rand = rnd.NextDouble();
                    if (rand < oreBrokeChance)
                    {
                        worldGenerator.DestroyBlockAt(frontTile);
                    }
                }
                else
                {
                    worldGenerator.DestroyBlockAt(frontTile);
                }
                
                
                gotBlock = true;
            }
        }
        



        

        if (gotBlock)
        {
            anim.SetTrigger("Mine");
            currentAttempts--;
            digUI.UpdateDigText(currentAttempts);
        }
        
        if (currentAttempts <= 0){
            EndGame();
        }

    }

    void CollectBlockAt(Vector3Int tilePos)
    {
        int theTileType = GetTileTypeAt(tilePos);
        
        switch (theTileType)
        {

                    
            case ChunkData.GOLD1:
                goldCount += 10;
                break;
                    
            case ChunkData.GOLD2:
                goldCount += 20;
                break;
                    
            case ChunkData.GOLD3:
                goldCount += 30;
                break;
                    
            case ChunkData.FOSSIL1:
                fossil1Count += 1;
                break;
                    
            case ChunkData.FOSSIL2:
                fossil2Count += 1;
                break;
                    
            case ChunkData.FOSSIL3:
                fossil3Count += 1;
                break;
                    
            case ChunkData.FOSSIL4:
                fossil4Count += 1;
                break;
                    
            case ChunkData.FOSSIL5:
                fossil5Count += 1;
                break;
                    
            case ChunkData.FOSSIL6:
                fossil6Count += 1;
                break;
                    
            default:
                break;
        }
    }




    bool IsBlockAt(Vector3Int tilePos)
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

    void EndGame(){
        Debug.Log("Out of attempts!");
    }
}
