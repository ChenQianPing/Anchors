using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    public class todoItem
    {
        private string mProcessID;
        private string mProcessName;
        private string mTaskID;
        private string mEntityID;
        private string mTodoID;
        private string mFromTodoID;

        private string mActID;
        private string mPreActID;

        private string mName;
        private string mFromName;

        private string mFromUserCode;
        private string mFromUser;
        private DateTime mSendTime;
        private string mDesc;
        private string mUrl;
        private string mKeyField;
        private string mTableName;
        private string mPreAction;
        private int mTimeAllow;
        private string mEntityName;

        public string ProcessID
        {
            get { return mProcessID; }
            set { mProcessID = value; }
        }

        public string ProcessName
        {
            get { return mProcessName; }
            set { mProcessName = value; }
        }

        public string TaskID
        {
            get { return mTaskID; }
            set { mTaskID = value; }
        }

        public string EntityID
        {
            get { return mEntityID; }
            set { mEntityID = value; }
        }

        public string TodoID
        {
            get { return mTodoID; }
            set { mTodoID = value; }
        }

        public string FromTodoID
        {
            get { return mFromTodoID; }
            set { mFromTodoID = value; }
        }

        public string ActID
        {
            get { return mActID; }
            set { mActID = value; }
        }

        public string PreActID
        {
            get { return mPreActID; }
            set { mPreActID = value; }
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public string FromName
        {
            get { return mFromName; }
            set { mFromName = value; }
        }

        public string FromUserCode
        {
            get { return mFromUserCode; }
            set { mFromUserCode = value; }
        }

        public string FromUser
        {
            get { return mFromUser; }
            set { mFromUser = value; }
        }

        public DateTime SendTime
        {
            get { return mSendTime; }
            set { mSendTime = value; }
        }

        public string Desc
        {
            get { return mDesc; }
            set { mDesc = value; }
        }

        public string Url
        {
            get { return mUrl; }
            set { mUrl = value; }
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

        public string EntityName
        {
            get { return mEntityName; }
            set { mEntityName = value; }
        }

        public string PreAction
        {
            get { return mPreAction; }
            set { mPreAction = value; }
        }

        public int TimeAllow
        {
            get { return mTimeAllow; }
            set { mTimeAllow = value; }
        }
    }
}
