using CSharpExercises.Part2;
using CSharpExercises.Part3;

Console.WriteLine("Part 1:");
Console.WriteLine("\nExercise1:");
Exercise1.Run();

Console.WriteLine("\nExercise2:");
Exercise2.Run();

Console.WriteLine("\nExercise3:");
Exercise3.Run();

Console.WriteLine("\nPart 2:");
LinkedList list = new();

list.Prepend(1);
list.Prepend(3);
list.Prepend(5);
list.Append(2);
list.Append(4);
list.Append(7);
list.Pop();
list.Unqueue();

list.Sort();

Console.Write("\nsorted list:");
Node current = list.Head;
while (current != null)
{
    Console.Write(current.Value + " ");
    current = current.Next;
}
Console.WriteLine();

Console.WriteLine($"Is Circular: {list.IsCircular()}");

Node max = list.GetMaxNode();
Console.WriteLine($"Max: {max.Value}");

Node min = list.GetMinNode();
Console.WriteLine($"Min: {min.Value}");