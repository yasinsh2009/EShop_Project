using EShop.Application.Extensions;
using EShop.Application.Services.Interface;
using EShop.Application.Utilities;
using EShop.Domain.DTOs.Site.Slider;
using EShop.Domain.Entities.Site;
using EShop.Domain.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using System.ComponentModel.DataAnnotations;

namespace EShop.Application.Services.Implementation
{
    public class SiteImagesService : ISiteImagesService
    {
        #region Ctor

        private readonly IGenericRepository<Slider> _sliderRepository;

        public SiteImagesService(IGenericRepository<Slider> sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        #endregion

        #region Silder

        public async Task<List<Slider>> GetAllSlides()
        {
            return await _sliderRepository
                .GetQuery()
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();
        }
        public async Task<List<Slider>> GetAllActiveSlides()
        {
            return await _sliderRepository
                .GetQuery()
                .Where(x => x.IsActive && !x.IsDelete)
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();
        }
        public async Task<CreateSliderResult> CreateSlide(CreateSliderDto slide, IFormFile slideImage, IFormFile? slideMobileImage)
        {
            try
            {
                string imageName = null;
                if (slideImage != null)
                {
                    if (slideImage.IsImage())
                    {
                        imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(slideImage.FileName);
                        slideImage.AddImageToServer(imageName, PathExtension.SliderOriginServer,
                            100, 100, PathExtension.SliderThumbServer);
                    }
                }

                string mobileImageName = null;
                if (slideMobileImage != null)
                {
                    if (slideMobileImage.IsImage())
                    {
                        mobileImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(slideMobileImage.FileName);
                        slideImage.AddImageToServer(mobileImageName, PathExtension.MobileSliderOriginServer,
                            100, 100, PathExtension.MobileSliderThumbServer);
                    }
                }

                var newSilde = new Slider
                {
                    ImageName = imageName,
                    MobileImageName = mobileImageName,
                    Link = slide.Link,
                    Description = slide.Description,
                    IsActive = slide.IsActive,
                    IsDelete = false,
                    CreateDate = DateTime.Now
                };

                await _sliderRepository.AddEntity(newSilde);
                await _sliderRepository.SaveChanges();

                return CreateSliderResult.Success;
            }
            catch (Exception)
            {
                return CreateSliderResult.Error;
            }
        }
        public async Task<EditSliderDto> GetSlideForEdit(long id)
        {
            var slide = await _sliderRepository
                .GetQuery()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (slide == null) return null;

            return new EditSliderDto
            {
                Id = slide.Id,
                Link = slide.Link,
                Description = slide.Description,
                IsActive = slide.IsActive,
            };
        }
        public async Task<EditSliderResult> EditSlide(EditSliderDto slide, IFormFile slideImage, IFormFile? slideMobileImage, string editorName)
        {
            try
            {
                var Slide = _sliderRepository
                    .GetQuery()
                    .SingleOrDefault(x => x.Id == slide.Id);

                if (slideImage != null)
                {
                    if (slideImage.IsImage())
                    {
                        var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(slideImage.FileName);
                        slideImage.AddImageToServer(imageName, PathExtension.SliderOriginServer,
                            100, 100, PathExtension.SliderThumbServer);

                        Slide.ImageName = imageName;
                    }
                }

                if (slideMobileImage != null)
                {
                    if (slideMobileImage.IsImage())
                    {
                        var mobileImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(slideMobileImage.FileName);
                        slideImage.AddImageToServer(mobileImageName, PathExtension.MobileSliderOriginServer,
                            100, 100, PathExtension.MobileSliderThumbServer);

                        Slide.MobileImageName = mobileImageName;
                    }
                }

                Slide.Link = slide.Link;
                Slide.Description = slide.Description;
                Slide.IsActive = slide.IsActive;
                Slide.LastUpdateDate = DateTime.Now;
                
                

                _sliderRepository.EditEntityByUser(Slide, editorName);
                await _sliderRepository.SaveChanges();

                return EditSliderResult.Success;
            }
            catch (Exception)
            {
                return EditSliderResult.Error;
            }
        }
        public async Task<bool> ActivateSlide(long id)
        {
            try
            {
                var slide = _sliderRepository
                .GetQuery()
                .SingleOrDefault(x => x.Id == id);

                slide.IsActive = true;
                slide.IsDelete = false;
                slide.LastUpdateDate = DateTime.Now;

                _sliderRepository.EditEntity(slide);
                await _sliderRepository.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeActivateSlide(long id)
        {
            try
            {
                var slide = _sliderRepository
                .GetQuery()
                .SingleOrDefault(x => x.Id == id);

                slide.IsActive = false;
                slide.IsDelete = true;
                slide.LastUpdateDate = DateTime.Now;

                _sliderRepository.EditEntity(slide);
                await _sliderRepository.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_sliderRepository != null)
            {
                await _sliderRepository.DisposeAsync();
            }
        }

        #endregion
    }
}
