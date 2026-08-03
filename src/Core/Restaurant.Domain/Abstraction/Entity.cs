using NanoidDotNet;

namespace Restaurant.Domain.Abstraction
{
    public abstract class Entity
    {
        public int Id { get; private set; }
        public string PublicId { get; private set; } = Nanoid.Generate(size: 10);
    }

    public abstract class AuditableEntity : Entity
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public void MarkCreated(DateTime now)
        {
            CreatedAt = now;
            UpdatedAt = now;
        }

        public void MarkUpdated(DateTime now)
        {
            UpdatedAt = now;
        }
    }

    public abstract class SoftDeletableEntity : AuditableEntity
    {
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
