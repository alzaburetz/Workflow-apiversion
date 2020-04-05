using System;
using System.Collections.Generic;
using System.Text;


namespace Workflow.Utils
{
    public static class ListStringifyer
    {
        public static string StringifyList(List<string> list)
        {
            string result = string.Empty;
            foreach (var str in list)
            {
                result += $"{str}, ";
            }
            return result.Trim().Remove(result.Length - 2, 1);
        }
    }
}
