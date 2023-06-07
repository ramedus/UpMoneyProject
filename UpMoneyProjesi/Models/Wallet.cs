using System;
using System.Collections.Generic;

#nullable disable

namespace UpMoneyProjesi.Models
{
    public partial class Wallet
    {
        public int WalletId { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }

        
        public virtual Customer Customer { get; set; }
    }
}
