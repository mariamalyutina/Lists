using System;


namespace Lists
{
    public class DoubleLinkedList : ILinkedList
    {
        public int Length { get; private set; }

        public int this[int index]
        {
            get
            {
                CheckIndex(index);
                return GetNodeByIndex(index).Value;

            }
            set
            {
                CheckIndex(index);
                GetNodeByIndex(index).Value = value;
            }
        }

        private DoubleNode _head; //первый элемент
        private DoubleNode _tail; //последний элемент

        public DoubleLinkedList()
        {
            CreateEmptyList();
        }

        public DoubleLinkedList(int value)
        {
            CreateListWithOneElement(value);
        }

        public DoubleLinkedList(int[] inputArray)
        {
            if (inputArray is null)
            {
                throw new NullReferenceException();
            }
            else if (inputArray.Length > 0)
            {
                CreateListWithOneElement(inputArray[0]);

                for (int i = 1; i < inputArray.Length; i++)
                {
                    if (i == 1) //перепресвоение _head.Next с null на след элемент,
                                //если в массиве 2 и более элементов
                    {
                        _head.Next = new DoubleNode(inputArray[1]);
                    }
                    Add(inputArray[i]);
                }
            }
            else
            {
                CreateEmptyList();
            }
        }

        //добавление значения в конец
        public void Add(int value)
        {
            AddAtIndex(value, Length);
        }

        //добавление значения в начало
        public void AddAtFirstIndex(int value)
        {
            AddAtIndex(value, 0);
        }

        //добавление значения по индексу
        public void AddAtIndex(int value, int index)
        {
            if (index > Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            } //отдельная проверка, так как тут можно добавить в пустой массив на индекс Length
            DoubleNode tmp = new DoubleNode(value);
            if (Length == 0)
            {
                CreateListWithOneElement(value);
            }
            else if (index == 0)
            {
                tmp.Prev = null;
                tmp.Next = _head;
                _head.Prev = tmp;
                _head = tmp;
                Length++;
            }
            else if (index == Length)
            {
                tmp.Next = null;
                tmp.Prev = _tail;
                _tail.Next = tmp;
                _tail = tmp;
                Length++;
            }
            else
            {
                tmp.Next = GetNodeByIndex(index);
                tmp.Prev = GetNodeByIndex(index).Prev;
                tmp.Next.Prev = tmp;
                tmp.Prev.Next = tmp;
                Length++;
            }
        }

        //удаление из конца одного элемента
        public void Remove()
        {
            RemoveByIndex(Length - 1);
        }

        //удаление из начала одного элемента
        public void RemoveAtFirstIndex()
        {
            RemoveByIndex(0);
        }

        //удаление по индексу одного элемента
        public void RemoveByIndex(int index)
        {
            CheckIndex(index);
            CheckArrayIsNotEmpty();
            if (Length == 1)
            {
                CreateEmptyList();
            }
            else if (index == 0)
            {
                _head = _head.Next;
                _head.Prev = null;
            }
            else if (index == Length - 1)
            {
                _tail = _tail.Prev;
                _tail.Next = null;
            }
            else
            {
                GetNodeByIndex(index).Prev.Next = GetNodeByIndex(index).Next;
                GetNodeByIndex(index).Next.Prev = GetNodeByIndex(index).Prev;
            }
            Length = Length == 0 ? 0 : --Length;
        }

        public int GetLength()
        {
            return this.Length;
        }

        //удаление из конца N элементов
        public void RemoveLastValues(int count)
        {
            //переиспользовать RemoveValuesByIndex
            CheckCountArgumentIsValid(count);
            CheckListHasEnoughElementsOrNotEmpty(count);

            if (Length == count)
            {
                CreateEmptyList();
            }
            else if (count > 0) //если count = 0 - ничего менять не нужно
            {
                int index = Length - count;
                _tail = GetNodeByIndex(index).Prev;
                _tail.Next = null;
                Length -= count;
            }
        }

        //удаление из начала N элементов
        public void RemoveFirstValues(int count)
        {
            RemoveValuesByIndex(0, count);
        }

