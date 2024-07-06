using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class Exit : BaseWallController
    {
        public Exit() : base(TileType.Exit)
        {
            isAccessibleOpen = true;
            isAccessibleClose = true;
            isDamagableOpen = false;
            isDamagableClose = false;
        }
    }
}