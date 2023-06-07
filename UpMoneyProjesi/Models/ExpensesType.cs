using System;
using System.Collections.Generic;

#nullable disable

namespace UpMoneyProjesi.Models
{
    public partial class ExpensesType
    {
        public ExpensesType()
        {
            Budgets = new HashSet<Budget>();
            Expenses = new HashSet<Expense>();
        }

        public int ExpensesTypeId { get; set; }
        public string ExpensesName { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
