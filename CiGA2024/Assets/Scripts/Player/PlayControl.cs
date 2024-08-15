using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnderCloud;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.Diagnostics;

public class PlayControl : MonoBehaviour
{
    public static PlayControl Instance;
    [SerializeField]
    private Camera mCamera;
    [Header("控制相关")]
    public Transform player;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime;
    public Vector3 position;
    public bool isWalk;
    public bool walking;
    Vector2 dir;
    [Header("撞墙相关")]
    public int timer = 80;
    public int time;
    public bool touchWall;
    public bool touchUpWall;
    public bool touchDownWall;
    public bool touchLeftWall;
    public bool touchRightWall;
    public bool upBesidePlayer;
    public bool downBesidePlayer;
    public bool leftBesidePlayer;
    public bool rightBesidePlayer;
    public bool upBesidePlayerTouchWall;
    public bool downBesidePlayerTouchWall;
    public bool leftBesidePlayerTouchWall;
    public bool rightBesidePlayerTouchWall;
    public Vector3 backUpPosition;
    public Vector3 backLeftPosition;
    public Vector3 backDownPosition;
    public Vector3 backRightPosition;
    public float backSmoothTime;
    public float distance;
    public SortingGroup sortingGroup;
    public PlayerState IsOpenEye => BlinkChecker.Instance.isOpenEye;
    public bool IsBlinking => BlinkChecker.Instance.isBlinking;
    public bool EyeOpening => BlinkChecker.Instance.EyeOpening;
    [Header("人物受伤")]
    public bool isHurt;
    public bool isDead;
    [Header("动画相关")]
    private Animator anim;
    public bool crashWall;
    [Header("音乐相关")]
    public AudioSource walk;
    public AudioSource hitWall;
    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        
        TouchWallEffect();
        if (EyeOpening == false)
        {
            sortingGroup.sortingLayerName = "Player";
            print("EyeOpening == false");
        }
        WalkTimer();

    }
    private void Update()
    {
        CheckWin();
        Timer();
        Move();
        IsStuckInWall();
        ControlCheck();
        SetAnimation();
    }

    public void Init()
    {
        gameObject.SetActive(true);
        isWalk = false;
        isHurt = false;
        isDead = false;
        touchWall = false;
        touchUpWall = false;
        touchLeftWall = false;
        touchRightWall = false;
        touchDownWall = false;
        anim.SetTrigger("Revive");
        velocity = Vector3.zero;
    }

    public static void SpawnPlayer(Vector3 position)
    {
        if (Instance == null)
        {
            return;
        }
        else
        {
            Instance.gameObject.SetActive(true);
            Instance.transform.position = position;
            Instance.position = position;
            Instance.Init();
        }
    }

    private void CheckWin()
    {
        if (MapManager.GetTile(new Vector2Int((int)position.x, (int)position.y))?.type == TileType.Exit)
        {
            gameObject.SetActive(false);
            PlayerWinChecker.ReachExit(0, new Vector2Int((int)position.x, (int)position.y));
            transform.position = Vector3.zero;
            position = Vector3.zero;
        }
    }

    private void IsStuckInWall()
    {
        if (touchWall && !isDead)
        {
            PlayerDie();
        }
    }

    public void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, smoothTime);
        if (Vector2.SqrMagnitude(player.position - position) < 0.001f)
        {
            player.position = position;
            isWalk = false;
        }
        // audio
        if (isWalk)
        {
            walk.Play();
        }
    }

    public void ControlCheck()
    {
        backUpPosition = new Vector3(position.x, position.y + 0.8f, position.z);
        backDownPosition = new Vector3(position.x, position.y - 0.8f, position.z);
        backLeftPosition = new Vector3(position.x - 0.8f, position.y, position.z);
        backRightPosition = new Vector3(position.x + 0.8f, position.y, position.z);
        if (isHurt == false && isWalk == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                sortingGroup.sortingLayerName = "Player";
                if (walking == false && isWalk == false && touchUpWall == false && crashWall == false && (upBesidePlayer == false || upBesidePlayerTouchWall == false))
                {
                    position.y += 1;
                    isWalk = true;
                }
                if (touchUpWall == true || upBesidePlayer == true && upBesidePlayerTouchWall == true)
                {
                    if (MapManager.IsDamagable(new Vector2Int((int)position.x, (int)position.y + 1), IsOpenEye))
                    {
                        PlayerDie();
                    }
                    crashWall = true;
                    walking = true;
                    transform.position = Vector3.SmoothDamp(transform.position, backUpPosition, ref velocity, backSmoothTime);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (walking == false && isWalk == false && touchDownWall == false && crashWall == false && (downBesidePlayer == false || downBesidePlayerTouchWall == false))
                {
                    position.y += -1;
                    isWalk = true;
                }
                if (touchDownWall == true || downBesidePlayer == true && downBesidePlayerTouchWall == true)
                {
                    sortingGroup.sortingLayerName = "CrashLayer";
                    if (MapManager.IsDamagable(new Vector2Int((int)position.x, (int)position.y - 1), IsOpenEye))
                    {
                        PlayerDie();
                    }
                    crashWall = true;
                    walking = true;
                    transform.position = Vector3.SmoothDamp(transform.position, backDownPosition, ref velocity, backSmoothTime);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                sortingGroup.sortingLayerName = "Player";
                player.localScale = new Vector3(-1, 1, 1);
                if (walking == false && isWalk == false && touchLeftWall == false && crashWall == false && (leftBesidePlayer == false || leftBesidePlayerTouchWall == false))
                {
                    position.x += -1;
                    isWalk = true;
                }
                if (touchLeftWall == true || leftBesidePlayer == true && leftBesidePlayerTouchWall == true)
                {
                    if (MapManager.IsDamagable(new Vector2Int((int)position.x - 1, (int)position.y), IsOpenEye))
                    {
                        PlayerDie();
                    }
                    crashWall = true;
                    walking = true;
                    transform.position = Vector3.SmoothDamp(transform.position, backLeftPosition, ref velocity, backSmoothTime);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                sortingGroup.sortingLayerName = "Player";
                player.localScale = new Vector3(1, 1, 1);
                if (walking == false && isWalk == false && touchRightWall == false && crashWall == false && (rightBesidePlayer == false || rightBesidePlayerTouchWall == false))
                {
                    position.x += 1;
                    isWalk = true;
                }
                if (touchRightWall == true || rightBesidePlayer == true && rightBesidePlayerTouchWall == true)
                {
                    if (MapManager.IsDamagable(new Vector2Int((int)position.x + 1, (int)position.y), IsOpenEye))
                    {
                        PlayerDie();
                    }
                    crashWall = true;
                    walking = true;
                    transform.position = Vector3.SmoothDamp(transform.position, backRightPosition, ref velocity, backSmoothTime);
                }

            }
        }
    }

    public void TouchWallEffect()
    {
        touchWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y), IsOpenEye);
        touchUpWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y + 1), IsOpenEye);
        touchDownWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y - 1), IsOpenEye);
        touchLeftWall = !MapManager.IsAccessible(new Vector2Int((int)position.x - 1, (int)position.y), IsOpenEye);
        touchRightWall = !MapManager.IsAccessible(new Vector2Int((int)position.x + 1, (int)position.y), IsOpenEye);
        upBesidePlayer = MapManager.IsPlayer(new Vector2Int((int)position.x, (int)position.y + 1));
        downBesidePlayer = MapManager.IsPlayer(new Vector2Int((int)position.x, (int)position.y - 1));
        leftBesidePlayer = MapManager.IsPlayer(new Vector2Int((int)position.x - 1, (int)position.y));
        rightBesidePlayer = MapManager.IsPlayer(new Vector2Int((int)position.x + 1, (int)position.y));
        upBesidePlayerTouchWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y + 2), IsOpenEye);
        downBesidePlayerTouchWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y - 2), IsOpenEye);
        leftBesidePlayerTouchWall = !MapManager.IsAccessible(new Vector2Int((int)position.x - 2, (int)position.y), IsOpenEye);
        rightBesidePlayerTouchWall = !MapManager.IsAccessible(new Vector2Int((int)position.x + 2, (int)position.y), IsOpenEye);

        //if(position.x > 0.5||position.y > 2.5)    //测试用的
        //{                                         //测试用的        
        //    touchWall = true;                     //测试用的            
        //}                                         //测试用的      
    }
    private void PlayerDie()
    {
        if (!isDead)
        {
            isDead = true;
            StartCoroutine(DelayDying());
        }
    }

    IEnumerator DelayDying()
    {
        yield return new WaitForSeconds(0.1f);
        isHurt = true;
        yield return new WaitForSeconds(1.5f);
        Messenger.Broadcast(MsgType.playerHurt);
        transform.position = new Vector3(200, 200, 0);
        position = transform.position;
        yield return null;
    }


    public void Timer()
    {
        float timer = 4;
        float time = 0;
        if (crashWall == true)
        {
            //audio
            hitWall.Play();
            time++;
            if (time < timer && crashWall == true)
            {
                crashWall = false;
                time = 0;
            }
        }

    }
    public void WalkTimer()
    {
        if (walking == true)
        {
            time++;
            if (time >= timer)
            {
                walking = false;
                time = 0;
            }
        }

    }



    //Animation
    public void SetAnimation()
    {
        anim.SetBool("isWalk", isWalk);
        anim.SetBool("crashWall", crashWall);
        anim.SetBool("isHurt", isHurt);
        anim.SetBool("eyeOpening", EyeOpening);
    }
}
