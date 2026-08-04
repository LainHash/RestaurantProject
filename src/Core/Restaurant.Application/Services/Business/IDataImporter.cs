namespace Restaurant.Application.Services.Business
{
    public interface IDataImporter
    {
        IReadOnlyList<T> Read<T>(string sheetName) where T : class, new();
    }
}
