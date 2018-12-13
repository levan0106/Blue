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
    public class CategoryController : BaseApiController
    {
        ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<CategoryModel> Get()
        {
            IEnumerable<CategoryModel> result = _categoryRepository.Get(this.CurrentUser);
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public CategoryModel Get(int id)
        {
            CategoryModel result = _categoryRepository.Get(id,this.CurrentUser);
            return result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] CategoryModel value)
        {
            _categoryRepository.Add(value,this.CurrentUser);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CategoryModel value)
        {
            _categoryRepository.Update(id, value,this.CurrentUser);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _categoryRepository.Delete(id,this.CurrentUser);
        }
    }
}
