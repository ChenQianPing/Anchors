using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    /// <summary>
    /// 活动前置关系
    /// </summary>
    class PreRule
    {
        private string mDepntID;
        private string mDepntActID;
        private string mDepntActStatus;

        /// <summary>
        /// 依赖ID
        /// </summary>
        public string DepntID
        {
            get { return mDepntID; }
            set { mDepntID = value; }
        }

        /// <summary>
        /// 依赖的活动ID
        /// </summary>
        public string DepntActID
        {
            get { return mDepntActID; }
            set { mDepntActID = value; }
        }

        /// <summary>
        /// 依赖活动的状态
        /// </summary>
        public string DepntActStatus
        {
            get { return mDepntActStatus; }
            set { mDepntActStatus = value; }
        }
    }
}
