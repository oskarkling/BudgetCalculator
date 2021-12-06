using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetCalculator
{
    /// <summary>
    /// A DTO of economic objects
    /// </summary>
    public abstract class EconomicObject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationTime { get; set; }
        public int Interval { get; set; }


        public int AccountId { get; set; }
        public Account Account { get; set; }

    }
}