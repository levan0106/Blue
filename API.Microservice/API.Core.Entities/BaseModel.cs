using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Entities
{
    public abstract class BaseModel
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDTS { get; set; }
        public DateTime? UpdateDTS { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
        public int? ActiveStatus { get; set; }
    }
}
