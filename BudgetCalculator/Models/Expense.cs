using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator
{
    public class Expense : AbstractEconomicObject
    {
        public bool Recurring { get; set; }
    }
}
