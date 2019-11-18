using System;
using System.Collections.Generic;


namespace CustomList
{
    public class ChangedArgs<T> : EventArgs
    {

        public T Item
        {
            get; set;
        }


    }
    public class CustomList<T> : ICollection<T>
    {
        private T[] _contents;
        private int _count = 0;
        private readonly int _firstIndex;
        

        public event EventHandler<ChangedArgs<T>> Added;
        public event EventHandler<ChangedArgs<T>> Deleted;

        public CustomList()
        {
            _firstIndex = 1;
            _contents = new T[4 + _firstIndex];
        }

        public CustomList(int firstIndex)
        {
            if (firstIndex < 0)
            {
                throw new CustomListException("Wrong first index! ", firstIndex);
            }
            else
            {
                _firstIndex = firstIndex;
                _contents = new T[4 + _firstIndex];
            }
        }

        public CustomList(int firstIndex, int size)
        {
            if (firstIndex < 0)
            {
                throw new CustomListException("Wrong first index! Index mustn`t be lesser than 0", firstIndex);
            }
            else
                _firstIndex = firstIndex;
            _contents = new T[size + firstIndex];
        }

        public int Count
        {
            get { return _count; }
        }

        public int Capacity

        {
            get
            {
                return _contents.Length;
            }
            set
            {
                if (value == _contents.Length) return;
                if (value > 0)
                {
                    T[] newItems = new T[value];
                    if (Capacity > 0)
                    {
                        Array.Copy(_contents, _firstIndex, newItems, _firstIndex, _count);
                    }
                    _contents = newItems;
                }
                else
                {
                    _contents = null;
                }
            }
        }



        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        public int LastIndex => _firstIndex + _count;

        protected virtual void OnAdd(T val)
        {
            if (Added != null)
                Added(this, new ChangedArgs<T>() { Item = val });
        }
        protected virtual void OnDelete(T val)
        {
            if (Deleted != null)
                Deleted(this, new ChangedArgs<T>() { Item = val });
        }



        public void Add(T value)
        {
            OnAdd(value);
            if (_count == 0)
            {
                _contents[_firstIndex] = value;
                _count++;               
            }
            else
            {
                if (_count + _firstIndex == _contents.Length)
                    EnsureCapacity(Capacity + _firstIndex + 1);
                _contents[_count + _firstIndex] = value;
                _count++;
            }

        }

        public void Clear()
        {
            if (_contents == null)
            {
                throw new CustomListException("Array is already empty!");
            }
            else if (_contents.Length > 0)
            {
                Array.Clear(_contents, 0, _contents.Length);
                Capacity = 0;
                _count = 0;
                
            }
        }

        public bool Contains(T value)
        {
            if (value == null)
            {
                for (int i = _firstIndex; i <= Count + _firstIndex; i++)
                    if (_contents[i] == null)
                        return true;
                return false;
            }
            else
            {
                EqualityComparer<T> c = EqualityComparer<T>.Default;
                for (int i = _firstIndex; i <= Count + _firstIndex; i++)
                {
                    if (c.Equals(_contents[i], value)) return true;
                }
                return false;
            }
        }

        public int IndexOf(T value)
        {
            EqualityComparer<T> c = EqualityComparer<T>.Default;
            for (int i = _firstIndex; i <= Count + _firstIndex; i++)
            {
                if (c.Equals(_contents[i], value)) return i;
            }
            return -1;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0) return false;
            RemoveAt(index);
            OnDelete(item);
            return true;
        }

        public bool RemoveAt(int index)
        {
            if ((index > 0) && (index <= Count + _firstIndex) && (index >= _firstIndex))
            {
                for (int i = index; i < Count + _firstIndex; i++)
                {
                    _contents[i] = _contents[i + 1];
                }
                Capacity--;
                return true;
            }

            return false;
        }

        public T this[int index]
        {
            get
            {

                if (index > Capacity || index < _firstIndex)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                    return _contents[index];
            }

            set
            {
                if (index > Capacity || index < _firstIndex)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                    _contents[index] = value;
            }

        }

        public void CopyTo(T[] array, int index)
        {
            if (LastIndex - _firstIndex > array.Length)
                throw new CustomListException("Not enough size of the array to copy all elements!");
            else
                for (int i = _firstIndex; i <= Count; i++)
                {
                    array.SetValue(_contents[i], index++);
                }
        }

        public void EnsureCapacity(int min)
        {
            if (_contents.Length < min)
            {
                int newCapacity = _contents.Length == 0 ? 4 : _contents.Length * 2;
                if ((uint)newCapacity > 0X7FEFFFFF) newCapacity = 0X7FEFFFFF;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
               
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in _contents)
            {
                yield return value;
            }

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

       


    }
}
