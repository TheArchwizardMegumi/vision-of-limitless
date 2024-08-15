using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public static Camera mainCamera;
    public static MainCameraController instance;
    private CameraState state = CameraState.Default;
    private void Awake()
    {
        instance = this;
        Messenger.AddListener(MsgType.levelStart, InitCameraState);
        mainCamera = Camera.main;
    }

    public void InitCameraState()
    {
        if (PlayControl.Instance.isActiveAndEnabled)
        {
            if (Player2.Instance.isActiveAndEnabled)
            {
                SwitchState(CameraState.Center);
            }
            else
            {
                SwitchState(CameraState.FollowP1);
            }
        }
        else if (Player2.Instance.isActiveAndEnabled)
        {
            SwitchState(CameraState.FollowP2);
        }
        else
        {
            SwitchState(CameraState.Default);
        }
    }
    void Update()
    {
        Vector3 target;
        switch (state)
        {
            case CameraState.Center:
                if (!Player2.Instance.isActiveAndEnabled)
                {
                    SwitchState(CameraState.FollowP1);
                }
                if (!PlayControl.Instance.isActiveAndEnabled)
                {
                    SwitchState(CameraState.FollowP2);
                }
                target = (PlayControl.Instance.transform.position + Player2.Instance.transform.position) / 2 + new Vector3(0, 0, -10);
                transform.position = Vector3.Lerp(transform.position, target, 0.02f);
                break;
            case CameraState.FollowP1:
                if (Player2.Instance.isActiveAndEnabled)
                {
                    SwitchState(CameraState.Center);
                }
                else if (PlayControl.Instance.isActiveAndEnabled)
                {
                    target = new Vector3(PlayControl.Instance.transform.position.x, PlayControl.Instance.transform.position.y, -10);
                    transform.position = Vector3.Lerp(transform.position, target, 0.02f);
                }
                else
                    SwitchState(CameraState.Default);
                break;
            case CameraState.FollowP2:
                if (PlayControl.Instance.isActiveAndEnabled)
                {
                    SwitchState(CameraState.Center);
                }
                else if (Player2.Instance.isActiveAndEnabled)
                {

                    target = new Vector3(Player2.Instance.transform.position.x, Player2.Instance.transform.position.y, -10);
                    transform.position = Vector3.Lerp(transform.position, target, 0.02f);
                }
                else
                    SwitchState(CameraState.Default);
                break;
            case CameraState.Default:
                transform.position = new Vector3 (0, 0, -10);
                break;
        }
    }
    public void SwitchState(CameraState state)
    {
        switch (state)
        {
            case CameraState.Center:
                this.state = CameraState.Center;
                if (!PlayControl.Instance.isActiveAndEnabled)
                    SwitchState(CameraState.FollowP2);
                if (!Player2.Instance.isActiveAndEnabled)
                    SwitchState(CameraState.FollowP1);
                break;
            case CameraState.FollowP1:
                if (PlayControl.Instance)
                {
                    this.state = CameraState.FollowP1;
                }
                else
                {
                    Debug.Log("P1 doesn't exist");
                }
                break;
            case CameraState.FollowP2:
                if (Player2.Instance)
                {
                    this.state = CameraState.FollowP2;
                }
                else
                {
                    Debug.Log("P2 doesn't exist");
                }
                break;
        }
    }
}

public enum CameraState
{
    FollowP1,
    FollowP2,
    Center,
    Default
}