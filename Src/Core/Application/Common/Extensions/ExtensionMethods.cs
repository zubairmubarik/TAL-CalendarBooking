//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;

namespace Application.Common.Extensions
{
    public static class ExtensionMethods
    {
        #region To 


        /// <summary>
        /// Parse Exact to DateTime Format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParseExactToDateTime(this DateTime dateTime, string input, string format, out DateTime result) => DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out result);

        /// <summary>
        /// Convert DateTime to specific string Format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringDateTime(this DateTime dateTime, string format = "dd/MM HH:mm") => dateTime.ToString("dd/MM HH:mm");
       

        /// <summary>
        /// Date Time Is Null
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsNull(this DateTime? dateTime)=> !dateTime.HasValue;

        /// <summary>
        /// Date Time minimum value
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsMinValue(this DateTime dateTime) => (dateTime == DateTime.MinValue);

        #endregion

    }
}
