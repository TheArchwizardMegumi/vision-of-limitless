using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class DontRemoveWallController : BaseWallController
    {
        public DontRemoveWallController() : base(TileType.DontRemoveWall)
        {
            isAccessibleOpen = false;
            isAccessibleClose = false;
            isDamagableOpen = false;
            isDamagableClose = false;
        }
    }
}