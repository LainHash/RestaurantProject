using AutoMapper;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Persistence.DataRecords.Storage;

namespace Restaurant.Persistence.Mapping.Storage
{
    internal class ImageMapping : Profile
    {
        public ImageMapping()
        {
            CreateMap<ImageRecord, Image>();
        }
    }
}
