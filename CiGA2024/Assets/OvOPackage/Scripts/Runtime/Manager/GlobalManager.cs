using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OvO;
using UnderCloud;
using static GlobalData;

namespace OvO
{
    public class GlobalManager : Singleton<GlobalManager>
    {
        private void Start()
        {
            TimerManagerInstance = gameObject.AddComponent<TimerManager>();
            MapManagerInstance = gameObject.AddComponent<MapManager>();
        }
    }
}