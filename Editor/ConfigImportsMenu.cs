using Kimicu.ExcelImporter.Utils;
using UnityEngine;

namespace Kimicu.ExcelImporter
{
    public static class ConfigImportsMenu
    {
        public static async void LoadItemsSettings(string credentialsPath, string spreadsheetID, string itemsSheetName,
            string settingsFileName, IGoogleSheetParser googleSheetParser, GameSettings gameSettings)
        {
            var sheetImporter = new GoogleSheetsImporter(credentialsPath, spreadsheetID);
            
            await sheetImporter.DownloadAndParseSheet(itemsSheetName, googleSheetParser);
            
            var jsonForSaving = JsonUtility.ToJson(gameSettings);
            JsonWriter.SaveJson(settingsFileName, jsonForSaving, "Resources");
        }

        public static GameSettings LoadSettings(string settingsFileName)
        {
            string jsonLoaded = JsonWriter.LoadJson(settingsFileName, "Resources");
            return !string.IsNullOrEmpty(jsonLoaded)
                ? JsonUtility.FromJson<GameSettings>(jsonLoaded)
                : new GameSettings();
        }
    }
}