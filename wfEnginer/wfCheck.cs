using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace wfEnginer
{
    public class wfCheck
    {
        private string ConnStr;

        public string ConnectionStr
        {
            get { return ConnStr; }
            set { ConnStr = value; }
        }

        /// <summary>
        /// ��ȡ�û���������
        /// </summary>
        /// <param name="UserCode">��ǰ�û�����</param>
        /// <returns></returns>
        public TodoList GetTodoList(string UserCode)
        {
            TodoList rc = new TodoList();
            string sSQL = "select  F_TASK_ID, F_SERIAL_NO, F_PROCESS_ID, F_ENTITY_ID, F_PRE_ACT_ID, F_CURR_ACT_ID, "+
                " F_STAFF_ID, F_GRANTOR_ID, F_TASK_STATUS,"+
                " F_DATE_CREATED, F_DATE_ACCEPTED, F_ACTION, F_DESC, F_URL, F_KEYFIELD,F_TABLENAME,F_ENTITY_NAME "+
                " from  WF_TODO_TASK_LIST where F_TASK_STATUS='" + wfConsts.TASK_STATUS_WAIT + "'";
            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            while (dr.Read())
            {
                string todoid = dr.GetValue(1).ToString();
                string actID = dr.GetValue(5).ToString();
                if (CheckAssgnUser(actID, UserCode))
                {
                    todoItem item = new todoItem();
                    rc.Add(item);
                    item.ActID = dr.GetValue(5).ToString();
                    item.Desc = (dr.IsDBNull(12) ? "" : dr.GetString(12));
                    item.EntityID = dr.GetString(3);
                    item.PreActID = dr.GetValue(4).ToString();
                    item.FromName = dbData.ActName(ConnStr, item.PreActID);
                    //item.FromTodoID =
                    item.FromUserCode = dr.GetString(6);
                    item.FromUser = dbData.UserNameFromUserCode(ConnStr, item.FromUserCode);
                    item.KeyField = dr.IsDBNull(14) ? "" : dr.GetString(14);
                    item.Name = dbData.ActName(ConnStr, item.ActID);
                    //item.PreAction = dbData.GetFieldStr(ConnStr, "select  F_ACTION from WF_TODO_TASK_LIST where F_PRE);
                    item.ProcessID = dr.GetValue(2).ToString();
                    item.ProcessName = dbData.GetProcessName(ConnStr, item.ProcessID);
                    item.SendTime = dr.GetDateTime(9);
                    item.TableName =(dr.IsDBNull(15) ? "": dr.GetString(15));
                    item.TaskID = dr.GetValue(0).ToString();
                    //item.TimeAllow =
                    item.TodoID = dr.GetValue(1).ToString();
                    item.Url = (dr.IsDBNull(13) ? "": dr.GetString(13));
                    item.EntityName = (dr.IsDBNull(16) ? "" : dr.GetString(16));
                    
                }
            }

            return rc;

        }


        /// <summary>
        /// �������״̬
        /// </summary>
        /// <param name="ProcessID">����ID</param>
        /// <param name="EntityID">ҵ������ID</param>
        /// <returns>0:δ��ʼ��1:�ѽ�����2:���ڽ���,-1:������޷�ȷ��</returns>
        public int CheckTaskStatus(string ProcessID, string EntityID)
        {
            int rc = -1; 
           
            string sSQL = "SELECT F_STATUS FROM WF_TASK_LIST " +
                " where F_PROCESS_ID=" + ProcessID + " and F_ENTITY_ID='" + EntityID.Replace("'", "''") + "'";

            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            if (dr != null)
            {
                if (dr.Read())
                {
                    rc = dr.GetInt16(0);
                }

                dr.Close();
            }

            return rc;
        }

        /// <summary>
        /// ����������״̬
        /// </summary>
        /// <param name="ProcessID">����ID</param>
        /// <param name="EntityID">ҵ������ID</param>
        /// <returns>0:δ��ʼ��1:�ѽ�����2:���ڽ���,-1:������޷�ȷ��</returns>
        public int CheckAssembledTaskStatus(string ProcessID, string F_ASSEMBLED_ID)
        {
            int rc = -1;

            string sSQL = "SELECT F_STATUS FROM WF_TASK_LIST " +
                " where F_PROCESS_ID=" + ProcessID + " and F_ASSEMBLED_ID='" + F_ASSEMBLED_ID.Replace("'", "''") + "' GROUP BY F_STATUS";

            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            if (dr != null)
            {
                if (dr.Read())
                {
                    rc = dr.GetInt16(0);
                }

                dr.Close();
            }

            return rc;
        }

        /// <summary>
        /// �û��Ƿ����������������
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="EntityID"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public wfActivity GetNextTask(string processID, string EntityID, string UserCode)
        {
            int taskStatus = CheckTaskStatus(processID, EntityID);
            wfActivity rc = null;
            if (taskStatus == 0)     //δ��ʼ������û��Ƿ����������̵�Ȩ��
            {
                rc = GetAndCheckStart(processID, UserCode);
            }
            else if (taskStatus==2)    //���ڽ��У����Todolist
            {
                rc = GetAndCheckTodoList(processID,EntityID, UserCode);
            }

            return rc;
        }

        public ProjectList GetProjectList(string UserCode)
        {
            ProjectList rc = new ProjectList();
            string sSQL = "select F_ID,F_NAME,F_DESC from WF_PROCESS";
            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            if (dr != null)
            {
                while (dr.Read())
                {
                    string sID = dr.GetValue(0).ToString();
                    string actID = GetProcessStartID(sID);
                    if (actID != "")
                    {
                        //if (CheckCanStartProcess(UserCode, actID))
                        if (CheckAssgnUser(actID, UserCode))
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

        public ProjectList GetProjectList()
        {
            ProjectList rc = new ProjectList();
            string sSQL = "select F_ID,F_NAME,F_DESC from WF_PROCESS";
            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            if (dr != null)
            {
                while (dr.Read())
                {
                    string sID = dr.GetValue(0).ToString();
                    ProjectItem pjt = new ProjectItem();
                    pjt.ID = sID;
                    pjt.Name = dr.GetString(1);
                    pjt.Desc = dr.GetString(2);
                    rc.Add(pjt);
                }
            }
            return rc;
        }

        /// <summary>
        /// ���Ȩ�޲����ؿ�ʼ�ڵ�
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        private wfActivity GetAndCheckStart(string processID, string UserCode)
        {
            string ActID = GetProcessStartID(processID);

            if (ActID!="")
            {
                if (CheckAssgnUser(ActID, UserCode))   //���ڵ��Ƿ������û�����
                {
                    return ResultActByID(ActID,"","");
                }
            }
            return null;
        }

        private wfActivity GetAndCheckTodoList(string processID, string EntityID, string UserCode)
        {
            string sSQL = "select * from WF_TASK_LIST where F_PROCESS_ID=" + processID + " and F_ENTITY_ID='" +
                EntityID.Replace("'", "''") + "' and F_STATUS=" + wfConsts.PROCESS_STATUS_PROCESSING;
            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            if (dr != null && dr.Read())
            {
                string instanceID = dr.GetValue(0).ToString();   //��ȡʵ��ID
                dr.Close();
                dr = null;
                sSQL ="SELECT F_TASK_ID, F_SERIAL_NO, F_ENTITY_ID, F_PRE_ACT_ID, F_CURR_ACT_ID, "+
                    "F_STAFF_ID, F_GRANTOR_ID, F_TASK_STATUS, F_DATE_CREATED,F_DATE_ACCEPTED "+
                    " FROM WF_TODO_TASK_LIST where F_TASK_ID="+instanceID+" and F_TASK_STATUS='"+
                    wfConsts.TASK_STATUS_WAIT+"'";
                dr = dbData.getDataReader(ConnStr, sSQL);
                if (dr != null)
                {
                    while (dr.Read())      //ѭ����ȡ�������Ȩ��
                    {
                        string actID = dr.GetValue(4).ToString();
                        string todoID = dr.GetValue(1).ToString();
                        if (CheckAssgnUser(actID, UserCode))   //���ڵ��Ƿ������û�����
                        {
                            dr.Close();
                            dr = null;
                            return ResultActByID(actID, instanceID,todoID);
                        }

                    }

                    dr.Close();
                    dr = null;
                }
            }
            return null;
        }

        
        /// <summary>
        /// ȡ������ʼ�ڵ�ID
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        private string GetProcessStartID(string processID)
        {
            string sSQL = "select F_ID from WF_ACTIVITY where F_PROCESS_ID=" + processID + " and F_ACT_TYPE='" + wfConsts.NODE_START+"'";
            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            string rc = "";
            if (dr!=null)
            {
                if (dr.Read())
                {
                    rc= dr.GetValue(0).ToString();
                }
                dr.Close();
            }

            return rc;
        }
     
        /// <summary>
        /// ����ڵ��Ƿ���Ȩ�û�
        /// </summary>
        /// <param name="actID"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public bool CheckAssgnUser(string actID,string UserCode)
        {
            bool rc = false;
            string sSQL = "select * from WF_ASSGN_RULE where F_ACT_ID = " + actID + 
                " and F_USER_ID='" + UserCode.Replace("'", "''")+"' and F_BASED_ON='"+wfConsts.ASSIGN_TYPE_USER+"'";
            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            if ((dr!=null) && dr.Read())
            {
                rc = true;
                dr.Close();
            }

            if (!rc)
            {
                sSQL = "select A.F_ACT_ID,A.F_USER_ID from WF_ASSGN_RULE A, " +
                "(select distinct A.F_ROLE_ID " +
                " from  T_ACCOUNTS_ROLE_User A,T_ACCOUNTS_USER_INFO B " +
                " where A.F_USER_ID=B.F_USER_ID " +
                " and B.F_USER_CODE='" + UserCode + "') B " +
                " where A.F_BASED_ON ='" + wfConsts.ASSIGN_TYPE_ROLE + "' and A.F_USER_ID = B.F_ROLE_ID "+
                " and A.F_ACT_ID="+actID;
                dr = dbData.getDataReader(ConnStr, sSQL);
                if ((dr != null) && dr.Read())
                {
                    rc = true;
                    dr.Close();
                }

            }

            return rc;
        }

        /// <summary>
        /// ���ض���
        /// </summary>
        /// <param name="actID"></param>
        /// <param name="instanceID"></param>
        /// <param name="todoID"></param>
        /// <returns></returns>
        private wfActivity ResultActByID(string actID,string instanceID,string todoID)
        {
            string sSQL = "select  F_ID, F_PROCESS_ID, F_NAME, F_TIME_ALLOWED, F_RULE_APPLIED, "+
                "F_EX_PRE_RULE_FUNC, F_EX_POST_RULE_FUNC, F_ACT_TYPE,"+ 
                " F_OR_MERGE_FLAG, F_NUM_VOTE_NEEDED, F_AUTO_EXEC, F_ACT_DESC "+
                "from WF_ACTIVITY where F_ID=" + actID;
            IDataReader dr = dbData.getDataReader(ConnStr, sSQL);
            if ((dr != null) && dr.Read())
            {
                wfActivity act = new wfActivity();
                act.ID = actID;
                act.Name = dr.GetString(2);
                act.TimeAllowed = dr.GetInt32(3);
                act.ActType = dr.GetString(7);
                act.Desc = dr.GetString(11);
                act.InstanceID = instanceID;
                act.TodoID = todoID;

                dr.Close();

                return act;
            }


            return null;
        }
    }
}

