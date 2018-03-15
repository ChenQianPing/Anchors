using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Collections.Specialized;

namespace wfEnginer
{
    public class wfRun
    {
        private IDbConnection conn = null;
        private string connStr = "";
        private string mUrl = "";
        private string mKeyField = "";
        private string mTableName = "";
        private NameValueCollection FileNames = new NameValueCollection();
        private StringCollection mSkiptoList = new StringCollection();
        private string mEntityName = "";
        private string mEntityType = "";
        /// <summary>
        /// 启动审批流程
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="processID">流程ID</param>
        /// <param name="SerialID">序列号ID</param>
        /// <param name="Desc">说明</param>
        /// <returns></returns>
        public int StartProcess(string UserID, string processID, string EntityID,string Desc)
        {
            try
            {
                //检查流程状态
                int tstatus = CheckProcState(processID, EntityID);
                if ((tstatus == 0) || (tstatus == 1) || (tstatus == 2) || (tstatus == 3))
                    return 1;                                       //流程已启动或已结束或被取消　
                //检查流程信息
                FlowChart fw = GetProcessData(processID);
                wfActivity act = fw.StartNode;
                string TaskID;
                dbData dd = new dbData();
                dd.ConnectionStr = connStr;
                TaskID = dd.GetSEQ("SEQ_TODO_LIST");
                //检查用户权限
                //添加select * from  WF_TODO_TASK_LIST  //待办
                //添加SELECT F_INSTANCE_ID,F_PROCESS_ID,F_ENTITY_ID,F_STATUS,F_DATE,F_START_DATE FROM WF_TASK_LIST　　　//状态索引表
                //添加WF_HAVE_DOWN_TASKS  //已结束步骤
                //分配下面的流程活动
                return procActivity(UserID, fw, act, TaskID, EntityID, wfConsts.RUN_STATE_ACCEPT, Desc);
            }
            catch (Exception e)
            {
                wfLogs.Logs(connStr, 1, UserID, "wfEngine.StartProcess", e.Message);
                throw;
            }
            finally
            {
                wfLogs.Logs(connStr, 0, UserID, "wfEngine.DoActivity", "操作:ProcessID=" + processID);
            }
        }

        /// <summary>
        /// 执行流程活动
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="TaskID">任务ID</param>
        /// <param name="EntityID">实体ID</param>
        /// <param name="ActID">要执行的活动ID</param>
        /// <param name="Status">审批状态</param>
        /// <param name="Desc">审批批示</param>
        /// <returns></returns>
        public int DoActivity(string UserID, string todoID, string ActID, string EntityID, string Status, string Desc)
        {
            //int rc = 0;
            string processID = "";
            try
            {
                processID = GetTodoProcessID(todoID);          //取流程ID
                if (processID == "") return -1;
                string TaskID = getTaskID(todoID);                      //取实例ID
                if (TaskID == "") return -1;

                //检查流程状态
                int tstatus = CheckProcState(processID, EntityID);
                if ((tstatus != 2))
                    return 1;                                       //流程已启动或已结束
                //检查流程信息
                FlowChart fw = GetProcessData(processID);
                wfActivity act = fw.GetActivityByID(ActID);
                act.InstanceID = TaskID;
                act.TodoID = todoID;

                if (actProced(todoID)) return -1;
                //检查用户权限
                //更新＼添加select * from  WF_TODO_TASK_LIST  //待办
                //添加WF_HAVE_DOWN_TASKS  //已结束步骤
                //分配下面的流程活动
                //如果流程结束　删除todo
                //更新标记SELECT F_INSTANCE_ID,F_PROCESS_ID,F_ENTITY_ID,F_STATUS,F_DATE,F_START_DATE FROM WF_TASK_LIST　　　//状态索引表
                return procActivity(UserID, fw, act, TaskID, EntityID, Status, Desc);
            }
            catch (Exception e)
            {
                wfLogs.Logs(connStr, 1, UserID, "wfEngine.DoActivity", e.Message);
                throw;
            }
            finally
            {
                wfLogs.Logs(connStr, 0, UserID, "wfEngine.DoActivity", "操作:ProcessID=" + processID + ",todoID=" + todoID);
            }
        }

        /// <summary>
        /// 检测任务是否是开始结点，如果是则禁止再回退操作
        /// </summary>
        /// <param name="processID">流程ID</param>
        /// <param name="ActID">节点ID</param>
        /// <returns></returns>
        public bool IsStartNode(string processID, string ActID)
        {
            string sSQL = "select * from WF_ACTIVITY where F_PROCESS_ID=" + processID + " AND F_ID=" + ActID + " AND F_ACT_TYPE='开始'";
            IDataReader dr = dbData.getDataReader(connStr, sSQL);
            if (dr == null) return true;
            if (dr.Read()) return true;
            return false;
        }


        public string ConnectionStr
        {
            get { return connStr; }
            set { connStr = value; }
        }

        /// <summary>
        /// 检查是否被执行了
        /// </summary>
        /// <param name="todoID"></param>
        /// <returns></returns>
        private bool actProced(string todoID)
        {
            string sSQL = "select F_TASK_STATUS from WF_TODO_TASK_LIST where F_SERIAL_NO=" + todoID;
            IDataReader dr = dbData.getDataReader(connStr, sSQL);
            if (dr != null && dr.Read())
            {
                return (dr.GetString(0) == wfConsts.TASK_STATUS_END);
            }

            return false;
        }

