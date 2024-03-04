using System;
using System.Text.RegularExpressions;

namespace TSUBASAMUSU.Google.Spreadsheet
{
    public static class SpreadsheetUtility
    {
        /// <summary>
        /// Convert a row and a column number to a string of a cell number.
        /// </summary>
        public static string GetStringValueFromCellValues(int row, int columm) => ConvertIntegerToAlphabets(columm) + row.ToString();

        /// <summary>
        /// Convert a integer value to an alphabetic character.
        /// </summary>
        public static string ConvertIntegerToAlphabets(int number)
        {
            if (number <= 0) return string.Empty;

            string alphabet = string.Empty;

            do
            {
                int remainder = number % 26;

                int quotient = number / 26;

                if (remainder == 0)
                {
                    remainder = 26;

                    quotient--;
                }

                alphabet = ((char)((int)'A' + remainder - 1)).ToString() + alphabet;

                number = quotient;

            }
            while (number > 26);

            alphabet = ((char)((int)'A' + number - 1)).ToString() + alphabet;

            return alphabet.Replace("@", string.Empty);
        }

        /// <summary>
        /// Convert an alphabetic character to a integer value.
        /// </summary>
        public static int ConvertAlphabetToInteger(string alphabet)
        {
            if (string.IsNullOrEmpty(alphabet)) return -1;

            if (new Regex("^[A-Z]+$").IsMatch(alphabet))
            {
                int number = 0;

                for (int i = 0; i < alphabet.Length; i++)
                {
                    int num = Convert.ToChar(alphabet[alphabet.Length - i - 1]) - 65;

                    num++;

                    number += (int)(num * Math.Pow(26, i));
                }

                return number;
            }

            return -1;
        }
    }
}