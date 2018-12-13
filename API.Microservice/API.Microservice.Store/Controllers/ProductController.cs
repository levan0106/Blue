using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Common;
using API.Microservice.Store.Entities;
using API.Microservice.Store.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Microservice.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : BaseApiController
    {
        IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            IEnumerable<ProductModel> result = _productRepository.Get(this.CurrentUser);
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            ProductModel result = _productRepository.Get(id,this.CurrentUser);
            return result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] ProductModel value)
        {
            _productRepository.Add(value,this.CurrentUser);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductModel value)
        {
            _productRepository.Update(id, value,this.CurrentUser);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productRepository.Delete(id,this.CurrentUser);
        }
    }
}
