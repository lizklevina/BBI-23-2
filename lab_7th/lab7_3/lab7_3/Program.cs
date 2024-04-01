using System;
using System.Collections.Generic;
using System.Linq;

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
        Russia russiaPoll = new Russia();
        Japan japanPoll = new Japan();

        russiaPoll.FillResponses("Какое животное Вы связываете с Россией и русскими?");
        russiaPoll.PrintTopResponses("Животное");

        japanPoll.FillResponses("Какое животное Вы связываете с Японией и японцами?");
        japanPoll.PrintTopResponses("Животное");

        russiaPoll.FillResponses("Какую черту характера Вы связываете с Россией и русскими?");
        russiaPoll.PrintTopResponses("Черта характера");

        japanPoll.FillResponses("Какую черту характера Вы связываете с Японией и японцами?");
        japanPoll.PrintTopResponses("Черта характера");

        russiaPoll.FillResponses("Какой неодушевленный предмет Вы связываете с Россией и русскими?");
        russiaPoll.PrintTopResponses("Неодушевленный предмет");

        japanPoll.FillResponses("Какой неодушевленный предмет Вы связываете с Японией и японцами?");
        japanPoll.PrintTopResponses("Неодушевленный предмет");

        CombinePollResults(russiaPoll, japanPoll);
    }

    static void CombinePollResults(Russia russia, Japan japan)
    {
        Console.WriteLine("\nРезультаты опроса по обеим странам вместе взятым:");

        Dictionary<string, int> combinedResponses = new Dictionary<string, int>();

        foreach (var kvp in russia.Responses)
        {
            combinedResponses.TryGetValue(kvp.Key, out int count);
            combinedResponses[kvp.Key] = count + kvp.Value;
        }

        foreach (var kvp in japan.Responses)
        {
            combinedResponses.TryGetValue(kvp.Key, out int count);
            combinedResponses[kvp.Key] = count + kvp.Value;
        }

        PrintTopResponses(combinedResponses, "Ответы по обеим странам");
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
