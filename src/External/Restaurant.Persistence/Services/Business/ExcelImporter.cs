using MiniExcelLibs;
using Restaurant.Application.Services.Business;

namespace Restaurant.Persistence.Services.Business
{
    internal class ExcelImporter : IDataImporter
    {
        private readonly string _filePath;

        public ExcelImporter()
        {
            _filePath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "RestaurantData.xlsx");
        }

        public IReadOnlyList<T> Read<T>(string sheetName)
            where T : class, new()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException(_filePath);

            return MiniExcel
                .Query<T>(_filePath, sheetName)
                .ToList();
        }
    }
}
