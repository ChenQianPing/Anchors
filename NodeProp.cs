using System;
using System.Collections.Generic;
using System.Text;

namespace Anchors
{
    class NodeProp
    {
        public static string NODE_TYPE_START="开始";
        public static string NODE_TYPE_END = "结束";
        public static string NODE_TYPE_NODE = "普通";

        private string mID;
        private string mDesc;
        private int mTimeout;
        private int mVoteNum;
        private System.Collections.ArrayList mUsers=new System.Collections.ArrayList();
        private string mNodeType;

        public NodeProp()
        {
        }

        ~NodeProp()
        {

        }

        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public string Desc
        {
            get { return mDesc; }
            set { mDesc = value; }
        }

        public int Timeout
        {
            get { return mTimeout; }
            set { mTimeout = value; }
        }

        public string NodeType
        {
            get { return mNodeType; }
            set { mNodeType = value; }
        }

        public int UserCount
        {
            get { return mUsers.Count; }
        }

        public NodeAssgn Users(int index)
        {
            return (NodeAssgn)mUsers[index];
        }

        public int addUser(NodeAssgn value)
        {
            return mUsers.Add(value);
        }

        public void DeleteUser(int index)
        {
            mUsers.RemoveAt(index);
        }

        public void RemoveUser(NodeAssgn value)
        {
            mUsers.Remove(value);
        }
    }
}
