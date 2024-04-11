namespace Kimicu.ExcelImporter
{
    public interface IGoogleSheetParser
    {
        public void Parse(string header, string token);
    }
}