using System.IO;

namespace Kimicu.ExcelImporter.Utils
{
    public static class JsonWriter
    {
        public static void SaveJson(string nameFile, string json, string path)
        {
            string fullPath = Path.Combine(path, nameFile);
            File.WriteAllText(fullPath, json);
        }
        
        public static string LoadJson(string nameFile, string path)
        {
            string fullPath = Path.Combine(path, nameFile);
            if (File.Exists(fullPath))
            {
                return File.ReadAllText(fullPath);
            }

            throw new FileNotFoundException($"File '{nameFile}' not found at path '{path}'.");
        }
    }
}