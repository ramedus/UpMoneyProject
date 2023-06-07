using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace UpMoneyProjesi.Models
{
    public partial class MySubscribe
    {
        
        public int SubscribeId { get; set; }
        public int SubscribeTypeId { get; set; }
        public decimal SubscribeValue { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual SubscribeType SubscribeType { get; set; }
    }
}
