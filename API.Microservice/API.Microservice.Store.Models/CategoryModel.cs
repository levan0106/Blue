using API.Core.Entities;
using System;

namespace API.Microservice.Store.Entities
{
    public class CategoryModel:BaseModel
    {
        public string Description { get; set; }
        public string Image { get; set; }
        public int LocationId { get; set; }
        public bool FixedPosition { get; set; }
    }
}
