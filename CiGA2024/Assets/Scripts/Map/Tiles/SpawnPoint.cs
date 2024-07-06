using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class SpawnPoint : BaseWallController
    {
        public SpawnPoint() : base(TileType.SpawnPoint)
        {
            isAccessibleOpen = true;
            isAccessibleClose = true;
            isDamagableOpen = false;
            isDamagableClose = false;
        }
    }
}