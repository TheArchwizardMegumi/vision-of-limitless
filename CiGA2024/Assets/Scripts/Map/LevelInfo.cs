using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public int level;
    public int Chapter => level < 18 ? 1 : 2;
}
