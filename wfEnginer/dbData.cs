using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Configuration;

namespace wfEnginer
{
    public class dbData
    {
        private IDbConnection conn = null;
        private string connStr = "";

        public dbData()
        {
        }

        ~dbData()
        {
            if (conn != null)
            {
                conn = null;
            }
        }

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="sCmd"></param>
        /// <returns></returns>
        public IDataReader getData(string sCmd)
        {
            if (!InitConnection()) return null;
            //////
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sCmd;
            IDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        public static IDataReader getDataReader(string connStr, string sCmd)
        {
            IDbConnection conn = new OleDbConnection();
            
            if (connStr == "") return null;
            conn.ConnectionString = connStr;

            conn.Open();
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sCmd;
            IDataReader dr = cmd.ExecuteReader();

            return dr;
            

        }

        public static string UserNameFromUserCode(string connStr,string UserCode)
        {
            string rc =UserCode;
            IDbConnection conn = new OleDbConnection();
            
            if (connStr == "") return null;
            conn.ConnectionString = connStr;
            conn.Open();
            IDbCommand cmd = conn.CreateCommand();
            //cmd.CommandText = "select F_DISPLAY_NAME from T_ACCOUNTS_USER_INFO where F_USER_CODE='"+UserCode.Replace("'","''")+"'";
            cmd.CommandText = "select F_USER_NAME from T_ACCOUNTS_USER_INFO where F_USER_CODE='" + UserCode.Replace("'", "''") + "'";
            IDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                rc = dr.GetString(0);
            }
            dr.Close();
            cmd.Dispose();
            conn.Dispose();

            return rc;
        }

        public static string UserDeptID(string connStr, string UserCode)
        {
            string rc = UserCode;
            IDbConnection conn = new OleDbConnection();

            if (connStr == "") return null;
            conn.ConnectionString = connStr;
            conn.Open();
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select A.F_DEPT_ID FROM T_ACCOUNTS_DEPT_USER A,T_ACCOUNTS_USER_INFO B where A.F_USER_ID=B.F_USER_ID and B.F_USER_CODE='" + 
                UserCode.Replace("'", "''") + "'";
            IDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                rc = dr.GetString(0);
            }
            dr.Close();
            cmd.Dispose();
            conn.Dispose();

            return rc;
        }

        public static string UserDeptParentID(string connStr, string UserCode)
        {
            string rc = UserCode;
            IDbConnection conn = new OleDbConnection();

            if (connStr == "") return null;
            conn.ConnectionString = connStr;
            conn.Open();
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select A..F_Parent_DeptID FROM T_ACCOUNTS_DEPT_USER A,T_ACCOUNTS_USER_INFO B where A.F_USER_ID=B.F_USER_ID and B.F_USER_CODE='" +
                UserCode.Replace("'", "''") + "'";
            IDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                rc = dr.GetString(0);
            }
            dr.Close();
            cmd.Dispose();
            conn.Dispose();

            return rc;
        }

        public static string RoleNameFromRoleID(string connStr, string RoleID)
        {
            string rc = RoleID;
            IDbConnection conn = new OleDbConnection();

            if (connStr == "") return null;
            conn.ConnectionString = connStr;
            conn.Open();
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select F_ROLE_NAME from T_ACCOUNTS_ROLE where F_ROLE_ID=" + RoleID;
            IDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                rc = dr.GetString(0);
            }
            dr.Close();
            cmd.Dispose();
            conn.Dispose();

            return rc;
        }

        public static string ActName(string connStr, string ActID)
        {
            string rc = ActID;
            IDbConnection conn = new OleDbConnection();
            
            if (connStr == "") return null;
            conn.ConnectionString = connStr;
            conn.Open();
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select F_NAME from WF_ACTIVITY where F_ID="+ActID;
            IDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                rc = dr.GetString(0);
            }
            dr.Close();
            cmd.Dispose();
            conn.Dispose();

            return rc;
        }

        public static string GetProcessName(string connStr, string processID)
        {
            string rc = processID;
            IDbConnection conn = new OleDbConnection();
            
            if (connStr == "") return null;
            conn.ConnectionString = connStr;
            conn.Open();
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select F_NAME from WF_PROCESS where F_ID=" + processID;
            IDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                rc = dr.GetString(0);
            }
            dr.Close();
            cmd.Dispose();
            conn.Dispose();

            return rc;
        }
        

        public string ConnectionStr
        {
            get { return connStr; }
            set { connStr = value; }
        }


        public string GetSEQ(string SEQName)
        {
            Int64 rc = 1;
            bool SeqNew =true;
            string sSQL ="select * from WF_SEQS where F_SEQNAME='"+ SEQName.Replace("'","''")+"'";
            lock (this)
            {
                IDataReader dr = getData(sSQL);
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        rc = Int64.Parse(dr.GetValue(1).ToString()) + Int64.Parse(dr.GetValue(2).ToString());
                        SeqNew = false;
                    }

                    dr.Close();
                    dr = null;
                }

                if (SeqNew)
                {
                    sSQL = "INSERT INTO WF_SEQS (F_SEQNAME,F_SEQ,F_STEP,F_LAST_DATE) VALUES ('" +
                        SEQName.Replace("'", "''") + "',1,1,getdate())";
                }
                else
                {
                    sSQL = "UPDATE WF_SEQS set F_SEQ= F_SEQ+F_STEP,F_LAST_DATE=getdate() where F_SEQNAME='" +
                        SEQName.Replace("'", "''") + "'";
                }

                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sSQL;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            return rc.ToString();
        }
        
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private bool InitConnection()
        {
            if (conn != null) conn.Close();
            conn = null;


            conn = new OleDbConnection();
            string connStr = GetConnString();
            if (connStr == "") return false;
            conn.ConnectionString =connStr;
            conn.Open();
            return conn.State==ConnectionState.Open;
        }

        /// <summary>
        /// 从配置文件读连接串
        /// </summary>
        /// <returns></returns>
        private string GetConnString()
        {
            //return ConfigurationManager.AppSettings["WorkFlowConnString"];
            if (connStr == "")
            {
                const string cfgFile = "wfConfig.xml";

                if (!System.IO.File.Exists(cfgFile)) return "";

                System.Xml.XmlDocument xdoc = new XmlDocument();

                xdoc.Load(cfgFile);

                if (xdoc.DocumentElement != null)
                {

                    XmlNode node = xdoc.DocumentElement.SelectSingleNode("\\\\WorkFlow\\database\\connection");
                    if (node != null)
                        return node.Value;

                }


            }
            else
                return connStr;

            return "";

        }

    }
}
