using System.Collections.Generic;
using UnderCloud;
using UnityEngine;

public readonly struct MapUpdate
{
    public readonly List<(Vector2Int positionsToUpdate, BaseWallController newTile)> values;
    public MapUpdate(List<(Vector2Int positionsToUpdate, BaseWallController newTile)> values)
    {
        this.values = values;
    }
    public MapUpdate(Vector2Int positionsToUpdate, BaseWallController newTile)
    {
        values = new List<(Vector2Int positionsToUpdate, BaseWallController newTile)>() { (positionsToUpdate, newTile) };
    }
}