        /// <summary>
        /// 分配下面的流程活动
        /// </summary>
        /// <param name="UserCode">用户代码</param>
        /// <param name="wf">流程类</param>
        /// <param name="act">当前节点</param>
        /// <param name="TaskID">任务ID</param>
        /// <param name="EntityID">实体ID</param>
        /// <param name="Status">状态</param>
        /// <param name="Desc">说明</param>
        /// <returns></returns>
        private int procActivity(string UserCode, FlowChart wf, wfActivity act, string TaskID, string EntityID, string Status, string Desc)
        {
            int rc = 0;
            if (act==null) return -1;                //无效
            if (!ICanDo(UserCode, act))  return -1;  //无权限

            InitConnection();
            //通过审批状态，如果是通过，找到下一个节点，检查下一节点的前置条件是否充分
            //更新当前节点的状态

            //流程启动时才插入任务
            UpdateTodoStatus(UserCode, wf, act, TaskID, EntityID, Status, Desc);
            AppendFiles(TaskID);     //添加附件
            
            if (Status == wfConsts.RUN_STATE_MANUAL)      //手工跳转
            {
                if (this.mSkiptoList.Count == 0)
                {
                    return -1;
                }
                else
                {
                    doSkipto(UserCode, wf, act, TaskID, EntityID, Status, Desc);
                }
            }
            else if (Status == wfConsts.RUN_STATE_CANEL)      //退回  退回上一步，重启动上一步
            {
                RollbackAct(UserCode, wf, act, TaskID, EntityID, Status, Desc);
            }
            else
            {
                //通过否决  
                int rcCount=CheckAndDoActivity(UserCode, wf, act, TaskID, EntityID, Status, Desc);
                if (rcCount == 0)  //没有使用
                {
                    MoveToHaveDone(TaskID, Status, wfConsts.PROCESS_STATUS_CANCEL, UserCode);
                }
            }
            /*if (Status == wfConsts.RUN_STATE_ACCEPT)  //通过
            {
                int chCount = act.ChildCount;            //后面的活动有几个，应该只有一个，除非当前节点是条件分支判断
                if (chCount > 0)
                {
                    for (int i = 0; i < chCount; i++)
                    {
                        actRule actNext = act.ChildNode(i);
                        if (actNext.Node.ActType == wfConsts.NODE_IF_BRANCH)  //条件分支
                        {
                            //检查条件，决定分支走向
                        }
                        else      //普通节点
                        {
                            //检查每个节点需要的前置条件是否满足
                            //如果前置条件满足
                            if (CheckActPre(TaskID, wf, actNext.Node))
                            {
                                AddTodoStatus(UserCode, act.ID, wf, actNext.Node, TaskID, EntityID, Status, Desc);
                                InsertParentID(act.TodoID, actNext.Node.TodoID,act.ID,actNext.Node.ID);       //添加路径
                            }
                        }
                        
                    }
                }
                else   //没有后续的操作，只好把流程设置为结束状态
                {
                    //移到已完成区域 取消流转
                    MoveToHaveDone(TaskID, Status, wfConsts.PROCESS_STATUS_CANCEL);
                    return 1;
                }
            }
            /*else if (Status == wfConsts.RUN_STATE_REJECT)  //否决 结束流程
            {
                //移到已完成区域 取消流转
                MoveToHaveDone(TaskID, Status, wfConsts.PROCESS_STATUS_CANCEL);
                return 1;

            }*/

            //检查是否已到结束节点
            if (CheckTaskEnd(TaskID, wf.EndNode.ID))
            {
                //移到已完成区域 取消流转
                MoveToHaveDone(TaskID, Status, wfConsts.PROCESS_STATUS_CANCEL,UserCode);
                return 1;
            }

            return rc;
        }

        private bool CheckTaskEnd(string TaskID, string actEndID)
        {
            bool rc = false;
            IDataReader dr = dbData.getDataReader(connStr,"select F_TASK_STATUS from WF_TODO_TASK_LIST "+
                " where  F_TASK_ID="+TaskID+" and F_CURR_ACT_ID="+actEndID);
            if (dr != null)
            {
                if (dr.Read())
                    rc = true;
                dr.Close();
            }

            return rc;
        }
        /// <summary>
        /// 执行手工跳转
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="wf"></param>
        /// <param name="act"></param>
        /// <param name="InstanceID"></param>
        /// <param name="EntityID"></param>
        /// <param name="Status"></param>
        /// <param name="Desc"></param>
        private int doSkipto(string UserCode, FlowChart wf, wfActivity act, string InstanceID, string EntityID, string Status, string Desc)
        {
            int doCount=0;
            for (int i = 0; i < mSkiptoList.Count; i++)
            {
                wfActivity actNext = wf.GetActivityByID(mSkiptoList[i]);
                if (actNext != null)
                {
                    AddTodoStatus(UserCode, act.ID, wf, actNext, InstanceID, EntityID, Status, Desc);
                    InsertParentID(act.TodoID, actNext.TodoID, act.ID, actNext.ID);       //添加路径
                    doCount++;
                }
            }

            return doCount;
        }

