using System;
using System.Collections.Generic;
using Kimicu.ExcelImporter.Samples.ProductExample;

namespace Kimicu.ExcelImporter
{
    [Serializable]
    public partial class GameSettings
    {
        public List<ItemSettings> Products;
    }
}