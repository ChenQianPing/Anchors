using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{

    public class actRule
    {
        private wfActivity mNode;
        private string mRule;

        ~actRule()
        {
            mNode = null;
        }

        public wfActivity Node
        {
            get { return mNode; }
            set { mNode = value; }
        }

        public string Rule
        {
            get { return mRule; }
            set { mRule = value; }
        }
    }
}
