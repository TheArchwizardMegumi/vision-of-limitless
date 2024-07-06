using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class NormalWallController : BaseWallController
    {
        public NormalWallController() : base(TileType.NormalWall)
        {
            isAccessibleOpen = false;
            isAccessibleClose = true;
            isDamagableOpen = false;
            isDamagableClose = false;
        }
    }
}