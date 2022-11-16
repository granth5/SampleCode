using System;
using System.Data;
using System.Data.SqlClient;

namespace GHHSoftware.Common.ExtensionMethods
{
  public static class DataTableExtensions
    {
        public static string GetStringSafe(this DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? row[columnName].ToString() : string.Empty;
        }

        public static T GetValueSafe<T>(this DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? (T)row[columnName] : default(T);
        }

        public static T GetValueSafe<T>(this DataRow row, string columnName, T defaultOverride)
        {
            return row[columnName] != DBNull.Value ? (T)row[columnName] : defaultOverride;
        }

        public static DateTime? GetDateTimeOrNull(this DataRow row, string columnName)
        {
            if (row[columnName] == DBNull.Value)
            {
                return null;
            }
            else if (Convert.ToDateTime(row[columnName]).ToString("MM/dd/yyyy") == "01/01/1901")
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(row[columnName]);
            }
        }

        public static T GetValueSafe<T>(this SqlDataReader reader, string columnName, T defaultOverride)
        {
            return reader[columnName] != DBNull.Value ? (T)reader[columnName] : defaultOverride;
        }
    }
}