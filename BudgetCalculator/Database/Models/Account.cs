using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetCalculator
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required]
        //public bool IsAdmin { get; set; }

        public List<Goal> Goals {get; set;}
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
        public List<Saving> Savings { get; set; }
    }
}