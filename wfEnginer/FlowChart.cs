using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace wfEnginer
{
    /// <summary>
    /// 工作流的一个流程
    /// </summary>
    public class FlowChart
    {
        const string ACT_TYPE_START="开始";
        const string ACT_TYPE_END = "结束";
        const string ACT_TYPE_NODE = "节点";
        private string mName;
        private string mID;
        private string mUserCode;
        private DateTime mLastChange;
        private string mDesc;
        private int mTimeout;
        private System.Collections.ArrayList actList = new System.Collections.ArrayList();

        public FlowChart()
        {
        }

        ~FlowChart()
        {
            Clear();
        }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        /// <summary>
        /// 流程ID
        /// </summary>
        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }

        /// <summary>
        /// 对流程的简要描述
        /// </summary>
        public string Desc
        {
            get { return mDesc; }
            set { mDesc = value; }
        }

        /// <summary>
        /// 流程超时时间：单位小时，0为无限制
        /// </summary>
        public int Timeout
        {
            get { return mTimeout; }
            set { mTimeout = value; }
        }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public string UserCode
        {
            get { return mUserCode; }
            set { mUserCode = value; }
        }

        /// <summary>
        /// 最后维护时间
        /// </summary>
        public DateTime LastChange
        {
            get { return mLastChange; }
            set { mLastChange = value; }
        }

        /// <summary>
        /// 节点计数
        /// </summary>
        public int Count
        {
            get { return actList.Count; }
        }


        /// <summary>
        /// 开始节点
        /// </summary>
        public wfActivity StartNode
        {
            get { return getStartNode(); }
        }

        private wfActivity getStartNode()
        {
            for (int i = 0; i < actList.Count; i++)
            {
                wfActivity act = (wfActivity)actList[i];
                if (act.ActType == ACT_TYPE_START)
                {
                    return act;
                }
            }

            return null;
        }
        /// <summary>
        /// 结束节点
        /// </summary>
        public wfActivity EndNode
        {
            get { return getEndNode(); }
        }

        private wfActivity getEndNode()
        {
            for (int i = 0; i < actList.Count; i++)
            {
                wfActivity act = (wfActivity)actList[i];
                if (act.ActType == ACT_TYPE_END)
                {
                    return act;
                }
            }

            return null;
        }

        /// <summary>
        /// 当前节点的上级节点数 
        /// </summary>
        /// <param name="cNode">当前节点</param>
        /// <returns></returns>
        public int ParentCount(wfActivity cNode)
        {
            return cNode.ParentCount; 
        }

        /// <summary>
        ///当前节点的下级节点数 
        /// </summary>
        /// <param name="cNode">当前节点</param>
        /// <returns></returns>
        public int ChildCount(wfActivity cNode)
        {
            return cNode.ChildCount;
        }

        /// <summary>
        /// 获取当前节点的上级节点
        /// </summary>
        /// <param name="cNode">当前节点</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public actRule ParentNode(wfActivity cNode, int index)
        {
            return cNode.ParentNode(index);
        }

        /// <summary>
        /// 获取当前节点的子节点
        /// </summary>
        /// <param name="cNode">当前节点</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public actRule ChildNode(wfActivity cNode, int index)
        {
            return cNode.ChildNode(index);
        }

        /// <summary>
        /// 导入导出XML脚本
        /// </summary>
        public string asXML
        {
            get { return ExportAsXML(); }
            set { LoadFromString(value);}
        }


        public void Clear()
        {
            //Free instance
            //for (int i = 0; i < actList.Count; i++)
            //{
            //    wfActivity act = actList[i];
            //    
            //}
            actList.Clear();
        }

        /// <summary>
        /// 从文本加载
        /// </summary>
        /// <param name="text">文本</param>
        protected virtual void LoadFromString(string text)
        {

        }


        /// <summary>
        /// 导出为XML
        /// </summary>
        /// <returns></returns>
        protected virtual string ExportAsXML()
        {
            return "";
        }

        /// <summary>
        /// 添加节点实例到列表
        /// </summary>
        /// <param name="act">活动节点</param>
        public void AddActivity(wfActivity act)
        {
            actList.Add(act);
        }

        /// <summary>
        /// 获取添加顺序节点
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public wfActivity Activities(int index)
        {
            
            return (wfActivity)actList[index];
        }

        public wfActivity GetActivityByID(string actID)
        {
            for (int i = 0; i < actList.Count; i++)
            {
                wfActivity obj = (wfActivity)actList[i];
                if (obj.ID == actID)
                    return obj;
            }
            return null;
        }

        public wfActivity Items(int index)
        {
            return (wfActivity)actList[index];
        }
    }
}
