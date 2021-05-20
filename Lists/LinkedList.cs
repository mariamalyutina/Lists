using System;

namespace Lists
{
    public class LinkedList: ILinkedList
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

        private Node _head; //первый элемент
        private Node _tail; //последний элемент

        public LinkedList()
        {
            CreateEmptyList();
        }

        public LinkedList(int value)
        {
            CreateListWithOneElement(value);
        }

        public LinkedList(int[] inputArray)
        {
            if (inputArray is null)
            {
                throw new NullReferenceException();
            }

            if (inputArray.Length > 0)
            {
                CreateListWithOneElement(inputArray[0]);
                for (int i = 1; i < inputArray.Length; i++)
                {

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
            if (index > Length  || index < 0)
            {
                throw new IndexOutOfRangeException();
            } //отдельная проверка, так как тут можно добавить в пустой массив на индекс Length

            if (Length == 0)
            {
                CreateListWithOneElement(value);
            }
            else if (index == 0)
            {
                Node current = new Node(value);
                current.Next = _head;
                _head = current;
                Length++;
            }
            else if (index == Length)
            {
                _tail.Next = new Node(value);
                _tail = _tail.Next;
                Length++;
            }
            else
            {
                int prevIndex = index - 1; //мы меняем ссылку в предыдущем элементе
                Node current = GetNodeByIndex(prevIndex); 
                Node tmp = new Node(value);
                tmp.Next = current.Next;
                current.Next = tmp;
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
            }
            else if (index == Length - 1)
            {
                int prevIndex = index - 1;
                Node current = GetNodeByIndex(prevIndex);
                current.Next = null; //удаляем ссылку на последний элемент
                _tail = current;
            }
            else
            {
                int prevIndex = index - 1;
                Node current = GetNodeByIndex(prevIndex);
                current.Next = current.Next.Next;
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
            CheckCountArgumentIsValid(count);
            CheckListHasEnoughElementsOrNotEmpty(count);

            if (Length == count)
            {
                CreateEmptyList();
            }
            else
            {
                int prevIndex = Length - count - 1;
                Node current = GetNodeByIndex(prevIndex);
                current.Next = null;
                _tail = current;
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
                Length -= count;
            }
            else //если удаление с 1го индекса
            {
                int prevIndex = index - 1;
                Node current = GetNodeByIndex(prevIndex);
                if (Length - index == count) //если удаляются элементы до конца
                {
                    _tail = current;
                    _tail.Next = null;
                }
                else //если удаляются элементы не до конца
                {
                    current.Next = GetNodeByIndex(index + count);
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
            Node current = _head;
            Node previous = null;
            Node next = null;
            _tail = _head;
            while (!(current is null))
            {
                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }
            _head = previous;
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
                    if (GetNodeByIndex(j).Value < GetNodeByIndex(j - 1).Value)
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
                    if (GetNodeByIndex(j).Value > GetNodeByIndex(j - 1).Value)
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

            LinkedList copyList = CopyList(list);

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
                    _head = copyList._head;
                }
                else if (index == Length) //добавление в конец
                {
                    _tail.Next = copyList._head;
                    _tail = copyList._tail;
                }
                else //добавление в середину
                {
                    int prevIndex = index - 1;
                    Node tmp = this.GetNodeByIndex(prevIndex);
                    copyList._tail.Next = tmp.Next;
                    tmp.Next = copyList._head;
                }
            }
            this.Length += copyList.Length;
        }

        public override string ToString()
        {
            string s = "";
            if (Length != 0)
            {
                Node current = _head;
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
            LinkedList list = (LinkedList)obj;
            if (Length != list.Length)
            {
                return false;
            }

            if (Length == 0 && list.Length == 0)
            {
                return true;
            }

            Node currentThis = _head;
            Node currentList = list._head;

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

        private Node GetNodeByIndex(int index)
        {
            Node current = _head;
            for (int i = 1; i <= index; i++)
            {
                current = current.Next;
            }
            return current;
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
            _head = new Node(value);
            _tail = _head;
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
            int tmp = GetNodeByIndex(index - 1).Value;
            GetNodeByIndex(index - 1).Value = GetNodeByIndex(index).Value;
            GetNodeByIndex(index).Value = tmp;
        }

        private LinkedList CopyList(ILinkedList list)
        {
            LinkedList copy = new LinkedList();
            for (int i = 0; i < list.GetLength(); i++)
            {
                copy.Add(list[i]);
            }
            return copy;
        }
    }
}
