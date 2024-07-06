using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static OvO.KeySet;

namespace OvO
{
    public static partial class KeyBinding
    {
        public static KeyCode GetKeyCode(KeyName operation)
        {
            if (AllKeys.ContainsKey(operation))
                return AllKeys[operation];
            else
                Debug.LogError($"Key name {operation} doesn't exist.");
            return KeyCode.None;
        }
        public static bool GetKey(KeyName operation)
        {
            if (AllKeys.ContainsKey(operation))
                return Input.GetKey(AllKeys[operation]);
            else
                Debug.LogError($"Key name {operation} doesn't exist.");
            return false;
        }
        public static bool GetKeyUp(KeyName operation)
        {
            if (AllKeys.ContainsKey(operation))
                return Input.GetKeyUp(AllKeys[operation]);
            else
                Debug.LogError($"Key name {operation} doesn't exist.");
            return false;
        }
        public static bool GetKeyDown(KeyName operation)
        {
            if (AllKeys.ContainsKey(operation))
                return Input.GetKeyDown(AllKeys[operation]);
            else
                Debug.LogError($"Key name {operation} doesn't exist.");
            return false;
        }
        public static void SaveKeyBinding()
        {
            //Convert key set to string pairs
            Dictionary<string, string> set = new Dictionary<string, string>();
            foreach (KeyValuePair<KeyName, KeyCode> kv in AllKeys)
            {
                set.Add(kv.Key.ToString(), kv.Value.ToString());
            }

            PersistentDataSaving.SavePlayerPreference(set);
        }
        public static void LoadKeyBindingFromPlayerPref()
        {
            var keys = AllKeys.Keys.ToArray();
            foreach (KeyName key in keys)
            {
                SetKeyBinding(key, Enum.Parse<KeyCode>(PlayerPrefs.GetString(key.ToString())));
            }
        }
        public static void SetKeyBinding(KeyName keyToChange, KeyCode newKey)
        {
            if (AllKeys.ContainsKey(keyToChange))
                AllKeys[keyToChange] = newKey;
            else
                Debug.LogError($"keyName {keyToChange} doesn't exist");
        }
    }
}