using System;
using System.Collections.Generic;

public class FixedStringArray
{
    private string[] _array;
    public static int InstanceCount { get; private set; } = 0;

    // Властивість для довжини масиву
    public int Length => _array.Length;

    // Індексатор з контролем виходу за межі
    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= _array.Length)
                throw new IndexOutOfRangeException("Index out of range.");
            return _array[index];
        }
        set
        {
            if (index < 0 || index >= _array.Length)
                throw new IndexOutOfRangeException("Index out of range.");
            _array[index] = value;
        }
    }

    // Конструктор 1: за розміром
    public FixedStringArray(int size)
    {
        _array = new string[size];
        InstanceCount++;
    }

    // Конструктор 2: з масиву рядків
    public FixedStringArray(string[] source)
    {
        _array = new string[source.Length];
        for (int i = 0; i < source.Length; i++)
            _array[i] = source[i] ?? string.Empty;
        InstanceCount++;
    }

    // Конструктор 3: копіювання іншого FixedStringArray
    public FixedStringArray(FixedStringArray other)
    {
        _array = new string[other.Length];
        for (int i = 0; i < other.Length; i++)
            _array[i] = other[i];
        InstanceCount++;
    }

    // Поелементне зчеплення (перевантаження методу)
    public static FixedStringArray ConcatElementwise(FixedStringArray a, FixedStringArray b)
    {
        int len = Math.Min(a.Length, b.Length);
        var result = new FixedStringArray(len);
        for (int i = 0; i < len; i++)
            result[i] = (a[i] ?? "") + (b[i] ?? "");
        return result;
    }

    // Злиття без повторів (перевантаження методу)
    public static FixedStringArray MergeUnique(FixedStringArray a, FixedStringArray b)
    {
        var set = new HashSet<string>(a._array ?? new string[0]);
        foreach (var s in b._array ?? new string[0])
            set.Add(s ?? string.Empty);
        return new FixedStringArray(new List<string>(set).ToArray());
    }

    // Додає елемент у перше вільне місце (null або "")
    public bool Add(string value)
    {
        for (int i = 0; i < _array.Length; i++)
        {
            if (string.IsNullOrEmpty(_array[i]))
            {
                _array[i] = value;
                return true;
            }
        }
        return false; // Масив повний
    }

    // Перевіряє, чи містить масив елемент
    public bool Contains(string value)
    {
        foreach (var s in _array)
            if (s == value)
                return true;
        return false;
    }

    // Очищення масиву
    public void Clear()
    {
        for (int i = 0; i < _array.Length; i++)
            _array[i] = string.Empty;
    }

    // Друк одного елемента
    public void Print(int index)
    {
        Console.WriteLine(this[index]);
    }

    // Друк усього масиву
    public void PrintAll()
    {
        foreach (var s in _array)
            Console.WriteLine(s);
    }

    // Для Windows Forms: повертає масив для відображення
    public string[] ToArray() => (string[])_array.Clone();

    // Повертає рядкове представлення масиву
    public override string ToString()
    {
        return string.Join(", ", _array);
    }

    // Статичний метод для створення масиву із заповненням значенням за замовчуванням
    public static FixedStringArray CreateFilled(int size, string value)
    {
        var arr = new string[size];
        for (int i = 0; i < size; i++)
            arr[i] = value;
        return new FixedStringArray(arr);
    }
}