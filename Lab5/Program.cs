using System;
using System.Linq;
using System.Text;

namespace Lab5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Щоб коректно відображати українські символи в консолі
            Console.OutputEncoding = Encoding.UTF8;

            string[] lines = {
                "Привіт",
                "Світ",
            };

            foreach (var line in lines)
            {
                Console.WriteLine($"Рядок: {line}");
                Console.Write("Коди символів: ");
                foreach (char c in line)
                {
                    Console.Write($"{(int)c} ");
                }
                Console.WriteLine();

                // Сортування символів у рядку за кодами
                var sorted = new string(line.OrderBy(c => c).ToArray());
                Console.WriteLine($"Відсортовано: {sorted}");
                Console.WriteLine();
            }
        }
    }
}