
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BudgetCalculator
{
    public class EconomicController
    {
        public List<Expense> listOfExpenses = new();
        public List<Income> listOfIncomes = new();
        public List<Saving> listOfSavings = new();
        public List<Goal> listOfGoals = new();

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

        private bool CalculateEndDate(Goal obj)
        {
            try
            {
                // Rounds up becouse of months saved.
                obj.MonthsToGoal = (int)MathF.Ceiling((float)obj.Amount / (float)obj.SaveEachMonth);
                return true;
            }
            catch (Exception e) { ErrorLogger.Add(e.Message); }

            return false;
        }

        private bool CalculateAmountToSave(Goal obj)
        {
            try
            {
                obj.SaveEachMonth = obj.Amount / obj.MonthsToGoal;
                return true;
            }
            catch (Exception e) { ErrorLogger.Add(e.Message); }

            return false;
        }
    }
}