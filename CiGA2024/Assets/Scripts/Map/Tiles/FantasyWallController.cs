using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class FantasyWallController : BaseWallController
    {
        public FantasyWallController() : base(TileType.NormalWall)
        {
            isAccessibleOpen = true;
            isAccessibleClose = false;
            isDamagableOpen = false;
            isDamagableClose = false;
        }
    }
}