using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Storage
{
    public partial class ProductImage : AuditableEntity
    {
        public int DisplayOrder { get; private set; }
        public bool IsPrimary { get; private set; }

        public int ProductId { get; private set; }
        public int ImageId { get; private set; }

        public Product Product { get; private set; } = null!;
        public Image Image { get; private set; } = null!;
    }

    public partial class ProductImage
    {
        public ProductImage() { }

        public ProductImage(int productId, int imageId, bool isPrimary, int displayOrder)
        {
            ProductId = productId;
            ImageId = imageId;
            IsPrimary = isPrimary;
            DisplayOrder = displayOrder;
        }

        public static ProductImage Create(int productId, int imageId, bool isPrimary, int displayOrder)
        {
            return new ProductImage(productId, imageId, isPrimary, displayOrder);
        }

        public void RemovePrimary()
        {
            IsPrimary = false;
        }

        public ProductImage SetProduct(int productId)
        {
            ProductId = productId;
            return this;
        }

        public ProductImage SetImage(int imageId)
        {
            ImageId = imageId;
            return this;
        }
    }
}
