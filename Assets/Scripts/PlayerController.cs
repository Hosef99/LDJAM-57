using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    private enum FacingDirection { Up, Down, Left, Right }
    private FacingDirection facing = FacingDirection.Down;

    public GameObject boomPrefab;
    public float moveSpeed = 5f;
    public int maxAttempts = 200;
    public int horizontalDigCount  = 1;

    public int verticalDigCount = 1;
    public float oreBrokeChance = 1f;
    private bool isMoving = false;
    private Vector3 targetPos;
    private Random rnd = new Random();
    private WorldGenerator worldGenerator;
    public PlayerData playerData ;

    private int currentAttempts;
    public DigUI digUI;
    private SpriteRenderer sr;
    private Animator anim;
    public GameObject stoneParticle;



    public int masterYi = 0;
    private int lastHitOnRow = 0;
    private int rowStreak = 0;

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
            if (facing == FacingDirection.Down || facing == FacingDirection.Up)
            {
                DestroyBlocksInFront(verticalDigCount);
            }
            else
            {
                DestroyBlocksInFront(horizontalDigCount);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
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
            GameObject boomClone = Instantiate(boomPrefab);

            if (!IsBlockAt(frontTile))
            {
                boomClone.transform.position = frontTile;
            }
            else
            {
                boomClone.transform.position = transform.position;

            }
            boomClone.GetComponent<BoomScript>().enabled = true;


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

        if (currentTile.y == lastHitOnRow)
        {
            rowStreak += 1;
        }
        else
        {
            lastHitOnRow = currentTile.y;
            rowStreak = 1;
        }

        if (masterYi > 0)
        {

            switch (masterYi)
            {
                case 1:
                    if (rowStreak % 5 == 0)
                    {
                        count *= 2;
                        rowStreak += 1;
                    }
                    break;
                case 2:
                    if (rowStreak % 4 == 0)
                    {
                        count *= 2;
                    }
                    break;
                case 3:
                    if (rowStreak % 3 == 0)
                    {
                        count *= 2;
                        rowStreak += 1;
                    }
                    break;
                default:
                    break;
            }
        }



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
                    GetTileTypeAt(frontTile) != ChunkData.OBSIDIAN1 &&
                    GetTileTypeAt(frontTile) != ChunkData.OBSIDIAN2 &&
                    GetTileTypeAt(frontTile) != ChunkData.OBSIDIAN3)
                {
                    double rand = rnd.NextDouble();
                    if (rand < oreBrokeChance)
                    {
                        DestroyBlockAt(frontTile);
                    }
                }else if(GetTileTypeAt(frontTile) == ChunkData.OBSIDIAN1 ||
                         GetTileTypeAt(frontTile) == ChunkData.OBSIDIAN2 ||
                         GetTileTypeAt(frontTile) == ChunkData.OBSIDIAN3)
                {
                    
                    switch (GetTileTypeAt(frontTile))
                    {
                        case ChunkData.OBSIDIAN1:
                            ChangeBlockAt(frontTile, ChunkData.OBSIDIAN2);
                            break;
                        case ChunkData.OBSIDIAN2:
                            ChangeBlockAt(frontTile, ChunkData.OBSIDIAN3);
                            break;
                        case ChunkData.OBSIDIAN3:
                            DestroyBlockAt(frontTile);
                            break;

                    }
                }
                else
                {
                    DestroyBlockAt(frontTile);
                }



                gotBlock = true;
            }
        }

        if (gotBlock)
        {
            anim.SetTrigger("Mine");
            GameObject tempParticle = Instantiate(stoneParticle, (Vector3)frontTile-new Vector3(0,0.5f,0), Quaternion.identity);
            tempParticle.transform.eulerAngles = new Vector3(0,0,45);
            StartCoroutine("DestroyParticle", tempParticle);
            currentAttempts--;
            digUI.UpdateDigText(currentAttempts);
        }

        if (currentAttempts <= 0){
            EndGame();
        }

    }

    IEnumerator DestroyParticle(GameObject particle){
        yield return new WaitForSeconds(1f);
        Destroy(particle);
    }

    public void CollectBlockAt(Vector3Int tilePos)
    {
        int theTileType = GetTileTypeAt(tilePos);

        switch (theTileType)
        {


            case ChunkData.GOLD1:
                playerData.goldCount += 10;
                break;

            case ChunkData.GOLD2:
                playerData.goldCount += 20;
                break;

            case ChunkData.GOLD3:
                playerData.goldCount += 30;
                break;

            case ChunkData.FOSSIL1:
                playerData.fossil1Count += 1;
                break;

            case ChunkData.FOSSIL2:
                playerData.fossil2Count += 1;
                break;

            case ChunkData.FOSSIL3:
                playerData.fossil3Count += 1;
                break;

            case ChunkData.FOSSIL4:
                playerData.fossil4Count += 1;
                break;

            case ChunkData.FOSSIL5:
                playerData.fossil5Count += 1;
                break;

            case ChunkData.FOSSIL6:
                playerData.fossil6Count += 1;
                break;
            
            case ChunkData.DIAMOND:
                playerData.diamondCount += 1;
                break;
            
            case ChunkData.REDSTONE:
                playerData.redStoneCount += 10;
                break;

            default:
                break;
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

    void EndGame(){
        Debug.Log("Out of attempts!");
        SceneManager.LoadScene("PermanentShop");
    }

    public void DestroyBlockAt(Vector3Int tilePos)
    {
        ChangeBlockAt(tilePos, ChunkData.HOLE);
    }

    public void ChangeBlockAt(Vector3Int tilePos, int type)
    {
        Vector2Int chunkXY = worldGenerator.GetChunkXY(tilePos);
        int cx = chunkXY.x;
        int cy = chunkXY.y;

        ChunkData chunk = worldGenerator.GetOrGenerateChunk(cx, cy);
        Vector2Int localXY = worldGenerator.GetChunkLocalXY(tilePos);

        int localX = localXY.x;
        int localY = localXY.y;


        if (localX < 0 || localX >= ChunkData.CHUNK_SIZE ||
            localY < 0 || localY >= ChunkData.CHUNK_SIZE)
            return;

        chunk.tilesType[localX, localY] = type;

        if (worldGenerator.chunkRenderer != null)
        {
            worldGenerator.chunkRenderer.RefreshChunk(chunk);
        }

        GameObject tempParticle = Instantiate(stoneParticle, tilePos, Quaternion.identity);
        tempParticle.transform.eulerAngles = new Vector3(0,0,45);
        StartCoroutine("DestroyParticle", tempParticle);
    }
}
