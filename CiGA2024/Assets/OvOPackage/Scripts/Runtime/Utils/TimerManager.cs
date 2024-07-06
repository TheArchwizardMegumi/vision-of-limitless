using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OvO
{
    public class TimerManager : Singleton<TimerManager>
    {
        public GameObject AllTimers { get; set; }
        private void Start()
        {
            AllTimers = new GameObject("All Timers");
            AllTimers.transform.parent = transform;
        }
    }
}
