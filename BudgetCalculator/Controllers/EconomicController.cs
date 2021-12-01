
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BudgetCalculator
{
    /// <summary>
    /// The controller of EconomicObject objects.
    /// </summary>
    public class EconomicController
    {
        #region Public Methods

        /// <summary>
        /// Constructor for EconomicController
        /// instanciate a new list of EconomicObject
        /// </summary>
        public EconomicController()
        {
            GetList = new List<AbstractEconomicObject>();
        }

        public List<AbstractEconomicObject> GetList { get; }

        public bool AddEconomicObject(AbstractEconomicObject obj)
        {
            if (obj == null) return false;

            bool successs;

            if (obj is Goal) successs = IsGoal(obj as Goal);
            else if (obj is Income) successs = IsIncome(obj as Income);
            else if (obj is Expense) successs = IsExpense(obj as Expense);
            else successs = IsSaving(obj as Saving);

            return successs;
        }

        private void SaveObjToDatabase(AbstractEconomicObject obj)
        {
            //Spara in obj till databasen.

            throw new NotImplementedException();
        }

        private bool IsGoal(Goal obj)
        {
            if (obj.SaveToDate)
            {
                CalculateAmountToSave(obj);
            }
            else
            {
                CalculateEndDate(obj);
            }

            try
            {
                SaveObjToDatabase(obj);
            }
            catch (Exception e) { ErrorLogger.Add(e.Message); }

            return false;
        }

        private bool IsIncome(Income obj)
        {
            try
            {
                SaveObjToDatabase(obj);
                return true;
            }
            catch (Exception e) { ErrorLogger.Add(e.Message); }

            return false;
        }

        private bool IsExpense(Expense obj)
        {
            try
            {
                SaveObjToDatabase(obj);
                return true;
            }
            catch (Exception e) { ErrorLogger.Add(e.Message); }

            return false;
        }

        private bool IsSaving(Saving obj)
        {
            try
            {
                SaveObjToDatabase(obj);
                return true;
            }
            catch (Exception e) { ErrorLogger.Add(e.Message); }

            return false;
        }

        private bool CalculateEndDate(Goal obj)
        {
            try
            {
                obj.MonthsToGoal = (int)obj.Amount / (int)obj.SaveEachMonth;
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

        /// <summary>
        /// Remove an economic object from EconomicObjectList
        /// </summary>
        /// <param name="name">string name</param>
        /// <returns>bool true or false</returns>
        public bool RemoveEconomicObjectFromList(string name)
        {
            if (IsValidString(name))
            {
                if (DoListContainName(name))
                {
                    GetList.RemoveAll(x => x.Name == name);
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Updates an economic objects amount
        /// </summary>
        /// <param name="name">string name</param>
        /// <param name="newAmount">double new amount</param>
        /// <returns>bool true if success</returns>
        public bool UpdateEconomicObjectAmount(string name, double newAmount)
        {
            if (IsValidString(name) && IsAmountMoreThanZero(newAmount) && !IsAmountMoreThanMaxValue(newAmount))
            {
                if (DoListContainName(name))
                {
                    foreach (var ecoObj in GetList)
                    {
                        if (ecoObj.Name.Contains(name))
                        {
                            ecoObj.Amount = (decimal)newAmount;
                            return true;
                        }
                    }
                }
                else
                {
                    string errormsg = $"{this} Name Does not exist in the list, therefore cannot update.";
                    Debug.WriteLine(errormsg);
                    ErrorLogger.Add(errormsg);
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates an economic object name if name
        /// dosent exist
        /// </summary>
        /// <param name="oldName">string old name</param>
        /// <param name="newName">string new name</param>
        /// <returns>bool true if success else false</returns>
        public bool UpdateEconomicObjectName(string oldName, string newName)
        {
            if (IsValidString(oldName) && IsValidString(newName))
            {
                foreach (var ecoObj in GetList)
                {
                    if (ecoObj.Name.Contains(oldName))
                    {
                        ecoObj.Name = newName;
                        return true;
                    }
                }

                string errormsg = $"{this} String name does not exist in economic object list";
                Debug.WriteLine(errormsg);
                ErrorLogger.Add(errormsg);
                return false;
            }
            else
            {
                return false;
            }
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Checks if incoming value is more than double.MaxValue
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>True if value is over double.MaxValue</returns>
        private bool IsAmountMoreThanMaxValue(double amount)
        {
            if (amount > double.MaxValue)
            {
                return true;
            }
            else
            {
                string errormsg = $"{this} Amount was more than double.MaxValue";
                Debug.WriteLine(errormsg);
                ErrorLogger.Add(errormsg);
                return false;
            }
        }

        /// <summary>
        /// Checks so the string name dosent contain
        /// null, string empty and string whitespace
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>bool if success</returns>
        private bool IsValidString(string check)
        {
            string errormsg;
            if (!IsStringNull(check))
            {
                if (!IsStringEmpty(check))
                {
                    if (!IsStringPreWhitespace(check))
                    {
                        return true;
                    }
                    else
                    {
                        errormsg = $"{this} String name had a whitespace as first character";
                        Debug.WriteLine(errormsg);
                        ErrorLogger.Add(errormsg);
                        return false;
                    }
                }
                else
                {
                    errormsg = $"{this} String name was empty";
                    Debug.WriteLine(errormsg);
                    ErrorLogger.Add(errormsg);
                    return false;
                }
            }
            else
            {
                errormsg = $"{this} String name was null";
                Debug.WriteLine(errormsg);
                ErrorLogger.Add(errormsg);
                return false;
            }
        }

        /// <summary>
        /// Checks if string is null
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>if string is null return false</returns>
        private static bool IsStringNull(string check)
        {
            return check == null;
        }

        /// <summary>
        /// Checks if string is Empty
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>if string is empty return false</returns>
        private static bool IsStringEmpty(string check)
        {
            return check.Length == 0;
        }

        /// <summary>
        ///  Checks if string is starts with whitespace
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>if string starts with whitespace return false</returns>
        private static bool IsStringPreWhitespace(string check)
        {
            return check[0] == ' ';
        }

        /// <summary>
        /// Checks if objects amount is greater than 0
        /// </summary>
        /// <param name="amount">double amount</param>
        /// <returns>If double amount is greater than 0 return true</returns>
        private bool IsAmountMoreThanZero(double amount)
        {
            if (amount > 0)
            {
                return true;
            }
            else
            {
                string errormsg = $"{this} Amount was less than zero";
                Debug.WriteLine(errormsg);
                ErrorLogger.Add(errormsg);
                return false;
            }
        }

        /// <summary>
        /// Checks if objects name already exists
        /// </summary>
        /// <param name="name">string name</param>
        /// <returns>bool true if name dosent exists</returns>
        private bool DoListContainName(string name)
        {
            foreach (var ecoObj in GetList)
            {
                if (ecoObj.Name.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion Private Methods
    }
}