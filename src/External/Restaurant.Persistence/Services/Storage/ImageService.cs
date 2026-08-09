using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Storage.Images.Commands.Upload;
using Restaurant.Application.Features.Storage.Images.Queries.GetAll;
using Restaurant.Application.Features.Storage.Images.Queries.GetAllByProductId;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Storage;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Storage;
using System.Net;

namespace Restaurant.Persistence.Services.Storage
{
    internal class ImageService : IImageService
    {
        private const int MaxImagesPerProduct = 5;

        private readonly IImageRepository _imageRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ImageService> _logger;

        public ImageService(
            IImageRepository imageRepository,
            IProductImageRepository productImageRepository,
            IProductRepository productRepository,
            ICloudinaryService cloudinaryService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ImageService> logger)
        {
            _imageRepository = imageRepository;
            _productImageRepository = productImageRepository;
            _productRepository = productRepository;
            _cloudinaryService = cloudinaryService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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

        public async Task<Result<UploadImageResponse>> UploadProductImageAsync(
            UploadProductImageCommand command,
            UploadProductImageSpecification specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(command.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<UploadImageResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            var currentCount = await _productImageRepository.CountByProductIdAsync(product.Id, cancellationToken);
            if (currentCount >= MaxImagesPerProduct)
            {
                return Result<UploadImageResponse>
                    .Fail($"Product đã đạt giới hạn tối đa {MaxImagesPerProduct} ảnh.", HttpStatusCode.UnprocessableEntity);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var uploadResult = await _cloudinaryService.UploadAsync(
                    fileStream: command.FileStream,
                    fileName: command.FileName,
                    folder: "Foods",
                    cancellationToken);

                if (!uploadResult.IsSuccess)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result<UploadImageResponse>
                        .Fail(uploadResult.ErrorMessage ?? "Upload ảnh thất bại.", HttpStatusCode.BadGateway);
                }

                if (command.Metadata.IsPrimary)
                {
                    await _productImageRepository.UnsetPrimaryAsync(product.Id, cancellationToken);
                }

                var image = _mapper.Map<Image>(uploadResult)
                    .SetAltText(command.Metadata.AltText ?? command.FileName);

                _imageRepository.Add(image);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var productImage = ProductImage.Create(
                    productId: product.Id,
                    imageId: image.Id,
                    isPrimary: command.Metadata.IsPrimary,
                    displayOrder: currentCount + 1);

                _productImageRepository.Add(productImage);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);


                var response = _mapper.Map<UploadImageResponse>(image);
                _mapper.Map(productImage, response);
                return Result<UploadImageResponse>
                    .Succeed(response, "Upload ảnh thành công.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Lỗi khi upload ảnh cho Sản phẩm.");
                return Result<UploadImageResponse>
                    .Fail("Lỗi khi upload ảnh.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
