using System;
using System.Collections.Generic;

namespace Kimicu.ExcelImporter.Samples.ProductExample
{
    public class ItemsSettingsParser : IGoogleSheetParser
    {
        private readonly GameSettings _gameSettings;
        private ItemSettings _currentItemSettings;

        public ItemsSettingsParser(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _gameSettings.Products = new List<ItemSettings>();
        }

        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Id":
                    _currentItemSettings = new ItemSettings() { Id = token };
                    _gameSettings.Products.Add(_currentItemSettings);
                    break;
                case "Name":
                    _currentItemSettings.Name = token;
                    break;
                case "Price":
                    _currentItemSettings.Price = Convert.ToInt32(token);
                    break;
                default:
                    throw new Exception($"Invalid header: {header}!");
            }
        }
    }
}