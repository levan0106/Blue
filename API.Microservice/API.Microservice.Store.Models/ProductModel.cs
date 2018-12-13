using API.Core.Entities;
using System;

namespace API.Microservice.Store.Entities
{
    public class ProductModel:BaseModel
    {
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double Deposit { get; set; }
        public DateTime? AvailableFromDate { get; set; }
        public DateTime? AvailableToDate { get; set; }
        public int LocationId { get; set; }
        public bool Shipping { get; set; }
        public string Address { get; set; }
    }
}
