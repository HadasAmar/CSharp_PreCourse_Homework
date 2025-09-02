namespace CSharpExercises.Part3
{
    public class LinkedList
    {
        public Node Head { get; set; }
        public Node Tail { get; set; }
        public Node Max { get; set; } = null;
        public Node Min { get; set; } = null;

        //Add new node to the beginning O(1)
        public void Prepend(int value)
        {
            Node newNode = new(value) { Next = Head };
            Head = newNode;

            if (Tail == null)
                Tail = Head;

            //Update max and min
            if (Max == null || value > Max.Value)
                Max = newNode;

            if (Min == null || value < Min.Value)
                Min = newNode;
        }

        //Remove the last node and return his value O(n)
        public int Pop()
        {
            if (Head == null)
                throw new InvalidOperationException("List is empty");

            if (Head.Next == null)
            {
                int val = Head.Value;
                Head = Tail = Max = Min = null;
                return val;
            }

            Node current = Head;
            while (current.Next.Next != null)
            {
                current = current.Next;
            }

            int lastVal = current.Next.Value;
            current.Next = null;
            Tail = current;

            //Update max and min
            if (lastVal == Max.Value)
                UpdateMaxNode();

            if (lastVal == Min.Value)
                UpdateMinNode();

            return lastVal;
        }

        //Add new node to the end
        public void Append(int value)
        {
            Node newNode = new(value);

            if (Head == null)
            {
                Head = Tail = newNode;
            }

            else
            {
                Tail.Next = newNode;
                Tail = newNode;
            }

            //Update max and min
            if (Max == null || value > Max.Value)
                Max = newNode;

            if (Min == null || value < Min.Value)
                Min = newNode;
        }

        //Remove the first node and return his value
        public int Unqueue()
        {
            if (Head == null)
                throw new InvalidOperationException("List is empty");

            int val = Head.Value;
            if (Head == Tail)
                Head = Tail = Max = Min = null;
            else
                Head = Head.Next;

            //Update max and min
            if (val == Max.Value)
                UpdateMaxNode();

            if (val == Min.Value)
                UpdateMinNode();

            return val;
        }

        //Return the list as IEnumerable collection
        public IEnumerable<int> ToList()
        {
            List<int> list = new();
            Node current = Head;
            while (current != null)
            {
                list.Add(current.Value);
                current = current.Next;
            }

            return list;
        }

        //Check if the list has circular loop
        public bool IsCircular()
        {
            if (Head == null)
                return false;

            Node oneStep = Head, twoStep = Head.Next;
            while (twoStep != null && twoStep.Next != null)
            {
                oneStep = oneStep.Next;
                twoStep = twoStep.Next.Next;

                if (oneStep == twoStep)
                    return true;
            }
            return false;
        }

        //Sort the list in ascending order
        public void Sort()
        {
            List<int> list = ToList().ToList();

            list.Sort();

            Head = Tail = Max = Min = null;
            foreach (int val in list)
                Append(val);
        }

        //Return the node with maximum value O(1)
        public Node GetMaxNode()
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty");
            return Max;
        }

        //Return the node with minimum value O(1)
        public Node GetMinNode()
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty");
            return Min;
        }

        //Update the max pointer after removing the max node
        private void UpdateMaxNode()
        {
            Max = null;
            Node current = Head;
            while (current != null)
            {
                if (Max == null || current.Value > Max.Value)
                    Max = current;

                current = current.Next;
            }
        }

        //Update the min pointer after removing the min node
        private void UpdateMinNode()
        {
            Min = null;
            Node current = Head;
            while (current != null)
            {
                if (Min == null || current.Value < Min.Value)
                    Min = current;

                current = current.Next;
            }

        }
    }
}