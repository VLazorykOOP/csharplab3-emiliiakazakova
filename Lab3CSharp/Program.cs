using System;

public class Date
{
    // Захищені поля
    protected int day;
    protected int month;
    protected int year;

    // Конструктор
    public Date(int day, int month, int year)
    {
        if (IsValidDate(day, month, year))
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }
        else
        {
            throw new ArgumentException("Неправильна дата");
        }
    }

    // Властивості
    public int Day
    {
        get { return day; }
        set
        {
            if (IsValidDate(value, month, year))
            {
                day = value;
            }
            else
            {
                throw new ArgumentException("Неправильний день");
            }
        }
    }

    public int Month
    {
        get { return month; }
        set
        {
            if (IsValidDate(day, value, year))
            {
                month = value;
            }
            else
            {
                throw new ArgumentException("Неправильний місяць");
            }
        }
    }

    public int Year
    {
        get { return year; }
        set
        {
            if (IsValidDate(day, month, value))
            {
                year = value;
            }
            else
            {
                throw new ArgumentException("Неправильний рік");
            }
        }
    }

    public int Century
    {
        get { return (year / 100) + 1; }
    }

    // Метод для перевірки коректності дати
    public static bool IsValidDate(int day, int month, int year)
    {
        if (year < 1 || month < 1 || month > 12 || day < 1 || day > 31)
            return false;

        int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        // Перевірка на високосний рік
        if (IsLeapYear(year))
        {
            daysInMonth[1] = 29;
        }

        return day <= daysInMonth[month - 1];
    }

    // Метод для перевірки високосного року
    public static bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    // Метод для виведення дати у форматі "05.01.2022"
    public string ToShortDateString()
    {
        return $"{day:D2}.{month:D2}.{year}";
    }

    // Метод для виведення дати у форматі "5 січня 2022 року"
    public string ToLongDateString()
    {
        string[] months = { "січня", "лютого", "березня", "квітня", "травня", "червня", "липня", "серпня", "вересня", "жовтня", "листопада", "грудня" };
        return $"{day} {months[month - 1]} {year} року";
    }

    // Метод для обчислення кількості днів між двома датами
    public static int DaysBetween(Date date1, Date date2)
    {
        DateTime d1 = new DateTime(date1.year, date1.month, date1.day);
        DateTime d2 = new DateTime(date2.year, date2.month, date2.day);
        return (d2 - d1).Days;
    }
}

// Демонстрація використання класу Date
public class Program
{
    public static void Main()
    {
        Date[] dates = new Date[]
        {
            new Date(5, 1, 2022),
            new Date(15, 6, 2021),
            new Date(9, 11, 2020),
            new Date(23, 3, 2023)
        };

        Console.WriteLine("Введені дати:");
        foreach (var date in dates)
        {
            Console.WriteLine(date.ToLongDateString());
        }

        // Сортування дат
        Array.Sort(dates, (d1, d2) => new DateTime(d1.Year, d1.Month, d1.Day).CompareTo(new DateTime(d2.Year, d2.Month, d2.Day)));

        Console.WriteLine("\nВідсортовані дати:");
        foreach (var date in dates)
        {
            Console.WriteLine(date.ToLongDateString());
        }

        // Пошук найбільшої кількості днів між датами
        int maxDaysBetween = 0;
        for (int i = 1; i < dates.Length; i++)
        {
            int daysBetween = Date.DaysBetween(dates[i - 1], dates[i]);
            if (daysBetween > maxDaysBetween)
            {
                maxDaysBetween = daysBetween;
            }
        }

        Console.WriteLine($"\nНайбільша кількість днів між датами: {maxDaysBetween}");
    }
}
