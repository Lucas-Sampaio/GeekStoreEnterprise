﻿using System.Linq;

namespace GeekStore.Core.Utils
{
    public static class StringUtils
    {
        public static string ApenasNumeros(this string str)
        {
            return new string(str.Where(char.IsDigit).ToArray());
        }
    }
}