        private void AppendFiles(string todoID)
        {
            if (FileNames.Count < 1) return;

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "insert into WF_TASK_DATA (F_ID,F_TASK_ID,F_FILE_NAME,F_FILE_PATH,F_DESC) values " +
                "(? ,? ,? ,? ,?)";

            IDbDataParameter pm = cmd.CreateParameter();
            pm.DbType = DbType.Int64;
            pm.ParameterName = "@F_ID";
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.Int64;
            pm.ParameterName = "@F_TASK_ID";
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.String;
            pm.ParameterName = "@F_FILE_NAME";
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.String;
            pm.ParameterName = "@F_FILE_PATH";
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.String;
            pm.ParameterName = "@F_DESC";
            cmd.Parameters.Add(pm);
            dbData dd =new dbData();
            dd.ConnectionStr = connStr;

            for (int i = 0; i < this.FileNames.Count; i++)
            {
                string fPath = FileNames.Keys[i];
                string fDesc = FileNames[fPath];
                string fName = System.IO.Path.GetFileName(fPath);
                string sID = dd.GetSEQ("SEQ_WF_TASK_DATA");
                pm = (IDbDataParameter)cmd.Parameters[0];
                pm.Value = sID;

                pm = (IDbDataParameter)cmd.Parameters[1];
                pm.Value = todoID;

                pm = (IDbDataParameter)cmd.Parameters[2];
                pm.Value = fName;

                pm = (IDbDataParameter)cmd.Parameters[3];
                pm.Value = fPath;

                pm = (IDbDataParameter)cmd.Parameters[4];
                pm.Value = fDesc;

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 退回操作
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="act"></param>
        private void RollbackAct(string UserCode, FlowChart wf, wfActivity act, string InstanceID,string EntityID,string Status,string Desc)
        {
            string sSQL = "select * from WF_TASK_PATH where F_ACT_INSTANCE=" + act.TodoID;
            IDataReader dr= dbData.getDataReader(connStr, sSQL);
            while (dr.Read())
            {
                string pid = dr.GetValue(1).ToString(); //F_PARENT_ID
                string ptodoid = dr.GetValue(3).ToString(); //F_PARENT_todo ID
                wfActivity cAct = wf.GetActivityByID(pid);
                cAct.TodoID = dr.GetValue(3).ToString(); //F_PARENT_INSTANCE
                cAct.InstanceID = InstanceID;

                if (cAct.ActType == wfConsts.NODE_IF_BRANCH) //判断节点，非人为控制
                {
                    RollbackAct(UserCode, wf, cAct, InstanceID, EntityID, Status, Desc);
                }
                else
                {
                    //将上一节点下来的所有待处理设置为取消
                    sSQL = "update WF_TODO_TASK_LIST set F_TASK_STATUS='" + wfConsts.TASK_STATUS_CANCEL + "',F_ACTION='" + wfConsts.RUN_STATE_CANELED + "',F_GRANTOR_ID='" +
                        UserCode.Replace("'", "''") + "',F_DATE_ACCEPTED=getDate(),F_DESC='被[" + UserCode.Replace("'", "''") + "]退回' where F_SERIAL_NO in (" +
                        "select F_ACT_INSTANCE from WF_TASK_PATH where F_PARENT_INSTANCE=" + ptodoid + " and F_ACT_INSTANCE<>" + act.TodoID + ")";
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sSQL;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    AddTodoStatus(UserCode, act.ID, wf, cAct, InstanceID, EntityID, Status, Desc);
                    InsertParentID(act.TodoID, cAct.TodoID, act.ID, cAct.ID);       //添加路径
                }

            }
        }

        private int CheckAndDoActivity(string UserCode, FlowChart wf, wfActivity act, string InstanceID, string EntityID, string Status, string Desc)
        {
            //检查　act的下一步走向
            //检查是否满足转入条件
            int rc = 0;

            for (int i = 0; i < act.ChildCount; i++)
            {
                actRule actNext = act.ChildNode(i);
                if (actNext.Node.ActType == wfConsts.NODE_IF_BRANCH)  //判断，非流转
                {
                    AppendTodoStatus(UserCode, wf, actNext.Node, InstanceID, EntityID, Status, Desc);
                    InsertParentID(act.TodoID, act.TodoID, act.ID, actNext.Node.ID);       //添加路径
                    rc +=CheckAndDoActivity(UserCode, wf, actNext.Node, InstanceID, EntityID, Status, Desc);
                }
                else
                {
                    if (CheckActPre(InstanceID, wf, act, actNext, Status, EntityID, UserCode))
                    {
                        AddTodoStatus(UserCode, act.ID, wf, actNext.Node, InstanceID, EntityID, Status, Desc);
                        InsertParentID(act.TodoID, actNext.Node.TodoID, act.ID, actNext.Node.ID);       //添加路径
                        rc++;
                    }
                    else
                    {
                        if (actNext.Node.ParentCount > 1 && actNext.Rule == Status)
                            rc++;
                    }
                }
            }
            return rc;
        }

        private bool CheckActPre(string instanceID, FlowChart wf, wfActivity act,actRule Rule,string Status,string EntityID,string UserCode)
        {
            //与聚合：等待所有上级完成
            //或聚合：取消其他未审批活动
            bool pass = CheckRouting(Rule.Rule, Status,instanceID,EntityID,UserCode);   //检查条件是否满足
            if (pass)
            {
                if (Rule.Node.ActType == wfConsts.NODE_MERGE_VOTE && Rule.Node.NumVotesNeeded > 1)                               //投票聚合
                {
                }
                else if (Rule.Node.ActType == wfConsts.NODE_MERGE_AND && Rule.Node.ParentCount > 1)                                //与聚合
                {
                    for (int i = 0; i < Rule.Node.ParentCount; i++)
                    {
                        if (!CheckActWait(instanceID, Rule.Node.ParentNode(i).Node))
                            return false;
                    }

                    return true;
                }
                else if (Rule.Node.ActType == wfConsts.NODE_MERGE_OR && Rule.Node.ParentCount > 1)
                {
                    for (int i = 0; i < Rule.Node.ParentCount; i++)
                    {
                        actRule pr = Rule.Node.ParentNode(i);
                        CancelWait(instanceID, pr.Node);    //取消等待的或
                    }
                }

            }
            else
                return false;
            /*
            if (act.ActType == wfConsts.NODE_MERGE_VOTE && act.NumVotesNeeded>1)                               //投票聚合
            {
            }
            else if (act.ActType == wfConsts.NODE_MERGE_AND && act.ParentCount>1 )                                //与聚合
            {
                for (int i = 0; i < act.ParentCount; i++)
                {
                    if (!CheckActWait(instanceID,act.ParentNode(i).Node))
                        return false;
                }

                return true;
            }
            else if (act.ActType == wfConsts.NODE_MERGE_OR && act.ParentCount > 1)
            {
                for (int i = 0; i < act.ParentCount; i++)
                {
                    actRule pr= act.ParentNode(i);
                    CancelWait(instanceID, pr.Node);    //取消等待的或
                }
            }
            else if (act.ActType == wfConsts.NODE_IF_BRANCH)                                                    //条件分支
            {
            }
            else //if ((act.ActType == wfConsts.NODE_MERGE_OR) || (act.ParentCount < 2) || 
            //((act.NumVotesNeeded<2) && (act.ActType==wfConsts.NODE_MERGE_VOTE)))        //或聚合 或者上层仅一个节点 或者投票只要一个结果 
            {
            }
             * */
            return true;
        }

        private bool CheckRouting(string Rule, string Status, string instanceID, string EntityID, string UserCode)
        {
            //如果为通过，且规则简单匹配，则认为通过 rule=""默认为通过
            if ((Rule == "" && Status == wfConsts.RUN_STATE_ACCEPT) || (Rule == Status))
                return true;
            string tmpstr = Rule.ToLower();
            tmpstr.Replace("\n"," ");
            tmpstr.Replace("\r"," ");
            tmpstr.Replace("\t"," ");
            if (Rule.IndexOf("select ") >= 0)  //判断SQL语句
            {
                string sSQL = ReplaceParams(Rule,instanceID,EntityID,UserCode,Status);
                IDataReader dr = dbData.getDataReader(connStr, sSQL);
                if (dr.Read())    //只判断有返回数据集
                    return true;
                else
                    return false;
            }
            return false;
        }

        private string ReplaceParams(string sSQL,string instanceID,string EntityID,string UserCode,string Status)
        {
            StringBuilder srcb = new StringBuilder();
            const string qtStr=" '\"\t\n\r=";
            for (int i = 0; i < sSQL.Length; i++)
            {
                char ch = sSQL[i];
                if (ch == '#')   //变量名开始
                {
                    string paramName = "";
                    for (int j = i + 1; j < sSQL.Length; j++)
                    {
                        char ch2 = sSQL[j];
                        if (qtStr.IndexOf(ch2) > -1)  //结尾
                        {
                            paramName = sSQL.Substring(i,j-i);
                            i = j;
                            break;
                        }
                    }
                    paramName = paramName.ToLower();
                    if (paramName == "#entityid")
                    {
                        srcb.Append(EntityID);
                    }
                    else if (paramName == "#startusercode")
                    {
                        string sCode = GetStartUserCode(instanceID);
                        srcb.Append(sCode);
                    }
                    else if (paramName == "#usercode")
                    {
                        srcb.Append(UserCode);
                    }
                    else if (paramName == "#status")
                        srcb.Append(Status);
                    else if (paramName == "#startdept")
                    {
                        string sCode = GetStartUserCode(instanceID);
                        srcb.Append(dbData.UserDeptID(connStr, sCode));
                    }
                    else if (paramName == "#dept")
                    {
                        srcb.Append(dbData.UserDeptID(connStr, UserCode));
                    }
                    else if (paramName == "#startdeptparent")
                    {
                        srcb.Append(dbData.UserDeptParentID(connStr, UserCode));
                    }
                }
                else
                    srcb.Append(ch);
            }

            return srcb.ToString();
        }

        private string GetStartUserCode(string instanceID)
        {
            string sSQL = "select A.F_STAFF_ID from WF_TODO_TASK_LIST A,WF_ACTIVITY B " +
                " where A. F_SERIAL_NO=" + instanceID + " and A.F_CURR_ACT_ID= B.F_ID and B.F_ACT_TYPE='" +
                wfConsts.NODE_START + "'";
            IDataReader dr = dbData.getDataReader(connStr, sSQL);
            if (dr.Read())
                return dr.IsDBNull(0) ? "" : dr.GetString(0);
            else
                return "";

        }

        public string Url
        {
            set { mUrl = value; }
            get { return mUrl; }
        }

        public string KeyField
        {
            get { return mKeyField; }
            set { mKeyField = value; }
        }

        public string TableName
        {
            get { return mTableName; }
            set { mTableName = value; }
        }


        private void CancelWait(string InstanceID, wfActivity act)
        {
            string sSQL = "select F_SERIAL_NO,F_TASK_STATUS from  WF_TODO_TASK_LIST where  F_TASK_ID=" + InstanceID +
                " and F_CURR_ACT_ID=" + act.ID;
            IDataReader dr = dbData.getDataReader(connStr, sSQL);
            if (dr != null && dr.Read())
            {
                string sst=dr.GetString(1);
                if (sst == wfConsts.TASK_STATUS_END)
                {
                    return;
                }
                else
                {
                    sSQL = "Update WF_TODO_TASK_LIST set F_TASK_STATUS='"+wfConsts.TASK_STATUS_END+"', F_DESC='或聚合自动取消' "+
                        " where F_SERIAL_NO="+dr.GetValue(0).ToString();
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sSQL;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return;
                }

            }

            for (int i = 0; i < act.ParentCount; i++)
            {
                actRule pr = act.ParentNode(i);
                CancelWait(InstanceID, pr.Node);    //取消等待的或
            }
        }

        private bool CheckActWait(string InstanceID, wfActivity act)
        {
            string sSQL = "select F_TASK_STATUS from  WF_TODO_TASK_LIST where F_TASK_ID="+InstanceID+" and F_CURR_ACT_ID="+act.ID;
            IDataReader dr = dbData.getDataReader(connStr, sSQL);
            if (dr == null) return false;
            if (dr.Read())
            {
                if (dr.GetValue(0).ToString() == wfConsts.TASK_STATUS_WAIT)
                    return false;
            }
            return true;
        }

        private int AppendTodoStatus(string UserCode, FlowChart wf, wfActivity act, string InstanceID, string EntityID, string Status, string Desc)
        {
            //int rc = 0;
            int count;
            string sSQL;

            IDbCommand cmd = conn.CreateCommand();

            string sn;
            dbData dd = new dbData();
            dd.ConnectionStr = connStr;
            sn = dd.GetSEQ("SEQ_WF_TODO_TASK_LIST");
            act.TodoID = sn;
            act.InstanceID = InstanceID;
            //添加待办清单
            //sSQL = "insert into WF_TODO_TASK_LIST (F_TASK_ID, F_SERIAL_NO, F_ENTITY_ID, F_PRE_ACT_ID, F_CURR_ACT_ID, F_STAFF_ID," +
            //    "F_GRANTOR_ID, F_TASK_STATUS, F_DATE_CREATED,F_DATE_ACCEPTED,  F_PROCESS_ID) " +
            //    " values ({0},{1},'{2}',{3},{4},'{5}','{6}','{7}',{8},{9},{10})";
            sSQL = "insert into WF_TODO_TASK_LIST (F_TASK_ID, F_SERIAL_NO, F_ENTITY_ID, F_PRE_ACT_ID, F_CURR_ACT_ID, F_STAFF_ID," +
                "F_GRANTOR_ID, F_TASK_STATUS, F_DATE_CREATED,F_DATE_ACCEPTED, F_ACTION,F_DESC, F_PROCESS_ID,F_ENTITY_NAME) " +
                " values ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) ";
            //sSQL = string.Format(sSQL, InstanceID, sn, EntityID.Replace("'", "''"), "0", act.ID, UserCode.Replace("'", "''"),
            //    UserCode.Replace("'", "''"), wfConsts.TASK_STATUS_END, "getDate()", "getDate()", wf.ID);
            cmd.CommandText = sSQL;
            IDataParameter pm = cmd.CreateParameter();
            pm.DbType = DbType.Int64;
            pm.ParameterName = "@F_TASK_ID";
            pm.Value = InstanceID;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.Int64;
            pm.ParameterName = "@F_SERIAL_NO";
            pm.Value = sn;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.String;
            pm.ParameterName = "@F_ENTITY_ID";
            pm.Value = EntityID;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.Int64;
            pm.ParameterName = "@F_PRE_ACT_ID";
            pm.Value = 0;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.Int64;
            pm.ParameterName = "@F_CURR_ACT_ID";
            pm.Value = act.ID;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.String;
            pm.ParameterName = "@F_STAFF_ID";
            pm.Value = UserCode;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.String;
            pm.ParameterName = "@F_GRANTOR_ID";
            pm.Value = "";
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.String;
            pm.ParameterName = "@F_TASK_STATUS";
            pm.Value = wfConsts.TASK_STATUS_END;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.DateTime;
            pm.ParameterName = "@F_DATE_CREATED";
            pm.Value = DateTime.Now;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.DateTime;
            pm.ParameterName = "@F_DATE_ACCEPTED";
            pm.Value = DateTime.Now;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.String;
            pm.ParameterName = "@F_ACTION";
            pm.Value = Status;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.String;
            pm.ParameterName = "@F_DESC";
            pm.Value = Desc;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.Int64;
            pm.ParameterName = "@F_PROCESS_ID";
            pm.Value = wf.ID;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.DbType = DbType.Int64;
            pm.ParameterName = "@mEntityName";
            pm.Value = wf.ID;
            cmd.Parameters.Add(pm);
            

            count = cmd.ExecuteNonQuery();

            return 0;
        }


        private bool IsExistTask(string InstanceID,string entityID)
        {
            string sSQL = "select * from  WF_TASK_LIST where F_INSTANCE_ID=" + InstanceID + " and F_ENTITY_ID='" + entityID+"'";
            IDataReader dr = dbData.getDataReader(connStr, sSQL);
            if (dr == null) return false;
            if (!dr.Read()) return false;
            return true;
        }

        private int UpdateTodoStatus(string UserCode, FlowChart wf, wfActivity act, string InstanceID, string EntityID, string Status, string Desc)
        {
            //int rc = 0;
            int count;
            string sSQL;

            IDbCommand cmd = conn.CreateCommand();
            //by:ruijc
            //添加了任务判断条件
            if (act.ActType == wfConsts.NODE_START && !IsExistTask(InstanceID, EntityID))   //如果是开始节点，可以认为直接新增Todo 
            {
                //流程执行清单
                //sSQL = "insert into WF_TASK_LIST "+
                //    "(F_INSTANCE_ID,F_PROCESS_ID,F_ENTITY_ID,F_STATUS,F_DATE,F_START_DATE,F_URL,F_KEYFIELD,F_TABLENAME,F_ENTITY_NAME) " +
                //    " values (" + InstanceID + "," + wf.ID + ",'" + EntityID.Replace("'", "''") + "',"+wfConsts.PROCESS_STATUS_PROCESSING
                //    +",getDate(),GetDate(),'"+mUrl.Replace("'","''")+"','"+
                //    mKeyField.Replace("'","''")+"','"+
                //    mTableName.Replace("'","''")+"','"+
                //    mEntityName.Replace("'","''")+"')";
                sSQL = "insert into WF_TASK_LIST "+
                    "(F_INSTANCE_ID,F_PROCESS_ID,F_ENTITY_ID,F_ENTITY_NAME,F_ENTITY_TYPE,F_ASSEMBLED_ID,F_ASSEMBLED_NAME,F_STATUS,F_DATE,F_START_DATE,F_URL,F_KEYFIELD,F_TABLENAME,F_START_USER,F_LAST_USER) " +
                    " values ( " + InstanceID + ", " + wf.ID + ", '" + EntityID + "', '" + mEntityName + "', '" + mEntityType + "',"+
                    "'" + EntityID + "','" + mEntityName + "'," + wfConsts.PROCESS_STATUS_PROCESSING + ", '" + DateTime.Now + "',"+
                    "'" + DateTime.Now + "', '" + mUrl + "', '" + mKeyField + "', '" + mTableName + "', '" + UserCode + "','" + UserCode + "')";
                cmd.CommandText = sSQL;
                
                //IDbDataParameter pm= cmd.CreateParameter();
                //pm.DbType = DbType.Int64;
                //pm.ParameterName = "@F_INSTANCE_ID";
                //pm.Value = InstanceID;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.Int64;
                //pm.ParameterName = "@F_PROCESS_ID";
                //pm.Value = wf.ID;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_ENTITY_ID";
                //pm.Value = EntityID;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_ENTITY_NAME";
                //pm.Value = mEntityName;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_ENTITY_TYPE";
                //pm.Value = mEntityType;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_ASSEMBLED_ID";
                //pm.Value = EntityID;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_ASSEMBLED_NAME";
                //pm.Value = mEntityName;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.Int16;
                //pm.ParameterName = "@F_STATUS";
                //pm.Value = wfConsts.PROCESS_STATUS_PROCESSING;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.DateTime;
                //pm.ParameterName = "@F_DATE";
                //pm.Value = DateTime.Now;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.DateTime;
                //pm.ParameterName = "@F_START_DATE";
                //pm.Value = DateTime.Now;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_URL";
                //pm.Value = mUrl;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_KEYFIELD";
                //pm.Value = mKeyField;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_TABLENAME";
                //pm.Value = mTableName;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_START_USER";
                //pm.Value = UserCode;
                //cmd.Parameters.Add(pm);

                //pm = cmd.CreateParameter();
                //pm.DbType = DbType.String;
                //pm.ParameterName = "@F_LAST_USER";
                //pm.Value = UserCode;
                //cmd.Parameters.Add(pm);

                count = cmd.ExecuteNonQuery();

                AppendTodoStatus(UserCode, wf, act, InstanceID, EntityID, Status, Desc);
                /*string sn;
                dbData dd = new dbData();
                dd.ConnectionStr = connStr;
                sn = dd.GetSEQ("SEQ_WF_TODO_TASK_LIST");
                act.TodoID = sn;
                act.InstanceID = InstanceID;
                //添加待办清单
                //sSQL = "insert into WF_TODO_TASK_LIST (F_TASK_ID, F_SERIAL_NO, F_ENTITY_ID, F_PRE_ACT_ID, F_CURR_ACT_ID, F_STAFF_ID," +
                //    "F_GRANTOR_ID, F_TASK_STATUS, F_DATE_CREATED,F_DATE_ACCEPTED,  F_PROCESS_ID) " +
                //    " values ({0},{1},'{2}',{3},{4},'{5}','{6}','{7}',{8},{9},{10})";
                sSQL = "insert into WF_TODO_TASK_LIST (F_TASK_ID, F_SERIAL_NO, F_ENTITY_ID, F_PRE_ACT_ID, F_CURR_ACT_ID, F_STAFF_ID," +
                    "F_GRANTOR_ID, F_TASK_STATUS, F_DATE_CREATED,F_DATE_ACCEPTED, F_ACTION,F_DESC, F_PROCESS_ID) " +
                    " values ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? ) ";
                //sSQL = string.Format(sSQL, InstanceID, sn, EntityID.Replace("'", "''"), "0", act.ID, UserCode.Replace("'", "''"),
                //    UserCode.Replace("'", "''"), wfConsts.TASK_STATUS_END, "getDate()", "getDate()", wf.ID);
                cmd.CommandText = sSQL;
                IDataParameter pm= cmd.CreateParameter();
                pm.DbType = DbType.Int64;
                pm.ParameterName = "@F_TASK_ID";
                pm.Value = InstanceID;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.Int64;
                pm.ParameterName = "@F_SERIAL_NO";
                pm.Value = sn;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_ENTITY_ID";
                pm.Value = EntityID;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.Int64;
                pm.ParameterName = "@F_PRE_ACT_ID";
                pm.Value = 0;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.Int64;
                pm.ParameterName = "@F_CURR_ACT_ID";
                pm.Value = act.ID;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_STAFF_ID";
                pm.Value = UserCode;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_GRANTOR_ID";
                pm.Value = "";
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_TASK_STATUS";
                pm.Value = wfConsts.TASK_STATUS_END;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.DateTime;
                pm.ParameterName = "@F_DATE_CREATED";
                pm.Value = DateTime.Now;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.DateTime;
                pm.ParameterName = "@F_DATE_ACCEPTED";
                pm.Value = DateTime.Now;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_ACTION";
                pm.Value = Status;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_DESC";
                pm.Value = Desc;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.Int64;
                pm.ParameterName = "@F_PROCESS_ID";
                pm.Value = wf.ID;
                cmd.Parameters.Add(pm);

                count = cmd.ExecuteNonQuery();*/
            }
            else                      //正在执行
            {
                //更新待办清单
                //cmd.CommandText = "Update WF_TODO_TASK_LIST set F_TASK_STATUS='" +wfConsts.TASK_STATUS_END +
                //    "',F_DATE_ACCEPTED=getDate() ,F_ACTION='" + Status + "',F_GRANTOR_ID='"+UserCode.Replace("'","''")+"' " +
                //    " where F_SERIAL_NO=" +
                //    act.TodoID;
                cmd.CommandText = "Update WF_TODO_TASK_LIST set F_TASK_STATUS= ? ,F_DATE_ACCEPTED= ? ,F_ACTION= ? ,F_GRANTOR_ID= ?,F_DESC= ? "+
                    " where F_SERIAL_NO=" +act.TodoID;
                IDataParameter pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_TASK_STATUS";
                pm.Value = wfConsts.TASK_STATUS_END;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.DateTime;
                pm.ParameterName = "@F_DATE_ACCEPTED";
                pm.Value = DateTime.Now;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_ACTION";
                pm.Value = Status;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_GRANTOR_ID";
                pm.Value = UserCode;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.DbType = DbType.String;
                pm.ParameterName = "@F_DESC";
                pm.Value = Desc;
                cmd.Parameters.Add(pm);

                count = cmd.ExecuteNonQuery();
            }
            
            return 0;
        }

        private void InsertParentID(string ParentID, string CurrID, string ParentActID, string CurrActID)
        {
            string sSQL = "insert into WF_TASK_PATH (F_ACT_INSTANCE,F_ACT_ID,F_PARENT_INSTANCE,F_PARENT_ID,F_DATE) values (" +
                CurrID + "," + CurrActID + "," + ParentID +","+ ParentActID +  ",getDate())";
            IDbCommand cmd = conn.CreateCommand();   
            cmd.CommandText = sSQL;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        private int AddTodoStatus(string UserCode,string PreActID, FlowChart wf, wfActivity act, string InstanceID, string EntityID, string Status, string Desc)
        {
            //int rc = 0;
            int count;
            string sSQL;

            IDbCommand cmd = conn.CreateCommand();

            //流程执行清单 更新时间
            sSQL = "update WF_TASK_LIST set F_DATE=GetDate(),F_LAST_USER='"+UserCode.Replace("'","''")+"' " +
                " where  F_INSTANCE_ID=" + InstanceID;
            cmd.CommandText = sSQL;
            count = cmd.ExecuteNonQuery();

            string sn;
            dbData dd = new dbData();
            dd.ConnectionStr = connStr;
            sn = dd.GetSEQ("SEQ_WF_TODO_TASK_LIST");
            act.TodoID = sn;
            act.InstanceID = InstanceID;
            //添加待办清单
            sSQL = "insert into WF_TODO_TASK_LIST (F_TASK_ID, F_SERIAL_NO, F_ENTITY_ID, F_PRE_ACT_ID, F_CURR_ACT_ID, F_STAFF_ID," +
                "F_GRANTOR_ID, F_TASK_STATUS, F_DATE_CREATED,  F_PROCESS_ID,F_URL ,F_KEYFIELD,F_TABLENAME,F_ENTITY_NAME) " +
                " values ({0},{1},'{2}',{3},{4},'{5}','{6}','{7}',{8},{9},'{10}','{11}','{12}','{13}')";
            if (act.ActType==wfConsts.NODE_END) //结束流程
                sSQL = string.Format(sSQL, InstanceID, sn, EntityID.Replace("'", "''"), PreActID, act.ID, UserCode.Replace("'", "''"),
                    "", wfConsts.TASK_STATUS_END, "getDate()", wf.ID,mUrl.Replace("'","''"),mKeyField.Replace("'","''"),
                    mTableName.Replace("'", "''"), mEntityName.Replace("'","''"));
            else
                sSQL = string.Format(sSQL, InstanceID, sn, EntityID.Replace("'", "''"), PreActID, act.ID, UserCode.Replace("'", "''"),
                    "", wfConsts.TASK_STATUS_WAIT, "getDate()", wf.ID, mUrl.Replace("'", "''"),
                    mKeyField.Replace("'", "''"), mTableName.Replace("'", "''"), mEntityName.Replace("'", "''"));
            cmd.CommandText = sSQL;
            count = cmd.ExecuteNonQuery();

            if (act.ActType == wfConsts.NODE_END)
            {
                //移到已完成区域
                MoveToHaveDone(InstanceID,Status,wfConsts.PROCESS_STATUS_FINISHED,UserCode);
            }

            if (!MoveToHaveDoneSuccess(InstanceID))
            {
                //删除余留的数据
                sSQL = "delete from WF_TODO_TASK_LIST where F_TASK_ID=" + InstanceID;
                cmd.CommandText = sSQL;
                cmd.ExecuteNonQuery();
            }
            return 0;
        }

        //检测是否有未移成功的数据
        private bool MoveToHaveDoneSuccess(string InstanceID)
        {
            string sSQL = "SELECT * FROM WF_HAVE_DONE_TASKS WHERE F_TASK_ID=" + InstanceID;
            IDataReader dr = dbData.getDataReader(connStr, sSQL);
            if (dr != null && dr.Read())
            {
                return false;
            }
            return true;
        }

        private void MoveToHaveDone(string InstanceID,string Status,string state,string UserCode)
        {
            //向HaveDone表插入数据
            string sSQL = "INSERT WF_HAVE_DONE_TASKS " +
                "SELECT F_TASK_ID,F_SERIAL_NO,F_PROCESS_ID,F_ENTITY_ID,F_PRE_ACT_ID,F_CURR_ACT_ID " +
                ",F_STAFF_ID,F_GRANTOR_ID,F_TASK_STATUS,F_DATE_CREATED,F_DATE_ACCEPTED," +
                "getDate() as F_DATE_COMPLETED,F_ACTION,F_DESC,F_URL,F_KEYFIELD,F_TABLENAME,F_ENTITY_NAME " +
                " FROM WF_TODO_TASK_LIST where F_TASK_ID=" + InstanceID;
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sSQL;
            cmd.ExecuteNonQuery();

            //删除todo list
            sSQL = "delete from WF_TODO_TASK_LIST where F_TASK_ID=" + InstanceID;
            cmd.CommandText = sSQL;
            cmd.ExecuteNonQuery();

            //更新list
            sSQL = "update WF_TASK_LIST set F_STATUS=" + state + ",F_LAST_USER='"+UserCode.Replace("'","''")+"' where F_INSTANCE_ID=" + InstanceID;
            cmd.CommandText = sSQL;
            cmd.ExecuteNonQuery();

            cmd.Dispose();
        }

        /// <summary>
        /// 检测用户是否被授权执行操作
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        private bool ICanDo(string UserCode, wfActivity act)
        {
            wfCheck check = new wfCheck();
            check.ConnectionStr = connStr;
            if (check.CheckAssgnUser(act.ID, UserCode))   //检测用户或角色权限
                return true;

            string sSQL;
                //= "SELECT * FROM WF_ASSGN_RULE where F_ACT_ID=" + act.ID + " and F_USER_ID='" + UserCode.Replace("'", "''") + "'"+
                //" and F_BASED_ON='"+wfConsts.ASSIGN_TYPE_USER+"'";
            IDataReader dr;// = dbData.getDataReader(connStr, sSQL);
            //if (dr != null && dr.Read())
            //{
            //    dr.Close();
            //    return true;
           // }
            //else   //检查Todo里是否被单独授权
            //{
                sSQL = "select * from WF_TODO_TASK_LIST where F_CURR_ACT_ID=" + act.ID + " and F_GRANTOR_ID='" + UserCode.Replace("'", "''") + "'";
                dr = dbData.getDataReader(connStr, sSQL);
                if (dr != null && dr.Read())
                {
                    return true;
                }
            //}

            return false;
        }

        private string GetTodoProcessID(string todoID)
        {
            string sSQL = "select F_PROCESS_ID from WF_TODO_TASK_LIST where F_SERIAL_NO=" + todoID;
            IDataReader dr = dbData.getDataReader(connStr, sSQL);
            string rc="";
            if (dr != null && dr.Read())
            {
                rc = dr.GetValue(0).ToString();

                dr.Close();
            }

            return rc;
        }

        private string getTaskID(string todoID)
        {
            string sSQL = "select F_TASK_ID from WF_TODO_TASK_LIST where F_SERIAL_NO=" + todoID;
            IDataReader dr = dbData.getDataReader(connStr, sSQL);
            string rc = "";
            if (dr != null && dr.Read())
            {
                rc = dr.GetValue(0).ToString();

                dr.Close();
            }

            return rc;
        }

        private int CheckProcState(string processID, string EntityID)
        {
            wfCheck check = new wfCheck();
            check.ConnectionStr = connStr;
            return check.CheckTaskStatus(processID, EntityID);
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
            conn.ConnectionString = connStr;
            conn.Open();
            return conn.State == ConnectionState.Open;
        }

        /// <summary>
        /// 获取流程信息树
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        private FlowChart GetProcessData(string processID)
        {
            wfProcessStor ps = new wfProcessStor();
            ps.ConnectionStr = connStr;
            return ps.GetProcess(processID);
        }

        /// <summary>
        /// 从配置文件读连接串
        /// </summary>
        /// <returns></returns>
        private string GetConnString()
        {
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
        
        /// <summary>
        /// 添加附件路径
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Desc"></param>
        public void AddFileName(string filename,string Desc)
        {
            this.FileNames.Add(filename,Desc);
        }

        /// <summary>
        /// 添加到跳转清单
        /// </summary>
        /// <param name="aActID"></param>
        public void AddSkipto(string aActID)
        {
            mSkiptoList.Add(aActID);
        }

        public string EntityName
        {
            get { return mEntityName; }
            set { mEntityName = value; }
        }

        public string EntityType
        {
            get { return mEntityType; }
            set { mEntityType = value; }
        }
    }
}
