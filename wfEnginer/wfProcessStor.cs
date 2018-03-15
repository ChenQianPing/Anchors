using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Xml;


namespace wfEnginer
{
    /// <summary>
    /// 从数据库加载
    /// </summary>
    public class wfProcessStor
    {
        private OleDbConnection conn=null;
        private string ConnStr="";
        private string mUserCode;
        private string mRoleCode;
        private string mDeptCode;
        

        /// <summary>
        /// 获取流程活动定义信息列表
        /// </summary>
        /// <param name="ProcessID">流程</param>
        /// <returns></returns>
        public FlowChart GetProcess(string ProcessID)
        {
            if (!InitConnection()) return null;
            //////
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select F_ID,F_NAME,F_DESC,F_CHANGETIME,F_CHANGEUSER from WF_PROCESS where F_ID=" + ProcessID;
            OleDbDataReader dr= cmd.ExecuteReader();

            FlowChart fc = null;
            if (dr.Read())
            {
                fc = new FlowChart();
                fc.ID = ProcessID;
                FlowchartInit(fc,dr);   //从表填充数据  
                //读活动信息
                GetFlowchartActs(fc);
                //构建上级关系
                GetActPreRule(fc);
                //构建下层关系
                GetActRoutingRule(fc);
                //读节点手选节点列表
                GetSelectNodes(fc);
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return fc;
        }

        /// <summary>
        /// 检查任务状态
        /// </summary>
        /// <param name="ProcessID">流程ID</param>
        /// <param name="EntityID">业务数据ID</param>
        /// <returns>0:未开始，1:已结束，2:正在进行,-1:错误或无法确定</returns>
        public int CheckTaskStatus( string ProcessID,string EntityID)
        {
            string sSQL = "SELECT F_STATUS " +
                "FROM WF_TASK_LIST where F_PROCESS_ID=" + ProcessID + " and F_ENTITY_ID='" + EntityID.Replace("'", "''")+"'";
            if (!InitConnection()) return -1;
            //////
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sSQL;
            OleDbDataReader dr = cmd.ExecuteReader();
            int rc = 0;

            if (dr.Read())
            {
                rc= dr.GetInt16(0);
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return rc;
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        public string UserCode
        {
            set { mUserCode = value; }
            get {return  mUserCode;}
        }

        public string DeptCode
        {
            set { mDeptCode = value; }
            get { return mDeptCode; }
        }

        public string RoleCode
        {
            set { mRoleCode = value; }
            get { return mRoleCode; }
        }

        public string ConnectionStr
        {
            get { return ConnStr; }
            set { ConnStr = value; }
        }

        /// <summary>
        /// 获取当前用户可操作活动
        /// </summary>
        /// <param name="processID">流程ID</param>
        /// <param name="EntityID">实体ID</param>
        /// <returns></returns>
        public string GetTodoActivitys(string processID,string EntityID)
        {
            //检测流程是否已启动：如果流程已结束返回无，如果没有启动，检测用户是否
            //被授权开始流程（申报），如果已启动，检测todoList内是否有用户待办
            int taskStatus =CheckTaskStatus(processID,EntityID); //0:未开始，1:已结束，2:正在进行,-1:错误或无法确定
            string rc = "";

            if (taskStatus == 0)  //未开始
            {
                string StartActID = getStartID(processID);
                if ((StartActID != "") && CanStart(processID, StartActID))
                   return processID;
            }
            else if (taskStatus == 2)  //正在进行
            {
                //select * from WF_TODO_TASK_LIST where F_ENTITY_ID= and F_TASK_STATUS='等待处理'
                //如果有数据，检测是否被授权
            }

            return rc;
        }

        private bool CanStart(string processID,string StartActID)
        {
            if (StartActID == "") return false;

            string sSQL = "select * from WF_ASSGN_RULE where F_ACT_ID=" + StartActID + " and  F_USER_ID='" + mUserCode.Replace("'", "''") + "'";

            int rc = 0;
            OleDbDataReader dr = getData(sSQL);
            if (dr!=null)
                if (dr.Read())
                {
                    rc = 1;
                }

            dr.Close();
            dr.Dispose();
            return rc == 1;
        }

        /// <summary>
        /// 取流程开始节点的ID
        /// </summary>
        /// <param name="processID">流程ID</param>
        /// <returns></returns>
        public string getStartID(string processID)
        {
            string sSQL = "select F_ID from WF_ACTIVITY where F_PROCESS_ID=" + processID + " and F_ACT_TYPE='开始'";
            OleDbDataReader dr = getData(sSQL);
            string rc="";

            if (dr != null)
            {
                if (dr.Read())
                {
                    rc = dr.GetValue(0).ToString();
                }
            }

            dr.Close();
            dr.Dispose();
            return rc;
        }

        private OleDbDataReader getData(string sCmd)
        {
            if (!InitConnection()) return null;
            //////
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sCmd;
            OleDbDataReader dr = cmd.ExecuteReader();

            return dr;
        }
        //////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 将数据表中的数据填充到实例
        /// </summary>
        /// <param name="obj">实例对象</param>
        /// <param name="dr">DataReader</param>
        private void FlowchartInit(FlowChart obj,IDataReader dr)
        {
            //select F_ID,F_NAME,F_DESC,F_CHANGETIME,F_CHANGEUSER from WF_PROCESS
            obj.Name = dr.IsDBNull(1) ? "" : dr.GetString(1);
            obj.Desc = dr.IsDBNull(2) ? "" : dr.GetString(2);
            obj.LastChange = dr.IsDBNull(3) ? DateTime.MinValue : dr.GetDateTime(3);
            obj.UserCode = dr.IsDBNull(4) ? "" : dr.GetString(4);
        }

        /// <summary>
        /// 从数据库读流程节点前依赖关系
        /// </summary>
        /// <param name="obj">流程信息</param>
        private void GetActPreRule(FlowChart obj)
        {
            string sCmd = "SELECT F_DEPNT_ID,F_DEPNT_ACT_ID,F_DEPNT_ACT_STATUS,F_PROC_ID " +
                "FROM WF_PRE_RULE where F_PROC_ID=" + obj.ID;
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sCmd;
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string currID = dr.IsDBNull(0) ? "" : dr.GetValue(0).ToString();    //F_DEPNT_ID
                string pID = dr.IsDBNull(1) ? "" : dr.GetValue(1).ToString();       //F_DEPNT_ACT_ID
                string pStatus = dr.IsDBNull(2) ? "" : dr.GetString(2);             //F_DEPNT_ACT_STATUS
                
                //检索列表
                wfActivity currAct = obj.GetActivityByID(currID);
                wfActivity pAct = obj.GetActivityByID(pID);
                if ((currAct != null) && (pAct!=null))
                {
                    currAct.AddParent(pAct,pStatus);
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
        }

        private void GetSelectNodes(FlowChart obj)
        {
            string sCmd = "SELECT F_ACT_ID,F_SELECT_ID " +
                "FROM WF_SELECT_ROUNTING where F_PROCESS_ID=" + obj.ID;
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sCmd;
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string currID = dr.IsDBNull(0) ? "" : dr.GetValue(0).ToString();    //F_ACT_ID
                string sID = dr.IsDBNull(1) ? "" : dr.GetValue(1).ToString();       //F_SELECT_ID

                //检索列表
                wfActivity currAct = obj.GetActivityByID(currID);
                wfActivity sAct = obj.GetActivityByID(sID);
                if ((currAct != null) && (sAct!=null))
                {
                    currAct.SelectNodes.Add(sID);
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
        }

        /// <summary>
        /// 从数据库读流程节点分派关系
        /// </summary>
        /// <param name="obj"></param>
        private void GetActRoutingRule(FlowChart obj)
        {
            string sCmd = "SELECT F_PRE_ACT_ID,F_CURR_ACT_ID,F_COMPLETION_FLAG,F_NEXT_ACT_ID_LIST,F_PRE_DEPNT_SET,F_PROC_ID "+
                "FROM WF_ROUTING_RULE where F_PROC_ID=" + obj.ID;
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sCmd;
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string currID = dr.IsDBNull(0) ? "" : dr.GetValue(0).ToString();    //F_PRE_ACT_ID
                string pID = dr.IsDBNull(1) ? "" : dr.GetValue(1).ToString();       //F_CURR_ACT_ID
                string pStatus = dr.IsDBNull(2) ? "" : dr.GetString(2);             //F_COMPLETION_FLAG

                //检索列表
                wfActivity currAct = obj.GetActivityByID(currID);
                wfActivity pAct = obj.GetActivityByID(pID);
                if ((currAct != null) && (pAct != null))
                {
                    currAct.AddChild(pAct,pStatus);
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
        }

        /// <summary>
        /// 从数据库读流程节点信息
        /// </summary>
        /// <param name="obj"></param>
        private void GetFlowchartActs(FlowChart obj)
        {
            string sCmd = "SELECT F_ID,F_PROCESS_ID,F_NAME,F_TIME_ALLOWED,F_RULE_APPLIED," +
            "F_EX_PRE_RULE_FUNC,F_EX_POST_RULE_FUNC,F_ACT_TYPE,F_OR_MERGE_FLAG,F_NUM_VOTE_NEEDED," +
            "F_AUTO_EXEC,F_ACT_DESC  FROM WF_ACTIVITY where F_PROCESS_ID=" + obj.ID;
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sCmd;
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //数据实例化
                wfActivity newact = new wfActivity();
                newact.ID = dr.GetValue(0).ToString();  //F_ID
                newact.Name = dr.IsDBNull(2) ? "" : dr.GetString(2);          //F_NAME
                newact.TimeAllowed = dr.IsDBNull(3) ? 0: dr.GetInt32(3);  //F_TIME_ALLOWED
                newact.ActType = dr.IsDBNull(7) ? "" : dr.GetString(7);   //F_ACT_TYPE
                newact.OrMergeFlag = dr.IsDBNull(8) ? 0 : (int)dr.GetInt16(8);   //F_OR_MERGE_FLAG
                newact.NumVotesNeeded = dr.IsDBNull(9) ? 0 : (int)dr.GetInt16(9);    //F_NUM_VOTE_NEEDED
                newact.AutoExecutive = dr.IsDBNull(10) ? false : dr.GetInt16(10) == 1;  //F_AUTO_EXEC
                newact.Desc = dr.IsDBNull(11) ? "" : dr.GetString(11); //F_ACT_DESC
                newact.PreRule = dr.IsDBNull(5) ? "" : dr.GetString(5);
                //添加到fchart 活动列表
                obj.AddActivity(newact);

                //读授权信息
                GetActAssgn(newact);
                

            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();

        }

        /// <summary>
        /// 获取活动授权用户信息
        /// </summary>
        /// <param name="act"></param>
        private void GetActAssgn(wfActivity act)
        {
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select  F_BASED_ON,F_USER_ID from  WF_ASSGN_RULE where F_ACT_ID="+act.ID;
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(1))
                {
                    string uid = dr.GetString(1);
                    string utype = dr.GetString(0);
                    string uName = "";
                    if (utype == wfConsts.ASSIGN_TYPE_USER)
                        uName = dbData.UserNameFromUserCode(ConnStr, uid);
                    else
                        uName = dbData.RoleNameFromRoleID(ConnStr, uid);
                    act.UserList.Add(uid, utype, uName);
                }
            }
            
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
            if (ConnStr == "")
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
                return ConnStr;

            return "";

        }

        public ProjectList GetProjectList(string UserCode)
        {
            ProjectList rc = new ProjectList();
            string sSQL = "select F_ID,F_NAME,F_DESC from WF_PROCESS";
            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            wfCheck check = new wfCheck();
            check.ConnectionStr = ConnStr;
            if (dr != null)
            {
                while (dr.Read())
                {
                    string sID = dr.GetValue(0).ToString();
                    string actID = getStartID(sID);
                    if (actID != "")
                    {
                        //if (CheckCanStartProcess(UserCode, actID))
                        if (check.CheckAssgnUser(actID,UserCode))
                        {
                            ProjectItem pjt = new ProjectItem();
                            pjt.ID = sID;
                            pjt.Name = dr.GetString(1);
                            pjt.Desc = dr.GetString(2);
                            rc.Add(pjt);
                        }
                    }
                }
            }
            return rc;
        }

        /// <summary>
        /// 检查用户是否具有执行启动权限
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="sID"></param>
        /// <returns></returns>
        /*private bool CheckCanStartProcess(string UserCode, string actID)
        {
            
            
            string sSQL = "select A.F_ACT_ID,A.F_USER_ID from WF_ASSGN_RULE A "+
                " where A.F_BASED_ON= '"+wfConsts.ASSIGN_TYPE_USER+"'"+
                " and A.F_USER_ID='" + UserCode + "' and A.F_ACT_ID=" + actID;
            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            if (dr != null)
            {
                if (dr.Read())
                {
                    dr.Dispose();
                    return true;
                }
            }

            dr.Dispose();

            //检查角色
            sSQL = "select A.F_ACT_ID,A.F_ROLE_ID from WF_ASSGN_RULE A, " +
                "(select distinct A.F_ROLE_ID " +
                " from  T_ACCOUNTS_ROLE_User A,T_ACCOUNTS_USER_INFO B " +
                " where A.F_USER_ID=B.F_USER_ID " +
                " and B.F_USER_CODE='" + UserCode + "') B " +
                " where A.F_BASED_ON ='" + wfConsts.ASSIGN_TYPE_ROLE + "' and A.F_ROLE_ID = B.F_ROLE_ID and "+
                "A.F_ACT_ID="+actID;

            dr = dbData.getDataReader(ConnStr, sSQL);
            if (dr != null)
            {
                if (dr.Read())
                {
                    dr.Dispose();
                    return true;
                }
            }

            dr.Dispose();
             * 
            //return false;
        }*/
    }
}
