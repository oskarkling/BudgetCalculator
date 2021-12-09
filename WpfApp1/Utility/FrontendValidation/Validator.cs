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

        public static bool GetAddExpenseValidaton()
        {
            return AddExpenseValidator;
        }
        public static void SetAddExpenseValidaton(bool statement)
        {
            AddExpenseValidator = statement;
        }
    }
}
