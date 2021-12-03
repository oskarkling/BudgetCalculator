using System;

namespace BudgetCalculator
{
    public class Goal : EconomicObject
    {
        public decimal GoalAmount { get; set; }
        public decimal AmountSavedSoFar { get; set; }
        public int MonthsToGoal { get; set; }
        public DateTime CurrentTime { get; set; }
        public bool SaveToDate { get; set; }
        public decimal SaveEachMonth { get; set; }

    }
}
