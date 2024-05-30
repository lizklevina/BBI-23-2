// See https://aka.ms/new-console-template for more information
using SerializationLibrary;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
public class BaseRunner
{
    [JsonInclude]
    public string lastName;
    [JsonInclude]
    public string group;
    [JsonInclude]
    public string teacherLastName;
    [JsonInclude]
    public double result;

    public double Result => result;

    public BaseRunner() { } // Безпараметрический конструктор

    public BaseRunner(string lastName, string group, string teacherLastName, double result)
    {
        this.lastName = lastName;
        this.group = group;
        this.teacherLastName = teacherLastName;
        this.result = result;

    }
}
public class Runner: BaseRunner
{
    public Runner() { }
    public Runner(string lastName, string group, string teacherLastName, double result)
    {
        this.lastName = lastName;
        this.group = group;
        this.teacherLastName = teacherLastName;
        this.result = result;
    }

    
}

public class Runner100m : Runner
{
    
    public Runner100m(string lastName, string group, string teacherLastName, double result)
        : base(lastName, group, teacherLastName, result)
    {
    }

    public  void Print()
    {
        Console.WriteLine($"Фамилия: {lastName} | Группа: {group} | Преподаватель: {teacherLastName} | Результат (100 м): {result}");
    }
}

public class Runner500m : Runner
{
    public Runner500m(string lastName, string group, string teacherLastName, double result)
        : base(lastName, group, teacherLastName, result)
    {
    }

    public void Print()
    {
        Console.WriteLine($"Фамилия: {lastName} | Группа: {group} | Преподаватель: {teacherLastName} | Результат (500 м): {result}");
    }
}



class Program
{
    static void Main()
    {
        Runner[] participants100 = new Runner[5];
        participants100[0] = new Runner100m("Попова", "Группа 1", "Иванов", 210);
        participants100[1] = new Runner100m("Иванова", "Группа 2", "Петров", 180);
        participants100[2] = new Runner100m("Сидорова", "Группа 1", "Смирнов", 195);
        participants100[3] = new Runner100m("Петрова", "Группа 3", "Козлов", 220);
        participants100[4] = new Runner100m("Смирнова", "Группа 2", "Васильев", 190);

        Runner[] participants500 = new Runner[5];
        participants500[0] = new Runner500m("Астафьев", "Группа 1", "Иванов", 310);
        participants500[1] = new Runner500m("Солженицин", "Группа 2", "Петров", 280);
        participants500[2] = new Runner500m("Гоголь", "Группа 1", "Смирнов", 295);
        participants500[3] = new Runner500m("Лермонтов", "Группа 3", "Козлов", 330);
        participants500[4] = new Runner500m("Пушкин", "Группа 2", "Васильев", 320);



        Array.Sort(participants100, (x, y) => x.Result.CompareTo(y.Result));
        

        Console.WriteLine("\nРезультаты забега:");
        Console.WriteLine("=============================================");
        Console.WriteLine("Фамилия\t\tГруппа\t\tПреподаватель\t\tРезультат");
        Console.WriteLine("=============================================");

        // Создаем папку для сохранения файлов
        string outputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RunnerData");
        Directory.CreateDirectory(outputDirectory);

        // Сериализатор
        MySerializer serializer = new JsonMySerializer();

        // Сохраняем данные в JSON-файлы
        for (int i = 0; i < participants100.Length; i++)
        {
            string fileName = $"runner_{i + 1}.json";
            string filePath = Path.Combine(outputDirectory, fileName);
            serializer.Write(participants100[i], filePath);     
        }

        for (int i = 0;i < participants500.Length; i++)
        {
            string fileName1 = $"runners_{i}.json";
            string filePath1 = Path.Combine(outputDirectory, fileName1);
            serializer.Write(participants500[i], filePath1);
        }

        // Выводим данные из файлов
        Console.WriteLine("\nДанные из файлов:");
        Console.WriteLine("=============================================");

        for (int i = 0; i < participants100.Length; i++)
        {
            string fileName = $"runner_{i + 1}.json";
            string filePath = Path.Combine(outputDirectory, fileName);
            Runner readRunner = serializer.Read<Runner>(filePath);
            Console.WriteLine($"Фамилия: {readRunner.lastName} | Группа: {readRunner.group} | Преподаватель: {readRunner.teacherLastName} | Результат (100 м): {readRunner.Result}");
            
        }
        Console.WriteLine("=============================================");
        for (int i = 0; i < participants500.Length; i++)
        {
            string fileName1 = $"runners_{i}.json";
            string filePath1 = Path.Combine(outputDirectory, fileName1);
            Runner readRunner1 = serializer.Read<Runner>(filePath1);
            Console.WriteLine($"Фамилия: {readRunner1.lastName} | Группа: {readRunner1.group} | Преподаватель: {readRunner1.teacherLastName} | Результат (500 м): {readRunner1.Result}"); ;
        }

        Console.WriteLine("=============================================");
    }
}
