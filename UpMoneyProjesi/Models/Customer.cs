using System;
using System.Collections.Generic;

#nullable disable

namespace UpMoneyProjesi.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Budgets = new HashSet<Budget>();
            Expenses = new HashSet<Expense>();
            MySubscribes = new HashSet<MySubscribe>();
            Wallets = new HashSet<Wallet>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerEmail { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<MySubscribe> MySubscribes { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
