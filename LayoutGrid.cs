using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Anchors
{
    class LayoutGrid
    {
    }
    
    class LayoutItem : ArrayList
    {
        private LayoutGroup mParent = null;
        private int[] mLefts=new int[512];

        internal LayoutGroup Parent
        {
            get { return mParent; }
            set { mParent =value; }
        }

        public int[] Left
        {
            get { return mLefts; }
        }

        public override int Add(object value)
        {
            int rc=base.Add(value);

            return rc;
        }

        internal bool FindItem(object value)
        {
            for (int i = 0; i < Count; i++)
            {

            }
            return false;
        }
    }

    class LayoutGroup : ArrayList
    {
        private int mColWidth;

        new public LayoutItem this[int index]
        {
            get { return (LayoutItem)base[index]; }
        }

        public override int Add(object value)
        {
            int rc= base.Add(value);
            ((LayoutItem)value).Parent = this;
            return rc;
        }

        public void Add(object Parent, object value)
        {
            for (int i = 0; i < Count; i++)
            {
                LayoutItem item = this[i];
                if (item.FindItem(Parent))
                {
                }
            }
        }

        public int ColWidth
        {
            get {return mColWidth;}
            set {mColWidth = value;}
        }
    }

}
