
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BudgetCalculator
{
    public class EconomicController
    {
        public List<EconomicObject> ListOfEcoObjs { get; }
        public decimal RemainingBalance { get; set; }
        
        public EconomicController()
        {
            ListOfEcoObjs = new List<EconomicObject>();
        }

        public bool UpdateEcoObjects()
        {
            try{
                foreach(var ecoobj in ListOfEcoObjs)
                {
                    if(ecoobj is Goal)
                    {
                        ((Goal)ecoobj).AmountSavedSoFar += ((Goal)ecoobj).Amount;
                        ((Goal)ecoobj).CurrentTime = ((Goal)ecoobj).CurrentTime.AddMonths(1);
                        ((Goal)ecoobj).MonthsToGoal = ((Goal)ecoobj).MonthsToGoal - 1;
                    }   
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Adds an economic object to list
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> true if successful, false if not</returns>
        public bool AddEconomicObject(EconomicObject obj)
        {
            var dbcon = new DatabaseConnection();
            if (obj == null) return false;

            bool successs;

            if (obj is Goal) successs = dbcon.CreateEco(obj as Goal);
            else if (obj is Income) successs = dbcon.CreateEco(obj as Income);
            else if (obj is Expense) successs = dbcon.CreateEco(obj as Expense);
            else successs = dbcon.CreateEco(obj as Saving);

            return false;
        }

        //private bool IsGoal(Goal obj)
        //{
        //    if (obj.SaveToDate)
        //    {
        //        CalculateAmountToSave(obj);
        //    }
        //    else
        //    {
        //        CalculateEndDate(obj);
        //    }

        //    try
        //    {
        //        SaveObjToDatabase(obj);
        //    }
        //    catch (Exception e) { ErrorLogger.Add(e.Message); }

        //    return false;
        //}

        
        public decimal CalculateTotalIncome(List<Income> income)
        {
            decimal total = 0;
            if(income != null)
            {
                foreach (var item in income)
                {
                    total += item.Amount;
                }
            }
            return total;
        }
        public decimal CalculateTotalSavings(List<Saving> savings)
        {
            decimal total = 0;
            if (savings != null)
            {
                foreach (var item in savings)
                {
                    total += item.Amount;
                }
            }
            return total;
        }
        public decimal CalculateTotalExpenses(List<Expense> expenses)
        {
            decimal total = 0;
            if (expenses != null)
            {
                foreach (var item in expenses)
                {
                    total += item.Amount;
                }
            }
            return total;
        }
        //public void CalculateCurrentGoals(List<Goal> goals)
        //{
        //    var goalSummery = new List<Goal>();
        //    decimal total = 0;
        //    if (goals != null)
        //    {
        //        foreach (var item in goals)
        //        {
        //            if(item.)
        //        }
        //    }
        //}

        public DateTime CalculateEndDate(Goal obj)
        {
            try
            {
                obj.MonthsToGoal = (int)MathF.Ceiling((float)obj.Amount / (float)obj.SaveEachMonth);
                return obj.CreationTime.AddMonths(obj.MonthsToGoal);
            }
            catch (Exception e) { ErrorLogger.Add(e.Message); }
            return new DateTime();
        }

        public decimal CalculateAmountToSave(Goal obj)
        {
            try
            {
                return obj.SaveEachMonth = obj.Amount / obj.MonthsToGoal;
            }
            catch (Exception e) { ErrorLogger.Add(e.Message); }

            return 0;
        }
        public decimal CalculateTotalGoals(List<Goal> goals)
        {
            decimal sum = 0;
            if(goals != null)
            {
                foreach (var item in goals)
                {
                    sum += item.Amount;
                }
                return sum;
            }
            return 0;
        }
        public decimal CalculateBalance(decimal income, decimal expenses, decimal savings, decimal goals)
        {
            return income - (expenses + savings + goals);
        }
    }
}