using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using Unity.VisualScripting;
using System.Runtime.Serialization;

namespace OvO
{
    public static class PersistentDataSaving
    {
        /// <summary>
        /// Save data at exist save file
        /// </summary>
        /// <param name="saveName">save name</param>
        /// <param name="saveStr">string converted from game data</param>
        public static void SaveData(string saveName, string saveStr)
        {
            //Create a new file if current save not exist.
            if (!File.Exists(Application.persistentDataPath + $"/Saves/{saveName}"))
                CreateNewSave(saveName);
            //more info about persistentDataPath in https://docs.unity.cn/cn/2021.1/ScriptReference/Application-persistentDataPath.html
            StreamWriter writer = new(Application.persistentDataPath + $"/Saves/{saveName}");
            writer.Write(saveStr);
            writer.Close();
        }
        /// <summary>
        /// Load data
        /// </summary>
        /// <typeparam name="T">Save file type.</typeparam>
        /// <param name="saveName"></param>
        /// <returns></returns>
        public static T LoadData<T>(string saveName) where T : SerializableSave
        {
            string save;
            if (File.Exists(Application.persistentDataPath + $"/Saves/{saveName}"))
            {
                save = File.ReadAllText(Application.persistentDataPath + $"/Saves/{saveName}");
            }
            else
            {
                Debug.LogError("Save file not exists");
                return default;
            }
            //JsonSerializerSettings setting = new();
            //setting.Converters.Add(new JsonVectorConverter());
            T saveLoaded = JsonConvert.DeserializeObject<T>(save, new JsonVectorConverter());
            return saveLoaded;
        }
        /// <summary>
        /// Save player preference like key binding and else.
        /// </summary>
        /// <param name="prefFields">key value pairs of string and float, int and string</param>
        public static void SavePlayerPreference(Dictionary<string, int> prefFields)
        {
            foreach (KeyValuePair<string, int> kv in prefFields)
                PlayerPrefs.SetInt(kv.Key, kv.Value);
        }
        /// <summary>
        /// Save player preference like key binding and else.
        /// </summary>
        /// <param name="prefFields">key value pairs of string and float, int and string</param>
        public static void SavePlayerPreference(Dictionary<string, float> prefFields)
        {
            foreach (KeyValuePair<string, float> kv in prefFields)
                PlayerPrefs.SetFloat(kv.Key, kv.Value);
        }
        /// <summary>
        /// Save player preference like key binding and else.
        /// </summary>
        /// <param name="prefFields">key value pairs of string and float, int and string</param>
        public static void SavePlayerPreference(Dictionary<string, string> prefFields)
        {
            foreach (KeyValuePair<string, string> kv in prefFields)
                PlayerPrefs.SetString(kv.Key, kv.Value);
        }
        /// <summary>
        /// Create a new save at unity's default persistent data path
        /// </summary>
        /// <param name="saveName"></param>
        private static void CreateNewSave(string saveName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
            }
            if (!File.Exists(Application.persistentDataPath + $"/Saves/{saveName}"))
            {
                File.Create(Application.persistentDataPath + $"/Saves/{saveName}");
            }
        }
    }

    [Serializable]
    public abstract class SerializableSave { }
}