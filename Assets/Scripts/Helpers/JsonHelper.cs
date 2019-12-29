using System;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    /*
     Code for this Helper found here: https://www.boxheadproductions.com.au/deserializing-top-level-arrays-in-json-with-unity/ and https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
     Found on December 28, 2019.
     Unity's JsonUtility only supports single object json, not an array of json objects. This helper is a few lines and is meant to solve this issue.

    Written by Boxhead Productions
    */
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
