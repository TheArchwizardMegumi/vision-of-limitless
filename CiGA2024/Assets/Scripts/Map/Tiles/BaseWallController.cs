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
        public bool IsAccessibleOpen => isAccessibleOpen;
        protected bool isAccessibleClose;
        public bool IsAccessibleClose => isAccessibleClose;
        protected bool isDamagableOpen;
        public bool IsDamagableOpen => isDamagableOpen;
        protected bool isDamagableClose;
        public bool IsDamagableClose => isDamagableClose;
        public BaseWallController(TileType type)
        {
            this.type = type;
        }
        protected virtual void OnchangeOpenCloseEye(PlayerState arg1)
        {
        }
    }
}