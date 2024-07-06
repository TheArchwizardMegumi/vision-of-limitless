using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnderCloud;


public class PlayControl : MonoBehaviour
{
    [Header("控制相关")]
    public Transform player;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime;
    public Vector3 position;
    public bool isWalk;
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
    [Header("睁眼闭眼")]
    public PlayerState isOpenEye = PlayerState.Open;
    [Header("人物受伤")]
    public bool isHurt;
    private void FixedUpdate()
    {
        TouchWallEffect();

    }
    private void Update()
    {
        Move();
        PlayerDie();
        ControlCheck();



    }

    public void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, smoothTime);
        if (Vector2.SqrMagnitude(player.position - position) < 0.001f)
        {
            player.position = position;
            isWalk = false;
        }
                
    }

    public void ControlCheck()
    {
        backUpPosition = new Vector3(position.x, position.y + 0.8f, position.z);
        backDownPosition = new Vector3(position.x, position.y -  0.8f, position.z);
        backLeftPosition = new Vector3(position.x - 0.8f, position.y, position.z);
        backRightPosition = new Vector3(position.x + 0.8f, position.y, position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (touchWall == false && isWalk == false)
            {
                ChangeEyeState();
            }

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isWalk == false && touchUpWall == false)
            {
                position.y += 1;
                position.z += -0.01f;
                isWalk = true;
                
            }
            if(touchUpWall == true)
            {
                transform.position = Vector3.SmoothDamp(transform.position, backUpPosition, ref velocity, backSmoothTime);
            }



        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isWalk == false && touchDownWall == false)
            {
                position.y += -1;
                position.z += 0.01f;
                isWalk = true;
            }
            if (touchDownWall == true)
            {
                transform.position = Vector3.SmoothDamp(transform.position, backDownPosition, ref velocity, backSmoothTime);
            }
           

        }

        if (Input.GetKeyDown(KeyCode.A))
        {if (isWalk == false&&touchLeftWall==false)
            {   
                position.x += -1;
                isWalk = true;
                
            }
        if (touchLeftWall == true)
            {
                transform.position = Vector3.SmoothDamp(transform.position,backLeftPosition ,ref velocity, backSmoothTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isWalk == false&&touchRightWall==false)
            {
                position.x += 1;
                isWalk = true;
            }
            if (touchRightWall == true)
            {
                transform.position = Vector3.SmoothDamp(transform.position, backRightPosition, ref velocity, backSmoothTime);
            }

        }

    }

    public void TouchWallEffect()
    {  
        touchWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y), isOpenEye);
        touchUpWall = !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y+1), isOpenEye);
        touchDownWall= !MapManager.IsAccessible(new Vector2Int((int)position.x, (int)position.y-1), isOpenEye);
        touchLeftWall= !MapManager.IsAccessible(new Vector2Int((int)position.x-1, (int)position.y), isOpenEye);
        touchRightWall= !MapManager.IsAccessible(new Vector2Int((int)position.x+1, (int)position.y), isOpenEye);

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
        if (touchWall == true)
        {
            isHurt = true;
        }
        if (isHurt == true)
        {
            Messenger.Broadcast(MsgType.playerHurt);

        }
    }



}
