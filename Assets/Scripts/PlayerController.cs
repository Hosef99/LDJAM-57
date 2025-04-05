using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum FacingDirection { Up, Down, Left, Right }
    private FacingDirection facing = FacingDirection.Down;

    public float moveSpeed = 5f;
    private bool isMoving = false;
    private Vector3 targetPos;

    private WorldGenerator worldGenerator;
    public int maxDigAttempts = 100;
    private int currentAttempts;
    public DigUI digUI;

    void Start()
    {
        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        transform.position = new Vector3Int(currentPos.x , ChunkData.CHUNK_SIZE/2 + 1, currentPos.z);
        targetPos = transform.position;

        worldGenerator = FindObjectOfType<WorldGenerator>();

        currentAttempts = maxDigAttempts;
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
            MoveHorizontal(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            facing = FacingDirection.Right;
            MoveHorizontal(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyBlockInFront();
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

        if (isMoving) return;

        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        Vector3Int belowPos = currentPos + new Vector3Int(0, -1, 0);

        if (!IsBlockAt(belowPos))
        {
            targetPos = belowPos;
            isMoving = true;


        }
    }

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



        if (worldGenerator != null && IsBlockAt(frontTile))
        {
            if (currentAttempts > 0)
            {
                currentAttempts--;
                digUI.UpdateDigText(currentAttempts);
            }

            worldGenerator.DestroyBlockAt(frontTile);
        }
        

        if (currentAttempts <= 0){
            EndGame();
        }

    }


    bool IsBlockAt(Vector3Int tilePos)
    {
        if (worldGenerator == null) return false;

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
            return false;
        }

        Color c = chunk.tiles[localX, localY];
        return c.a > 0.0f;
    }

    void EndGame(){
        Debug.Log("Out of attempts!");
    }
}
