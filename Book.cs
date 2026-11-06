using System;

class Book
{
    public string Title;
    public string Author;
    public int Year;
    public bool IsAvailable = true;
    public string BorrowedBy;
    public DateTime DueDate;

    public void DisplayInfo()
    {
        string status = IsAvailable ? "доступна" : $"выдана ({BorrowedBy}, вернуть до {DueDate:dd.MM.yyyy})";
        Console.WriteLine($"Название: {Title}");
        Console.WriteLine($"Автор: {Author}");
        Console.WriteLine($"Год: {Year}");
        Console.WriteLine($"Статус: {status}");
    }

    public void SetTitle(string title) => Title = title;

    public bool IsBookAvailable() => IsAvailable;

    public int CalculateBookAge(int currentYear) => currentYear - Year;
}