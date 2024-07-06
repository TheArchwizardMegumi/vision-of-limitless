using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Linq;
using Unity.VisualScripting;

public class JsonVectorConverter : JsonConverter<Dictionary<Vector2, string>>
{
    public override bool CanWrite => false;
    public override Dictionary<Vector2, string> ReadJson(JsonReader reader,
                                                        Type objectType,
                                                        Dictionary<Vector2, string> existingValue,
                                                        bool hasExistingValue,
                                                        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }
        JObject jObject = JObject.Load(reader);

        Regex regex = new("-?[0-9]+(.\\d+)?");
        Vector2 v2 = new();
        MatchCollection matches;
        Dictionary<Vector2, string> dict = new();

        foreach (var kv in jObject)
        {
            matches = regex.Matches(kv.Key);
            v2.Set(Convert.ToSingle(matches[0].Value), Convert.ToSingle(matches[1].Value));
            dict.Add(v2, kv.Value.ToString());
        }

        return dict;
    }
    public override void WriteJson(JsonWriter writer, Dictionary<Vector2, string> value, JsonSerializer serializer)
    {
        writer.WriteStartArray();
    }
}
