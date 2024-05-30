using SerializationLibrary;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LR9Final
{
    
    public enum DisciplineType
    {
        LongJump,
        HighJump
    }

    public class Participant
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
    public class Discipline
    {
        [JsonInclude]
        public string disciplineName;
        [JsonInclude]
        public Participant[] participants;
        [JsonInclude]
        public int participantCount;
        [JsonConstructor]
        public Discipline() { }
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

        

        public void PrintParticipants()
        {
            foreach (var participant in participants)
            {
                if (participant != null)
                    participant.Print();
            }
            Console.WriteLine("=============================================");
        }

        [JsonInclude]
        public DisciplineType DisciplineType { get; set; }
    }

    class LongJump : Discipline
    {
        [JsonConstructor]
        public LongJump() { }
        
        public LongJump(string name, int maxParticipants) : base(name, maxParticipants)
        {
            DisciplineType = DisciplineType.LongJump;
        }

        public void PrintHeader()
        {
            Console.WriteLine($"\nПротокол соревнований по {disciplineName}:");
            Console.WriteLine("=============================================");
            Console.WriteLine("Фамилия\t\tЛучший результат");
            Console.WriteLine("=============================================");
        }
    }


    class HighJump : Discipline
    {
        [JsonConstructor]
        public HighJump() { }
        public HighJump(string name, int maxParticipants) : base(name, maxParticipants)
        {
            DisciplineType = DisciplineType.HighJump;
        }

        public void PrintHeader()
        {
            Console.WriteLine($"\nПротокол соревнований по {disciplineName}:");
            Console.WriteLine("=============================================");
            Console.WriteLine("Фамилия\t\tЛучший результат");
            Console.WriteLine("=============================================");
        }
    }

    

   

    class Program
    {
        static void Main()
        {
            // ... (создание участников, сортировка, сериализация) ...
            LongJump longJump = new LongJump("прыжкам в длину", 5);
            HighJump highJump = new HighJump("прыжкам в высоту", 5);

            longJump.AddParticipant(new Participant("Попов", new double[] { 4.5, 4.6, 4.7 }));
            longJump.AddParticipant(new Participant("Иванов", new double[] { 4.8, 4.9, 5.0 }));
            longJump.AddParticipant(new Participant("Сидоров", new double[] { 5.1, 5.2, 5.3 }));
            longJump.AddParticipant(new Participant("Петров", new double[] { 5.4, 5.5, 5.6 }));
            longJump.AddParticipant(new Participant("Смирнов", new double[] { 5.7, 5.8, 5.9 }));

            highJump.AddParticipant(new Participant("Тютчев", new double[] { 1.5, 1.6, 1.7 }));
            highJump.AddParticipant(new Participant("Стругакций", new double[] { 1.8, 1.9, 2.0 }));
            highJump.AddParticipant(new Participant("Глуховский", new double[] { 2.1, 2.2, 2.3 }));
            highJump.AddParticipant(new Participant("Мамин - Сибиряк", new double[] { 2.4, 2.5, 2.6 }));
            highJump.AddParticipant(new Participant("Маркс", new double[] { 2.7, 2.8, 2.9 }));

            //
            
            string outputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RunnerData2");
            Directory.CreateDirectory(outputDirectory);
            //

            MySerializer serializer = new JsonMySerializer();
            //
            string fileName1 = $"jumps_1.json";
            string filePath1 = Path.Combine(outputDirectory, fileName1);
            serializer.Write(highJump, filePath1);

            string fileName0 = $"jumps_2.json";
            string filePath0 = Path.Combine(outputDirectory, fileName0);
            serializer.Write(longJump, filePath0);
            

            
                
            

            

           

            // ... (чтение файла) ...

            Console.WriteLine("\nДанные из файлов:");
            Console.WriteLine("=============================================");
            string fileName = $"jumps_1.json";
            string filePath = Path.Combine(outputDirectory, fileName);

            LongJump readDiscipline = serializer.Read<LongJump>(filePath);
            Console.WriteLine($"\nПротокол соревнований по {longJump.disciplineName}:");
            Console.WriteLine("=============================================");
            Console.WriteLine($"Фамилия Лучший результат");
            Console.WriteLine("=============================================");
            foreach (var participant in readDiscipline.participants)
            {
                if (participant != null)
                    participant.Print();
            }
            Console.WriteLine("=============================================");
            Console.WriteLine("\nДанные из файлов:");
            Console.WriteLine("=============================================");
            string fileName2 = $"jumps_2.json";
            string filePath2 = Path.Combine(outputDirectory, fileName2);
            HighJump readDiscipline1 = serializer.Read<HighJump>(filePath2);
            Console.WriteLine($"\nПротокол соревнований по {highJump.disciplineName}:");
            Console.WriteLine("=============================================");
            Console.WriteLine($"Фамилия Лучший результат");
            Console.WriteLine("=============================================");
            foreach (var participant1 in readDiscipline1.participants)
            {
                if (participant1 != null)
                    participant1.Print();
            }
            Console.WriteLine("=============================================");
            //if (readDiscipline.DisciplineType == 0)
            //{


            //}
            //{ readDiscipline.disciplineName}
            //else
            //{
            //    Console.WriteLine($"\nПротокол соревнований по {readDiscipline.disciplineName}:");
            //    Console.WriteLine("=============================================");
            //    Console.WriteLine("Фамилия\t\tЛучший результат");
            //    Console.WriteLine("=============================================");
            //    foreach (var participant in readDiscipline.participants)
            //    {
            //        if (participant != null)
            //            participant.Print();
            //    }
            //    Console.WriteLine("=============================================");
            //}



            //    Console.WriteLine("=============================================");








        }
    }
}