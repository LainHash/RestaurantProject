using AutoMapper;
using Restaurant.Application.Features.Storage.Images.Queries.GetAll;
using Restaurant.Application.Features.Storage.Images.Queries.GetAllByProductId;
using Restaurant.Application.Services.Storage;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Storage;

namespace Restaurant.Persistence.Services.Storage
{
    internal class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        private readonly IMapper _mapper;

        public ImageService(
            IImageRepository imageRepository,
            IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<IEnumerable<ImageResponse>>> GetAllAsync(
            GetAllImagesSpecification specification,
            CancellationToken cancellationToken)
        {
            var images = await _imageRepository.ToListAsync(specification, cancellationToken);

            var totalItems = await _imageRepository.CountAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ImageResponse>>(images);
            return PageResult<IEnumerable<ImageResponse>>
                .Succeed(response, Success<Image>.Retrieved, totalItems, specification.Skip, specification.Take);
        }

        public async Task<PageResult<IEnumerable<ImageResponse>>> GetAllByProductIdAsync(
            GetAllImagesByProductIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var images = await _imageRepository.ToListAsync(specification, cancellationToken);

            var totalItems = await _imageRepository.CountAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ImageResponse>>(images);
            return PageResult<IEnumerable<ImageResponse>>
                .Succeed(response, Success<Image>.Retrieved, totalItems, specification.Skip, specification.Take);
        }
    }
}
