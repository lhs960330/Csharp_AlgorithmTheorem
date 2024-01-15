using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._List
{
    public class List<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;
        private int size;

        public List()
        {
            items = new T[DefaultCapacity];
            size = 0;
        }

        public int Capacity { get { return items.Length; } }
        public int Count { get { return size; } }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                    throw new IndexOutOfRangeException();

                return items[index];
            }
            set
            {
                if (index < 0 || index >= size)
                    throw new IndexOutOfRangeException();

                items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (size >= items.Length)
                Grow();

            items[size++] = item;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || size < index)
                throw new ArgumentOutOfRangeException();

            if (size >= items.Length)
                Grow();

            if (index < size)
                Array.Copy(items, index, items, index + 1, size - index);

            items[index] = item;
            size++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
                throw new IndexOutOfRangeException();

            size--;
            Array.Copy(items, index + 1, items, index, size - index);
        }

        public void Clear()
        {
            items = new T[DefaultCapacity];
            size = 0;
        }

        public int IndexOf(T item)
        {
            if (item == null)
            {
                for (int i = 0; i < size; i++)
                {
                    if (null == items[i])
                        return i;
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    if (item.Equals(items[i]))
                        return i;
                }
            }

            return -1;
        }

        public int FindIndex(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException();

            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return i;
            }
            return -1;
        }

        private void Grow()
        {
            int newCapacity = items.Length * 2;
            T[] newItems = new T[newCapacity];
            Array.Copy(items, 0, newItems, 0, size);
            items = newItems;
        }
    }
}
