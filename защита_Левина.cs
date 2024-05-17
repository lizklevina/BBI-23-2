using System;

abstract class Runner
{
    protected string lastName;
    protected string group;
    protected string teacherLastName;
    protected double result;

    public double Result => result;

    public Runner(string lastName, string group, string teacherLastName, double result)
    {
        this.lastName = lastName;
        this.group = group;
        this.teacherLastName = teacherLastName;
        this.result = result;
    }

    public abstract void Print();

    public static void QuickSort(Runner[] array, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(array, left, right);
            QuickSort(array, left, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, right);
        }
    }

    private static int Partition(Runner[] array, int left, int right)
    {
        double pivot = array[right].Result;
        int i = left - 1;
        for (int j = left; j < right; j++)
        {
            if (array[j].Result <= pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }
        Swap(array, i + 1, right);
        return i + 1;
    }

    private static void Swap(Runner[] array, int a, int b)
    {
        Runner temp = array[a];
        array[a] = array[b];
        array[b] = temp;
    }
}

class Runner100m : Runner
{
    public Runner100m(string lastName, string group, string teacherLastName, double result)
        : base(lastName, group, teacherLastName, result)
    {
    }

    public override void Print()
    {
        Console.WriteLine($"Фамилия: {lastName} | Группа: {group} | Преподаватель: {teacherLastName} | Результат (100 м): {result}");
    }
}

class Runner500m : Runner
{
    public Runner500m(string lastName, string group, string teacherLastName, double result)
        : base(lastName, group, teacherLastName, result)
    {
    }

    public override void Print()
    {
        Console.WriteLine($"Фамилия: {lastName} | Группа: {group} | Преподаватель: {teacherLastName} | Результат (500 м): {result}");
    }
}

class Program
{
    static void Main()
    {
        Runner[] participants = new Runner[5];
        participants[0] = new Runner500m("Попова", "Группа 1", "Иванов", 210);
        participants[1] = new Runner100m("Иванова", "Группа 2", "Петров", 180);
        participants[2] = new Runner100m("Сидорова", "Группа 1", "Смирнов", 195);
        participants[3] = new Runner500m("Петрова", "Группа 3", "Козлов", 220);
        participants[4] = new Runner100m("Смирнова", "Группа 2", "Васильев", 190);

        Runner.QuickSort(participants, 0, participants.Length - 1);

        Console.WriteLine("\nРезультаты забега:");
        Console.WriteLine("=============================================");
        Console.WriteLine("Фамилия\t\tГруппа\t\tПреподаватель\t\tРезультат");
        Console.WriteLine("=============================================");

        foreach (var participant in participants)
        {
            participant.Print();
        }

        Console.WriteLine("=============================================");
    }
}
