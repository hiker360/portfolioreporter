using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PortfolioReporter.Utils
{
    internal static class MySqlDbUtils
    {

        public static T ExecuteSingleton<T>(MySqlConnection conn, String sql, String fieldName, T defaultValue)
        {
            T fieldValue = defaultValue;
            using MySqlCommand  cmd = new MySqlCommand (sql, conn);
            using MySqlDataReader  reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                fieldValue = MySqlDbUtils.GetValue<T>(reader[fieldName]);
            }


            return fieldValue;
        }

        public static void ExecuteNonQuery(MySqlConnection conn, String sql)
        {
            using MySqlCommand  cmd = new MySqlCommand (sql, conn);
            cmd.ExecuteNonQuery();
        }


        public static T ExecuteSingleton<T>(MySqlCommand  cmd, String fieldName, T defaultValue)
        {
            T fieldValue = defaultValue;
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                fieldValue = MySqlDbUtils.GetValue<T>(reader[fieldName]);
            }

            return fieldValue;
        }

        public static bool Exists(MySqlConnection conn, MySqlCommand  cmd)
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



        public static T ExecuteSingleton<T>(MySqlCommand  cmd, String fieldName)
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

        //public static void AddParameterValue<T>(MySqlCommand  cmd, String parameterName, SqlDbType paramType, T value)
        //{
        //    cmd.Parameters.Add(parameterName, paramType).Value = (T)value;
        //}

        public static void ResetTableSeed(MySqlConnection conn, String tableName)
        {
            using var cmd = new MySqlCommand ("DBCC CHECKIDENT('[" + tableName + "]', RESEED, 0);", conn);
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
