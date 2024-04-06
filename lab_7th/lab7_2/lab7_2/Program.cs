using System;

abstract class Discipline
{
    protected string disciplineName;
    protected Participant[] participants;
    protected int participantCount;

    public Discipline(string name, int maxParticipants)
    {
        disciplineName = name;
        participants = new Participant[maxParticipants];
        participantCount = 0;
    }

    public void AddParticipant(Participant participant)
    {
        if (participantCount < participants.Length)
        {
            participants[participantCount] = participant;
            participantCount++;
        }
        else
        {
            Console.WriteLine("Достигнуто максимальное количество участников.");
        }
    }

    public abstract void PrintHeader();

    public void PrintParticipants()
    {
        foreach (var participant in participants)
        {
            if (participant != null)
                participant.Print();
        }
        Console.WriteLine("=============================================");
    }
}

class LongJump : Discipline
{
    public LongJump(string name, int maxParticipants) : base(name, maxParticipants) { }

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
    public HighJump(string name, int maxParticipants) : base(name, maxParticipants) { }

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
        LongJump longJump = new LongJump("прыжкам в длину", 5);
        HighJump highJump = new HighJump("прыжкам в высоту", 5);

        longJump.AddParticipant(new Participant("Попов", new double[] { 4.5, 4.6, 4.7 }));
        longJump.AddParticipant(new Participant("Иванов", new double[] { 4.8, 4.9, 5.0 }));
        longJump.AddParticipant(new Participant("Сидоров", new double[] { 5.1, 5.2, 5.3 }));
        longJump.AddParticipant(new Participant("Петров", new double[] { 5.4, 5.5, 5.6 }));
        longJump.AddParticipant(new Participant("Смирнов", new double[] { 5.7, 5.8, 5.9 }));

        highJump.AddParticipant(new Participant("Попов", new double[] { 1.5, 1.6, 1.7 }));
        highJump.AddParticipant(new Participant("Иванов", new double[] { 1.8, 1.9, 2.0 }));
        highJump.AddParticipant(new Participant("Сидоров", new double[] { 2.1, 2.2, 2.3 }));
        highJump.AddParticipant(new Participant("Петров", new double[] { 2.4, 2.5, 2.6 }));
        highJump.AddParticipant(new Participant("Смирнов", new double[] { 2.7, 2.8, 2.9 }));

        longJump.PrintHeader();
        longJump.PrintParticipants();

        highJump.PrintHeader();
        highJump.PrintParticipants();
    }
}
