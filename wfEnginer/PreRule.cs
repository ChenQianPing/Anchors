using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    /// <summary>
    /// �ǰ�ù�ϵ
    /// </summary>
    class PreRule
    {
        private string mDepntID;
        private string mDepntActID;
        private string mDepntActStatus;

        /// <summary>
        /// ����ID
        /// </summary>
        public string DepntID
        {
            get { return mDepntID; }
            set { mDepntID = value; }
        }

        /// <summary>
        /// �����ĻID
        /// </summary>
        public string DepntActID
        {
            get { return mDepntActID; }
            set { mDepntActID = value; }
        }

        /// <summary>
        /// �������״̬
        /// </summary>
        public string DepntActStatus
        {
            get { return mDepntActStatus; }
            set { mDepntActStatus = value; }
        }
    }
}
