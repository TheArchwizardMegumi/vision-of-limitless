using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
[CreateAssetMenu(fileName = "CustomAnimatedTile.asset", menuName = "CustomTile")]
public class CustomAnimatedTile : RuleTile
{
    public TileType type;
}
