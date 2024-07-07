using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OvO
{
    public interface ITimer
    {
        public string Name { get; }
        public bool IsActive { get; }
        public bool IsCountdown { get; }
        public float Time { get; }
        public void RemoveTimer();
        public void StartTimer();
        public void StopTimer();
        public void ResetTimer(bool startImmediately);
    }
    public class Timer : MonoBehaviour, ITimer
    {
        private static readonly Dictionary<string, List<Timer>> allTimers = new();

        public string Name { get; protected set; }
        public bool IsActive { get; protected set; }
        public bool IsCountdown { get; protected set; }
        protected float time;
        public float Time => time;

        private List<(float keyTime, Delegate action, bool invoked, object[] args)> behaviours;
        protected bool markedAsRemoved;

        public static bool ContainTimer(string name) => allTimers.ContainsKey(name);
        /// <summary>
        /// Create a timer
        /// </summary>
        /// <param name="name">The name of the timer</param>
        /// <param name="active">Whether the timer start running immediately</param>
        public static Timer CreateTimer(string name, bool active, bool isCountdown = false, float initTime = 0f)
        {
            Timer timer = CreatePrototype(name);
            timer.IsCountdown = isCountdown;
            timer.time = initTime;
            if (active)
                timer.StartTimer();
            return timer;
        }
        /// <summary>
        /// Create a timer
        /// </summary>
        /// <param name="name">The name of the timer</param>
        /// <param name="active">Whether the timer start running immediately</param>
        /// <param name="behaviour">An action to invoke at a specific time</param>
        /// <param name="isCountdown">If the timer counts down</param>
        /// <param name="initTime">The time will be set as this value once the timer is created</param>
        public static Timer CreateTimer(string name, bool active, (float keyTime, Callback action) behaviour, bool isCountdown = false, float initTime = 0f)
        {
            Timer timer = CreatePrototype(name);
            timer.IsCountdown = isCountdown;
            timer.time = initTime;
            timer.AddBehaviour(behaviour.keyTime, behaviour.action);
            if (active)
                timer.StartTimer();
            return timer;
        }
        /// <summary>
        /// Create a timer
        /// </summary>
        /// <param name="name">The name of the timer</param>
        /// <param name="active">Whether the timer start running immediately</param>
        /// <param name="behaviours">A list contains actions to invoke at a specific time</param>
        /// <param name="isCountdown">If the timer counts down</param>
        /// <param name="initTime">The time will be set as this value once the timer is created</param>
        public static Timer CreateTimer(string name, bool active, List<(float keyTime, Callback action)> behaviours, bool isCountdown = false, float initTime = 0f)
        {
            Timer timer = CreatePrototype(name);
            timer.IsCountdown = isCountdown;
            timer.time = initTime;
            foreach ((float t, Callback a) in behaviours)
                timer.AddBehaviour(t, a);
            if (active)
                timer.StartTimer();
            return timer;
        }
        /// <summary>
        /// Create a timer
        /// </summary>
        /// <param name="name">The name of the timer</param>
        /// <param name="active">Whether the timer start running immediately</param>
        /// <param name="behaviour">A list contains actions to invoke at a specific time</param>
        /// <param name="isCountdown">If the timer counts down</param>
        /// <param name="initTime">The time will be set as this value once the timer is created</param>
        public static Timer CreateTimer(string name, bool active, (float keyTime, MultiParamsCallback action, object[] args) behaviour, bool isCountdown = false, float initTime = 0f)
        {
            Timer timer = CreatePrototype(name);
            timer.IsCountdown = isCountdown;
            timer.time = initTime;
            (float t, MultiParamsCallback a, object[] args) = behaviour;
            timer.AddBehaviour(t, a, args);
            if (active)
                timer.StartTimer();
            return timer;
        }
        /// <summary>
        /// Create a timer
        /// </summary>
        /// <param name="name">The name of the timer</param>
        /// <param name="active">Whether the timer start running immediately</param>
        /// <param name="behaviours">A list contains actions to invoke at a specific time</param>
        /// <param name="isCountdown">If the timer counts down</param>
        /// <param name="initTime">The time will be set as this value once the timer is created</param>
        public static Timer CreateTimer(string name, bool active, List<(float keyTime, MultiParamsCallback action, object[] args)> behaviours, bool isCountdown = false, float initTime = 0f)
        {
            Timer timer = CreatePrototype(name);
            timer.IsCountdown = isCountdown;
            timer.time = initTime;
            foreach ((float t, MultiParamsCallback a, object[] args) in behaviours)
                timer.AddBehaviour(t, a, args);
            if (active)
                timer.StartTimer();
            return timer;
        }
        /// <summary>
        /// Get all timers with the same name
        /// </summary>
        public static List<Timer> GetTimers(string name)
        {
            if (allTimers.ContainsKey(name))
            {
                if (allTimers[name] != null)
                {
                    return allTimers[name];
                }
                else
                {
                    allTimers.Remove(name);
                    return null;
                }
            }
            else
            {
                Debug.LogWarning("Can't find specific timer, please check if the name is correct or timer exists");
                return null;
            }
        }
        public static void RemoveTimer(Timer timer)
        {
            timer.RemoveTimer();
        }
        /// <summary>
        /// Remove all timers with the same name
        /// </summary>
        public static void RemoveTimers(string name)
        {
            foreach (Timer timer in GetTimers(name))
            {
                timer.RemoveTimer();
            }
        }
        /// <summary>
        /// Add an listener with a specific invocation time
        /// </summary>
        /// <param name="time">The time when the action was invoked</param>
        /// <param name="behaviour">Action to invoke</param>
        public void AddBehaviour(float time, Callback behaviour)
        {
            if (IsCountdown)
            {
                for (int i = 0; i < behaviours.Count; i++)
                {
                    if (behaviours[i].keyTime < time)
                    {
                        behaviours.Insert(i, (time, behaviour, false, null));
                        return;
                    }
                }
                behaviours.Add((time, behaviour, false, null));
            }
            else
            {
                for (int i = 0; i < behaviours.Count; i++)
                {
                    if (behaviours[i].keyTime > time)
                    {
                        behaviours.Insert(i, (time, behaviour, false, null));
                        return;
                    }
                }
                behaviours.Add((time, behaviour, false, null));
            }
        }
        /// <summary>
        /// Add listeners with specific invocation time
        /// </summary>
        public void AddBehaviours(List<(float time, Callback action)> behaviours)
        {
            foreach ((float time, Callback action) in behaviours)
            {
                AddBehaviour(time, action);
            }
        }
        /// <summary>
        /// Add an listener with a specific invocation time
        /// </summary>
        /// <param name="time">The time when the action was invoked</param>
        /// <param name="behaviour">Action to invoke</param>
        public void AddBehaviour(float time, MultiParamsCallback behaviour, object[] args)
        {
            if (IsCountdown)
            {
                for (int i = 0; i < behaviours.Count; i++)
                {
                    if (behaviours[i].keyTime <= time)
                    {
                        behaviours.Insert(i, (time, behaviour, false, args));
                        return;
                    }
                }
                behaviours.Add((time, behaviour, false, args));
            }
            else
            {
                for (int i = 0; i < behaviours.Count; i++)
                {
                    if (behaviours[i].keyTime > time)
                    {
                        behaviours.Insert(i, (time, behaviour, false, args));
                        return;
                    }
                }
                behaviours.Add((time, behaviour, false, args));
            }
        }
        /// <summary>
        /// Add listeners with specific invocation time
        /// </summary>
        public void AddBehaviours(List<(float time, MultiParamsCallback action)> behaviours, object[] args)
        {
            foreach ((float time, MultiParamsCallback action) in behaviours)
            {
                AddBehaviour(time, action, args);
            }
        }
        public void RemoveTimer()
        {
            markedAsRemoved = true;
        }
        public void StartTimer()
        {
            IsActive = true;
        }
        public void StopTimer()
        {
            IsActive = false;
        }
        public void ResetTimer(bool startImmediately)
        {
            time = 0;
            (float keyTime, Action action, bool isInvoked) timerEvent;
            for (int i = 0; i < behaviours.Count; i++)
                timerEvent.isInvoked = false;
            if (startImmediately)
                StartTimer();
        }
        private static Timer CreatePrototype(string name)
        {
            Timer timer = TimerManager.Instance.AllTimers.AddComponent<Timer>();
            timer.Name = name;
            timer.behaviours = new();
            if (allTimers.ContainsKey(name))
            {
                allTimers[name].Add(timer);
            }
            else
            {
                allTimers.Add(name, new() { timer } );
            }
            return timer;
        }
        protected virtual void UpdateTimer()
        {
            if (IsActive)
            {
                if (IsCountdown)
                {
                    time -= UnityEngine.Time.deltaTime;
                    for (int i = 0; i < behaviours.Count; i++)
                    {
                        (float keyTime, Delegate action, bool isInvoked, object[] args) = behaviours[i];
                        if (time > keyTime)
                            break;
                        else if (!isInvoked)
                        {
                            action.DynamicInvoke(args);
                            behaviours[i] = (keyTime, action, true, args);
                        }
                    }
                }
                else
                {
                    time += UnityEngine.Time.deltaTime;
                    for (int i = 0; i < behaviours.Count; i++)
                    {
                        (float keyTime, Delegate action, bool isInvoked, object[] args) = behaviours[i];
                        if (time < keyTime)
                            break;
                        else if (!isInvoked)
                        {
                            action.DynamicInvoke(args);
                            behaviours[i] = (keyTime, action, true, args);
                        }
                    }
                }
            }

            if (markedAsRemoved)
            {
                Debug.Log($"allTimers: {allTimers[Name][0]};   This: {this}; Is equal: {allTimers[Name][0] == this}");
                allTimers[Name].Remove(this);
                if (allTimers[Name].Count == 0)
                    allTimers.Remove(Name);
                Destroy(this);
            }
        }
        private void Update()
        {
            UpdateTimer();
        }
    }
}