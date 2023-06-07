using System;
using System.Collections.Generic;

#nullable disable

namespace UpMoneyProjesi.Models
{
    public partial class Budget
    {
        public int BudgetId { get; set; }
        public decimal Budget1 { get; set; }
        public int BudgetTypeId { get; set; }
        public int CustomerId { get; set; }

        public virtual ExpensesType BudgetType { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
