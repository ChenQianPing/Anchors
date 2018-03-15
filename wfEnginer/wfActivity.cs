using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace wfEnginer
{
    enum NodeType
    {
        NodeStart,NodeEnd,NodeActivity

    }
    /// <summary>
    /// 工作流定义活动节点属性
    /// </summary>
    public class wfActivity
    {
        private string mName;
        private string mID;
        private int mTimeAllowed;
        private string mDesc;
        private string mActType;
        private int mOrMergeFlag;
        private int mNumVotesNeeded;
        private bool mAutoExecutive;
        private string mInstanceID;
        private string mTodoID;
        private System.Collections.ArrayList Parents = new System.Collections.ArrayList();
        private System.Collections.ArrayList Childs = new System.Collections.ArrayList();
        private System.Collections.Hashtable attrs = new System.Collections.Hashtable();
        private byte mChanged = 0;
        private AssgnList mUsers = new AssgnList();
        private string mPreRule;
        private StringCollection mSelectNodes = new StringCollection(); 

        public wfActivity()
        {
        }

        ~wfActivity()
        {
            for (int i = 0; i < Parents.Count; i++)
            {
                actRule rule = (actRule)Parents[i];
                rule.Node = null;
            }
            Parents.Clear();
            for (int i = 0; i < Childs.Count; i++)
            {
                actRule rule = (actRule)Childs[i];
                rule.Node = null;
            }
            Childs.Clear();
        }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }

        /// <summary>
        /// 节点停留时间控制
        /// </summary>
        public int TimeAllowed
        {
            get { return mTimeAllowed; }
            set { mTimeAllowed = value; }
        }


        /// <summary>
        /// 节点描述
        /// </summary>
        public string Desc
        {
            get { return mDesc; }
            set { mDesc = value; }
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        public string ActType
        {
            get { return mActType; }
            set { mActType = value; }
        }

        /// <summary>
        /// 合并标记
        /// </summary>
        public int OrMergeFlag
        {
            get { return mOrMergeFlag; }
            set { mOrMergeFlag = value; }
        }

        /// <summary>
        /// 投票需要的数量
        /// </summary>
        public int NumVotesNeeded
        {
            get { return mNumVotesNeeded; }
            set { mNumVotesNeeded = value; }
        }

        /// <summary>
        /// 是否自动完成活动
        /// </summary>
        public bool AutoExecutive
        {
            get { return mAutoExecutive; }
            set { mAutoExecutive  = value; }
        }

        public string InstanceID
        {
            get { return mInstanceID; }
            set { mInstanceID = value; }
        }

        public string TodoID
        {
            get { return mTodoID; }
            set { mTodoID = value; }
        }

        public int ChildCount
        {
            get { return Childs.Count; }
        }

        public int ParentCount
        {
            get { return Parents.Count; }
        }

        /// <summary>
        /// 添加上级节点
        /// </summary>
        /// <param name="obj"></param>
        public void AddParent(wfActivity obj,string StrRule)
        {
            actRule rule = new actRule();
            rule.Node = obj;
            rule.Rule = StrRule;
            Parents.Add(rule);

        }

        /// <summary>
        /// 添加下层节点
        /// </summary>
        /// <param name="obj"></param>
        public actRule AddChild(wfActivity obj, string StrRule)
        {
            actRule rule = new actRule();
            rule.Node = obj;
            rule.Rule = StrRule;
            Childs.Add(rule);
            return rule;
        }

        public void RemoveParent(wfActivity obj)
        {
            for (int i = 0; i < Parents.Count; i++)
            {
                actRule rule = (actRule)Parents[i];
                if (rule.Node == obj)
                {
                    rule.Node = null;
                    Parents.RemoveAt(i);
                }
            }
        }

        public void RemoveChild(wfActivity obj)
        {
            for (int i = 0; i < Childs.Count; i++)
            {
                actRule rule = (actRule)Childs[i];
                if (rule.Node == obj)
                {
                    rule.Node = null;
                    Childs.RemoveAt(i);
                }
            }
        }

        public actRule ParentNode(int index)
        {
            if ((index < 0) || (index >= Parents.Count))
                return null;
            else
                return (actRule)Parents[index];
        }

        public actRule ChildNode(int index)
        {
            if ((index < 0) || (index >= Childs.Count))
                return null;
            else
                return (actRule)Childs[index];
        }

        public Hashtable Attributes
        {
            get { return attrs; }
        }

        public byte Changed
        {
            get { return mChanged; }
            set { mChanged = value; }
        }

        public AssgnList UserList
        {
            get { return mUsers; }
        }

        public string PreRule
        {
            get { return mPreRule; }
            set { mPreRule = value; }
        }

        public StringCollection SelectNodes
        {
            get { return mSelectNodes; }
        }
    }
}
