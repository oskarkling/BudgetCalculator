using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Utility.FrontendValidation
{
    public static class Validator
    {
        public static bool AddExpenseValidator { get; set; }

        public static bool GetAddExpenseValidation()
        {
            return AddExpenseValidator;
        }
        public static void SetAddExpenseValidation(bool statement)
        {
            AddExpenseValidator = statement;
        }
    }
}
