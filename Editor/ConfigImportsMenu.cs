using System.Threading.Tasks;
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
            await JsonWriter.SaveJson(settingsFileName, jsonForSaving);
        }

        public static async Task<GameSettings> LoadSettings(string settingsFileName)
        {
            string jsonLoaded = await JsonWriter.LoadJson(settingsFileName);
            return !string.IsNullOrEmpty(jsonLoaded)
                ? JsonUtility.FromJson<GameSettings>(jsonLoaded)
                : new GameSettings();
        }
    }
}