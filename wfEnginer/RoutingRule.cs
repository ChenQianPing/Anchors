using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    /// <summary>
    /// ���ת������
    /// </summary>
    class RoutingRule
    {
        private string mPreActID;
        private string mCurrActID;
        private string mCompletionFlag;

        /// <summary>
        /// ��һ���ID
        /// </summary>
        public string PreActID
        {
            get { return mPreActID; }
            set { mPreActID = value; }
        }

        /// <summary>
        /// ��ǰ�ID
        /// </summary>
        public string CurrActID
        {
            get { return mCurrActID; }
            set { mCurrActID = value; }
        }

        /// <summary>
        /// ��ɱ��
        /// </summary>
        public string CompletionFlag
        {
            get { return mCompletionFlag; }
            set { mCompletionFlag = value; }
        }

    }
}
