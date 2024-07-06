using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class FantasyDamageWallController : BaseWallController
    {
        public FantasyDamageWallController() : base(TileType.FantasyDamageWall)
        {
            isAccessibleOpen = false;
            isAccessibleClose = true;
            isDamagableOpen = true;
            isDamagableClose = false;
        }
    }
}