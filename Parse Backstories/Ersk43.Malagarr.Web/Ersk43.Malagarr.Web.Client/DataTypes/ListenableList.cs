using Ersk43.Malagarr.Web.Client.Events.Args.ListenableList;
using System.Collections;

namespace Ersk43.Malagarr.Web.Client.DataTypes
{
    

    public class ListenableList<T> : IList<T>
    {


        private List<T> list;

        public ListenableList()
        {
            this.list = new();
        }
        public ListenableList(List<T> list) 
        {
            // clone
            this.list = list.ToList();
        }

        ///////////////////////////////////////////////////////////////////////////

        public event EventHandler<AddEventArgs<T>>? AddEvent;
        public void Add(T item)
        {
            list.Add(item);
            AddEvent?.Invoke(this, new(item));
        }

        public event EventHandler<ClearEventArgs>? ClearEvent;
        public void Clear()
        {
            list.Clear();
            ClearEvent?.Invoke(this, new());
        }



        public event EventHandler<InsertEventArgs<T>>? InsertEvent;
        public void Insert(int index, T item)
        {
            list.Insert(index, item);
            InsertEvent?.Invoke(this, new(index, item));
        }

        public event EventHandler<RemoveEventArgs<T>>? RemoveEvent;
        public bool Remove(T item)
        {
            bool success = list.Remove(item);
            if (!success)
            {
                return false;
            }
            RemoveEvent?.Invoke(this, new(item));
            return true;
        }
        
        public event EventHandler<RemoveAtEventArgs<T>>? RemoveAtEvent;
        public void RemoveAt(int index)
        {
            T removeItem = list[index];
            list.RemoveAt(index);
            RemoveAtEvent?.Invoke(this, new(index, removeItem));
        }



        ///////////////////////////////////////////////////////////////////////////

        public int Count => list.Count();

        public bool IsReadOnly => false;

        T IList<T>.this[int index] 
        { 
            get => list[index]; 
            set => throw new InvalidOperationException(); // dont allow setting
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
           return list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
