// Copyright [2011] [PagSeguro Internet Ltda.]
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace Uol.PagSeguro.Util
{
    /// <summary>
    /// 
    /// </summary>
    internal static class PagSeguroUtil
    {

        /// <summary>
        /// Truncate a String and add final end chars to them
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <param name="endChars"></param>
        /// <returns></returns>
        public static string TruncateValue(string value, int limit, string endChars)
        {
            if (!value.Equals(null) && value.Length > limit)
                value = value.Substring(0, limit - endChars.Length) + endChars;
            return value;
        }

        /// <summary>
        /// Remove extra spaces from String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveExtraSpaces(string value)
        {
            return value.Replace("( +)", " ").Trim();
        }
        
        /// <summary>
        /// Format a String dropping extra spaces and truncate value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <param name="endChars"></param>
        /// <returns></returns>
        public static string FormatString(string value, int limit, string endChars)
        {
            return PagSeguroUtil.TruncateValue(PagSeguroUtil.RemoveExtraSpaces(value), limit, endChars);
        }

        /// <summary>
        /// Get only numbers from a string value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetOnlyNumbers(String value)
        {
            return Regex.Replace(value, @"\D+", string.Empty);
        }

        /// <summary>
        /// Format a decimal number, just two decimal places
        /// </summary>
        /// <param name="numeric"></param>
        /// <returns></returns>
        public static string DecimalFormat(decimal numeric) 
        {
            return string.Format("{0:0.00}", numeric).Replace(",", ".");
        }

        /// <summary>
        /// Format a double number, just two decimal places
        /// </summary>
        /// <param name="numeric"></param>
        /// <returns></returns>
        /// 
        public static string DecimalFormat(double numeric)
        {
            return string.Format("{0:0.00}", numeric).Replace(",", ".");
        }

        /// <summary>
        /// Converts a double number into a integer
        /// </summary>
        /// <param name="numeric"></param>
        /// <returns></returns>
        public static string DoubleToInt(double numeric)
        {
            return string.Format("{0:0}", numeric);
        }

        /// <summary>
        /// Check if var is empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(PagSeguroUtil.RemoveExtraSpaces(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateXml(DateTime date)
        {
            date.ToUniversalTime();
            date.AddHours(-3);
            return XmlConvert.ToString(date, XmlDateTimeSerializationMode.RoundtripKind);
        }
    }
}
