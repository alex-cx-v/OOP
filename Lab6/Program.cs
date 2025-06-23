using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    // Структура для нечіткого числа (наприклад, інтервал [a, b])
    struct FuzzyNumber
    {
        public double a; // Ліва межа
        public double b; // Права межа

        public FuzzyNumber(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public override string ToString()
        {
            return $"[{a}; {b}]";
        }
    }

    internal class Program
    {
        // Функція для знаходження зворотного нечіткого числа
        static FuzzyNumber Inverse(FuzzyNumber x)
        {
            // Перевірка на нуль, щоб уникнути ділення на нуль
            if (x.a == 0 || x.b == 0)
                throw new ArgumentException("Межі нечіткого числа не можуть дорівнювати нулю.");

            // Для інтервалу [a, b] зворотний інтервал [1/b, 1/a] (якщо a < b)
            double invA = 1.0 / x.a;
            double invB = 1.0 / x.b;

            // Повертаємо інтервал у правильному порядку
            return new FuzzyNumber(Math.Min(invA, invB), Math.Max(invA, invB));
        }

        static void Main(string[] args)
        {
            // Приклад використання
            FuzzyNumber x = new FuzzyNumber(2, 4);
            FuzzyNumber inv = Inverse(x);
            Console.WriteLine($"Зворотне до {x} = {inv}");
        }
    }
}