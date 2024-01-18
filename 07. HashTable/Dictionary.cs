using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Dictionary<Tkey, Tvalue> where Tkey : IEquatable<Tkey>
    {
        private const int DefaultCapacity = 1000;  // 소수일수록 분산이 잘되니(오류가 덜 난다.) 원래는 소수로 정해주는게 좋다.

        private struct Entry
        {
            public enum State { None, Using, Deleted}

            public State state;
            public Tkey Key;
            public Tvalue value;
        }

        private Entry[] table;
        private int usedCount;

        public Dictionary()
        {
            table = new Entry[DefaultCapacity];
            usedCount = 0;
        }

        public Tvalue this[Tkey key]
        {
            get
            {
                if(Find(key, out int index))
                {
                    return table[index].value;
                }
                else
                {
                    throw new KeyNotFoundException();
                }

            }
            set
            {
                if(Find(key, out int index) )
                {
                    table[index].value = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public void Add(Tkey key, Tvalue value)
        {
            if(Find(key, out int index))
            {
              throw new InvalidOperationException("Already exist key");
            }
            else
            {
                if(usedCount > table.Length * 0.7f)
                {
                    ReHashing();
                }

                table[index].Key = key;
                table[index].value = value;
                table[index].state =Entry.State.Using;
                usedCount++;
            }
        }

        public bool Remove(Tkey key)
        {
            if(Find(key, out int index) )
            {
                table[index].state = Entry.State.Deleted; 
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContainsKey(Tkey key)
        {
            if(Find(key, out int index) ) { return true; }
            else { return false; }
        }

        private bool Find(Tkey key, out int index)
        {
            index = Hash(key); // 해싱

            for (int i = 0; i < table.Length; i++)
            {
                if (table[index].state == Entry.State.None)
                {
                    return false;
                }
                else if (table[index].state == Entry.State.Using)
                {
                    if (key.Equals(table[index].Key))
                    {
                        return true;
                    }
                    else
                    {
                        // 다음칸
                        index = Hash2(index);
                    }
                }
                else // if (table[index].state == Entry.State.Deleted)
                {
                    // 다음칸
                    index = Hash2(index);
                }
            }
            index = -1;
            throw new InvalidOperationException();
        }
        
        private int Hash(Tkey key)  // 해시 함수
        {
            return Math.Abs( key.GetHashCode() % table.Length);
        }

        private int Hash2(int index)
        {
            //선형탐사
            return (index +1) % table.Length;

            // 제곱 탐사
            // return (index + 1) * (index + 1) % table.Length;

            // 이중 해싱
            // return Math.Abs((index + 1).GetHashCode() % table.Length);
        }

        private void ReHashing()
        {
            Entry[] oldTable =table;
            table =new Entry[table.Length *2];
            usedCount= 0;

            for(int i = 0 ; i < oldTable.Length; i++)
            {
                Entry entry = oldTable[i];
                if(entry.state == Entry.State.Using)
                {
                    Add(entry.Key, entry.value);
                }

            }
        }
    }
}
