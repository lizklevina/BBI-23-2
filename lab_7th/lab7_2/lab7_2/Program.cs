using System;

abstract class Discipline
{
    protected string disciplineName;

    public Discipline(string name)
    {
        disciplineName = name;
    }

    public abstract void PrintHeader();
}

class LongJump : Discipline
{
    public LongJump(string name) : base(name) { }

    public override void PrintHeader()
    {
        Console.WriteLine($"\nПротокол соревнований по {disciplineName}:");
        Console.WriteLine("=============================================");
        Console.WriteLine("Фамилия\t\tЛучший результат");
        Console.WriteLine("=============================================");
    }
}

class HighJump : Discipline
{
    public HighJump(string name) : base(name) { }

    public override void PrintHeader()
    {
        Console.WriteLine($"\nПротокол соревнований по {disciplineName}:");
        Console.WriteLine("=============================================");
        Console.WriteLine("Фамилия\t\tЛучший результат");
        Console.WriteLine("=============================================");
    }
}

class Participant
{
    private string lastName;
    private double[] results;

    public string LastName => lastName;
    public double[] Results => results;

    public Participant(string lastName, double[] results)
    {
        this.lastName = lastName;
        this.results = results;
    }

    public double BestResult()
    {
        double bestResult = results[0];
        for (int i = 1; i < results.Length; i++)
        {
            if (results[i] > bestResult)
            {
                bestResult = results[i];
            }
        }
        return bestResult;
    }

    public void Print()
    {
        Console.WriteLine($"Фамилия: {LastName} | Лучший результат: {BestResult()}");
    }
}

class Program
{
    static void Main()
    {
        Discipline longJump = new LongJump("прыжкам в длину");
        Discipline highJump = new HighJump("прыжкам в высоту");

        Participant[] participants = new Participant[5];
        participants[0] = new Participant("Попов", new double[] { 4.5, 4.6, 4.7 });
        participants[1] = new Participant("Иванов", new double[] { 4.8, 4.9, 5.0 });
        participants[2] = new Participant("Сидоров", new double[] { 5.1, 5.2, 5.3 });
        participants[3] = new Participant("Петров", new double[] { 5.4, 5.5, 5.6 });
        participants[4] = new Participant("Смирнов", new double[] { 5.7, 5.8, 5.9 });

        longJump.PrintHeader();
        foreach (var participant in participants)
        {
            participant.Print();
        }
        Console.WriteLine("=============================================");

        highJump.PrintHeader();
        foreach (var participant in participants)
        {
            participant.Print();
        }
        Console.WriteLine("=============================================");
    }
}
