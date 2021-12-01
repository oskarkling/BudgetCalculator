using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator
{
    public class Goal : EconomicObject
    {
        public decimal GoalAmount { get; set; }
        public decimal AmountSavedSoFar { get; set; }
        public DateTime GoalDate { get; set; }
        public DateTime CurrentTime { get; set; }
        public bool SaveToDate { get; set; }

        public Goal()
        {
            CurrentTime = base.CreationTime;
        }
    }
}
