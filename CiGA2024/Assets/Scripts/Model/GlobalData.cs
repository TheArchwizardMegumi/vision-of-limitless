using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OvO;
using UnderCloud;


public class GlobalData : MonoBehaviour
{
    public static PlayerController playerController;
    public static MapManager MapManagerInstance { get; set; }
    public static TimerManager TimerManagerInstance { get; set; }
    public static int TransformWallLayerNum;
}
