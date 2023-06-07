using System;
using System.Collections.Generic;

#nullable disable

namespace UpMoneyProjesi.Models
{
    public partial class SubscribeType
    {
        public SubscribeType()
        {
            MySubscribes = new HashSet<MySubscribe>();
        }

        public int SubscribeTypeId { get; set; }
        public string SubscribeName { get; set; }

        public virtual ICollection<MySubscribe> MySubscribes { get; set; }
    }
}
