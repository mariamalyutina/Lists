using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public interface ILinkedList
    {

        public int this[int index]
        {
            get;

            set;
        }

        public void Add(int value);

        public void AddAtFirstIndex(int value);

        public void AddAtIndex(int value, int index);

        public void Remove();

        public void RemoveAtFirstIndex();

        public void RemoveByIndex(int index);

        public int GetLength();

        public void RemoveLastValues(int count);

        public void RemoveFirstValues(int count);

        public void RemoveValuesByIndex(int index, int count);

        public int GetFirstIndexByValue(int value);

        public void Reverse();

        public int GetMaxValue();

        public int GetIndexOfMaxValue();

        public int GetMinValue();

        public int GetIndexOfMinValue();

        public void SortAscending();

        public void SortDescending();

        public int RemoveFirstByValue(int value);

        public int RemoveAllByValue(int value);

        public void AddList(ILinkedList list);

        public void AddListAtBeginning(ILinkedList list);

        public void AddListByIndex(ILinkedList list, int index);

    }
}
