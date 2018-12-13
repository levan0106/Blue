using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Common;
using API.Microservice.Image.Models;
using API.Microservice.Image.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.ImageMicroservice.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ImageController : BaseApiController
    {
        IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public IEnumerable<ImageModel> Get()
        {
            IEnumerable<ImageModel> result = _imageRepository.Get(this.CurrentUser);
            return result;
        }

        [HttpGet("{id}")]
        public ImageModel Get(int id)
        {
            ImageModel result = _imageRepository.Get(id, this.CurrentUser);
            return result;
        }

        [HttpGet("[action]/{type}/{id}")]
        public IEnumerable<ImageModel> GetByCategory(string type, int id)
        {
            IEnumerable<ImageModel> result = _imageRepository.GetByCategoryId(id, type, this.CurrentUser);
            return result;
        }
        [HttpGet("[action]/{type}")]
        public IEnumerable<ImageModel> GetByCategory(string type)
        {
            IEnumerable<ImageModel> result = _imageRepository.GetByCategory(type, this.CurrentUser);
            return result;
        }
    }
}