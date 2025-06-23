using System;
using System.Linq;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Завдання 1
            Console.WriteLine("Завдання 1:");
            int n;
            while (true)
            {
                Console.Write("Введіть розмір одномірного масиву n: ");
                string nInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nInput) && int.TryParse(nInput, out n) && n > 0)
                    break;
                Console.WriteLine("Попередження: Ви не ввели коректне число. Спробуйте ще раз.");
            }

            double[] arr = new double[n];
            Console.WriteLine("Введіть елементи масиву:");
            for (int i = 0; i < n; i++)
            {
                while (true)
                {
                    string elementInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(elementInput) && double.TryParse(elementInput, out arr[i]))
                        break;
                    Console.WriteLine($"Попередження: Ви не ввели коректне число для елемента {i + 1}. Спробуйте ще раз.");
                }
            }

            // а) Кількість елементів, рівних 0
            int zeroCount = arr.Count(x => x == 0);
            Console.WriteLine($"Кількість елементів, рівних 0: {zeroCount}");

            // б) Сума елементів після мінімального
            int minIndex = Array.IndexOf(arr, arr.Min());
            double sumAfterMin = arr.Skip(minIndex + 1).Sum();
            Console.WriteLine($"Сума елементів після мінімального: {sumAfterMin}");

            // Упорядкування за зростанням модулів
            double[] sortedByAbs = arr.OrderBy(x => Math.Abs(x)).ToArray();
            Console.WriteLine("Масив, впорядкований за зростанням модулів:");
            Console.WriteLine(string.Join(" ", sortedByAbs));

            // Завдання 2
            Console.WriteLine("\nЗавдання 2:");
            int m, k;
            while (true)
            {
                Console.Write("Введіть кількість рядків m: ");
                string mInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(mInput) && int.TryParse(mInput, out m) && m > 0)
                    break;
                Console.WriteLine("Попередження: Ви не ввели коректне число. Спробуйте ще раз.");
            }
            while (true)
            {
                Console.Write("Введіть кількість стовпців k: ");
                string kInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(kInput) && int.TryParse(kInput, out k) && k > 0)
                    break;
                Console.WriteLine("Попередження: Ви не ввели коректне число. Спробуйте ще раз.");
            }

            double[,] matrix = new double[m, k];
            Console.WriteLine("Введіть елементи двовимірного масиву:");
            for (int i = 0; i < m; i++)
                for (int j = 0; j < k; j++)
                {
                    while (true)
                    {
                        string matrixInput = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(matrixInput) && double.TryParse(matrixInput, out matrix[i, j]))
                            break;
                        Console.WriteLine($"Попередження: Ви не ввели коректне число для елемента [{i + 1},{j + 1}]. Спробуйте ще раз.");
                    }
                }

            // а) Заміна будь-якого елемента другого стовпця на 13
            if (k >= 2)
            {
                int rowToChange;
                while (true)
                {
                    Console.Write($"Введіть номер рядка (0..{m - 1}) для заміни у другому стовпці: ");
                    string rowToChangeInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(rowToChangeInput) && int.TryParse(rowToChangeInput, out rowToChange) && rowToChange >= 0 && rowToChange < m)
                        break;
                    Console.WriteLine("Попередження: Ви не ввели коректний номер рядка. Спробуйте ще раз.");
                }
                matrix[rowToChange, 1] = 13;
            }
            else
            {
                Console.WriteLine("У масиві менше двох стовпців, пропускаємо завдання 2а.");
            }

            // б) Заміна будь-якого елемента на число b
            int rowB, colB;
            while (true)
            {
                Console.Write($"Введіть номер рядка (0..{m - 1}) для заміни: ");
                string rowBInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(rowBInput) && int.TryParse(rowBInput, out rowB) && rowB >= 0 && rowB < m)
                    break;
                Console.WriteLine("Попередження: Ви не ввели коректний номер рядка. Спробуйте ще раз.");
            }
            while (true)
            {
                Console.Write($"Введіть номер стовпця (0..{k - 1}) для заміни: ");
                string colBInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(colBInput) && int.TryParse(colBInput, out colB) && colB >= 0 && colB < k)
                    break;
                Console.WriteLine("Попередження: Ви не ввели коректний номер стовпця. Спробуйте ще раз.");
            }
            double b;
            while (true)
            {
                Console.Write("Введіть число b: ");
                string bInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(bInput) && double.TryParse(bInput, out b))
                    break;
                Console.WriteLine("Попередження: Ви не ввели коректне число. Спробуйте ще раз.");
            }
            matrix[rowB, colB] = b;

            // Вивід результату
            Console.WriteLine("Результуючий масив:");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < k; j++)
                    Console.Write(matrix[i, j] + "\t");
                Console.WriteLine();
            }
        }
    }
}