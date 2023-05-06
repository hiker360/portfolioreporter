using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace PortfolioReporter.Utils
{
    internal static class OleDbUtils
    {

        public static T ExecuteSingleton<T>(OleDbConnection conn, String sql, String fieldName, T defaultValue)
        {
            T fieldValue = defaultValue;
            using OleDbCommand cmd = new OleDbCommand(sql, conn);
            using OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                fieldValue = OleDbUtils.GetValue<T>(reader[fieldName]);
            }


            return fieldValue;
        }

        public static void ExecuteNonQuery(OleDbConnection conn, String sql)
        {
            using OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }


        public static T ExecuteSingleton<T>(OleDbCommand cmd, String fieldName, T defaultValue)
        {
            T fieldValue = defaultValue;
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                fieldValue = OleDbUtils.GetValue<T>(reader[fieldName]);
            }

            return fieldValue;
        }

        public static bool Exists(OleDbConnection conn, OleDbCommand cmd)
        {
            bool exists = false;
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    exists = true;
                }
            }
            return exists;
        }



        public static T ExecuteSingleton<T>(OleDbCommand cmd, String fieldName)
        {
            T fieldValue = ExecuteSingleton(cmd, fieldName, default(T));
            return fieldValue;
        }


        public static T GetValue<T>(object obj)
        {
            if (typeof(DBNull) != obj.GetType())
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            return default(T);
        }

        public static T GetValue<T>(object obj, object defaultValue)
        {
            if (typeof(DBNull) != obj.GetType())
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            return (T)defaultValue;
        }

        public static void AddParameterValue<T>(OleDbCommand cmd, String parameterName, SqlDbType paramType, T value)
        {
            cmd.Parameters.Add(parameterName, paramType).Value = (T)value;
        }

        public static void ResetTableSeed(OleDbConnection conn, String tableName)
        {
            using var cmd = new OleDbCommand("DBCC CHECKIDENT('[" + tableName + "]', RESEED, 0);", conn);
            cmd.ExecuteNonQuery();
        }

        public static String FormatListForContains<T>(IList<T> list)
        {
            var str = "";
            var sep = "";
            foreach (var item in list)
            {
                str += "\'" + (sep + item.ToString()) + "\'";
                sep = ",";

            }

            return str;
        }

    }
}
