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
        private static Vector3 spawnPoint;
        public MapManager()
        {
            tiles ??= new Dictionary<Vector2Int, BaseWallController>();
            //设置变化墙初始图层
            GlobalData.TransformWallLayerNum = 0;
        }
        public static void InitWhenLevelStart()
        {
            tiles ??= new Dictionary<Vector2Int, BaseWallController>();
            GlobalData.TransformWallLayerNum = 0;
            spawnPoint = Vector3.zero;

            LoadMapOfCurrentLevel();
            PlayControl.SpawnPlayer(spawnPoint);
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
                    return tile.IsDamagableOpen;
                else
                    return tile.IsDamagableClose;
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
            spawnPoint = Vector3.zero;

            TileBase tile;
            GameObject parent = GameObject.FindWithTag(TagName.TileMap);
            GameObject child;
            //刷新变化墙图层状态
            parent.transform.GetChild(GlobalData.TransformWallLayerNum).gameObject.SetActive(true);
            parent.transform.GetChild(1 - GlobalData.TransformWallLayerNum).gameObject.SetActive(false);

            for (int c = 0; c < parent.transform.childCount; c++)
            {
                child = parent.transform.GetChild(c).gameObject;
                if (child.activeSelf)
                {
                    if (child.TryGetComponent(out Tilemap map))
                    {
                        for (int i = -32; i < 32; i++)
                        {
                            for (int j = -32; j < 32; j++)
                            {
                                tile = map.GetTile(new Vector3Int(i, j, 0));
                                if (tile != null)
                                {
                                    if (tile is CustomRuleTile customTile)
                                    {
                                        if (!tiles.ContainsKey(new Vector2Int(i, j)))
                                        {
                                            tiles.Add(new Vector2Int(i, j), GenerateTile(customTile.type));
                                        }
                                        if (customTile.type == TileType.SpawnPoint)
                                            spawnPoint = new Vector3(i, j, 0);
                                        //map.SetTransformMatrix(new Vector3Int(i, j, 0), Matrix4x4.TRS(new Vector3(i, j, j * 1f), Quaternion.identity, Vector3.one));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError($"Map的第{c}个子物体没有Tilemap组件");
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
        /// <summary>
        /// 切换变化墙状态
        /// </summary>
        public static void SwitchTransformWallState(PlayerState playerState)
        {
            if (playerState == PlayerState.Open)
            {
                GlobalData.TransformWallLayerNum = 1 - GlobalData.TransformWallLayerNum;
                LoadMapOfCurrentLevel();
            }
        }
        private static BaseWallController GenerateTile(TileType type)
        {
            return type switch
            {
                TileType.NormalWall => new NormalWallController(),
                TileType.FantasyWall => new FantasyWallController(),
                TileType.FantasyDamageWall => new FantasyDamageWallController(),
                TileType.DamageWall => new DamageWallController(),
                TileType.DontRemoveWall => new DontRemoveWallController(),
                TileType.TransformWall => new TransformWallController(),
                TileType.SpawnPoint => new SpawnPoint(),
                TileType.Exit => new Exit(),
                _ => null,
            };
        }
    }
}