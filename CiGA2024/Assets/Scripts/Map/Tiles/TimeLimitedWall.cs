using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderCloud
{
    public class TimeLimitedWall : BaseWallController
    {
        private static int stackCount;
        public static int StackCount
        {
            get
            {
                return stackCount;
            }
            set
            {
                if (value > 3)
                    stackCount = 3;
                else
                    stackCount = value;
            }
        }
        public override bool IsAccessibleOpen => StackCount < 3;
        public override bool IsAccessibleClose => StackCount < 3;
        public TimeLimitedWall() : base(TileType.TimeLimitedWall)
        {   
            isDamagableOpen = false;
            isDamagableClose = false;
        }
    }
}
