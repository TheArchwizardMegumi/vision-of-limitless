using System.Collections;
using System.Collections.Generic;
using UnderCloud;
using UnityEngine;

namespace UnderCloud
{
    public class BaseWallController
    {
        public readonly TileType type;
        protected bool isAccessibleOpen;
        public virtual bool IsAccessibleOpen => isAccessibleOpen;
        protected bool isAccessibleClose;
        public virtual bool IsAccessibleClose => isAccessibleClose;
        protected bool isDamagableOpen;
        public virtual bool IsDamagableOpen => isDamagableOpen;
        protected bool isDamagableClose;
        public virtual bool IsDamagableClose => isDamagableClose;
        public BaseWallController(TileType type)
        {
            this.type = type;
        }
    }
}