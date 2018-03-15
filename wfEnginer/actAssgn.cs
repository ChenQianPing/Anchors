using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    public class actAssgn
    {
        private string mID;
        private string mUserType;
        private string mDesc;

        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public string UserType
        {
            get { return mUserType; }
            set { mUserType = value; }
        }

        public string Desc
        {
            get { return mDesc; }
            set { mDesc = value; }
        }
    }
}
