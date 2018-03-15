using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    public class AssgnList:ArrayList,ICollection
    {
        public actAssgn this[int index]
        {
            get { return (actAssgn)base[index]; }
        }

        public int Add(string ID, string UserType, string Desc)
        {
            for (int i=0;i<Count;i++)
            {
                actAssgn aa=this[i];
                if (aa.ID == ID && aa.UserType == UserType)
                    return -1;
            }

            actAssgn assgn = new actAssgn();
            assgn.ID = ID;
            assgn.UserType = UserType;
            assgn.Desc = Desc;
            return Add(assgn);
        }

        public void Delete(string ID,string UserType)
        {
            for (int i = 0; i < Count; i++)
            {
                actAssgn aa = this[i];
                if (aa.Desc == ID && aa.UserType == UserType)
                {
                   RemoveAt(i);
                   return;
                }
            }
        }
    }
}
