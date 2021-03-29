

namespace Lists
{
    class Node
    {
        public int Value { get; set; }

        public Node Next { get; set; } //ссылка на след Node

        public Node(int value)
        {
            Value = value;
            Next = null;
        }
    }
}
