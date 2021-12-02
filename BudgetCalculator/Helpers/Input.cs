using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BudgetCalculator
{
    public class Input
    {
        
        /// <summary>
        /// Checks so the string name dosent contain
        /// null, string empty and string whitespace
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>bool if success</returns>
        public bool IsValidString(string check)
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
        public static bool IsStringNull(string check)
        {
            return check == null;
        }

        /// <summary>
        /// Checks if string is Empty
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>if string is empty return false</returns>
        public static bool IsStringEmpty(string check)
        {
            return check.Length == 0;
        }

        /// <summary>
        ///  Checks if string is starts with whitespace
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>if string starts with whitespace return false</returns>
        public static bool IsStringPreWhitespace(string check)
        {
            return check[0] == ' ';
        }

        /// <summary>
        /// Checks if objects amount is greater than 0
        /// </summary>
        /// <param name="amount">double amount</param>
        /// <returns>If double amount is greater than 0 return true</returns>
        public bool IsAmountMoreThanZero(decimal amount)
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
    }
}