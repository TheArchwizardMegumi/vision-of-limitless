using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace OvO
{
    [Serializable]
    public class SaveFile
    {

        public static string CreateSaveData()
        {
            //Record game data
            SaveFile save = new();

            //Convert save data to Json string
            //Add new Converter objects in variable "setting" if extra converters are needed.
            JsonSerializerSettings setting = new();
            setting.Converters.Add(new JsonVectorConverter());
            string jsonStr = JsonConvert.SerializeObject(save, Formatting.None, setting);
            return jsonStr;
        }
    }
}