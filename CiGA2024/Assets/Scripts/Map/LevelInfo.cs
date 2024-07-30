using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public int level;
    public int Chapter => level < 10 ? 1 : 2;
}
