using System;
using System.IO;
using System.Text.Json;

// Папка для библиотеки
namespace SerializationLibrary
{
    // Абстрактный класс для сериализации и десериализации
    public abstract class Serializer
    {
        protected string filePath;

        public Serializer(string filePath)
        {
            this.filePath = filePath;
        }

        public abstract void Serialize(object obj);
        public abstract object Deserialize();
    }

    // Класс для сериализации/десериализации в JSON
    public class JsonSerializer : Serializer
    {
        public JsonSerializer(string filePath) : base(filePath) { }

        public override void Serialize(object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            File.WriteAllText(filePath, json);
        }

        public override object Deserialize()
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<object>(json);
        }
    }
}