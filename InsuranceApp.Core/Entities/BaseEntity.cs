using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApp.Core.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EffectedDate { get; set; }
    }
}
