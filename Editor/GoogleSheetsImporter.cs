using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace Kimicu.ExcelImporter
{
    public class GoogleSheetsImporter
    {
        private readonly List<string> _headers = new List<string>();
        private readonly string _sheetId;
        private readonly SheetsService _service;

        public GoogleSheetsImporter(string credentialsPath, string sheetId)
        {
            _sheetId = sheetId;

            GoogleCredential credential;
            using FileStream stream = new FileStream(credentialsPath, FileMode.Open);
            credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets);

            _service = new SheetsService(new BaseClientService.Initializer { HttpClientInitializer = credential });
        }

        public async Task DownloadAndParseSheet(string sheetName, IGoogleSheetParser parser)
        {
            Debug.Log($"Starting download sheet (${sheetName})...");

            var range = $"{sheetName}!A1:Z";
            var request = _service.Spreadsheets.Values.Get(_sheetId, range);

            ValueRange response;
            try
            {
                response = await request.ExecuteAsync();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error retrieving Google Sheets data: {e.Message}");
                return;
            }

            if (response != null && response.Values != null)
            {
                var tableArray = response.Values;
                Debug.Log($"Sheet downloaded successfully: {sheetName}.");
                var firstRow = tableArray[0];
                foreach (var cell in firstRow)
                {
                    _headers.Add(cell.ToString());
                }

                var rowsCount = tableArray.Count;
                for (int i = 1; i < rowsCount; i++)
                {
                    var row = tableArray[i];
                    var rowLength = row.Count;

                    for (int j = 0; j < rowLength; j++)
                    {
                        var cell = row[j];
                        var header = _headers[j];

                        parser.Parse(header, cell.ToString());
                    }
                }

                Debug.Log($"Sheet parsed successfully.");
            }
            else
            {
                Debug.LogWarning("No data found in Google Sheets.");
            }
        }
    }
}