        //удаление по индексу N элементов
        public void RemoveValuesByIndex(int index, int count)
        {
            CheckCountArgumentIsValid(count);
            CheckIndex(index);
            CheckListHasEnoughElementsOrNotEmpty(count);

            if (Length - index < count) //проверка subarray
            {
                throw new Exception($"Removing more elements than left in array from index {index}");
            }

            if (Length == count) //удаление сначала до конца
            {
                CreateEmptyList();
            }
            else if (index == 0) //удаление сначала не до конца
            {
                _head = GetNodeByIndex(count);
                _head.Prev = null;
                Length -= count;
            }
            else //если удаление с 1го индекса
            {
                
                if (Length - index == count) //если удаляются элементы до конца
                {
                    _tail = GetNodeByIndex(index).Prev;
                    _tail.Next = null;
                }
                else if (count > 0) //если удаляются элементы не до конца
                {
                    int lastIndex = index + count;
                    GetNodeByIndex(lastIndex).Prev = GetNodeByIndex(index).Prev;
                    GetNodeByIndex(index).Prev.Next = GetNodeByIndex(lastIndex);
                }
                Length -= count;
            }
        }

        //первый индекс по значению (вернуть -1, если элемента нет)
        public int GetFirstIndexByValue(int value)
        {
            for (int i = 0; i < Length; i++)
            {
                if (value == GetNodeByIndex(i).Value)
                {
                    return i;
                }
            }
            return -1;
        }

        //реверс (123 -> 321)
        public void Reverse()
        {
            //_tail = _head;
            DoubleNode current = _head;
            DoubleNode tmp = null;
            while (!(current is null))
            {
                tmp = current.Prev;
                current.Prev = current.Next;
                current.Next = tmp;
                current = current.Prev;
            }
            if (!(tmp is null))
            {
                _tail = GetNodeByIndex(0);
                _head = tmp.Prev;
            }
        }

        //поиск значения максимального элемента
        public int GetMaxValue()
        {
            return this[GetIndexOfMaxValue()];
        }

