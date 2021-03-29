using System;


namespace Lists
{
    public class ArrayList
    {
        public int Length { get; private set; }

        private int[] _array;

        //индексатор
        public int this[int index]
        {
            get
            {
                CheckIndex(index);
                return _array[index];
            }
            set
            {
                CheckIndex(index);
                _array[index] = value;
            }
        }

        public ArrayList()
        {
            Length = 0;
            _array = new int[10];
        }

        public ArrayList(int value)
        {
            Length = 1;
            _array = new int[10];
            _array[0] = value;
        }

        public ArrayList(int[] inputArray)
        {
            Length = inputArray.Length;
            _array = inputArray;
            UpSize(Length);
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
            } //отдельная проверка на индекс, так как можем добавить в конец (на Length)
            if (Length == _array.Length)
            {
                UpSize();
            }
            //Length++;
            ShiftArrayToTheRight(index);
            _array[index] = value;
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
        public void RemoveByIndex(int index, int count = 1)
        {
            CheckIndex(index);
            CheckArrayIsNotEmpty();
            ShiftArrayToTheLeft(index, count);
            if (Length < _array.Length * 0.5)
            {
                DownSize();
            }
        }

        //удаление из конца N элементов
        public void RemoveLastValues(int count)
        {
            CheckCountArgumentIsValid(count);
            CheckArrayHasEnoughElements(0, count);
            RemoveByIndex(Length - 1, count);
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
            CheckArrayHasEnoughElements(index, count);
            RemoveByIndex(index, count);
        }

        //первый индекс по значению (вернуть -1, если элемента нет)
        public int GetFirstIndexByValue(int value)
        {
            int index = -1;
            for (int i = 0; i < Length; i++)
            {
                if (_array[i] == value)
                {
                    return i;
                }
            }
            return index;
        }


        //реверс (123 -> 321)
        public void Reverse()
        {
            for (int i = 0; i < Length * 0.5; i++)
            {
                int temp = _array[i];
                _array[i] = _array[Length - i - 1];
                _array[Length - i - 1] = temp;
            }
        }

        //поиск индекса максимального элемента
        public int GetIndexOfMaxValue()
        {
            CheckArrayIsNotEmpty();
            int max = _array[0];
            int index = 0;
            for (int i = 1; i < Length; i++)
            {
                if (_array[i] > max)
                {
                    max = _array[i];
                    index = i;
                }
            }
            return index;
        }

        //поиск индекс минимального элемента
        public int GetIndexOfMinValue()
        {
            CheckArrayIsNotEmpty();
            int min = _array[0];
            int index = 0;
            for (int i = 1; i < Length; i++)
            {
                if (_array[i] < min)
                {
                    min = _array[i];
                    index = i;
                }
            }
            return index;
        }

        //поиск значения максимального элемента
        public int GetMaxValue()
        {
            return _array[GetIndexOfMaxValue()];
        }

        //поиск значения минимального элемента
        public int GetMinValue()
        {
            return _array[GetIndexOfMinValue()];
        }

        //сортировка по возрастанию
        public void SortAscending()
        {

            for (int i = 0; i < Length - 1; i++)
            {
                int minIndx = i;
                for (int j = i + 1; j < Length; j++)
                {
                    if (_array[j] < _array[minIndx])
                    {
                        minIndx = j;
                    }
                }
                Swap(i, minIndx);
            }
        }

        //сортировка по убыванию
        public void SortDescending()
        {
            for (int i = 0; i < Length - 1; i++)
            {
                int maxIndx = i;
                for (int j = i + 1; j < Length; j++)
                {
                    if (_array[j] > _array[maxIndx])
                    {
                        maxIndx = j;
                    }
                }
                Swap(i, maxIndx);
            }
        }

        //удаление по значению первого (?вернуть индекс)
        public int RemoveFirstByValue(int value)
        {
            int index = -1;
            for (int i = 0; i < Length; i++)
            {
                if (_array[i] == value)
                {
                    RemoveByIndex(i);
                    return i;
                }
            }
            return index;
        }

        //удаление по значению всех (?вернуть кол-во)
        public int RemoveAllByValue(int value)
        {
            int count = 0;
            for (int i = 0; i < Length; i++)
            {
                if (_array[i] == value)
                {
                    RemoveByIndex(i);
                    i--;
                    count++;
                }
            }
            return count;
        }

        //добавление списка в конец
        public void AddArrayList(ArrayList list)
        {
            AddArrayListByIndex(list, Length);
        }

        //добавление списка в начало
        public void AddArrayListAtBeginning(ArrayList list)
        {
            AddArrayListByIndex(list, 0);
        }

        //добавление списка по индексу
        public void AddArrayListByIndex(ArrayList list, int index)
        {
            CheckArgumentIsNotNull(list);

            if (index > Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            } //отдельная проверка на индекс, так как можем добавить в конец (на Length)

            int newLength = Length + list.Length;
            CheckNewLength(newLength);

            int tmp = index + list.Length;

            ShiftArrayToTheRight(tmp - 1, list.Length);

            //вставка
            for (int i = index; i < tmp; i++)
            {
                _array[i] = list[i - index];
            }
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Length; i++)
            {
                s += _array[i] + " ";
            }
            return s;
        }

        public override bool Equals(object obj)
        {
            ArrayList arrList = (ArrayList)obj;

            if (this.Length != arrList.Length)
            {
                return false;
            }
            for (int i = 0; i < Length; i++)
            {
                if (this._array[i] != arrList._array[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void UpSize(int newLength = 0)
        {
            if (newLength == 0)
            {
                newLength = (int)(_array.Length * 1.33d + 1);
            }
            else
            {
                newLength = (int)(newLength * 1.33d + 1);
            }
            int[] tmpArray = new int[newLength];
            for (int i = 0; i < _array.Length; i++)
            {
                tmpArray[i] = _array[i];
            }
            _array = tmpArray;
        }

        private void DownSize()
        {
            int newLength = (int)(_array.Length * 0.67 + 1);
            int[] tmpArray = new int[newLength];
            for (int i = 0; i < newLength; i++)
            {
                tmpArray[i] = _array[i];
            }
            _array = tmpArray;
        }

        private void ShiftArrayToTheRight(int fromIndex, int steps = 1)
        {
            Length += steps;
            for (int i = Length - 1; i > fromIndex; i--)
            {
                _array[i] = _array[i - steps];
            }
        }

        private void ShiftArrayToTheLeft(int fromIndex, int steps)
        {
            for (int i = fromIndex; i < Length - steps; i++)
            {
                _array[i] = _array[i + steps];
            }
            Length -= steps;
        }

        private void CheckArrayIsNotEmpty()
        {
            if (Length == 0)
            {
                throw new Exception("Array is empty");
            }
        }

        private void CheckIndex(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void CheckCountArgumentIsValid(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Invalid input");
            }
        }

        private void CheckArrayHasEnoughElements(int index, int count)
        {
            if (Length - index < count)
            {
                throw new Exception("Removing more elements than left in array");
            }
        }

        private void CheckNewLength(int newLength)
        {
            if (_array.Length < newLength)
            {
                UpSize(newLength);
            }
        }

        private void CheckArgumentIsNotNull(ArrayList list)
        {
            if (list is null)
            {
                throw new NullReferenceException();
            }
        }

        private void Swap(int i, int index)
        {
            int temp = _array[i];
            _array[i] = _array[index];
            _array[index] = temp;
        }
    }
}
