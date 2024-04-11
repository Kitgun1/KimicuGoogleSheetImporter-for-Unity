using System;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Kimicu.ExcelImporter.Utils
{
    public static class JsonWriter
    {
        public static async Task SaveJson(string nameFile, string json)
        {
            string path = Path.Combine(Application.dataPath, @"Resources\Tables");
            string fullPath = Path.Combine(path, nameFile + ".json");
            string directory = Path.GetDirectoryName(fullPath);

#if UNITY_EDITOR
            if (!Directory.Exists(directory)) AssetDatabase.CreateFolder(@"Resources", "Tables");
#endif

            Debug.Log($"fullPath: {fullPath}");
            await File.WriteAllTextAsync(fullPath, json);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }

        public static async Task<string> LoadJson(string nameFile)
        {
            string path = Path.Combine(Application.dataPath, @"Assets\Resources\Tables");
            string fullPath = Path.Combine(path, nameFile + ".json");
            string directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(fullPath))
            {
                await File.WriteAllTextAsync(fullPath, "{}");
            }

            return await File.ReadAllTextAsync(fullPath);
        }
    }
}