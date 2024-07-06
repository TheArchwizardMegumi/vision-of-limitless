using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
[CreateAssetMenu(fileName = "CustomRuleTile.asset", menuName = "CustomTile")]
public class CustomRuleTile : RuleTile
{
    public TileType type;
}
