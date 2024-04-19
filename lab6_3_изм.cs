using System;
using System.Collections.Generic;
using System.Linq;

struct PollQuestion
{
    private string question;
    private Dictionary<string, int> responses;
    private int totalVotes; 

    public string Question => question;
    public Dictionary<string, int> Responses => responses;
    public int TotalVotes => totalVotes; 

    public PollQuestion(string question)
    {
        this.question = question;
        responses = new Dictionary<string, int>();
        totalVotes = 0; 
    }

    public void AddVote(string response)
    {
        if (responses.ContainsKey(response))
            responses[response]++;
        else
            responses[response] = 1;

        totalVotes++; 
    }
}

class RadioPoll
{
    static void Main()
    {
        PollQuestion animalPoll = new PollQuestion("Какое животное Вы связываете с Японией и японцами?");
        PollQuestion characterTraitPoll = new PollQuestion("Какую черту характера Вы связываете с Японией и японцами?");
        PollQuestion objectPoll = new PollQuestion("Какой неодушевленный предмет Вы связываете с Японией и японцами?");

        FillResponses(animalPoll, animalPoll.Question);
        FillResponses(characterTraitPoll, characterTraitPoll.Question);
        FillResponses(objectPoll, objectPoll.Question);

        PrintTopResponses(animalPoll.Responses, animalPoll.Question, animalPoll.TotalVotes);
        PrintTopResponses(characterTraitPoll.Responses, characterTraitPoll.Question, characterTraitPoll.TotalVotes);
        PrintTopResponses(objectPoll.Responses, objectPoll.Question, objectPoll.TotalVotes);
    }

    static void FillResponses(PollQuestion pollQuestion, string question)
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

            pollQuestion.AddVote(answer);
        }
    }

    static void PrintTopResponses(Dictionary<string, int> responses, string question, int totalVotes)
    {
        Console.WriteLine($"\nТоп-5 наиболее часто встречающихся ответов на вопрос: {question}");

        if (responses.Count == 0)
        {
            Console.WriteLine("Ответы отсутствуют.");
            return;
        }

        var sortedResponses = responses.OrderByDescending(x => x.Value).Take(5);

        foreach (var response in sortedResponses)
        {
            double percentage = (double)response.Value / totalVotes * 100;
            Console.WriteLine($"{response.Key}: {response.Value} ({percentage:f2}%)");
        }
    }
}