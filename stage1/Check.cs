using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace stage1
{
    class Check
    {
        bool IsByte (string field)
        {
            return Regex.IsMatch(field, @"^[cCxX]'.+'$");
        }

        bool IsOperands (string field)
        {
            return Regex.IsMatch(field, @"^\w+\s*,\s*\w+$");
        }


     
    }
}