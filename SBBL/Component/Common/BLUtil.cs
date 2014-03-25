using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System;
using System.Data;
using System.ComponentModel;

namespace BL.Component.Common
{
    public class BLUtil
    {
        private static CultureInfo culture = new CultureInfo("en-US");
        private static string formatDate = "dd/MM/yyyy";
        private static string formatDateTime = "dd/MM/yyyy HH:mm";
        public static List<string> InjectionList = new List<string>() { @"'", ";" };

        public static string NVLString(object obj)
        {
            string value = string.Empty;
            if (obj != null && obj != DBNull.Value)
            {
                value = Convert.ToString(obj).Trim();

                // replace injection
                foreach (string Injection in InjectionList)
                {
                    value = value.Replace(Injection, "");
                }
            }

            return value;
        }

        public static string NVLString(object obj, string defaultValue)
        {
            string value = defaultValue;
            if (obj != null && obj != DBNull.Value)
            {
                value = Convert.ToString(obj).Trim();

                // replace injection
                foreach (string Injection in InjectionList)
                {
                    value = value.Replace(Injection, "");
                }
            }

            return value;
        }

        public static int NVLInt(object obj)
        {
            int value = 0;
            if (obj != null && obj != DBNull.Value && obj.ToString() != "")
            {
                value = Convert.ToInt32(obj);
            }

            return value;
        }

        public static long NVLLong(object obj)
        {
            long value = 0;
            if (obj != null && obj != DBNull.Value && obj.ToString() != "")
            {
                value = Convert.ToInt64(obj);
            }

            return value;
        }

        public static decimal NVLDecimal(object obj)
        {
            decimal value = 0;
            if (obj != null && obj != DBNull.Value && !string.IsNullOrEmpty(NVLString(obj)))
            {
                value = Convert.ToDecimal(obj);
            }

            return value;
        }

        public static double NVLDouble(object obj)
        {
            double value = 0;
            if (obj != null && obj != DBNull.Value)
            {
                value = Convert.ToDouble(obj);
            }

            return value;
        }

        public static bool NVLBool(object obj)
        {
            bool value = false;
            if (obj != null && obj != DBNull.Value)
            {
                value = Convert.ToBoolean(obj);
            }

            return value;
        }

        public static DateTime NVLDate(object obj)
        {
            DateTime value = DateTime.Now;
            if (obj is string)
            {
                if (obj != null && NVLString(obj) != "")
                {
                    DateTime temp;
                    if (DateTime.TryParseExact(obj.ToString(), formatDate, null, DateTimeStyles.None, out temp))
                    {
                        value = temp;
                    }
                }
            }
            else if (obj is DateTime)
            {
                if (obj != null && obj != DBNull.Value)
                {
                    value = Convert.ToDateTime(obj, culture);
                }
            }
            return value;
        }

        public static DateTime NVLDate(object obj, string format)
        {
            DateTime value = DateTime.Now;
            if (obj is string)
            {
                if (obj != null && NVLString(obj) != "")
                {
                    DateTime temp;
                    if (DateTime.TryParseExact(obj.ToString(), format, null, DateTimeStyles.None, out temp))
                    {
                        value = temp;
                    }
                }
            }
            else if (obj is DateTime)
            {
                if (obj != null && obj != DBNull.Value)
                {
                    value = Convert.ToDateTime(obj, culture);
                }
            }
            return value;
        }

        public static DateTime? NVLDateNull(object obj)
        {
            DateTime? value = null;
            if (obj is string)
            {
                if (obj != null && NVLString(obj) != "")
                {
                    DateTime temp;
                    if (DateTime.TryParseExact(obj.ToString(), formatDate, null, DateTimeStyles.None, out temp))
                    {
                        value = temp;
                    }
                }
            }
            else if (obj is DateTime)
            {
                if (obj != null && obj != DBNull.Value)
                {
                    value = Convert.ToDateTime(obj);
                }
            }
            return value;
        }

        public static DateTime? NVLDateNull(object obj, string format)
        {
            DateTime? value = null;
            if (obj is string)
            {
                if (obj != null && NVLString(obj) != "")
                {
                    DateTime temp;
                    if (DateTime.TryParseExact(obj.ToString(), format, null, DateTimeStyles.None, out temp))
                    {
                        value = temp;
                    }
                }
            }
            else if (obj is DateTime)
            {
                if (obj != null && obj != DBNull.Value)
                {
                    value = Convert.ToDateTime(obj, culture);
                }
            }
            return value;
        }

        public static string FormatDate(DateTime? obj)
        {
            if (obj.HasValue)
            {
                return obj.Value.ToString(formatDate);
            }
            else
            {
                return "";
            }
        }

        public static string FormatDateTime(DateTime? obj)
        {
            if (obj.HasValue)
            {
                return obj.Value.ToString(formatDateTime);
            }
            else
            {
                return "";
            }
        }

        public static string EngToThaiYear(string eng)
        {
            //errer handling outside
            string[] engArr = eng.Split('/');
            //string[] yearArr = engArr[2].Split(' ');
            int year = Convert.ToInt32(engArr[2]) + 543;
            return String.Format("{0}/{1}/{2}", engArr[0], engArr[1], year.ToString());
        }

        public static string ThaiToEngYear(string thai)
        {
            string[] thaiArr = thai.Split('/');
            int year = Convert.ToInt32(thaiArr[2]) - 543;
            return String.Format("{0}/{1}/{2}", thaiArr[0], thaiArr[1], year.ToString());
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

    }
}
