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

        public List<EconomicObject> ListOfEconomicObjects {get; set;}

    }
}