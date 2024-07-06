using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransformWallData")]
[Serializable]
public class TransformWallData : ScriptableObject
{
    public List<Vector2Int>[] data = new List<Vector2Int>[2];
}