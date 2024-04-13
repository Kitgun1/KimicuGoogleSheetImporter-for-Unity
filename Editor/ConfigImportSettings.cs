using UnityEngine;

namespace Kimicu.ExcelImporter
{
    public class ConfigImportSettings : ScriptableObject
    {
        public string SpreadsheetID = "";
        public string CredentialsPath = "";
        public string ItemsSheetName = "";
        public string SettingsFileName = "";
    }
}