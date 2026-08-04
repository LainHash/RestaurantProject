namespace Restaurant.Application.Models.Messages
{
    public static class Error<TEntity> where TEntity : class
    {
        public static string EmptyList = $"{typeof(TEntity).Name} list empty.";

        public static string NotFound = $"{typeof(TEntity).Name} not found.";

        public static string NotYetDeleted = $"{typeof(TEntity).Name} not yet deleted.";

        public static string AlreadyDeleted = $"{typeof(TEntity).Name} already deleted.";

        public static string AlreadyAdded = $"{typeof(TEntity).Name} already added.";

        public static string OutOfStock = $"{typeof(TEntity).Name} out of stock.";

        public static string Occupied = $"{typeof(TEntity).Name} occupied.";

        public static string Empty = $"{typeof(TEntity).Name} empty.";

        public static string ExistedName = $"{typeof(TEntity).Name} with this name already existed.";
    }
}
