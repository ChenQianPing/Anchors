using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    /// <summary>
    /// ��ɫ
    /// </summary>
    public class Role
    {
        private string mID;
        private string mName;
        private string mCode;
        private string mHValue;


        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Code
        {
            get { return mCode; }
            set { mCode = value; }
        }

        /// <summary>
        /// ��ֵ֤
        /// </summary>
        public string HValue
        {
            get { return mHValue; }
            set { mHValue = value; }
        }
    }
}
