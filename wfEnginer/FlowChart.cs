using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace wfEnginer
{
    /// <summary>
    /// ��������һ������
    /// </summary>
    public class FlowChart
    {
        const string ACT_TYPE_START="��ʼ";
        const string ACT_TYPE_END = "����";
        const string ACT_TYPE_NODE = "�ڵ�";
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
        /// ��������
        /// </summary>
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }

        /// <summary>
        /// �����̵ļ�Ҫ����
        /// </summary>
        public string Desc
        {
            get { return mDesc; }
            set { mDesc = value; }
        }

        /// <summary>
        /// ���̳�ʱʱ�䣺��λСʱ��0Ϊ������
        /// </summary>
        public int Timeout
        {
            get { return mTimeout; }
            set { mTimeout = value; }
        }

        /// <summary>
        /// ����޸���
        /// </summary>
        public string UserCode
        {
            get { return mUserCode; }
            set { mUserCode = value; }
        }

        /// <summary>
        /// ���ά��ʱ��
        /// </summary>
        public DateTime LastChange
        {
            get { return mLastChange; }
            set { mLastChange = value; }
        }

        /// <summary>
        /// �ڵ����
        /// </summary>
        public int Count
        {
            get { return actList.Count; }
        }


        /// <summary>
        /// ��ʼ�ڵ�
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
        /// �����ڵ�
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
        /// ��ǰ�ڵ���ϼ��ڵ��� 
        /// </summary>
        /// <param name="cNode">��ǰ�ڵ�</param>
        /// <returns></returns>
        public int ParentCount(wfActivity cNode)
        {
            return cNode.ParentCount; 
        }

        /// <summary>
        ///��ǰ�ڵ���¼��ڵ��� 
        /// </summary>
        /// <param name="cNode">��ǰ�ڵ�</param>
        /// <returns></returns>
        public int ChildCount(wfActivity cNode)
        {
            return cNode.ChildCount;
        }

        /// <summary>
        /// ��ȡ��ǰ�ڵ���ϼ��ڵ�
        /// </summary>
        /// <param name="cNode">��ǰ�ڵ�</param>
        /// <param name="index">����</param>
        /// <returns></returns>
        public actRule ParentNode(wfActivity cNode, int index)
        {
            return cNode.ParentNode(index);
        }

        /// <summary>
        /// ��ȡ��ǰ�ڵ���ӽڵ�
        /// </summary>
        /// <param name="cNode">��ǰ�ڵ�</param>
        /// <param name="index">����</param>
        /// <returns></returns>
        public actRule ChildNode(wfActivity cNode, int index)
        {
            return cNode.ChildNode(index);
        }

        /// <summary>
        /// ���뵼��XML�ű�
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
        /// ���ı�����
        /// </summary>
        /// <param name="text">�ı�</param>
        protected virtual void LoadFromString(string text)
        {

        }


        /// <summary>
        /// ����ΪXML
        /// </summary>
        /// <returns></returns>
        protected virtual string ExportAsXML()
        {
            return "";
        }

        /// <summary>
        /// ��ӽڵ�ʵ�����б�
        /// </summary>
        /// <param name="act">��ڵ�</param>
        public void AddActivity(wfActivity act)
        {
            actList.Add(act);
        }

        /// <summary>
        /// ��ȡ���˳��ڵ�
        /// </summary>
        /// <param name="index">����</param>
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
