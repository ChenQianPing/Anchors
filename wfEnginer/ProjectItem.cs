using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    public class ProjectItem
    {
        private string mName;
        private string mID;
        private string mDesc;

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public string Desc
        {
            get { return mDesc; }
            set { mDesc = value; }
        }
    }
}
