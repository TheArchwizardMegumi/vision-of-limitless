using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class DamageWallController : BaseWallController
    {
        public DamageWallController() : base(TileType.NormalWall)
        {
            isAccessibleOpen = false;
            isAccessibleClose = true;
            isDamagableOpen = true;
            isDamagableClose = false;
        }
    }
}