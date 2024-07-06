using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransformWallData")]
[Serializable]
public class TransformWallData : ScriptableObject
{
    [SerializeField]
    private PosData[] data = new PosData[2];
    public List<Vector2Int>[] GetData()
    {
        return new List<Vector2Int>[2] { data[0].data, data[1].data };
    }
    [Serializable]
    internal struct PosData
    {
        public List<Vector2Int> data;
    }
}

