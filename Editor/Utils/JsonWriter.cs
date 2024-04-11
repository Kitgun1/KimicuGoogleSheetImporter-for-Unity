using System.IO;

namespace Kimicu.ExcelImporter.Utils
{
    public static class JsonWriter
    {
        public static void SaveJson(string nameFile, string json, string path)
        {
            string fullPath = Path.Combine(path, nameFile);
            string directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(fullPath, json);
        }
        
        public static string LoadJson(string nameFile, string path)
        {
            string fullPath = Path.Combine(path, nameFile);
    
            if (!File.Exists(fullPath))
            {
                File.WriteAllText(fullPath, "{}"); 
            }

            return File.ReadAllText(fullPath);
        }
    }
}