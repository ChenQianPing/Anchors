using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    /// <summary>
    /// 角色
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
        /// 名称
        /// </summary>
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get { return mCode; }
            set { mCode = value; }
        }

        /// <summary>
        /// 验证值
        /// </summary>
        public string HValue
        {
            get { return mHValue; }
            set { mHValue = value; }
        }
    }
}