        //поиск индекс максимального элемента
        public int GetIndexOfMaxValue()
        {
            CheckArrayIsNotEmpty();

            int max = GetNodeByIndex(0).Value;
            int maxIndex = 0;
            for (int i = 1; i < Length; i++)
            {
                if (GetNodeByIndex(i).Value > max)
                {
                    max = GetNodeByIndex(i).Value;
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        //поиск значения минимального элемента
        public int GetMinValue()
        {
            return this[GetIndexOfMinValue()];
        }

        //поиск индекс максимального элемента
        public int GetIndexOfMinValue()
        {
            CheckArrayIsNotEmpty();

            int min = GetNodeByIndex(0).Value;
            int minIndex = 0;
            for (int i = 1; i < Length; i++)
            {
                if (GetNodeByIndex(i).Value < min)
                {
                    min = GetNodeByIndex(i).Value;
                    minIndex = i;
                }
            }
            return minIndex;
        }

        //сортировка по возрастанию вставками
        public void SortAscending()
        {
            for (int i = 0; i < Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (GetNodeByIndex(j).Value < GetNodeByIndex(j).Prev.Value)
                    {
                        Swap(j);
                    }
                }
            }
        }

        //сортировка по убыванию вставками
        public void SortDescending()
        {
            for (int i = 0; i < Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (GetNodeByIndex(j).Value > GetNodeByIndex(j).Prev.Value)
                    {
                        Swap(j);
                    }
                }
            }
        }

        //удаление по значению первого (?вернуть индекс)
        public int RemoveFirstByValue(int value)
        {
            for (int i = 0; i < Length; i++)
            {
                if (GetNodeByIndex(i).Value == value)
                {
                    RemoveByIndex(i);
                    return i;
                }
            }
            return -1;
        }

        //удаление по значению всех (?вернуть кол-во)
        public int RemoveAllByValue(int value)
        {
            int count = 0;
            for (int i = 0; i < Length; i++)
            {
                if (GetNodeByIndex(i).Value == value)
                {
                    RemoveByIndex(i);
                    i--;
                    count++;
                }
            }
            return count;
        }

        //добавление списка в конец
        public void AddList(ILinkedList list)
        {
            AddListByIndex(list, Length);
        }

        //добавление списка в начало
        public void AddListAtBeginning(ILinkedList list)
        {
            AddListByIndex(list, 0);
        }

        //добавление списка по индексу
        public void AddListByIndex(ILinkedList list, int index)
        {
            CheckArgumentIsNotNull(list);

            if (index >= Length + 1 || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            DoubleLinkedList copyList = CopyList(list);

            if (this.Length == 0) //добавление к пустому листу
            {
                this._head = copyList._head;
                this._tail = copyList._tail;
            }
            else if (list.GetLength() > 0) //если в добавляемом массиве есть элементы
            {
                if (index == 0) //добавление в начало
                {
                    copyList._tail.Next = this._head;
                    this._head.Prev = copyList._tail;
                    _head = copyList._head;
                }
                else if (index == Length) //добавление в конец
                {
                    _tail.Next = copyList._head;
                    copyList._head.Prev = this._tail;
                    _tail = copyList._tail;
                }
                else //добавление в середину
                {
                    copyList._tail.Next = this.GetNodeByIndex(index);
                    this.GetNodeByIndex(index).Prev.Next = copyList._head;
                    copyList._head.Prev = this.GetNodeByIndex(index).Prev;
                    this.GetNodeByIndex(index).Prev = copyList._tail;
                }
            }
            this.Length += copyList.Length;
        }

        public override string ToString()
        {
            string s = "";
            if (Length != 0)
            {
                DoubleNode current = _head;
                s += current.Value;
                while (!(current.Next is null))
                {
                    current = current.Next;
                    s += " " + current.Value;
                }
                return s;
            }
            return s;
        }

        public override bool Equals(object obj)
        {
            DoubleLinkedList list = (DoubleLinkedList)obj;
            if (Length != list.Length)
            {
                return false;
            }

            if (Length == 0 && list.Length == 0)
            {
                return true;
            }

            DoubleNode currentThis = _head;
            DoubleNode currentList = list._head;

            if (currentThis.Value != currentList.Value)
            {
                return false;
            }

            while (!(currentThis.Next is null))
            {
                currentList = currentList.Next;
                currentThis = currentThis.Next;
                if (currentThis.Value != currentList.Value)
                {
                    return false;
                }
            }

            return true;
        }


        private void CreateEmptyList()
        {
            Length = 0;
            _head = null;
            _tail = _head;
        }

        private void CreateListWithOneElement(int value)
        {
            Length = 1;
            _head = new DoubleNode(value);
            _tail = _head;
        }

        private DoubleNode GetNodeByIndex(int index)
        {
            if (index < Length / 2) //если индекс ближе к началу, идем сначала
            {
                DoubleNode current = _head;
                for (int i = 1; i <= index; i++)
                {
                    current = current.Next;
                }
                return current;
            }
            else
            {
                DoubleNode current = _tail;
                for (int i = Length - 2; i >= index; i--)
                {
                    current = current.Prev;
                }
                return current;
            }
        }

        private void CheckIndex(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void CheckArrayIsNotEmpty()
        {
            if (Length == 0)
            {
                throw new Exception("Array is empty");
            }
        }

        private void CheckCountArgumentIsValid(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Invalid input");
            }
        }

        private void CheckListHasEnoughElementsOrNotEmpty(int count)
        {
            if (count > Length || Length == 0)
            {
                throw new Exception("Removing more elements than left in array");
            }
        }

        private void CheckArgumentIsNotNull(ILinkedList list)
        {
            if (list is null)
            {
                throw new NullReferenceException();
            }
        }

        private void Swap(int index)
        {
            int tmp = GetNodeByIndex(index).Value;
            GetNodeByIndex(index).Value = GetNodeByIndex(index).Prev.Value;
            GetNodeByIndex(index).Prev.Value = tmp;
        }

        private DoubleLinkedList CopyList(ILinkedList list)
        {
            DoubleLinkedList copy = new DoubleLinkedList();
            for (int i = 0; i < list.GetLength(); i++)
            {
                copy.Add(list[i]);
            }
            return copy;
        }
    }
}
