using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace wfEnginer
{
    public class ProjectList:ICollection 
    {
        private ArrayList items = new ArrayList();

        #region ICollection 成员

        public void CopyTo(Array array, int index)
        {
            items.CopyTo(array, index);
        }

        public int Count
        {
            get {return items.Count; }
        }

        public bool IsSynchronized
        {
            get {return items.IsSynchronized; }
        }

        public object SyncRoot
        {
            get {return items.SyncRoot; }
        }

        #endregion

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        public int Add(object item)
        {
            return items.Add(item);
        }

        public void Remove(object item)
        {
            items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        public void Clear()
        {
            items.Clear();
        }

        public ProjectItem this[int index]
        {
            get { return (ProjectItem)items[index]; }
        }

    }
}
