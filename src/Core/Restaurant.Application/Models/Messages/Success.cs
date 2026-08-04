namespace Restaurant.Application.Models.Messages
{
    public static class Success<TEntity> where TEntity : class
    {
        public readonly static string Retrieved = $"{typeof(TEntity).Name} retrived successfully.";

        public readonly static string Created = $"{typeof(TEntity).Name} created successfully.";
        public readonly static string Updated = $"{typeof(TEntity).Name} updated successfully.";

        public readonly static string Deleted = $"{typeof(TEntity).Name} deleted successfully.";
        public readonly static string Restored = $"{typeof(TEntity).Name} restored successfully.";

        public readonly static string Uploaded = $"{typeof(TEntity).Name} uploaded successfully.";
        public readonly static string Added = $"{typeof(TEntity).Name} added successfully.";

        public readonly static string Planted = $"{typeof(TEntity).Name} planted successfully.";
    }
}
