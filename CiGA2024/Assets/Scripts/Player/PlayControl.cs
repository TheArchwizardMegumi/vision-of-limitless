using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnderCloud;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class PlayControl : MonoBehaviour
{
    public static PlayControl Instance;
    [Header("控制相关")]
    public Transform player;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime;
    public Vector3 position;
    public bool isWalk;
    Vector2 dir;
    [Header("撞墙相关")]
    public bool touchWall;
    public bool touchUpWall;
    public bool touchDownWall;
    public bool touchLeftWall;
    public bool touchRightWall;
    public Vector3 backUpPosition;
    public Vector3 backLeftPosition;
    public Vector3 backDownPosition;
    public Vector3 backRightPosition;
    public float backSmoothTime;
    public float distance;
    public SortingGroup sortingGroup;
    [Header("睁眼闭眼")]
    public PlayerState isOpenEye = PlayerState.Open;
    public float blinkTime = 0.5f;
    public bool eyeOpening;
    bool isBlinking = false;
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
    }
    private void Update()
    {
        CheckWin();
        Timer();
        Move();
        IsStuckInWall();
        EyeStateCheck();
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
        isOpenEye = PlayerState.Open;
        anim.SetTrigger("Revive");
        velocity = Vector3.zero;
        SwitchCameraFollow(true);
    }

    private void SwitchCameraFollow(bool onOff)
    {
        //检查摄像机是否只有一个
        GameObject[] mCamera = GameObject.FindGameObjectsWithTag("MainCamera");
        if (mCamera.Length > 1)
        {
            for (int i = 1; i < mCamera.Length; i++)
            {
                Destroy(mCamera[i]);
            }
            mCamera[0].transform.SetParent(transform);
        }
        if (onOff)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).transform.SetParent(null);
        }
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
            SwitchCameraFollow(false);
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
        if (isHurt == false&&isWalk == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isBlinking)
            {
                isBlinking = true;
                ChangeEyeState();
                StartCoroutine(Blinking());
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                sortingGroup.sortingLayerName = "Player";
                if (isWalk == false && touchUpWall == false && crashWall == false)
                {
                    position.y += 1;
                    isWalk = true;
                    
                }
                if (touchUpWall == true)
                {
                    if (MapManager.IsDamagable(new Vector2Int((int)position.x, (int)position.y + 1), isOpenEye))
                    {
                        PlayerDie();
                    }
                    crashWall = true;
                    transform.position = Vector3.SmoothDamp(transform.position, backUpPosition, ref velocity, backSmoothTime);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (isWalk == false && touchDownWall == false && crashWall == false)
                {
                    position.y += -1;
                    isWalk = true;
                }
                if (touchDownWall == true)
                {
                    sortingGroup.sortingLayerName = "CrashLayer";
                    if (MapManager.IsDamagable(new Vector2Int((int)position.x, (int)position.y - 1), isOpenEye))
                    {
                        PlayerDie();
                    }
                    crashWall = true;
                    transform.position = Vector3.SmoothDamp(transform.position, backDownPosition, ref velocity, backSmoothTime);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                sortingGroup.sortingLayerName = "Player";
                player.localScale = new Vector3(-1, 1, 1);
                if (isWalk == false && touchLeftWall == false && crashWall == false)
                {
                    position.x += -1;
                    isWalk = true;
                }
                if (touchLeftWall == true)
                {
                    if (MapManager.IsDamagable(new Vector2Int((int)position.x - 1, (int)position.y), isOpenEye))
                    {
                        PlayerDie();
                    }
                    crashWall = true;
                    transform.position = Vector3.SmoothDamp(transform.position, backLeftPosition, ref velocity, backSmoothTime);

                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                sortingGroup.sortingLayerName = "Player";
                player.localScale = new Vector3(1, 1, 1);
                if (isWalk == false && touchRightWall == false && crashWall == false)
                {
                    position.x += 1;
                    isWalk = true;
                }
                if (touchRightWall == true)
                {
                    if (MapManager.IsDamagable(new Vector2Int((int)position.x + 1, (int)position.y), isOpenEye))
                    {
                        PlayerDie();
                    }
                    crashWall = true;
                    transform.position = Vector3.SmoothDamp(transform.position, backRightPosition, ref velocity, backSmoothTime);
                }

            }
        }
    }

    public void TouchWallEffect()
    {
        touchWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y), isOpenEye);
        touchUpWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y + 1), isOpenEye);
        touchDownWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y - 1), isOpenEye);
        touchLeftWall = !MapManager.IsAccessible(new Vector2Int((int)position.x - 1, (int)position.y), isOpenEye);
        touchRightWall = !MapManager.IsAccessible(new Vector2Int((int)position.x + 1, (int)position.y), isOpenEye);

        //if(position.x > 0.5||position.y > 2.5)    //测试用的
        //{                                         //测试用的        
        //    touchWall = true;                     //测试用的            
        //}                                         //测试用的      
    }
    private void ChangeEyeState()
    {
        isOpenEye = isOpenEye == PlayerState.Open ? PlayerState.Close : PlayerState.Open;
        Messenger.Broadcast<PlayerState>(MsgType.changeOpenCloseEye, isOpenEye);
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

    IEnumerator Blinking()
    {
        yield return new WaitForSeconds(blinkTime);
        isBlinking = false;
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

    void EyeStateCheck()
    {
        if (isOpenEye == PlayerState.Open)
        {
            eyeOpening = true;
        }
        else
        {
            eyeOpening = false;
        }
    }

    //Animation
    public void SetAnimation()
    {
        anim.SetBool("isWalk", isWalk);
        anim.SetBool("crashWall", crashWall);
        anim.SetBool("isHurt", isHurt);
        anim.SetBool("eyeOpening", eyeOpening);
    }
}
