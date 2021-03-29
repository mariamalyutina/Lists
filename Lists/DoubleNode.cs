

namespace Lists
{
    class DoubleNode
    {
        public int Value { get; set; }

        public DoubleNode Next { get; set; } //ссылка на след Node

        public DoubleNode Prev { get; set; }

        public DoubleNode(int value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
    }
}
