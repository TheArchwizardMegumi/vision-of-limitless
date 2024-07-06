using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OvO;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using System;

namespace UnderCloud
{
    public class MapManager : Singleton<MapManager>
    {
        private static Dictionary<Vector2Int, BaseWallController> tiles;
        public MapManager()
        {
            tiles ??= new Dictionary<Vector2Int, BaseWallController>();
        }
        /// <summary>
        /// 获取一个具体地块的信息
        /// </summary>
        /// <param name="position">地块所在位置</param>
        /// <param name="playerState">玩家当前睁眼闭眼状态</param>
        /// <returns></returns>
        public static BaseWallController GetTile(Vector2Int position)
        {
            if (tiles.TryGetValue(position, out BaseWallController tile))
            {
                return tile;
            }
            else
                return null;
        }
        /// <summary>
        /// 一个地块能否通行
        /// </summary>
        /// <param name="position">地块所在位置</param>
        /// <param name="playerState">玩家当前睁眼闭眼状态</param>
        /// <returns></returns>
        public static bool IsAccessible(Vector2Int position, PlayerState playerState)
        {
            if (tiles.TryGetValue(position, out BaseWallController tile))
            {
                if (playerState == PlayerState.Open)
                    return tile.IsAccessibleOpen;
                else
                    return tile.IsAccessibleClose;
            }
            else
                return true;
        }
        /// <summary>
        /// 一个地块是否能对玩家造成伤害
        /// </summary>
        /// <param name="position">地块所在位置</param>
        /// <param name="playerState">玩家当前睁眼闭眼状态</param>
        /// <returns></returns>
        public static bool IsDamagable(Vector2Int position, PlayerState playerState)
        {
            if (tiles.TryGetValue(position, out BaseWallController tile))
            {
                if (playerState == PlayerState.Open)
                    return tile.IsAccessibleOpen;
                else
                    return tile.IsAccessibleClose;
            }
            else
                return false;
        }
        /// <summary>
        /// 清除地图数据，加载一个新的关卡
        /// </summary>
        /// <param name="levelNum">关卡序号</param>
        public static void LoadMapOfCurrentLevel()
        {
            //扫描并录入当前地图
            tiles ??= new Dictionary<Vector2Int, BaseWallController>();
            tiles.Clear();
            TileBase tile;
            foreach (Tilemap map in GameObject.FindWithTag(TagName.TileMap).transform.GetComponentsInChildren<Tilemap>())
            {
                for (int i = -32; i < 32; i++)
                {
                    for(int j = -32; j < 32; j++)
                    {
                        tile = map.GetTile(new Vector3Int(i, j, 0));
                        if (tile != null)
                        {
                            if (tile is CustomAnimatedTile customTile)
                            {
                                if (!tiles.ContainsKey(new Vector2Int(i, j)))
                                {
                                    tiles.Add(new Vector2Int(i, j), GenerateTile(customTile.type));
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void UpdateMap(MapUpdate update)
        {
            foreach (var (positionsToUpdate, newTile) in update.values)
            {
                tiles[positionsToUpdate] = newTile;
            }
        }
        private static BaseWallController GenerateTile(TileType type)
        {
            return type switch
            {
                TileType.NormalWall => new NormalWallController(),
                _ => null,
            };
        }
    }
}