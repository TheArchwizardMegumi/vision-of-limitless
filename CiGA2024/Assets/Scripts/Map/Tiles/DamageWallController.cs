using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class DamageWallController : BaseWallController
    {
        public DamageWallController() : base(TileType.DamageWall)
        {
            isAccessibleOpen = false;
            isAccessibleClose = false;
            isDamagableOpen = true;
            isDamagableClose = true;
        }
    }
}