using Lists;
using NUnit.Framework;
//пробный вариант только для LinkedList и DoubleLinkedList

namespace ListsTests
{
    [TestFixture("LinkedList")] //вызывает конструктор тестового класса
    [TestFixture("DoubleLinkedList")]
    

    class ILinkedListsTests
    {
        ILinkedList actual;
        ILinkedList expected;
        ILinkedList additional;

        string type = "";

        public ILinkedListsTests(string s) //конструктор класса ILinkedListsTests
        {
            type = s;
        }

        public void SetUp(int[] inputArray, int[] expectedArray)
        {
            switch (type)
            {
                case "LinkedList":
                    actual = new LinkedList(inputArray);
                    expected = new LinkedList(expectedArray);
                    break;
                case "DoubleLinkedList":
                    actual = new DoubleLinkedList(inputArray);
                    expected = new DoubleLinkedList(expectedArray);
                    break;
            }
        }

        public void SetUpOneList(int[] inputArray)
        {
            switch (type)
            {
                case "LinkedList":
                    actual = new LinkedList(inputArray);
                    break;
                case "DoubleLinkedList":
                    actual = new DoubleLinkedList(inputArray);
                    break;
            }
        }

        public ILinkedList SetUpAdditionalList(int[] additionalList)
        {
            switch (type)
            {
                case "LinkedList":
                    additional = new LinkedList(additionalList);
                    return additional;
                case "DoubleLinkedList":
                    additional = new DoubleLinkedList(additionalList);
                    return additional;
                default: return null;
            }
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 8, new int[] { 1, 2, 3, 4, 8 })]
        [TestCase(new int[0], 8, new int[] { 8 })]
        [TestCase(new int[] { 1 }, 8, new int[] { 1, 8 })]
        public void AddTest(int[] inputArray, int value, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.Add(value);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 8, new int[] { 8, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
        [TestCase(new int[0], 8, new int[] { 8 })]
        [TestCase(new int[] { 1 }, 8, new int[] { 8, 1 })]
        public void AddAtFirstIndexTest(int[] inputArray, int value, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.AddAtFirstIndex(value);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 9999, 0, new int[] { 9999, 1, 2, 3, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 9999, 1, new int[] { 1, 9999, 2, 3, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 9999, 2, new int[] { 1, 2, 9999, 3, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, 9999, 7, new int[] { 1, 2, 3, 4, 99, 15, 14, 9999, 45, 88, 69, 78, 555, 23, 56, 74, 11 })]
        [TestCase(new int[0], 9999, 0, new int[] { 9999 })]
        [TestCase(new int[] { 1 }, 9999, 1, new int[] { 1, 9999 })]
        [TestCase(new int[] { 1 }, 9999, 0, new int[] { 9999, 1 })]
        public void AddAtIndexTest(int[] inputArray, int value, int index, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.AddAtIndex(value, index);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 9999, -1)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 9999, 5)]
        [TestCase(new int[0], 9999, 1)]
        public void AddAtIndex_WhenIndexOutOfRange_IndexOutOfRangeException(int[] inputArray, int value, int index)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.AddAtIndex(value, index);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74 })]
        [TestCase(new int[] { 1 }, new int[0])]
        public void RemoveTest(int[] inputArray, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.Remove();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0])]
        public void Remove_WhenArrayIsEmpty_Exception(int[] inputArray)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.Remove();
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 2, 3, 4 })]
        [TestCase(new int[] { 1, 2 }, new int[] { 2 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, new int[] { 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 })]
        [TestCase(new int[] { 1 }, new int[0])]
        public void RemoveAtFirstIndexTest(int[] inputArray, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.RemoveAtFirstIndex();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0])]
        public void RemoveAtFirstIndex_WhenArrayIsEmpty_Exception(int[] inputArray)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveAtFirstIndex();
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 0, new int[] { 2, 3, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 3, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, 5, new int[] { 1, 2, 3, 4, 99, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 })]
        [TestCase(new int[] { 1 }, 0, new int[0])]
        public void RemoveByIndexTest(int[] inputArray, int index, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.RemoveByIndex(index);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0], 0)]
        public void RemoveByIndex_WhenArrayIsEmpty_Exception(int[] inputArray, int index)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveByIndex(index);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, -1)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4)]
        public void RemoveByIndex_WhenIndexOutOfRange_IndexOutOfRangeException(int[] inputArray, int index)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveByIndex(index);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 8, 10 }, 8)]
        [TestCase(new int[0], 0)]
        [TestCase(new int[] { 1 }, 1)]
        public void GetLengthTest(int[] inputArray, int expected)
        {
            SetUpOneList(inputArray);
            int actual = this.actual.GetLength();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 3, new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 0, new int[] { 1, 2, 3, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, 16, new int[0])]
        [TestCase(new int[] { 1 }, 1, new int[0])]
        public void RemoveLastValuesTest(int[] inputArray, int count, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.RemoveLastValues(count);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, -5)]
        public void RemoveLastValues_WhenCountArgumentIsLessThanZero_ArgumentException(int[] inputArray, int count)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveLastValues(count);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[0], 0)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 10)]
        public void RemoveLastValues_WhenListIsLessThanCountOrEmpty_Exception(int[] inputArray, int count)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveLastValues(count);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 3, new int[] { 4 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4, new int[0])]
        [TestCase(new int[] { 1, 2, 3, 4 }, 0, new int[] { 1, 2, 3, 4})]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, 16, new int[0])]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, 8, new int[] { 88, 69, 78, 555, 23, 56, 74, 11 })]
        [TestCase(new int[] { 1 }, 1, new int[0])]
        public void RemoveFirstValuesTest(int[] inputArray, int count, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.RemoveFirstValues(count);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, -5)]
        public void RemoveFirstValues_WhenCountArgumentIsLessThanZero_ArgumentException(int[] inputArray, int count)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveFirstValues(count);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[0], 10)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 10)]
        public void RemoveFirstValues_WhenListIsLessThanCountOrEmpty_Exception(int[] inputArray, int count)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveFirstValues(count);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 0, 3, new int[] { 4 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 1, 3, new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 2, 2, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 3, 1, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 3, 0, new int[] { 1, 2, 3, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 0, 4, new int[0])]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, 2, 13, new int[] { 1, 2, 11 })]
        [TestCase(new int[] { 1 }, 0, 1, new int[0])]
        public void RemoveValuesByIndexTest(int[] inputArray, int index, int count, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.RemoveValuesByIndex(index, count);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0], 0, 0)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 1, -1)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 1, 4)]
        public void RemoveValuesByIndex_WhenIndexOutOfRange_IndexOutOfRangeException(int[] inputArray, int count, int index)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveValuesByIndex(count, index);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, -5, 0)]
        public void RemoveValuesByIndex_WhenCountArgumentIsLessThanZero_ArgumentException(int[] inputArray, int count, int index)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveValuesByIndex(count, index);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 20, 0)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 3, 2)]
        [TestCase(new int[0], 10, 0)]
        public void RemoveValuesByIndex_WhenSubArrayIsLessThanCountOrArrayIsEmpty_Exception(int[] inputArray, int count, int index)
        {
            SetUpOneList(inputArray);
            try
            {
                actual.RemoveValuesByIndex(count, index);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        //тест на доступ по индексу
        [TestCase(new int[] { 1, 2, 3, 4 }, 0, 1)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 3, 4)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 1, 2)]
        public void GetValueByIndexTest(int[] inputArray, int index, int expected)
        {
            SetUpOneList(inputArray);
            int actual = this.actual[index];
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0], 0)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4)]
        [TestCase(new int[] { 1, 2, 3, 4 }, -4)]
        public void GetValueByIndex_WhenIndexOutOfRange_IndexOutOfRangeException(int[] inputArray, int index)
        {
            SetUpOneList(inputArray);
            try
            {
                int value = this.actual[index];
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }


        //тест изменение по индексу
        [TestCase(new int[] { 1, 2, 3, 4 }, 0, 5, new int[] { 5, 2, 3, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, 3, 100, new int[] { 1, 2, 3, 100, 5, 6 })]
        [TestCase(new int[] { 1 }, 0, 100, new int[] { 100 })]
        public void SetValueByIndexTest(int[] inputArray, int index, int value, int[] expectedList)
        {
            SetUp(inputArray, expectedList);
            this.actual[index] = value;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0], 0, 5)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4, 7)]
        [TestCase(new int[] { 1, 2, 3, 4 }, -4, 9)]
        public void SetValueByIndex_WhenIndexOutOfRange_IndexOutOfRangeException(int[] inputArray, int index, int value)
        {
            SetUpOneList(inputArray);
            try
            {
                actual[index] = value;
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 1, 2, 3, 4 }, 1, 0)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 5, -1)]
        [TestCase(new int[] { 1, 1, 1, 1, 1 }, 1, 0)]
        [TestCase(new int[] { 2, 4, 6, 7, 1 }, 1, 4)]
        [TestCase(new int[] { 5, 6, 0, 3, 0 }, 0, 2)]
        [TestCase(new int[0], 1, -1)]
        public void GetFirstIndexByValue(int[] inputArray, int value, int expected)
        {
            SetUpOneList(inputArray);
            int actual = this.actual.GetFirstIndexByValue(value);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 1, 2, 3, 4 }, new int[] { 4, 3, 2, 1, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 4, 3, 2, 1 })]
        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, new int[] { 2, 1 })]
        [TestCase(new int[0], new int[0])]
        public void ReverseTest(int[] inputArray, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.Reverse();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 1, 2, 3, 4, 4 }, 4)]
        [TestCase(new int[] { 1, 1, 90, 3, 4 }, 90)]
        [TestCase(new int[] { 0 }, 0)]
        public void GetMaxValueTest(int[] inputArray, int expected)
        {
            SetUpOneList(inputArray);
            int actual = this.actual.GetMaxValue();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0])]
        public void GetMaxValue_WhenArrayIsEmpty_Exception(int[] inputArray)
        {
            SetUpOneList(inputArray);
            try
            {
                this.actual.GetMaxValue();
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 1, 2, 3, 4 }, 4)]
        [TestCase(new int[] { 1, 1, 90, 3, -4 }, 2)]
        [TestCase(new int[] { 0 }, 0)]
        public void GetIndexOfMaxValueTest(int[] inputArray, int expected)
        {
            SetUpOneList(inputArray);
            int actual = this.actual.GetIndexOfMaxValue();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0])]
        public void GetIndexOfMaxValue_WhenArrayIsEmpty_Exception(int[] inputArray)
        {
            SetUpOneList(inputArray);
            try
            {
                this.actual.GetIndexOfMaxValue();
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 1, 2, 3, 4 }, 1)]
        [TestCase(new int[] { 1, 1, 90, 3, -4 }, -4)]
        [TestCase(new int[] { 0 }, 0)]
        public void GetMinValueTest(int[] inputArray, int expected)
        {
            SetUpOneList(inputArray);
            int actual = this.actual.GetMinValue();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0])]
        public void GetMinValue_WhenArrayIsEmpty_Exception(int[] inputArray)
        {
            SetUpOneList(inputArray);
            try
            {
                this.actual.GetMinValue();
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 1, 2, 3, 4 }, 0)]
        [TestCase(new int[] { 1, 1, 90, 3, -4 }, 4)]
        [TestCase(new int[] { 0 }, 0)]
        public void GetIndexOfMinValueTest(int[] inputArray, int expected)
        {
            SetUpOneList(inputArray);
            int actual = this.actual.GetIndexOfMinValue();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0])]
        public void GetIndexOfMinValue_WhenArrayIsEmpty_Exception(int[] inputArray)
        {
            SetUpOneList(inputArray);
            try
            {
                this.actual.GetIndexOfMinValue();
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 10, 1, 100, 3, -4 }, new int[] { -4, 1, 3, 10, 100 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11, 11 }, new int[] { 1, 2, 3, 4, 11, 11, 14, 15, 23, 45, 56, 69, 74, 78, 88, 99, 555 })]
        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, new int[] { 1, 2 })]
        [TestCase(new int[0], new int[0])]
        public void SortAscendingTest(int[] inputArray, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.SortAscending();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 10, 1, 100, 3, -4 }, new int[] { 100, 10, 3, 1, -4 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, new int[] { 555, 99, 88, 78, 74, 69, 56, 45, 23, 15, 14, 11, 4, 3, 2, 1 })]
        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[0], new int[0])]
        public void SortDescendingTest(int[] inputArray, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.SortDescending();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 1, 2, 3, 4 }, 1, 0)]
        [TestCase(new int[] { 1, 1, 2, 3, 4 }, 15, -1)]
        [TestCase(new int[] { 1, 1, 90, 3, -4 }, -4, 4)]
        [TestCase(new int[] { 1, 55 }, 55, 1)]
        [TestCase(new int[] { 1, 55 }, 1, 0)]
        [TestCase(new int[] { 0 }, 0, 0)]
        [TestCase(new int[0], 10, -1)]
        public void RemoveFirstByValueTest(int[] inputArray, int value, int expected)
        {
            SetUpOneList(inputArray);
            int actual = this.actual.RemoveFirstByValue(value);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 1, 2, 3, 4 }, 1, 2)]
        [TestCase(new int[] { 1, 1, 2, 3, 4, 1 }, 1, 3)]
        [TestCase(new int[] { 1, 1, 1, 1 }, 1, 4)]
        [TestCase(new int[] { 1, 1, 90, 3, -4 }, 100, 0)]
        [TestCase(new int[] { 1, 55 }, 55, 1)]
        [TestCase(new int[] { 1, 55 }, 1, 1)]
        [TestCase(new int[] { 0 }, 0, 1)]
        [TestCase(new int[0], 10, 0)]
        public void RemoveAllByValueTest(int[] inputArray, int value, int expected)
        {
            SetUpOneList(inputArray);
            int actual = this.actual.RemoveAllByValue(value);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0], new int[] { -4, 1, 3, 10, 100 }, new int[] { -4, 1, 3, 10, 100 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, new int[0], new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 })]
        [TestCase(new int[] { 1 }, new int[] { 1, 8, 9, 10 }, new int[] { 1, 1, 8, 9, 10 })]
        [TestCase(new int[0], new int[0], new int[0])]
        [TestCase(new int[] { 1, 2, 3 }, new int[0], new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 15 }, new int[] { 12 }, new int[] { 15, 12 })]
        public void AddListTest(int[] inputArray, int[] additionalArray, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            ILinkedList additionalList = SetUpAdditionalList(additionalArray);
            actual.AddList(additionalList);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4 })]
        public void AddList_WhenArgumentIsNull_NullReferenceException(int[] inputArray)
        {
            SetUpOneList(inputArray);
            try
            {

                actual.AddList(null);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[0], new int[] { -4, 1, 3, 10, 100 }, new int[] { -4, 1, 3, 10, 100 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, new int[0], new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 })]
        [TestCase(new int[] { 1 }, new int[] { 1, 8, 9, 10 }, new int[] { 1, 8, 9, 10, 1 })]
        [TestCase(new int[] { 1 }, new int[] { 8 }, new int[] { 8, 1 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[0], new int[] { 1, 2, 3 })]
        [TestCase(new int[0], new int[0], new int[0])]
        public void AddListAtBeginningTest(int[] inputArray, int[] additionalArray, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            ILinkedList additionalList = SetUpAdditionalList(additionalArray);
            actual.AddListAtBeginning(additionalList);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4 })]
        public void AddListAtBeginning_WhenArgumentIsNull_NullReferenceException(int[] inputArray)
        {
            SetUpOneList(inputArray);
            try
            {

                actual.AddListAtBeginning(null);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[0], new int[] { -4, 1, 3, 10, 100 }, 0, new int[] { -4, 1, 3, 10, 100 })]
        [TestCase(new int[] { -4, 1, 3, 10, 100 }, new int[] { 600, 700 }, 0, new int[] { 600, 700, -4, 1, 3, 10, 100 })]
        [TestCase(new int[] { -4, 1, 3, 10, 100 }, new int[] { 600, 700 }, 1, new int[] { -4, 600, 700, 1, 3, 10, 100 })]
        [TestCase(new int[] { -4, 1, 3, 10, 100 }, new int[] { 600, 700 }, 2, new int[] { -4, 1, 600, 700, 3, 10, 100 })]
        [TestCase(new int[] { -4, 1, 3, 10, 100 }, new int[] { 600, 700 }, 5, new int[] { -4, 1, 3, 10, 100, 600, 700 })]
        [TestCase(new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 }, new int[0], 5, new int[] { 1, 2, 3, 4, 99, 15, 14, 45, 88, 69, 78, 555, 23, 56, 74, 11 })]
        [TestCase(new int[] { 1 }, new int[] { 1, 8, 9, 10 }, 0, new int[] { 1, 8, 9, 10, 1 })]
        [TestCase(new int[0], new int[0], 0, new int[0])]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7 }, new int[] { 3, 3, 3 }, 3, new int[] { 1, 2, 3, 3, 3, 3, 4, 5, 6, 7 })]
        public void AddListByIndexTest(int[] inputArray, int[] additionalArray, int index, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            ILinkedList additionalList = SetUpAdditionalList(additionalArray);
            actual.AddListByIndex(additionalList, index);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2 }, 5)]
        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2 }, -4)]
        public void AddListByIndex_WhenIndexOutOfRange_IndexOutOfRangeException(int[] inputArray, int[] additionalArray, int index)
        {
            SetUpOneList(inputArray);
            try
            {
                ILinkedList additionalList = SetUpAdditionalList(additionalArray);
                actual.AddListByIndex(additionalList, index);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 0)]
        public void AddListByIndex_WhenArgumentIsNull_NullReferenceException(int[] inputArray, int index)
        {
            SetUpOneList(inputArray);
            try
            {

                actual.AddListByIndex(null, index);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 100, 99, 88, 77, 5, 4, 3, 2, 1, 100, 200 })]
        public void AllMethodsTest(int[] inputArray, int[] expectedArray)
        {
            SetUp(inputArray, expectedArray);
            actual.Add(8);
            actual.AddAtFirstIndex(100);
            actual.AddAtIndex(20, 3);
            actual.Remove();
            actual.RemoveAtFirstIndex();
            actual.RemoveByIndex(1);
            actual.Add(9);
            actual.RemoveLastValues(3);
            actual.AddAtFirstIndex(100);
            actual.AddAtFirstIndex(25);
            actual.RemoveFirstValues(2);
            actual.AddListByIndex(SetUpAdditionalList(new int[] { 2, 3, 4, 5 }), 1);
            actual.RemoveValuesByIndex(5, 2);
            actual.Reverse();
            actual.SortAscending();
            actual.SortDescending();
            actual.AddList(SetUpAdditionalList(new int[] { 100, 200 }));
            actual.AddListAtBeginning(SetUpAdditionalList(new int[] { 99, 88, 77 }));
            actual.AddAtFirstIndex(100);

            //actual.AddListByIndex(SetUpAdditionalList(new int[] { 88, 96 }), 1);
            //actual.Add(8);
            //actual.Remove();
            //actual.AddAtFirstIndex(100);
            //actual.Add(8);
            //actual.Remove();
            //actual.RemoveAtFirstIndex();
            //actual.Add(8);
            //actual.RemoveFirstValues(2);
            //actual.AddList(SetUpAdditionalList(new int[] { 100, 200 }));
            //actual.Reverse(); 200, 100, 8, 5, 4, 3, 2, 96

            Assert.AreEqual(expected, actual);
        }

    }
}
