using System.Collections;
using System.Collections.Generic;
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
        GameObject[] pls = GameObject.FindGameObjectsWithTag("Player");
        if (pls.Length > 0)
        {
            if (pls.Length > 1)
            {
                if (pls[0].activeSelf && pls[1].activeSelf)
                {
                    SwitchState(CameraState.Center);
                }
            }
            else
            {
                SwitchState(CameraState.FollowP1);
            }
        }
        else
        {
            SwitchState(CameraState.Default);
        }
    }
    void Update()
    {
        switch (state)
        {
            case CameraState.Center:
                if (!Player2.Instance.isActiveAndEnabled)
                {
                    SwitchState(CameraState.FollowP1);
                }
                transform.position = (PlayControl.Instance.transform.position + Player2.Instance.transform.position) / 2 + new Vector3(0, 0, -10);
                break;
            case CameraState.FollowP1:
                if (PlayControl.Instance.isActiveAndEnabled)
                {
                    transform.position = new Vector3(PlayControl.Instance.transform.position.x, PlayControl.Instance.transform.position.y, -10);
                }
                else
                    SwitchState(CameraState.FollowP2);
                break;
            case CameraState.FollowP2:
                if (Player2.Instance.isActiveAndEnabled)
                {
                    transform.position = new Vector3(Player2.Instance.transform.position.x, Player2.Instance.transform.position.y, -10);
                }
                else
                    SwitchState(CameraState.FollowP1);
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
                GameObject[] pls = GameObject.FindGameObjectsWithTag("Player");
                if (pls.Length < 2)
                {
                    Debug.LogError("Player number less than 2");
                }
                else
                {
                    transform.SetParent(null);
                    this.state = CameraState.Center;
                }
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