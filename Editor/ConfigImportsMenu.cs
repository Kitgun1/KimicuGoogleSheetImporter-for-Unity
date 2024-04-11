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

            foreach (var product in gameSettings.Products)
            {
                Debug.Log($"added new item: {product.Id} | {product.Name} | {product.Price}");
            }

            var jsonForSaving = JsonUtility.ToJson(gameSettings);
            PlayerPrefs.SetString(settingsFileName, jsonForSaving);
            Debug.Log($"{jsonForSaving}");
        }

        public static GameSettings LoadSettings(string settingsFileName)
        {
            string jsonLoaded = PlayerPrefs.GetString(settingsFileName);
            return !string.IsNullOrEmpty(jsonLoaded)
                ? JsonUtility.FromJson<GameSettings>(jsonLoaded)
                : new GameSettings();
        }
    }
}