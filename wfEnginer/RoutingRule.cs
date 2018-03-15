using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    /// <summary>
    /// 活动后转发规则
    /// </summary>
    class RoutingRule
    {
        private string mPreActID;
        private string mCurrActID;
        private string mCompletionFlag;

        /// <summary>
        /// 上一步活动ID
        /// </summary>
        public string PreActID
        {
            get { return mPreActID; }
            set { mPreActID = value; }
        }

        /// <summary>
        /// 当前活动ID
        /// </summary>
        public string CurrActID
        {
            get { return mCurrActID; }
            set { mCurrActID = value; }
        }

        /// <summary>
        /// 完成标记
        /// </summary>
        public string CompletionFlag
        {
            get { return mCompletionFlag; }
            set { mCompletionFlag = value; }
        }

    }
}
