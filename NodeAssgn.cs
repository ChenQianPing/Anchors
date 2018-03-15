using System;
using System.Collections.Generic;
using System.Text;

namespace Anchors
{
    class NodeAssgn
    {
        private string mID;
        private string mName;
        private string mUserType;
        private string mHashCode;

        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public string UserType
        {
            get { return mUserType; }
            set { mUserType = value; }
        }

        public string HashCode
        {
            get { return mHashCode; }
            set { mHashCode = value; }
        }
    }
}
