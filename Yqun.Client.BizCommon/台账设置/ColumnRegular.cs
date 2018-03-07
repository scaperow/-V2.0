using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BizCommon
{
    public class ColumnRegular
    {
        public static Boolean JudgeColumnName(string ColumnName)
        {
            string RegularString = "^[A-Za-z]+[0-9]+$";
            Match w = Regex.Match(ColumnName, RegularString);
            if (w.Success)
                return true;
            else
                return false; 
        }
    }
}
