using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace wfEnginer
{
    class wfLogs
    {
        public static void Logs(string ConnStr,int hasErr,string UserCode,string option,string ErrTxt)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = ConnStr;
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into WF_LOGS ( F_ID, F_TIME, F_OPTION, F_USER, F_ERROR, F_ERRDESC) " +
                    "values (? , ?, ?, ?, ?, ?)";
                
                OleDbParameter pm= cmd.CreateParameter();
                pm.DbType = DbType.Int64;
                string iid;
                dbData dd = new dbData();
                dd.ConnectionStr = ConnStr;
                iid =dd.GetSEQ("SEQ_WF_LOG");
                dd = null;
                pm.ParameterName = "@F_ID";
                pm.Value = iid;
                cmd.Parameters.Add(pm);


                pm= cmd.CreateParameter();
                pm.ParameterName = "@F_TIME";
                pm.DbType = DbType.DateTime;
                pm.Value = DateTime.Now;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_OPTION";
                pm.DbType = DbType.String;
                pm.Value = option;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_USER";
                pm.DbType = DbType.String;
                pm.Value = UserCode;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_ERROR";
                pm.DbType = DbType.Int16;
                pm.Value = hasErr;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_ERRDESC";
                pm.DbType = DbType.String;
                pm.Value = ErrTxt;
                cmd.Parameters.Add(pm);

                cmd.ExecuteNonQuery();

                cmd.Dispose();

                conn.Close();
            }
            catch (Exception e)
            {
                //ºöÂÔ
            }
        }
    }
}
