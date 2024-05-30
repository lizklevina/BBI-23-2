using System;
using System.IO;
using System.Text.Json;

// Папка для библиотеки
namespace SerializationLibrary
{
    public abstract class MySerializer
    {
        public abstract void Write<T>(T obj, string filePath);
        public abstract T Read<T>(string filePath);
    }

    class JsonMySerializer : MySerializer
    {
        public override void Write<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, obj);
            }
        }

        public override T Read<T>(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return JsonSerializer.Deserialize<T>(fs);
                }
            }
            catch (FileNotFoundException)
            {
                // Обработка исключения: например, вывод сообщения об ошибке
                Console.WriteLine($"Файл {filePath} не найден.");
                return default(T);
            }
        }
    }
}