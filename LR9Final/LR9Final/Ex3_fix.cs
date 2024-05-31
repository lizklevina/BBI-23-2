using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using Serializator3;

// Абстрактный класс для сериализации и десериализации
class Country
{
    protected Dictionary<string, int> responses;

    public Dictionary<string, int> Responses => responses;

    public Country()
    {
        responses = new Dictionary<string, int>();
    }

    public virtual void FillResponses(string question)
    {
        Console.WriteLine($"Введите ответы на вопрос: {question} (для завершения введите 'done')");

        while (true)
        {
            Console.Write("Ответ: ");
            string answer = Console.ReadLine().Trim().ToLower();

            if (answer == "done")
                break;

            if (string.IsNullOrEmpty(answer))
            {
                Console.WriteLine("Ответ не может быть пустым. Повторите ввод.");
                continue;
            }

            if (responses.ContainsKey(answer))
                responses[answer]++;
            else
                responses[answer] = 1;
        }
    }

    public virtual void PrintTopResponses(string question)
    {
        Console.WriteLine($"\nТоп-5 наиболее часто встречающихся ответов на вопрос: {question}");

        if (responses.Count == 0)
        {
            Console.WriteLine("Ответы отсутствуют.");
            return;
        }

        var sortedResponses = responses.OrderByDescending(x => x.Value).Take(5);

        int totalResponses = responses.Sum(x => x.Value);
        foreach (var response in sortedResponses)
        {
            double percentage = (double)response.Value / totalResponses * 100;
            Console.WriteLine($"{response.Key}: {response.Value} ({percentage:f2}%)");
        }
    }
}

class Russia : Country
{
    public Russia()
    {
    }
}

class Japan : Country
{
    public Japan()
    {
    }
}

class RadioPoll
{
    static void Main()
    {
        // Создаем папку для хранения файлов
        string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PollData");
        Directory.CreateDirectory(dataDirectory);

        // Массивы вопросов
        string[] questions = {
            "Какое животное Вы связываете с Россией и русскими?",
            "Какую черту характера Вы связываете с Россией и русскими?",
            "Какой неодушевленный предмет Вы связываете с Россией и русскими?",
            "Какое животное Вы связываете с Японией и японцами?",
            "Какую черту характера Вы связываете с Японией и японцами?",
            "Какой неодушевленный предмет Вы связываете с Японией и японцами?"
        };

        // Массивы ответов для каждого вопроса
        string[] animalRussia = { "Слон", "Медведь", "Волк" };
        string[] characterRussia = { "Душа", "Сила", "Гостеприимство" };
        string[] objectRussia = { "Балалайка", "Водка", "Матрешка" };
        string[] animalJapan = { "Самурай", "Лиса", "Краб" };
        string[] characterJapan = { "Вежливость", "Сдержанность", "Уважение" };
        string[] objectJapan = { "Кимоно", "Суши", "Японский сад" };

        Russia russiaPoll = new Russia();
        Japan japanPoll = new Japan();

        // Россия
        russiaPoll.FillResponses(questions[0]);
        russiaPoll.PrintTopResponses(questions[0]);
        SavePollData(russiaPoll, Path.Combine(dataDirectory, "russia_animal.json"));
        russiaPoll.Responses.Clear(); // Очищаем словарь ответов

        russiaPoll.FillResponses(questions[1]);
        russiaPoll.PrintTopResponses(questions[1]);
        SavePollData(russiaPoll, Path.Combine(dataDirectory, "russia_character.json"));
        russiaPoll.Responses.Clear(); // Очищаем словарь ответов

        russiaPoll.FillResponses(questions[2]);
        russiaPoll.PrintTopResponses(questions[2]);
        SavePollData(russiaPoll, Path.Combine(dataDirectory, "russia_object.json"));
        russiaPoll.Responses.Clear(); // Очищаем словарь ответов

        // Япония
        japanPoll.FillResponses(questions[3]);
        japanPoll.PrintTopResponses(questions[3]);
        SavePollData(japanPoll, Path.Combine(dataDirectory, "japan_animal.json"));
        japanPoll.Responses.Clear(); // Очищаем словарь ответов

        japanPoll.FillResponses(questions[4]);
        japanPoll.PrintTopResponses(questions[4]);
        SavePollData(japanPoll, Path.Combine(dataDirectory, "japan_character.json"));
        japanPoll.Responses.Clear(); // Очищаем словарь ответов

        japanPoll.FillResponses(questions[5]);
        japanPoll.PrintTopResponses(questions[5]);
        SavePollData(japanPoll, Path.Combine(dataDirectory, "japan_object.json"));
        japanPoll.Responses.Clear(); // Очищаем словарь ответов

        // CombinePollResults(russiaPoll, japanPoll, combinedAnswers);
    }

    static void SavePollData(Country country, string filePath)
    {
        Serializator3.JsonSerializer serializer = new Serializator3.JsonSerializer(filePath);
        serializer.Serialize(country.Responses);
    }


    static void PrintTopResponses(Dictionary<string, int> responses, string question)
    {
        Console.WriteLine($"\nТоп-5 наиболее часто встречающихся ответов на вопрос: {question}");

        if (responses.Count == 0)
        {
            Console.WriteLine("Ответы отсутствуют.");
            return;
        }

        var sortedResponses = responses.OrderByDescending(x => x.Value).Take(5);

        int totalResponses = responses.Sum(x => x.Value);
        foreach (var response in sortedResponses)
        {
            double percentage = (double)response.Value / totalResponses * 100;
            Console.WriteLine($"{response.Key}: {response.Value} ({percentage:f2}%)");
        }
    }
}
