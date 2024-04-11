#if UNITY_EDITOR
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Kimicu.ExcelImporter
{
    public class ConfigImportsWindow : EditorWindow
    {
        private static ConfigImportSettings _config;

        private string _parserScriptName = "ItemsSettingsParser";

        [MenuItem("Kimicu/Config Import")]
        private static void ShowWindow()
        {
            var window = GetWindow<ConfigImportsWindow>();
            window.titleContent = new GUIContent("Config Import");
            window.Show();
        }

        private void OnGUI()
        {
            _config = (ConfigImportSettings)EditorGUILayout.ObjectField("Config", _config, typeof(ConfigImportSettings),
                false);

            if (_config == null && GUILayout.Button("Create new config"))
            {
                _config = CreateConfigSettings();
            }

            if (_config == null) return;

            GUILayout.Label("SPREADSHEET_ID:");
            _config.SpreadsheetID = EditorGUILayout.TextField(_config.SpreadsheetID);

            GUILayout.Label("CREDENTIALS_PATH:");
            _config.CredentialsPath = EditorGUILayout.TextField(_config.CredentialsPath);

            GUILayout.Label("ITEMS_SHEET_NAME:");
            _config.ItemsSheetName = EditorGUILayout.TextField(_config.ItemsSheetName);

            GUILayout.Label("SETTINGS_FILE_NAME:");
            _config.SettingsFileName = EditorGUILayout.TextField(_config.SettingsFileName);

            GUILayout.Label("PARSER NAME:");
            _parserScriptName = EditorGUILayout.TextField(_parserScriptName);

            if (GUILayout.Button("Load Items Settings")) Task.Run(LoadSettings);
        }

        private ConfigImportSettings CreateConfigSettings()
        {
            ConfigImportSettings newConfig = CreateInstance<ConfigImportSettings>();
            string assetPath = EditorUtility.SaveFilePanelInProject("Save Config Import Settings",
                "NewConfigImportSettings", "asset", "Please enter a file name to save the configuration settings.");

            if (!string.IsNullOrEmpty(assetPath))
            {
                AssetDatabase.CreateAsset(newConfig, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                return newConfig;
            }
            else
            {
                Debug.LogWarning("Config creation cancelled.");
                return null;
            }
        }

        private async void LoadSettings()
        {
            var gameSettings = await ConfigImportsMenu.LoadSettings(_config.SettingsFileName);
            ConfigImportsMenu.LoadItemsSettings(_config.CredentialsPath, _config.SpreadsheetID, _config.ItemsSheetName,
                _config.SettingsFileName, ParserUtils.GetParserByName(_parserScriptName, gameSettings), gameSettings);
            AssetDatabase.Refresh();
        }
    }
}
#endif