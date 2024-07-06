using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class TransformWallController : BaseWallController
    {
        public TransformWallController() : base(TileType.TransformWall)
        {
            isAccessibleOpen = false;
            isAccessibleClose = false;
            isDamagableOpen = false;
            isDamagableClose = false;
        }
    }
}