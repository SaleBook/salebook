using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Reflection;
using BL.Component.Common;
using SBBL.Component.Session;

namespace BL.Dao
{
    public class BaseDAO<T> where T : new()
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static T singleton = new T();

        public static T Instance
        {
            get
            {
                return singleton;
            }
        }

        protected SqlConnection CreateConnection()
        {
            string constr = ConfigurationManager.AppSettings["SBConstr"];
            //string dbpass = ConfigurationManager.AppSettings["DBPASS"];
            //string dbpassPain = DataCryptography.Decrypt(dbpass);
            //constr = constr + dbpassPain;
            SqlConnection con = new SqlConnection(constr);

            return con;
        }

        protected long GetIdentity(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            string sql = "SELECT @@IDENTITY AS 'Identity' ";
            command.CommandText = sql;
            int id = Convert.ToInt32(command.ExecuteScalar());
            return id;
        }

        protected void AddSQLParam(SqlParameterCollection @params, string name, object val)
        {
            AddSQLParam(@params, name, val, TypeConvertor.ToSqlDbType(val));
        }

        protected void AddSQLParam(SqlParameterCollection @params, string name, object val, System.Data.SqlDbType type)
        {
            object paramValue = null;

            if (val is int || val is long || val is decimal || val is double || val is bool ||
                val is string || val is DateTime)
            {
                paramValue = val;
            }
            else if (val is DateTime?)
            {
                DateTime? dt;
                dt = (DateTime?)val;
                if (dt.HasValue)
                {
                    paramValue = dt.Value;
                }
                else
                {
                    paramValue = DBNull.Value;
                }
            }
            else if (val is decimal?)
            {
                decimal? dc;
                dc = (decimal?)val;
                if (dc.HasValue)
                {
                    paramValue = dc.Value;
                }
                else
                {
                    paramValue = DBNull.Value;
                }
            }
            else
            {
                paramValue = DBNull.Value;
            }

            @params.Add(name, type).Value = paramValue;
        }

        protected DataTable GetDataTable(string sql, SqlConnection conn)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = conn.CreateCommand();
            adapter.SelectCommand.CommandText = sql;
            adapter.Fill(dt);

            return dt;
        }

        protected DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = conn.CreateCommand();
                adapter.SelectCommand.CommandText = sql;
                adapter.Fill(dt);
            }

            return dt;
        }

        protected DataTable GetDataTable(string sql, string condField, object val)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = conn.CreateCommand();
                adapter.SelectCommand.CommandText = sql;

                SqlParameterCollection param = adapter.SelectCommand.Parameters;
                param.Clear();
                AddSQLParam(param, condField, val);

                adapter.Fill(dt);
            }

            return dt;
        }

        protected DataTable GetDataTable(string sql, string condField, object val, SqlConnection conn)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = conn.CreateCommand();
            adapter.SelectCommand.CommandText = sql;

            SqlParameterCollection param = adapter.SelectCommand.Parameters;
            param.Clear();
            AddSQLParam(param, condField, val);

            adapter.Fill(dt);

            return dt;
        }

        protected DataTable GetDataTable(string sql, string[] condFields, object[] vals)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = conn.CreateCommand();
                adapter.SelectCommand.CommandText = sql;

                SqlParameterCollection param = adapter.SelectCommand.Parameters;
                param.Clear();
                for (int i = 0; i < condFields.Length; i++)
                {
                    AddSQLParam(param, condFields[i], vals[i]);
                }
                adapter.Fill(dt);
            }

            return dt;
        }

        protected DataTable GetDataTable(string sql, string[] condFields, object[] vals, SqlConnection conn)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = conn.CreateCommand();
            adapter.SelectCommand.CommandText = sql;

            SqlParameterCollection param = adapter.SelectCommand.Parameters;
            param.Clear();
            for (int i = 0; i < condFields.Length; i++)
            {
                AddSQLParam(param, condFields[i], vals[i]);
            }
            adapter.Fill(dt);

            return dt;
        }

        protected void InsertLog(BLEnum.LogType logType, string module, string createdBy, string note, SqlConnection conn)
        {
            try
            {
                    string sql = @"insert TBL_LOG (TYPE, MODULE, NOTE, CREATED_BY, CREATED_DATE) 
                                            values(@TYPE, @MODULE, @NOTE, @CREATED_BY, getdate())";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlParameterCollection param = cmd.Parameters;
                    param.Clear();

                    AddSQLParam(param, "@TYPE", logType.ToString());
                    AddSQLParam(param, "@MODULE", module);
                    AddSQLParam(param, "@note", note);
                    AddSQLParam(param, "@CREATED_BY", createdBy);
                    cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        protected void AddCreateUpdate(SqlParameterCollection @params)
        {
            AddSQLParam(@params, "@created_by", Login.User.UserFullName);
            AddSQLParam(@params, "@created_date", DateTime.Now);
            AddSQLParam(@params, "@updated_by", Login.User.UserFullName);
            AddSQLParam(@params, "@updated_date", DateTime.Now);
        }

    }

}

