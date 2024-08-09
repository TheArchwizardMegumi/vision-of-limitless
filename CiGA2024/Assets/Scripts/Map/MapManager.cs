using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OvO;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.Playables;

namespace UnderCloud
{
    public class MapManager : Singleton<MapManager>
    {
        public static Sprite[] timeLimitedWallSprites = new Sprite[3];
        private static Dictionary<Vector2Int, BaseWallController> tiles;
        private static readonly Vector3[] spawnPoints = new Vector3[2];
        private static List<Vector3> timeLimitedWalls = new();
        public MapManager()
        {
            tiles ??= new Dictionary<Vector2Int, BaseWallController>();
            GlobalData.TransformWallLayerNum = 0;

        }
        private void Start()
        {
            timeLimitedWallSprites[0] = Sprite.Create(Resources.Load<Texture2D>("Texures/Tile/TimeLimitedWall_1"), new Rect(0f, 0f, 287f, 287f), new Vector2(0.5f, 0.5f), 287f);
            timeLimitedWallSprites[1] = Sprite.Create(Resources.Load<Texture2D>("Texures/Tile/TimeLimitedWall_2"), new Rect(0f, 0f, 287f, 287f), new Vector2(0.5f, 0.5f), 287f);
            timeLimitedWallSprites[2] = Sprite.Create(Resources.Load<Texture2D>("Texures/Tile/TimeLimitedWall_3"), new Rect(0f, 0f, 287f, 287f), new Vector2(0.5f, 0.5f), 287f);
        }
        public static void InitWhenLevelStart()
        {
            tiles ??= new Dictionary<Vector2Int, BaseWallController>();
            GlobalData.TransformWallLayerNum = 0;
            spawnPoints[0] = Vector3.zero;
            spawnPoints[1] = Vector3.zero;
            timeLimitedWalls = new();

            LoadMapOfCurrentLevel();

            TimeLimitedWall.StackCount = 0;

            PlayControl.SpawnPlayer(spawnPoints[0]);
            if (FindObjectOfType<LevelInfo>().Chapter == 2)
            {
                Player2.SpawnPlayer(spawnPoints[1]);
            }
            else
            {
                Player2.Instance.gameObject.SetActive(false);
            }
            CountPlayerNum();
        }

        private static void CountPlayerNum()
        {
            GameObject[] pls = GameObject.FindGameObjectsWithTag("Player");
            if (pls.Length == 0)
            {
                Debug.LogError("player num is 0");
            }
            PlayerWinChecker.playerNum = 0;
            for (int i = 0; i < pls.Length; i++)
            {
                PlayerWinChecker.playerNum++;
            }
        }

        public static BaseWallController GetTile(Vector2Int position)
        {
            if (tiles.TryGetValue(position, out BaseWallController tile))
            {
                return tile;
            }
            else
                return null;
        }

        public static bool IsPlayer(Vector2Int position)
        {
            if(PlayControl.Instance.isActiveAndEnabled)
            {
                if ((int)PlayControl.Instance.position.x == position.x && (int)PlayControl.Instance.position.y == position.y)
                {
                    return true;
                }
            }
            if (Player2.Instance.isActiveAndEnabled)
            {
                if ((int)Player2.Instance.position.x == position.x && (int)Player2.Instance.position.y == position.y)
                {
                    return true;
                }
            }
            return false;
        }

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

        public static void LoadMapOfCurrentLevel()
        {
            tiles ??= new Dictionary<Vector2Int, BaseWallController>();
            tiles.Clear();
            timeLimitedWalls.Clear();

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i] = Vector3.zero;
            }

            TileBase tile;
            GameObject parent = GameObject.FindWithTag(TagName.TileMap);
            GameObject child;
            int SPCount = 0;
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
                                        SPCount = CheckTile(SPCount, i, j, customTile);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError($"Map�ĵ�{c}��������û��Tilemap���");
                    }
                }
            }

            static int CheckTile(int SPCount, int i, int j, CustomRuleTile customTile)
            {
                if (!tiles.ContainsKey(new Vector2Int(i, j)))
                {
                    tiles.Add(new Vector2Int(i, j), GenerateTile(customTile.type));
                }
                if (customTile.type == TileType.SpawnPoint)
                {
                    spawnPoints[SPCount] = new Vector3(i, j, 0);
                    SPCount++;
                }
                else if (customTile.type == TileType.TimeLimitedWall)
                {
                    timeLimitedWalls.Add(new Vector3(i, j));
                }
                return SPCount;
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
        /// �л��仯ǽ״̬
        /// </summary>
        public static void SwitchTransformWallState(PlayerState playerState)
        {
            if (playerState == PlayerState.Open)
            {
                GlobalData.TransformWallLayerNum = 1 - GlobalData.TransformWallLayerNum;
                LoadMapOfCurrentLevel();
            }
        }
        public static void CountTimeLimitedWall(PlayerState playerState)
        {
            if (playerState == PlayerState.Open)
            {
                TimeLimitedWall.StackCount++;
                Tilemap map = GameObject.FindWithTag("TileMap").transform.GetChild(2).GetComponent<Tilemap>();
                if (map != null)
                {
                    for (int i = 0; i < timeLimitedWalls.Count; i++)
                    {
                        Vector3Int pos = new((int)timeLimitedWalls[i].x, (int)timeLimitedWalls[i].y, 0);
                        CustomRuleTile newTile = new()
                        {
                            type = TileType.TimeLimitedWall,
                            m_DefaultSprite = timeLimitedWallSprites[TimeLimitedWall.StackCount - 1],
                        };
                        map.SetTile(pos, null);
                        map.SetTile(pos, newTile);
                    }
                }
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
                TileType.TimeLimitedWall => new TimeLimitedWall(),
                _ => null,
            }; ;
        }
    }
}