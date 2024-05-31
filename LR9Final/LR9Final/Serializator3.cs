using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializator3
{
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
            // Используйте JsonSerializer из System.Text.Json
            string json = System.Text.Json.JsonSerializer.Serialize(obj);
            File.WriteAllText(filePath, json);
        }

        public override object Deserialize()
        {
            string json = File.ReadAllText(filePath);
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, int>>(json);
        }
    }
}
