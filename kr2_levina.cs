using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class Task1
{
    private string text_1;

    public Task1(string text_2)
    {
        this.text_1 = text_1;
        Print1(text_1);
    }
    public void Print1(string text_1)
    {
        string text = Console.ReadLine();
        int maxCount = 0; 
        int thisCount = 0; 

       
        for (int i = 0; i < text.Length - 1; i++)
        {

            if (text[i] == text[i + 1])
            {
                thisCount++;
            }
            else 
            {
                if (thisCount > maxCount)
                {
                    maxCount = thisCount;
                }
       
                thisCount = 0;
            }
        }
  
        if (thisCount > maxCount)
        {
            maxCount = thisCount;
        }

        Console.WriteLine(maxCount + 1);
    }
}
public class Task2
{
    private string text_2;

    public Task2(string text_2)
    {
        this.text_2 = text_2;
        Print2(text_2)
    }
    public void Print2(string text_1)
    {
        string text1 = Console.ReadLine();

        char[] letter = text1.ToCharArray();

        for (int i = 0; i < letter.Length; i++)
        {
            if (i % 2 == 0)
            {
                letter[i] = Char.ToUpper(letter[i]);
            }
            else
            {
                letter[i] = Char.ToLower(letter[i]);
            }
        }

        string rez = new string(letter);
        Console.WriteLine(rez);
    }
}
class Program1
{
    static void Main()
    {
        Task1 task1 = new Task1("fffflbgnhtepl3lvvvvvvv");
        Task2 task2 = new Task2("Марк Анатольевич поставьтке ХОРОШИ1 балл пожалуйста");
    }

}

class Program2
{
    static void Main(string[] args)
    {
        string path = @"C:\Users\m2306680\Downloads";
        string newName = "Test";
    }
}
