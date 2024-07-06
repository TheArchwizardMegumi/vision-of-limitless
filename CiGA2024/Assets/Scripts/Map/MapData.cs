using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData")]
[Serializable]
public class MapData : ScriptableObject
{
    public List<MapGenerateModel> data;
}

[Serializable]
public struct MapGenerateModel
{
    public float X;
    public float Y;
    public TileType Type;
}