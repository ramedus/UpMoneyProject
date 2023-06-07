using System;
using System.Collections.Generic;

#nullable disable

namespace UpMoneyProjesi.Models
{
    public partial class Expense
    {
        public int ExpensesId { get; set; }
        public int ExpensesTypeId { get; set; }
        public decimal ExpensesFee { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ExpensesType ExpensesType { get; set; }
    }
}
